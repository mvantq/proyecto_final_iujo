using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Windows.Forms;

namespace proyecto_final_iujo.Visuales
{
	public partial class FormPartida : Form
	{
		private Queue<string> colaEquipoJugador = new Queue<string>();
		private Queue<string> colaEquipoRival = new Queue<string>();

		private string heroeJugadorActual = "";
		private string heroeRivalActual = "";
		private Image imgJugador = null;
		private Image imgRival = null;

		private int turno = 1;
		private int energiaJugador = 2;

		public FormPartida(List<string> seleccionados)
		{
			InitializeComponent();
			ConfigurarEquipos(seleccionados);
			CargarFondoEscenarioDifuminado();
			picEscenario.Paint += DibujarPersonajesEnArena;
			lblLogTurno.Text = "¡FIGHT! Turno de " + heroeJugadorActual.ToUpper();
		}

		private void ConfigurarEquipos(List<string> seleccionados)
		{
			colaEquipoJugador.Clear();
			foreach (var item in seleccionados)
			{
				string clave = item.Split(' ')[0].Trim();
				colaEquipoJugador.Enqueue(clave);
			}

			colaEquipoRival.Clear();
			colaEquipoRival.Enqueue("Bóreas");
			colaEquipoRival.Enqueue("Vulkan");
			colaEquipoRival.Enqueue("Gólem");
			colaEquipoRival.Enqueue("Nereus");
			colaEquipoRival.Enqueue("Pirox");

			PrepararHeroesActivos();
		}

		private void PrepararHeroesActivos()
		{
			if (colaEquipoJugador.Count > 0) heroeJugadorActual = colaEquipoJugador.Peek();
			if (colaEquipoRival.Count > 0) heroeRivalActual = colaEquipoRival.Peek();

			CargarImagenesCombatientes();

			lblNombreJugador.Text = heroeJugadorActual.ToUpper();
			lblNombreRival.Text = heroeRivalActual.ToUpper();
			lblEnergiaJugador.Text = "ENERGÍA: " + energiaJugador + "\nTURNO " + turno;
		}

		private void CargarFondoEscenarioDifuminado()
		{
			string[] posiblesRutas = new string[] {
				Path.Combine(Application.StartupPath, "arena_bosque.png"),
				Path.Combine(Application.StartupPath, "arena_bosque.jpg"),
				Path.GetFullPath(Path.Combine(Application.StartupPath, @"..\..\arena_bosque.png")),
				Path.GetFullPath(Path.Combine(Application.StartupPath, @"..\..\Personajes\arena_bosque.png"))
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
				// Aplica difuminado cinematográfico (Box Blur seguro)
				picEscenario.Image = AplicarDifuminado(imgBase, 960, 540);
				imgBase.Dispose();
			}
			else
			{
				Bitmap bmp = new Bitmap(960, 540);
				using (Graphics g = Graphics.FromImage(bmp))
				using (LinearGradientBrush lgb = new LinearGradientBrush(new Rectangle(0, 0, 960, 540), Color.FromArgb(18, 28, 20), Color.FromArgb(40, 20, 15), 45f))
				{
					g.FillRectangle(lgb, 0, 0, 960, 540);
				}
				picEscenario.Image = bmp;
			}

			pnlHUD.Parent = picEscenario;
			pnlAcciones.Parent = picEscenario;
			pnlHUD.Left = (this.ClientSize.Width - pnlHUD.Width) / 2;
			pnlAcciones.Left = (this.ClientSize.Width - pnlAcciones.Width) / 2;
		}

