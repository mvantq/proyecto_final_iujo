using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;
using proyecto_final_iujo.Logica.Vinculacion.Calculo;

namespace proyecto_final_iujo.Visuales
{
	public partial class FormPartida : Form
	{
		// Estructura interna para gestionar cada héroe en la cola con sus propios stats
		private class Luchador
		{
			public string Nombre { get; set; }
			public string Clave { get; set; }
			public string Elemento { get; set; }
			public int VidaActual { get; set; }
			public int VidaMaxima { get; set; }
			public int TurnosEnArena { get; set; }

			public Luchador(string nombre, string clave, string elemento, int vida)
			{
				Nombre = nombre;
				Clave = clave;
				Elemento = elemento;
				VidaActual = vida;
				VidaMaxima = vida;
				TurnosEnArena = 0;
			}
		}

		private Queue<Luchador> colaJugador = new Queue<Luchador>();
		private Queue<Luchador> colaRival = new Queue<Luchador>();

		private Luchador heroeJugadorActual = null;
		private Luchador heroeRivalActual = null;

		private Image imgJugador = null;
		private Image imgRival = null;

		private int turno = 1;
		private int turnoTerremoto;
		private bool terremotoOcurrido = false;
		private Random random = new Random();

		private bool poderFuegoActivo = false;
		private bool poderAguaActivo = false;
		private bool poderTierraActivo = false;
		private bool poderAireActivo = false;

		public FormPartida(List<string> seleccionados)
		{
			InitializeComponent();
			this.turnoTerremoto = random.Next(5, 8);

			InicializarEquipos(seleccionados);
			CargarFondoEscenarioDifuminado();

			picEscenario.Paint += DibujarPersonajesEnArena;

			lblLogTurno.Text = "¡COMIENZA EL COMBATE POR EL DESTINO DE LA ALDEA!";
			ActualizarInterfaz();
		}

		private void InicializarEquipos(List<string> seleccionados)
		{
			colaJugador.Clear();
			if (seleccionados != null && seleccionados.Count > 0)
			{
				foreach (var item in seleccionados)
				{
					colaJugador.Enqueue(CrearLuchadorDesdeCadena(item));
				}
			}
			else
			{
				colaJugador.Enqueue(new Luchador("Acuaris el Abisal", "Acuaris", "Agua", 100));
				colaJugador.Enqueue(new Luchador("Vulkan el Colérico", "Vulkan", "Fuego", 100));
				colaJugador.Enqueue(new Luchador("Terran el Guardián", "Terran", "Tierra", 100));
				colaJugador.Enqueue(new Luchador("Zephyr el Heraldo", "Zephyr", "Viento", 100));
				colaJugador.Enqueue(new Luchador("Ignis el Forjador", "Ignis", "Fuego", 100));
			}

			colaRival.Clear();
			colaRival.Enqueue(new Luchador("Nereus del Pantano", "Nereus", "Agua", 100));
			colaRival.Enqueue(new Luchador("Bóreas del Norte", "Bóreas", "Viento", 100));
			colaRival.Enqueue(new Luchador("Gólem Telúrico", "Gólem", "Tierra", 100));
			colaRival.Enqueue(new Luchador("Pirox el Rebelde", "Pirox", "Fuego", 100));
			colaRival.Enqueue(new Luchador("Glauco el Torrencial", "Glauco", "Agua", 100));

			PrepararHeroesActivos();
		}

		private Luchador CrearLuchadorDesdeCadena(string texto)
		{
			string nombre = texto;
			string elemento = "Fuego";
			string clave = texto.Split(' ')[0].Trim();

			if (texto.Contains("[Agua]")) elemento = "Agua";
			else if (texto.Contains("[Tierra]")) elemento = "Tierra";
			else if (texto.Contains("[Viento]")) elemento = "Viento";

			return new Luchador(nombre, clave, elemento, 100);
		}

