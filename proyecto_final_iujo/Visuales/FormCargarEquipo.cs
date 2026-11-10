using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Windows.Forms;

namespace proyecto_final_iujo.Visuales
{
	// ventana para seleccionar los 5 personajes del equipo antes de la pelea
	public partial class FormCargarEquipo : Form
	{
		// clase interna para almacenar las estadisticas y textos bilingues de cada heroe
		public class DatosPersonaje
		{
			// clave usada para ubicar el archivo de imagen en disco
			public string Clave { get; set; }

			// nombres del heroe en ambos idiomas
			public string NombreES { get; set; }
			public string NombreEN { get; set; }

			// nombres del elemento en ambos idiomas
			public string ElementoES { get; set; }
			public string ElementoEN { get; set; }

			// deidad tutelar asociada al arquetipo
			public string Deidad { get; set; }

			// estadisticas de combate base balanceadas a 100 puntos
			public int HP { get; set; }
			public int DEF { get; set; }
			public int ATK { get; set; }
			public int Empatia { get; set; }

			// color tematico para dibujar bordes y textos
			public Color ColorElemento { get; set; }

			// descripcion del lore y estadisticas en ambos idiomas
			public string DescripcionES { get; set; }
			public string DescripcionEN { get; set; }

			// devuelve el nombre segun el idioma activo
			public string NombreActual
			{
				get { return Idioma.Actual == Lenguaje.EN ? NombreEN : NombreES; }
			}

			// devuelve el elemento segun el idioma activo
			public string ElementoActual
			{
				get { return Idioma.Actual == Lenguaje.EN ? ElementoEN : ElementoES; }
			}

			// devuelve la descripcion segun el idioma activo
			public string DescripcionActual
			{
				get { return Idioma.Actual == Lenguaje.EN ? DescripcionEN : DescripcionES; }
			}
		}

		// lista con los 12 heroes disponibles para armar el equipo
		private List<DatosPersonaje> catalogoCompleto = new List<DatosPersonaje>();

		// variables de control para evitar repintados innecesarios al mover el cursor
		private int ultimoIndiceHover = -1;
		private Image imagenActualHeroe = null;
		private Color colorActualBorde = Color.Gold;

		// constructor del formulario
		public FormCargarEquipo()
		{
			// inicializa los controles creados por el disenador de sharpdevelop
			InitializeComponent();

			// activa buffer doble para evitar parpadeos en los dibujos personalizados
			this.DoubleBuffered = true;

			// aplica la imagen de fondo y estilos graficos al titulo y marco
			ConfigurarFondoYEstilos();

			// llena la coleccion con los 12 personajes del juego
			InicializarCatalogo12Personajes();

			// se suscribe al evento para refrescar textos cuando cambie el idioma
			Idioma.Cambiado += OnIdiomaCambiado;

			// carga los textos iniciales en los botones y etiquetas
			ActualizarTextosIdioma();

			// agrega los elementos a la lista con casillas de seleccion
			PoblarLista();

			// enlaza los eventos del mouse para cambiar la vista previa al pasar por encima
			ConfigurarHoverDinamico();
		}

		// manejador disparado cuando el usuario alterna el idioma
		private void OnIdiomaCambiado(object sender, EventArgs e)
		{
			// actualiza los textos estaticos
			ActualizarTextosIdioma();

			// refresca los nombres en la lista manteniendo los marcados
			PoblarLista();
		}