		// Difuminado estándar reduciendo y re-escalando con interpolación bicúbica
		private Bitmap AplicarDifuminado(Image original, int anchoFinal, int altoFinal)
		{
			int anchoReducido = anchoFinal / 12;
			int altoReducido = altoFinal / 12;

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

				// Capa oscura sutil para que los personajes resalten más
				using (SolidBrush tinte = new SolidBrush(Color.FromArgb(60, 0, 0, 0)))
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

			using (Image rawJugador = LeerImagenSegura(carpeta, heroeJugadorActual))
			{
				if (rawJugador != null) imgJugador = QuitarFondoNegro(rawJugador);
			}

			using (Image rawRival = LeerImagenSegura(carpeta, heroeRivalActual))
			{
				if (rawRival != null) imgRival = QuitarFondoNegro(rawRival);
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
					if (c.R <= 22 && c.G <= 22 && c.B <= 22)
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

		// Dibuja personajes con sombra y resplandor difuminado, ocultando el corte plano
		private void DibujarPersonajesEnArena(object sender, PaintEventArgs e)
		{
			Graphics g = e.Graphics;
			g.SmoothingMode = SmoothingMode.AntiAlias;

			int anchoHeroe = 320;
			int altoHeroe = 310;
			int yPiso = pnlAcciones.Top - altoHeroe + 20;

			// --- 1. Jugador (Izquierda) ---
			int xJugador = 70;
			if (imgJugador != null)
			{
				DibujarSombraYResplandor(g, xJugador, yPiso, anchoHeroe, altoHeroe, Color.FromArgb(90, 255, 120, 30));
				g.DrawImage(imgJugador, new Rectangle(xJugador, yPiso, anchoHeroe, altoHeroe));
			}

			// --- 2. Rival (Derecha volteado) ---
			int xRival = this.ClientSize.Width - anchoHeroe - 70;
			if (imgRival != null)
			{
				DibujarSombraYResplandor(g, xRival, yPiso, anchoHeroe, altoHeroe, Color.FromArgb(90, 90, 200, 255));
				Bitmap espejo = new Bitmap(imgRival);
				espejo.RotateFlip(RotateFlipType.RotateNoneFlipX);
				g.DrawImage(espejo, new Rectangle(xRival, yPiso, anchoHeroe, altoHeroe));
				espejo.Dispose();
			}
		}

		private void DibujarSombraYResplandor(Graphics g, int x, int y, int ancho, int alto, Color colorAura)
		{
			// 1. Sombra oscura difuminada proyectada
			for (int i = 5; i >= 1; i--)
			{
				int alfaSombra = 20 * i;
				using (SolidBrush brochaSombra = new SolidBrush(Color.FromArgb(alfaSombra, 0, 0, 0)))
				{
					g.FillEllipse(brochaSombra, x + 20 - (i * 4), y + alto - 35, ancho - 40 + (i * 8), 35 + (i * 4));
				}
			}

			// 2. Halo / Resplandor suave elemental en capas
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

		void BtnAtacarClick(object sender, EventArgs e)
		{
			if (energiaJugador < 2)
			{
				MessageBox.Show("No tienes suficiente energía (requiere 2).", "Sin Energía", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}
			energiaJugador -= 2;
			pbHpRival.Value = Math.Max(0, pbHpRival.Value - 25);
			lblLogTurno.Text = heroeJugadorActual.ToUpper() + " lanza un golpe elemental. El rival pierde vida.";
			lblEnergiaJugador.Text = "ENERGÍA: " + energiaJugador + "\nTURNO " + turno;
		}

		void BtnDefenderClick(object sender, EventArgs e)
		{
			if (energiaJugador < 1)
			{
				MessageBox.Show("No tienes suficiente energía (requiere 1).", "Sin Energía", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}
			energiaJugador -= 1;
			lblLogTurno.Text = heroeJugadorActual.ToUpper() + " activa defensa elemental mitigando daño.";
			lblEnergiaJugador.Text = "ENERGÍA: " + energiaJugador + "\nTURNO " + turno;
		}

		void BtnEspecialClick(object sender, EventArgs e)
		{
			if (turno < 2)
			{
				MessageBox.Show("La Habilidad Especial está descargada en el turno 1.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
				return;
			}
			if (energiaJugador < 2)
			{
				MessageBox.Show("Requiere al menos 2 energías para desatar el poder especial.", "Sin Energía", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}
			energiaJugador -= 2;
			pbHpRival.Value = Math.Max(0, pbHpRival.Value - 45);
			lblLogTurno.Text = "¡" + heroeJugadorActual.ToUpper() + " DESATA SU HABILIDAD ESPECIAL PRIMORDIAL!";
			lblEnergiaJugador.Text = "ENERGÍA: " + energiaJugador + "\nTURNO " + turno;
		}

		void BtnRelevoClick(object sender, EventArgs e)
		{
			turno++;
			energiaJugador = Math.Min(10, energiaJugador + 1);

			string viejoJugador = colaEquipoJugador.Dequeue();
			colaEquipoJugador.Enqueue(viejoJugador);

			string viejoRival = colaEquipoRival.Dequeue();
			colaEquipoRival.Enqueue(viejoRival);

			PrepararHeroesActivos();
			lblLogTurno.Text = "¡RELEVO! Entran " + heroeJugadorActual.ToUpper() + " y " + heroeRivalActual.ToUpper();
			picEscenario.Invalidate();
		}

		protected override void OnFormClosing(FormClosingEventArgs e)
		{
			if (imgJugador != null) imgJugador.Dispose();
			if (imgRival != null) imgRival.Dispose();
			base.OnFormClosing(e);
		}
	}
}