		private void PrepararHeroesActivos()
		{
			if (colaJugador.Count > 0) heroeJugadorActual = colaJugador.Peek();
			if (colaRival.Count > 0) heroeRivalActual = colaRival.Peek();

			CargarImagenesCombatientes();

			if (heroeJugadorActual != null)
				lblNombreJugador.Text = string.Format("{0} [{1}/5]", heroeJugadorActual.Nombre.ToUpper(), colaJugador.Count);

			if (heroeRivalActual != null)
				lblNombreRival.Text = string.Format("RIVAL: {0} [{1}/5]", heroeRivalActual.Nombre.ToUpper(), colaRival.Count);
		}

		// Multiplicador de debilidad y resistencia elemental
		private double ObtenerMultiplicadorElemental(string elemAtacante, string elemDefensor)
		{
			if (elemAtacante == "Fuego" && elemDefensor == "Viento") return 1.45;
			if (elemAtacante == "Viento" && elemDefensor == "Tierra") return 1.45;
			if (elemAtacante == "Tierra" && elemDefensor == "Agua") return 1.45;
			if (elemAtacante == "Agua" && elemDefensor == "Fuego") return 1.45;

			if (elemAtacante == "Viento" && elemDefensor == "Fuego") return 0.70;
			if (elemAtacante == "Tierra" && elemDefensor == "Viento") return 0.70;
			if (elemAtacante == "Agua" && elemDefensor == "Tierra") return 0.70;
			if (elemAtacante == "Fuego" && elemDefensor == "Agua") return 0.70;

			return 1.0;
		}