		// actualiza todas las etiquetas y textos sin depender de claves en idiomaa.cs
		private void ActualizarTextosIdioma()
		{
			if (Idioma.Actual == Lenguaje.EN)
			{
				// textos en ingles
				this.Text = "Elemental Arena - Squad Selection";
				lblTitulo.Text = "ELEMENTAL SQUAD SUMMONING";
				lblSubLista.Text = "AVAILABLE HEROES (CHECK TO SELECT)";
				btnVolver.Text = "BACK TO MENU";
				btnConfirmar.Text = "ENTER THE ARENA ▶";

				// texto por defecto si ningun heroe esta enfocado
				if (ultimoIndiceHover < 0 || ultimoIndiceHover >= catalogoCompleto.Count)
				{
					lblNombreHeroe.Text = "SELECT A HERO";
					lblDetallesHeroe.Text = "Hover over the list to see their mythological archetype and elemental affinity.";
				}
			}
			else
			{
				// textos en espanol
				this.Text = "Arena Elemental - Selección de Equipo";
				lblTitulo.Text = "CONVOCATORIA DEL ESCUADRÓN ELEMENTAL";
				lblSubLista.Text = "HÉROES DISPONIBLES (MARCA PARA SELECCIONAR)";
				btnVolver.Text = "VOLVER AL MENÚ";
				btnConfirmar.Text = "ENTRAR A LA ARENA ▶";

				// texto por defecto si ningun heroe esta enfocado
				if (ultimoIndiceHover < 0 || ultimoIndiceHover >= catalogoCompleto.Count)
				{
					lblNombreHeroe.Text = "SELECCIONA UN HÉROE";
					lblDetallesHeroe.Text = "Pasa el mouse sobre la lista para ver su arquetipo mitológico y afinidad elemental.";
				}
			}

			// recentra el titulo horizontalmente considerando el nuevo tamano del texto
			lblTitulo.Left = (this.ClientSize.Width - lblTitulo.Width) / 2;

			// si ya habia un heroe seleccionado refresca sus datos traducidos
			if (ultimoIndiceHover >= 0 && ultimoIndiceHover < catalogoCompleto.Count)
			{
				MostrarDetallePorIndice(ultimoIndiceHover);
			}
		}

		// configura la imagen de fondo y el pintado con sombra para el titulo
		private void ConfigurarFondoYEstilos()
		{
			// busca la imagen en la carpeta debug de ejecucion
			string rutaFondo = Path.Combine(Application.StartupPath, "fondo.jpg");
			if (!File.Exists(rutaFondo))
			{
				// si no esta en bin debug busca en la raiz del proyecto
				rutaFondo = Path.Combine(Directory.GetCurrentDirectory(), "fondo.jpg");
			}

			// asigna la imagen al picturebox del fondo si existe el archivo
			if (File.Exists(rutaFondo))
			{
				picFondo.Image = Image.FromFile(rutaFondo);
			}

			// emparenta el titulo con el fondo para que conserve transparencia
			lblTitulo.Parent = picFondo;
			lblTitulo.Left = (this.ClientSize.Width - lblTitulo.Width) / 2;

			// evento de dibujo manual para renderizar sombra oscura detras de las letras
			lblTitulo.Paint += (s, e) => {
				e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
				StringFormat sf = new StringFormat();
				sf.Alignment = StringAlignment.Center;
				sf.LineAlignment = StringAlignment.Center;

				// dibuja la sombra desplazada 3 pixeles
				using (SolidBrush sombra = new SolidBrush(Color.FromArgb(240, 10, 10, 10)))
				{
					Rectangle rSombra = new Rectangle(3, 3, lblTitulo.Width, lblTitulo.Height);
					e.Graphics.DrawString(lblTitulo.Text, lblTitulo.Font, sombra, rSombra, sf);
				}

				// dibuja el texto frontal con el color definido
				using (SolidBrush frente = new SolidBrush(lblTitulo.ForeColor))
				{
					Rectangle rFrente = new Rectangle(0, 0, lblTitulo.Width, lblTitulo.Height);
					e.Graphics.DrawString(lblTitulo.Text, lblTitulo.Font, frente, rFrente, sf);
				}
			};

			// configura el picturebox del heroe para dibujar el retrato circular
			picHeroe.BackColor = Color.Transparent;
			picHeroe.Image = null;
			picHeroe.Paint += PicHeroePaint;
		}

		// registra los 12 heroes con sus estadisticas balanceadas y textos en dos idiomas
		private void InicializarCatalogo12Personajes()
		{
			catalogoCompleto.Clear();

			// --- faccion fuego (3 personajes) ---
			catalogoCompleto.Add(new DatosPersonaje {
				Clave = "Ignis",
				NombreES = "Ignis el Forjador", NombreEN = "Ignis the Blacksmith",
				ElementoES = "Fuego", ElementoEN = "Fire",
				Deidad = "Hefesto", HP = 45, DEF = 25, ATK = 30, Empatia = 70,
				ColorElemento = Color.FromArgb(255, 80, 40),
				DescripcionES = "Arquetipo: Hefesto\nStats: HP 45 | DEF 25 | ATK 30 (Total: 100)\nEmpatía: 70 | Habilidad: Forja Ígnea",
				DescripcionEN = "Archetype: Hephaestus\nStats: HP 45 | DEF 25 | ATK 30 (Total: 100)\nEmpathy: 70 | Ability: Fiery Forge"
			});
			catalogoCompleto.Add(new DatosPersonaje {
				Clave = "Pirox",
				NombreES = "Pirox el Rebelde", NombreEN = "Pirox the Rebel",
				ElementoES = "Fuego", ElementoEN = "Fire",
				Deidad = "Prometeo", HP = 40, DEF = 20, ATK = 40, Empatia = 85,
				ColorElemento = Color.FromArgb(255, 95, 30),
				DescripcionES = "Arquetipo: Prometeo\nStats: HP 40 | DEF 20 | ATK 40 (Total: 100)\nEmpatía: 85 | Habilidad: Llama de Rebelión",
				DescripcionEN = "Archetype: Prometheus\nStats: HP 40 | DEF 20 | ATK 40 (Total: 100)\nEmpathy: 85 | Ability: Rebellion Flame"
			});
			catalogoCompleto.Add(new DatosPersonaje {
				Clave = "Vulkan",
				NombreES = "Vulkan el Colérico", NombreEN = "Vulkan the Wrathful",
				ElementoES = "Fuego", ElementoEN = "Fire",
				Deidad = "Ares", HP = 50, DEF = 15, ATK = 35, Empatia = 40,
				ColorElemento = Color.FromArgb(235, 60, 40),
				DescripcionES = "Arquetipo: Ares\nStats: HP 50 | DEF 15 | ATK 35 (Total: 100)\nEmpatía: 40 | Habilidad: Embate Volcánico",
				DescripcionEN = "Archetype: Ares\nStats: HP 50 | DEF 15 | ATK 35 (Total: 100)\nEmpathy: 40 | Ability: Volcanic Onslaught"
			});

			// --- faccion agua (3 personajes) ---
			catalogoCompleto.Add(new DatosPersonaje {
				Clave = "Acuaris",
				NombreES = "Acuaris el Abisal", NombreEN = "Acuaris the Abyssal",
				ElementoES = "Agua", ElementoEN = "Water",
				Deidad = "Poseidón", HP = 50, DEF = 30, ATK = 20, Empatia = 80,
				ColorElemento = Color.FromArgb(60, 170, 255),
				DescripcionES = "Arquetipo: Poseidón\nStats: HP 50 | DEF 30 | ATK 20 (Total: 100)\nEmpatía: 80 | Habilidad: Marea Tectónica",
				DescripcionEN = "Archetype: Poseidon\nStats: HP 50 | DEF 30 | ATK 20 (Total: 100)\nEmpathy: 80 | Ability: Tectonic Tide"
			});
			catalogoCompleto.Add(new DatosPersonaje {
				Clave = "Nereus",
				NombreES = "Nereus del Pantano", NombreEN = "Nereus of the Marsh",
				ElementoES = "Agua", ElementoEN = "Water",
				Deidad = "Tritón", HP = 55, DEF = 30, ATK = 15, Empatia = 90,
				ColorElemento = Color.FromArgb(80, 190, 240),
				DescripcionES = "Arquetipo: Tritón\nStats: HP 55 | DEF 30 | ATK 15 (Total: 100)\nEmpatía: 90 | Habilidad: Niebla Protectora",
				DescripcionEN = "Archetype: Triton\nStats: HP 55 | DEF 30 | ATK 15 (Total: 100)\nEmpathy: 90 | Ability: Protective Mist"
			});
			catalogoCompleto.Add(new DatosPersonaje {
				Clave = "Glauco",
				NombreES = "Glauco el Torrencial", NombreEN = "Glauco the Torrential",
				ElementoES = "Agua", ElementoEN = "Water",
				Deidad = "Océano", HP = 45, DEF = 35, ATK = 20, Empatia = 75,
				ColorElemento = Color.FromArgb(40, 140, 230),
				DescripcionES = "Arquetipo: Océano\nStats: HP 45 | DEF 35 | ATK 20 (Total: 100)\nEmpatía: 75 | Habilidad: Escudo de Resaca",
				DescripcionEN = "Archetype: Oceanus\nStats: HP 45 | DEF 35 | ATK 20 (Total: 100)\nEmpathy: 75 | Ability: Undertow Shield"
			});

			// --- faccion tierra (3 personajes) ---
			catalogoCompleto.Add(new DatosPersonaje {
				Clave = "Terran",
				NombreES = "Terran el Guardián", NombreEN = "Terran the Guardian",
				ElementoES = "Tierra", ElementoEN = "Earth",
				Deidad = "Hades", HP = 55, DEF = 35, ATK = 10, Empatia = 60,
				ColorElemento = Color.FromArgb(190, 145, 85),
				DescripcionES = "Arquetipo: Hades\nStats: HP 55 | DEF 35 | ATK 10 (Total: 100)\nEmpatía: 60 | Habilidad: Valla Sepulcral",
				DescripcionEN = "Archetype: Hades\nStats: HP 55 | DEF 35 | ATK 10 (Total: 100)\nEmpathy: 60 | Ability: Sepulchral Ward"
			});
			catalogoCompleto.Add(new DatosPersonaje {
				Clave = "Gaia",
				NombreES = "Gaia la Primordial", NombreEN = "Gaia the Primordial",
				ElementoES = "Tierra", ElementoEN = "Earth",
				Deidad = "Gea", HP = 60, DEF = 25, ATK = 15, Empatia = 95,
				ColorElemento = Color.FromArgb(160, 185, 95),
				DescripcionES = "Arquetipo: Gea\nStats: HP 60 | DEF 25 | ATK 15 (Total: 100)\nEmpatía: 95 | Habilidad: Resonancia Vital",
				DescripcionEN = "Archetype: Gaia\nStats: HP 60 | DEF 25 | ATK 15 (Total: 100)\nEmpathy: 95 | Ability: Vital Resonance"
			});
			catalogoCompleto.Add(new DatosPersonaje {
				Clave = "Gólem",
				NombreES = "Gólem Telúrico", NombreEN = "Telluric Golem",
				ElementoES = "Tierra", ElementoEN = "Earth",
				Deidad = "Crono", HP = 50, DEF = 40, ATK = 10, Empatia = 50,
				ColorElemento = Color.FromArgb(170, 130, 75),
				DescripcionES = "Arquetipo: Crono\nStats: HP 50 | DEF 40 | ATK 10 (Total: 100)\nEmpatía: 50 | Habilidad: Muralla Sísmica",
				DescripcionEN = "Archetype: Cronus\nStats: HP 50 | DEF 40 | ATK 10 (Total: 100)\nEmpathy: 50 | Ability: Seismic Wall"
			});

			// --- faccion viento (3 personajes) ---
			catalogoCompleto.Add(new DatosPersonaje {
				Clave = "Zephyr",
				NombreES = "Zephyr el Heraldo", NombreEN = "Zephyr the Herald",
				ElementoES = "Viento", ElementoEN = "Wind",
				Deidad = "Hermes", HP = 45, DEF = 20, ATK = 35, Empatia = 80,
				ColorElemento = Color.FromArgb(140, 240, 170),
				DescripcionES = "Arquetipo: Hermes\nStats: HP 45 | DEF 20 | ATK 35 (Total: 100)\nEmpatía: 80 | Habilidad: Paso Etéreo",
				DescripcionEN = "Archetype: Hermes\nStats: HP 45 | DEF 20 | ATK 35 (Total: 100)\nEmpathy: 80 | Ability: Ethereal Stride"
			});
			catalogoCompleto.Add(new DatosPersonaje {
				Clave = "Aura",
				NombreES = "Aura la Cazadora", NombreEN = "Aura the Huntress",
				ElementoES = "Viento", ElementoEN = "Wind",
				Deidad = "Ártemis", HP = 40, DEF = 25, ATK = 35, Empatia = 75,
				ColorElemento = Color.FromArgb(160, 250, 200),
				DescripcionES = "Arquetipo: Ártemis\nStats: HP 40 | DEF 25 | ATK 35 (Total: 100)\nEmpatía: 75 | Habilidad: Ráfaga Guía",
				DescripcionEN = "Archetype: Artemis\nStats: HP 40 | DEF 25 | ATK 35 (Total: 100)\nEmpathy: 75 | Ability: Guiding Gust"
			});
			catalogoCompleto.Add(new DatosPersonaje {
				Clave = "Bóreas",
				NombreES = "Bóreas del Norte", NombreEN = "Boreas of the North",
				ElementoES = "Viento", ElementoEN = "Wind",
				Deidad = "Eolo", HP = 45, DEF = 15, ATK = 40, Empatia = 65,
				ColorElemento = Color.FromArgb(120, 230, 210),
				DescripcionES = "Arquetipo: Eolo\nStats: HP 45 | DEF 15 | ATK 40 (Total: 100)\nEmpatía: 65 | Habilidad: Ventisca Hostil",
				DescripcionEN = "Archetype: Aeolus\nStats: HP 45 | DEF 15 | ATK 40 (Total: 100)\nEmpathy: 65 | Ability: Hostile Blizzard"
			});
		}