		private void ProcesarAccionTurno(string tipoAccion)
		{
			if (heroeJugadorActual == null || heroeRivalActual == null) return;

			heroeJugadorActual.TurnosEnArena++;
			heroeRivalActual.TurnosEnArena++;

			bool esTurnoTerremoto = (!terremotoOcurrido && turno == turnoTerremoto);

			string[] escenarios = { "Vórtice Huracanado", "Volcán Ardiente", "Ciénaga Helada", "Desfiladero Rocoso" };
			string escenarioActual = escenarios[turno % 4];

			ResultadoTurno res = AlgoritmoDano.ProcesarTurno(
				heroeJugadorActual.VidaActual, turno, escenarioActual,
				poderFuegoActivo, poderAguaActivo, poderTierraActivo, poderAireActivo,
				esTurnoTerremoto
			);

			if (esTurnoTerremoto) terremotoOcurrido = true;

			// 1. ATAQUE DEL JUGADOR
			int danoBaseJugador = 0;
			int curacion = 0;
			string descripcionAccion = "";
			string elemAccion = "Fuego";

			if (tipoAccion == "Ataque de Fuego")
			{
				elemAccion = "Fuego";
				danoBaseJugador = random.Next(18, 26);
				descripcionAccion = "🔥 Golpe Ígneo";
			}
			else if (tipoAccion == "Defensa de Tierra")
			{
				elemAccion = "Tierra";
				danoBaseJugador = random.Next(6, 12);
				descripcionAccion = "🛡️ Escudo Rocoso";
			}
			else if (tipoAccion == "Especial de Agua")
			{
				elemAccion = "Agua";
				danoBaseJugador = random.Next(10, 16);
				curacion = random.Next(14, 22);
				heroeJugadorActual.VidaActual = Math.Min(heroeJugadorActual.VidaMaxima, heroeJugadorActual.VidaActual + curacion);
				descripcionAccion = string.Format("🌊 Marea (+{0} HP)", curacion);
			}
			else if (tipoAccion == "Relevo de Aire")
			{
				elemAccion = "Viento";
				danoBaseJugador = random.Next(8, 14);
				descripcionAccion = "💨 Ráfaga de Relevo";
			}

			// Aplicar bonificador de afinidad elemental
			double multJugador = ObtenerMultiplicadorElemental(elemAccion, heroeRivalActual.Elemento);
			int danoInfligidoFinal = (int)(danoBaseJugador * multJugador);
			heroeRivalActual.VidaActual = Math.Max(0, heroeRivalActual.VidaActual - danoInfligidoFinal);

			// 2. COMPROBAR SI CAE EL RIVAL
			if (heroeRivalActual.VidaActual <= 0)
			{
				colaRival.Dequeue();
				if (colaRival.Count == 0)
				{
					ActualizarInterfaz();
					MessageBox.Show("¡VICTORIA TOTAL! Has derrotado a los 5 personajes del rival. La frontera de La Aldea está a salvo.", "Victoria Magna", MessageBoxButtons.OK, MessageBoxIcon.Information);
					VolverAlMenu();
					return;
				}

				MessageBox.Show(string.Format("¡K.O.! El rival {0} ha caído. El rival debe enviar a su siguiente combatiente.", heroeRivalActual.Nombre), "Baja Enemiga", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				PrepararHeroesActivos();
				ActualizarInterfaz();
				return;
			}

			// 3. CONTRAATAQUE DEL RIVAL (Con cálculo de Fatiga del Jugador)
			// Cuantos más turnos lleve el jugador sin relevar, más daño recibe (+10% acumulativo)
			double factorFatiga = 1.0 + (heroeJugadorActual.TurnosEnArena * 0.10);
			double multRival = ObtenerMultiplicadorElemental(heroeRivalActual.Elemento, heroeJugadorActual.Elemento);

			int danoBaseRival = random.Next(14, 22);
			int danoTotalRival = (int)(danoBaseRival * multRival * factorFatiga) + res.DanoRecibido;

			if (tipoAccion == "Defensa de Tierra")
			{
				danoTotalRival = (int)(danoTotalRival * 0.25); // Mitiga 75%
			}
			else if (tipoAccion == "Relevo de Aire")
			{
				danoTotalRival = (int)(danoTotalRival * 0.15); // Esquiva casi todo
			}

			heroeJugadorActual.VidaActual = Math.Max(0, heroeJugadorActual.VidaActual - danoTotalRival);
			turno++;
			ActualizarInterfaz();

			// 4. LOGS Y TERREMOTO
			if (esTurnoTerremoto)
			{
				lblLogTurno.ForeColor = Color.OrangeRed;
				lblLogTurno.Text = string.Format("⚠️ ¡SISMO EN LA ALDEA! {0} causó {1}. Recibes {2} daño sísmico.", descripcionAccion, danoInfligidoFinal, danoTotalRival);
				FormGraficaTerremoto grafica = new FormGraficaTerremoto();
				grafica.ShowDialog();
			}
			else
			{
				lblLogTurno.ForeColor = Color.FromArgb(255, 220, 90);
				lblLogTurno.Text = string.Format("[T{0}] {1} ({2} daño | mult x{3:0.0}). Recibes {4} de daño (Fatiga x{5:0.0}).",
					turno - 1, descripcionAccion, danoInfligidoFinal, multJugador, danoTotalRival, factorFatiga);
			}

			// 5. COMPROBAR SI CAE EL JUGADOR (Relevo forzado o Derrota)
			if (heroeJugadorActual.VidaActual <= 0)
			{
				colaJugador.Dequeue();
				if (colaJugador.Count == 0)
				{
					MessageBox.Show("DERROTA TOTAL: Todos tus 5 campeones han caído. La espesura del bosque ha reclamado a tu escuadrón.", "Derrota Definitiva", MessageBoxButtons.OK, MessageBoxIcon.Stop);
					VolverAlMenu();
					return;
				}

				MessageBox.Show(string.Format("¡TU HÉROE HA CAÍDO! {0} ya no puede combatir. Relevo forzado automático.", heroeJugadorActual.Nombre), "Caído en Combate", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				PrepararHeroesActivos();
				ActualizarInterfaz();
			}

			poderFuegoActivo = poderAguaActivo = poderTierraActivo = poderAireActivo = false;
		}

		private void EjecutarRelevoEstrategico()
		{
			if (colaJugador.Count <= 1)
			{
				MessageBox.Show("No te quedan personajes en la banca para relevar.", "Sin Relevos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}

			// El héroe que va a descansar se cura 15 HP en la banca y disipa su fatiga
			Luchador relevado = colaJugador.Dequeue();
			relevado.TurnosEnArena = 0;
			relevado.VidaActual = Math.Min(relevado.VidaMaxima, relevado.VidaActual + 15);
			colaJugador.Enqueue(relevado);

			// IA del rival: releva si su vida es baja (menor a 35) o tras varios turnos
			if (colaRival.Count > 1 && (heroeRivalActual.VidaActual < 35 || heroeRivalActual.TurnosEnArena >= 3))
			{
				Luchador relevadoRival = colaRival.Dequeue();
				relevadoRival.TurnosEnArena = 0;
				relevadoRival.VidaActual = Math.Min(relevadoRival.VidaMaxima, relevadoRival.VidaActual + 10);
				colaRival.Enqueue(relevadoRival);
			}

			PrepararHeroesActivos();
			lblLogTurno.Text = string.Format("¡RELEVO TÁCTICO! Entra {0} ({1}). Descansas y disipas fatiga.", heroeJugadorActual.Nombre, heroeJugadorActual.Elemento);
			picEscenario.Invalidate();
		}

		private void ActualizarInterfaz()
		{
			if (heroeJugadorActual != null)
			{
				pbHpJugador.Value = Math.Max(0, Math.Min(100, heroeJugadorActual.VidaActual));
				lblNombreJugador.Text = string.Format("{0} ({1} HP) [{2} en banca]", heroeJugadorActual.Nombre.ToUpper(), heroeJugadorActual.VidaActual, colaJugador.Count - 1);
			}

			if (heroeRivalActual != null)
			{
				pbHpRival.Value = Math.Max(0, Math.Min(100, heroeRivalActual.VidaActual));
				lblNombreRival.Text = string.Format("RIVAL: {0} ({1} HP) [{2} en banca]", heroeRivalActual.Nombre.ToUpper(), heroeRivalActual.VidaActual, colaRival.Count - 1);
			}

			lblEnergiaJugador.Text = string.Format("ENERGÍA: 5\nTURNO {0}", turno);
			picEscenario.Invalidate();
		}

		// --- MÉTODOS VISUALES ORIGINALES (DIFUMINADO, AURAS, SOMBRAS Y FONDO) ---
		private void CargarFondoEscenarioDifuminado()
		{
			string[] posiblesRutas = new string[] {
				Path.Combine(Application.StartupPath, "Gemini_Generated_Image.jpeg"),
				Path.Combine(Application.StartupPath, "aldea.png"),
				Path.Combine(Application.StartupPath, "Visuales", "aldea.png"),
				Path.Combine(Application.StartupPath, "arena_bosque.png"),
				Path.Combine(Application.StartupPath, "fondo.jpg"),
				Path.GetFullPath(Path.Combine(Application.StartupPath, @"..\..\Personajes\arena_bosque.png")),
				Path.GetFullPath(Path.Combine(Application.StartupPath, @"..\..\fondo.jpg"))
			};

			Image imgBase = null;
			foreach (string r in posiblesRutas)
			{
				if (File.Exists(r))
				{
					using (FileStream fs = new FileStream(r, FileMode.Open, FileAccess.Read))
					{
						imgBase = Image.FromStream(fs);
					}
					break;
				}
			}

			if (imgBase != null)
			{
				picEscenario.Image = AplicarDifuminado(imgBase, picEscenario.Width, picEscenario.Height);
				imgBase.Dispose();
			}
			else
			{
				Bitmap bmp = new Bitmap(picEscenario.Width, picEscenario.Height);
				using (Graphics g = Graphics.FromImage(bmp))
				using (LinearGradientBrush lgb = new LinearGradientBrush(new Rectangle(0, 0, picEscenario.Width, picEscenario.Height), Color.FromArgb(18, 28, 20), Color.FromArgb(40, 20, 15), 45f))
				{
					g.FillRectangle(lgb, 0, 0, picEscenario.Width, picEscenario.Height);
				}
				picEscenario.Image = bmp;
			}

			pnlHUD.Parent = picEscenario;
			pnlAcciones.Parent = picEscenario;
			pnlHUD.Left = (this.ClientSize.Width - pnlHUD.Width) / 2;
			pnlAcciones.Left = (this.ClientSize.Width - pnlAcciones.Width) / 2;
		}

		private Bitmap AplicarDifuminado(Image original, int anchoFinal, int altoFinal)
		{
			if (anchoFinal <= 0) anchoFinal = 960;
			if (altoFinal <= 0) altoFinal = 540;

			int anchoReducido = Math.Max(1, anchoFinal / 12);
			int altoReducido = Math.Max(1, altoFinal / 12);

			Bitmap miniatura = new Bitmap(anchoReducido, altoReducido);
			using (Graphics gMin = Graphics.FromImage(miniatura))
			{
				gMin.InterpolationMode = InterpolationMode.Bilinear;
				gMin.DrawImage(original, 0, 0, anchoReducido, altoReducido);
			}

			Bitmap resultado = new Bitmap(anchoFinal, altoFinal);
			using (Graphics gRes = Graphics.FromImage(resultado))
			{
				gRes.InterpolationMode = InterpolationMode.HighQualityBicubic;
				gRes.SmoothingMode = SmoothingMode.HighQuality;
				gRes.DrawImage(miniatura, 0, 0, anchoFinal, altoFinal);

				using (SolidBrush tinte = new SolidBrush(Color.FromArgb(70, 0, 0, 0)))
				{
					gRes.FillRectangle(tinte, 0, 0, anchoFinal, altoFinal);
				}
			}

			miniatura.Dispose();
			return resultado;
		}

		private void CargarImagenesCombatientes()
		{
			string carpeta = @"C:\Users\BOOM\Documents\SharpDevelop Projects\proyecto_final_iujo\proyecto_final_iujo\Personajes";
			if (!Directory.Exists(carpeta))
			{
				string rel = Path.GetFullPath(Path.Combine(Application.StartupPath, @"..\..\Personajes"));
				carpeta = Directory.Exists(rel) ? rel : Application.StartupPath;
			}

			if (imgJugador != null) { imgJugador.Dispose(); imgJugador = null; }
			if (imgRival != null) { imgRival.Dispose(); imgRival = null; }

			if (heroeJugadorActual != null)
			{
				using (Image rawJugador = LeerImagenSegura(carpeta, heroeJugadorActual.Clave))
				{
					if (rawJugador != null) imgJugador = QuitarFondoNegro(rawJugador);
				}
			}

			if (heroeRivalActual != null)
			{
				using (Image rawRival = LeerImagenSegura(carpeta, heroeRivalActual.Clave))
				{
					if (rawRival != null) imgRival = QuitarFondoNegro(rawRival);
				}
			}
		}

		private Bitmap QuitarFondoNegro(Image imgOriginal)
		{
			if (imgOriginal == null) return null;

			Bitmap bmp = new Bitmap(imgOriginal);
			bmp.MakeTransparent(Color.Black);

			for (int y = 0; y < bmp.Height; y++)
			{
				for (int x = 0; x < bmp.Width; x++)
				{
					Color c = bmp.GetPixel(x, y);
					if (c.R <= 26 && c.G <= 26 && c.B <= 26)
					{
						bmp.SetPixel(x, y, Color.Transparent);
					}
				}
			}
			return bmp;
		}

		private Image LeerImagenSegura(string carpeta, string clave)
		{
			string min = clave.ToLower();
			string sinTilde = min.Replace("ó", "o").Replace("é", "e").Replace("á", "a").Replace("í", "i").Replace("ú", "u");

			string rutaPng = Path.Combine(carpeta, min + ".png");
			if (!File.Exists(rutaPng)) rutaPng = Path.Combine(carpeta, sinTilde + ".png");

			string rutaJpg = Path.Combine(carpeta, min + ".jpg");
			if (!File.Exists(rutaJpg)) rutaJpg = Path.Combine(carpeta, sinTilde + ".jpg");

			if (File.Exists(rutaPng))
			{
				using (FileStream fs = new FileStream(rutaPng, FileMode.Open, FileAccess.Read))
					return new Bitmap(Image.FromStream(fs));
			}
			if (File.Exists(rutaJpg))
			{
				using (FileStream fs = new FileStream(rutaJpg, FileMode.Open, FileAccess.Read))
					return new Bitmap(Image.FromStream(fs));
			}
			return null;
		}

		private void DibujarPersonajesEnArena(object sender, PaintEventArgs e)
		{
			Graphics g = e.Graphics;
			g.SmoothingMode = SmoothingMode.AntiAlias;

			int anchoHeroe = 320;
			int altoHeroe = 310;
			int yPiso = pnlAcciones.Top - altoHeroe + 20;

			// 1. Jugador (Izquierda)
			int xJugador = 70;
			if (imgJugador != null)
			{
				Color colorAura = (heroeJugadorActual != null && heroeJugadorActual.Elemento == "Agua") ? Color.FromArgb(90, 60, 160, 255) : Color.FromArgb(90, 255, 120, 30);
				DibujarSombraYResplandor(g, xJugador, yPiso, anchoHeroe, altoHeroe, colorAura);
				g.DrawImage(imgJugador, new Rectangle(xJugador, yPiso, anchoHeroe, altoHeroe));
			}

			// 2. Rival (Derecha volteado)
			int xRival = this.ClientSize.Width - anchoHeroe - 70;
			if (imgRival != null)
			{
				Color colorAuraRival = (heroeRivalActual != null && heroeRivalActual.Elemento == "Viento") ? Color.FromArgb(90, 140, 240, 180) : Color.FromArgb(90, 90, 200, 255);
				DibujarSombraYResplandor(g, xRival, yPiso, anchoHeroe, altoHeroe, colorAuraRival);
				Bitmap espejo = new Bitmap(imgRival);
				espejo.RotateFlip(RotateFlipType.RotateNoneFlipX);
				g.DrawImage(espejo, new Rectangle(xRival, yPiso, anchoHeroe, altoHeroe));
				espejo.Dispose();
			}
		}

		private void DibujarSombraYResplandor(Graphics g, int x, int y, int ancho, int alto, Color colorAura)
		{
			for (int i = 5; i >= 1; i--)
			{
				int alfaSombra = 20 * i;
				using (SolidBrush brochaSombra = new SolidBrush(Color.FromArgb(alfaSombra, 0, 0, 0)))
				{
					g.FillEllipse(brochaSombra, x + 20 - (i * 4), y + alto - 35, ancho - 40 + (i * 8), 35 + (i * 4));
				}
			}

			using (GraphicsPath pathAura = new GraphicsPath())
			{
				pathAura.AddEllipse(x - 20, y - 10, ancho + 40, alto + 20);
				using (PathGradientBrush pgb = new PathGradientBrush(pathAura))
				{
					pgb.CenterColor = colorAura;
					pgb.SurroundColors = new Color[] { Color.Transparent };
					g.FillPath(pgb, pathAura);
				}
			}
		}

		private void VolverAlMenu()
		{
			MainForm inicio = new MainForm();
			inicio.Show();
			this.Close();
		}

		void BtnAtacarClick(object sender, EventArgs e)
		{
			poderFuegoActivo = true;
			ProcesarAccionTurno("Ataque de Fuego");
		}

		void BtnDefenderClick(object sender, EventArgs e)
		{
			poderTierraActivo = true;
			ProcesarAccionTurno("Defensa de Tierra");
		}

		void BtnEspecialClick(object sender, EventArgs e)
		{
			poderAguaActivo = true;
			ProcesarAccionTurno("Especial de Agua");
		}

		void BtnRelevoClick(object sender, EventArgs e)
		{
			poderAireActivo = true;
			EjecutarRelevoEstrategico();
			ProcesarAccionTurno("Relevo de Aire");
		}

		protected override void OnFormClosing(FormClosingEventArgs e)
		{
			if (imgJugador != null) imgJugador.Dispose();
			if (imgRival != null) imgRival.Dispose();
			if (picEscenario != null && picEscenario.Image != null) picEscenario.Image.Dispose();
			base.OnFormClosing(e);
		}
	}
}