		// llena el checkedlistbox con los nombres segun el idioma activo
		private void PoblarLista()
		{
			// guarda los indices de las casillas marcadas antes de limpiar la lista
			List<int> seleccionadosPrevios = new List<int>();
			for (int i = 0; i < clbPersonajes.Items.Count; i++)
			{
				if (clbPersonajes.GetItemChecked(i))
				{
					seleccionadosPrevios.Add(i);
				}
			}

			// limpia y repuebla con el idioma traducido
			clbPersonajes.Items.Clear();
			foreach (var p in catalogoCompleto)
			{
				clbPersonajes.Items.Add(p.NombreActual + " [" + p.ElementoActual + "]");
			}

			// restaura las marcas en las casillas correspondientes
			foreach (int idx in seleccionadosPrevios)
			{
				if (idx < clbPersonajes.Items.Count)
				{
					clbPersonajes.SetItemChecked(idx, true);
				}
			}

			// si hay personajes y no hay foco previo muestra el primero
			if (clbPersonajes.Items.Count > 0 && ultimoIndiceHover < 0)
			{
				MostrarDetallePorIndice(0);
			}
		}

		// configura el cambio de heroe al pasar el mouse o al hacer clic
		private void ConfigurarHoverDinamico()
		{
			// detecta movimiento del mouse para actualizar la tarjeta sin requerir clic
			clbPersonajes.MouseMove += (s, e) => {
				int index = clbPersonajes.IndexFromPoint(e.Location);
				if (index >= 0 && index != ultimoIndiceHover)
				{
					ultimoIndiceHover = index;
					MostrarDetallePorIndice(index);
				}
			};

			// detecta cambio de seleccion por teclado o clic directo
			clbPersonajes.SelectedIndexChanged += (s, e) => {
				if (clbPersonajes.SelectedIndex >= 0)
				{
					ultimoIndiceHover = clbPersonajes.SelectedIndex;
					MostrarDetallePorIndice(clbPersonajes.SelectedIndex);
				}
			};
		}

		// muestra nombre, color de faccion, lore y dispara la carga de imagen
		private void MostrarDetallePorIndice(int index)
		{
			// valida que el indice este dentro del rango de la lista
			if (index < 0 || index >= catalogoCompleto.Count) return;

			DatosPersonaje heroe = catalogoCompleto[index];

			// actualiza nombre y color tematico
			lblNombreHeroe.Text = heroe.NombreActual.ToUpper();
			lblNombreHeroe.ForeColor = heroe.ColorElemento;

			// actualiza texto descriptivo y estadisticas
			lblDetallesHeroe.Text = heroe.DescripcionActual;

			// carga y dibuja el retrato del combatiente
			CargarImagenPersonaje(heroe);
		}

		// busca el archivo de imagen en las carpetas y lo lee en memoria sin bloquearlo
		private void CargarImagenPersonaje(DatosPersonaje heroe)
		{
			// ruta absoluta fija de desarrollo
			string carpetaPersonajes = @"C:\Users\BOOM\Documents\SharpDevelop Projects\proyecto_final_iujo\proyecto_final_iujo\Personajes";

			// si la ruta fija no existe busca relativamente respecto al ejecutable
			if (!Directory.Exists(carpetaPersonajes))
			{
				string rutaRelativa = Path.GetFullPath(Path.Combine(Application.StartupPath, @"..\..\Personajes"));
				if (Directory.Exists(rutaRelativa))
				{
					carpetaPersonajes = rutaRelativa;
				}
				else
				{
					carpetaPersonajes = Application.StartupPath;
				}
			}

			// normaliza nombres con y sin tildes para evitar errores de archivo
			string claveMin = heroe.Clave.ToLower();
			string claveSinTilde = claveMin.Replace("ó", "o").Replace("é", "e").Replace("á", "a").Replace("í", "i").Replace("ú", "u");

			// verifica rutas para archivos con formato png
			string rutaPng = Path.Combine(carpetaPersonajes, claveMin + ".png");
			if (!File.Exists(rutaPng)) rutaPng = Path.Combine(carpetaPersonajes, claveSinTilde + ".png");

			// verifica rutas para archivos con formato jpg
			string rutaJpg = Path.Combine(carpetaPersonajes, claveMin + ".jpg");
			if (!File.Exists(rutaJpg)) rutaJpg = Path.Combine(carpetaPersonajes, claveSinTilde + ".jpg");

			// libera la imagen previa de la memoria si existia
			if (imagenActualHeroe != null)
			{
				imagenActualHeroe.Dispose();
				imagenActualHeroe = null;
			}

			// lee el archivo mediante filestream para evitar candados en el sistema de archivos
			if (File.Exists(rutaPng))
			{
				using (FileStream fs = new FileStream(rutaPng, FileMode.Open, FileAccess.Read))
				{
					imagenActualHeroe = new Bitmap(Image.FromStream(fs));
				}
			}
			else if (File.Exists(rutaJpg))
			{
				using (FileStream fs = new FileStream(rutaJpg, FileMode.Open, FileAccess.Read))
				{
					imagenActualHeroe = new Bitmap(Image.FromStream(fs));
				}
			}

			// actualiza el color del borde y fuerza el repintado
			colorActualBorde = heroe.ColorElemento;
			picHeroe.Invalidate();
		}

		// renderiza la imagen recortada en un circulo con borde de color
		private void PicHeroePaint(object sender, PaintEventArgs e)
		{
			Graphics g = e.Graphics;
			g.SmoothingMode = SmoothingMode.AntiAlias;
			g.PixelOffsetMode = PixelOffsetMode.HighQuality;

			// calcula las dimensiones para un circulo centrado en el control
			int diametro = Math.Min(picHeroe.Width, picHeroe.Height) - 8;
			int posX = (picHeroe.Width - diametro) / 2;
			int posY = (picHeroe.Height - diametro) / 2;
			Rectangle rectCirculo = new Rectangle(posX, posY, diametro, diametro);

			// dibuja el fondo base oscuro del marco
			using (SolidBrush brochaFondo = new SolidBrush(Color.FromArgb(16, 20, 16)))
			{
				g.FillEllipse(brochaFondo, rectCirculo);
			}

			// si se cargo la imagen aplica el recorte circular
			if (imagenActualHeroe != null)
			{
				using (GraphicsPath clipPath = new GraphicsPath())
				{
					clipPath.AddEllipse(rectCirculo);
					g.SetClip(clipPath);

					// escala la imagen manteniendo la proporcion
					float escala = Math.Max((float)diametro / imagenActualHeroe.Width, (float)diametro / imagenActualHeroe.Height);
					int anchoEscalado = (int)(imagenActualHeroe.Width * escala);
					int altoEscalado = (int)(imagenActualHeroe.Height * escala);
					int x = posX + (diametro - anchoEscalado) / 2;
					int y = posY + (diametro - altoEscalado) / 2;

					// dibuja la imagen escalada
					g.DrawImage(imagenActualHeroe, new Rectangle(x, y, anchoEscalado, altoEscalado));
					g.ResetClip();
				}
			}
			else
			{
				// si no se encuentra la imagen muestra un signo de interrogacion
				using (Font f = new Font("Impact", 36))
				using (SolidBrush brochaTexto = new SolidBrush(Color.White))
				{
					StringFormat sf = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
					g.DrawString("?", f, brochaTexto, rectCirculo, sf);
				}
			}

			// dibuja el anillo perimetral con el color del elemento
			using (Pen penBorde = new Pen(colorActualBorde, 4))
			{
				g.DrawEllipse(penBorde, rectCirculo);
			}
		}

		// valida la regla de seleccion y pasa los personajes al combate
		void BtnConfirmarClick(object sender, EventArgs e)
		{
			List<string> seleccionados = new List<string>();
			for (int i = 0; i < clbPersonajes.Items.Count; i++)
			{
				// revisa si el item esta marcado con casilla de verificacion
				if (clbPersonajes.GetItemChecked(i))
				{
					// guarda en formato estandar para no romper la logica de formpartida
					DatosPersonaje p = catalogoCompleto[i];
					seleccionados.Add(p.NombreES + " [" + p.ElementoES + "]");
				}
			}

			// valida que sean exactamente 5 personajes
			if (seleccionados.Count != 5)
			{
				// mensaje de advertencia bilingue sin depender de claves de archivo
				if (Idioma.Actual == Lenguaje.EN)
				{
					MessageBox.Show(
						string.Format("You must select exactly 5 characters to build your squad (selected: {0}).", seleccionados.Count),
						"Deck Rule",
						MessageBoxButtons.OK,
						MessageBoxIcon.Warning
					);
				}
				else
				{
					MessageBox.Show(
						string.Format("Debes seleccionar exactamente 5 personajes para armar tu equipo (seleccionados: {0}).", seleccionados.Count),
						"Regla de Mazo",
						MessageBoxButtons.OK,
						MessageBoxIcon.Warning
					);
				}
				return;
			}

			// desuscribe el evento de idioma para evitar fugas de memoria
			Idioma.Cambiado -= OnIdiomaCambiado;

			// abre la pantalla de combate con el escuadron validado
			FormPartida partida = new FormPartida(seleccionados);
			partida.Show();
			this.Dispose();
		}

		// boton para regresar a la pantalla del menu principal
		void BtnVolverClick(object sender, EventArgs e)
		{
			// desuscribe el evento de idioma
			Idioma.Cambiado -= OnIdiomaCambiado;

			// libera la imagen activa en memoria
			if (imagenActualHeroe != null)
			{
				imagenActualHeroe.Dispose();
				imagenActualHeroe = null;
			}

			// regresa al menu principal
			MainForm menu = new MainForm();
			menu.Show();
			this.Dispose();
		}

		// evento disparado al cerrar la ventana
		protected override void OnFormClosing(FormClosingEventArgs e)
		{
			// remueve la suscripcion al evento global de idioma
			Idioma.Cambiado -= OnIdiomaCambiado;

			// libera recursos graficos de imagen
			if (imagenActualHeroe != null)
			{
				imagenActualHeroe.Dispose();
				imagenActualHeroe = null;
			}

			base.OnFormClosing(e);

			// termina el proceso completamente si el usuario presiono la x de la ventana
			if (e.CloseReason == CloseReason.UserClosing)
			{
				Application.Exit();
			}
		}
	}
}