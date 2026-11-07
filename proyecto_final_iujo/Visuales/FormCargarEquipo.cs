using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Windows.Forms;

namespace proyecto_final_iujo.Visuales
{
	public partial class FormCargarEquipo : Form
	{
		public class DatosPersonaje
		{
			public string Clave { get; set; }
			public string Nombre { get; set; }
			public string Elemento { get; set; }
			public string Deidad { get; set; }
			public int HP { get; set; }
			public int DEF { get; set; }
			public int ATK { get; set; }
			public int Empatia { get; set; }
			public Color ColorElemento { get; set; }
			public string Descripcion { get; set; }
		}

		private List<DatosPersonaje> catalogoCompleto = new List<DatosPersonaje>();
		private int ultimoIndiceHover = -1;
		private Image imagenActualHeroe = null;
		private Color colorActualBorde = Color.Gold;

		public FormCargarEquipo()
		{
			InitializeComponent();
			ConfigurarFondoYEstilos();
			InicializarCatalogo12Personajes();
			PoblarLista();
			ConfigurarHoverDinamico();
		}

		private void ConfigurarFondoYEstilos()
		{
			string rutaFondo = Path.Combine(Application.StartupPath, "fondo.jpg");
			if (!File.Exists(rutaFondo))
			{
				rutaFondo = Path.Combine(Directory.GetCurrentDirectory(), "fondo.jpg");
			}

			if (File.Exists(rutaFondo))
			{
				picFondo.Image = Image.FromFile(rutaFondo);
			}

			lblTitulo.Parent = picFondo;
			lblTitulo.Left = (this.ClientSize.Width - lblTitulo.Width) / 2;

			// Sombra retro para el título
			lblTitulo.Paint += (s, e) => {
				e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
				StringFormat sf = new StringFormat();
				sf.Alignment = StringAlignment.Center;
				sf.LineAlignment = StringAlignment.Center;

				using (SolidBrush sombra = new SolidBrush(Color.FromArgb(240, 10, 10, 10)))
				{
					Rectangle rSombra = new Rectangle(3, 3, lblTitulo.Width, lblTitulo.Height);
					e.Graphics.DrawString(lblTitulo.Text, lblTitulo.Font, sombra, rSombra, sf);
				}

				using (SolidBrush frente = new SolidBrush(lblTitulo.ForeColor))
				{
					Rectangle rFrente = new Rectangle(0, 0, lblTitulo.Width, lblTitulo.Height);
					e.Graphics.DrawString(lblTitulo.Text, lblTitulo.Font, frente, rFrente, sf);
				}
			};

			// Configuración del PictureBox para renderizado circular limpio
			picHeroe.BackColor = Color.Transparent;
			picHeroe.Image = null;
			picHeroe.Paint += PicHeroePaint;
		}

		private void InicializarCatalogo12Personajes()
		{
			catalogoCompleto.Clear();

			// --- FACCION FUEGO (3) - HP + DEF + ATK = 100
			catalogoCompleto.Add(new DatosPersonaje {
				Clave = "Ignis", Nombre = "Ignis el Forjador", Elemento = "Fuego", Deidad = "Hefesto",
				HP = 45, DEF = 25, ATK = 30, Empatia = 70, ColorElemento = Color.FromArgb(255, 80, 40),
				Descripcion = "Arquetipo: Hefesto\nStats: HP 45 | DEF 25 | ATK 30 (Total: 100)\nEmpatía: 70 | Habilidad: Forja Ígnea"
			});
			catalogoCompleto.Add(new DatosPersonaje {
				Clave = "Pirox", Nombre = "Pirox el Rebelde", Elemento = "Fuego", Deidad = "Prometeo",
				HP = 40, DEF = 20, ATK = 40, Empatia = 85, ColorElemento = Color.FromArgb(255, 95, 30),
				Descripcion = "Arquetipo: Prometeo\nStats: HP 40 | DEF 20 | ATK 40 (Total: 100)\nEmpatía: 85 | Habilidad: Llama de Rebelión"
			});
			catalogoCompleto.Add(new DatosPersonaje {
				Clave = "Vulkan", Nombre = "Vulkan el Colérico", Elemento = "Fuego", Deidad = "Ares",
				HP = 50, DEF = 15, ATK = 35, Empatia = 40, ColorElemento = Color.FromArgb(235, 60, 40),
				Descripcion = "Arquetipo: Ares\nStats: HP 50 | DEF 15 | ATK 35 (Total: 100)\nEmpatía: 40 | Habilidad: Embate Volcánico"
			});

			// --- FACCION AGUA (3) - HP + DEF + ATK = 100
			catalogoCompleto.Add(new DatosPersonaje {
				Clave = "Acuaris", Nombre = "Acuaris el Abisal", Elemento = "Agua", Deidad = "Poseidón",
				HP = 50, DEF = 30, ATK = 20, Empatia = 80, ColorElemento = Color.FromArgb(60, 170, 255),
				Descripcion = "Arquetipo: Poseidón\nStats: HP 50 | DEF 30 | ATK 20 (Total: 100)\nEmpatía: 80 | Habilidad: Marea Tectónica"
			});
			catalogoCompleto.Add(new DatosPersonaje {
				Clave = "Nereus", Nombre = "Nereus del Pantano", Elemento = "Agua", Deidad = "Tritón",
				HP = 55, DEF = 30, ATK = 15, Empatia = 90, ColorElemento = Color.FromArgb(80, 190, 240),
				Descripcion = "Arquetipo: Tritón\nStats: HP 55 | DEF 30 | ATK 15 (Total: 100)\nEmpatía: 90 | Habilidad: Niebla Protectora"
			});
			catalogoCompleto.Add(new DatosPersonaje {
				Clave = "Glauco", Nombre = "Glauco el Torrencial", Elemento = "Agua", Deidad = "Océano",
				HP = 45, DEF = 35, ATK = 20, Empatia = 75, ColorElemento = Color.FromArgb(40, 140, 230),
				Descripcion = "Arquetipo: Océano\nStats: HP 45 | DEF 35 | ATK 20 (Total: 100)\nEmpatía: 75 | Habilidad: Escudo de Resaca"
			});

			// --- FACCION TIERRA (3) - HP + DEF + ATK = 100
			catalogoCompleto.Add(new DatosPersonaje {
				Clave = "Terran", Nombre = "Terran el Guardián", Elemento = "Tierra", Deidad = "Hades",
				HP = 55, DEF = 35, ATK = 10, Empatia = 60, ColorElemento = Color.FromArgb(190, 145, 85),
				Descripcion = "Arquetipo: Hades\nStats: HP 55 | DEF 35 | ATK 10 (Total: 100)\nEmpatía: 60 | Habilidad: Valla Sepulcral"
			});
			catalogoCompleto.Add(new DatosPersonaje {
				Clave = "Gaia", Nombre = "Gaia la Primordial", Elemento = "Tierra", Deidad = "Gea",
				HP = 60, DEF = 25, ATK = 15, Empatia = 95, ColorElemento = Color.FromArgb(160, 185, 95),
				Descripcion = "Arquetipo: Gea\nStats: HP 60 | DEF 25 | ATK 15 (Total: 100)\nEmpatía: 95 | Habilidad: Resonancia Vital"
			});
			catalogoCompleto.Add(new DatosPersonaje {
				Clave = "Gólem", Nombre = "Gólem Telúrico", Elemento = "Tierra", Deidad = "Crono",
				HP = 50, DEF = 40, ATK = 10, Empatia = 50, ColorElemento = Color.FromArgb(170, 130, 75),
				Descripcion = "Arquetipo: Crono\nStats: HP 50 | DEF 40 | ATK 10 (Total: 100)\nEmpatía: 50 | Habilidad: Muralla Sísmica"
			});

			// --- FACCION VIENTO (3) - HP + DEF + ATK = 100
			catalogoCompleto.Add(new DatosPersonaje {
				Clave = "Zephyr", Nombre = "Zephyr el Heraldo", Elemento = "Viento", Deidad = "Hermes",
				HP = 45, DEF = 20, ATK = 35, Empatia = 80, ColorElemento = Color.FromArgb(140, 240, 170),
				Descripcion = "Arquetipo: Hermes\nStats: HP 45 | DEF 20 | ATK 35 (Total: 100)\nEmpatía: 80 | Habilidad: Paso Etéreo"
			});
			catalogoCompleto.Add(new DatosPersonaje {
				Clave = "Aura", Nombre = "Aura la Cazadora", Elemento = "Viento", Deidad = "Ártemis",
				HP = 40, DEF = 25, ATK = 35, Empatia = 75, ColorElemento = Color.FromArgb(160, 250, 200),
				Descripcion = "Arquetipo: Ártemis\nStats: HP 40 | DEF 25 | ATK 35 (Total: 100)\nEmpatía: 75 | Habilidad: Ráfaga Guía"
			});
			catalogoCompleto.Add(new DatosPersonaje {
				Clave = "Bóreas", Nombre = "Bóreas del Norte", Elemento = "Viento", Deidad = "Eolo",
				HP = 45, DEF = 15, ATK = 40, Empatia = 65, ColorElemento = Color.FromArgb(120, 230, 210),
				Descripcion = "Arquetipo: Eolo\nStats: HP 45 | DEF 15 | ATK 40 (Total: 100)\nEmpatía: 65 | Habilidad: Ventisca Hostil"
			});
		}

		private void PoblarLista()
		{
			clbPersonajes.Items.Clear();
			foreach (var p in catalogoCompleto)
			{
				clbPersonajes.Items.Add(p.Nombre + " [" + p.Elemento + "]");
			}

			if (clbPersonajes.Items.Count > 0)
			{
				MostrarDetallePorIndice(0);
			}
		}

		private void ConfigurarHoverDinamico()
		{
			clbPersonajes.MouseMove += (s, e) => {
				int index = clbPersonajes.IndexFromPoint(e.Location);
				if (index >= 0 && index != ultimoIndiceHover)
				{
					ultimoIndiceHover = index;
					MostrarDetallePorIndice(index);
				}
			};

			clbPersonajes.SelectedIndexChanged += (s, e) => {
				if (clbPersonajes.SelectedIndex >= 0)
				{
					MostrarDetallePorIndice(clbPersonajes.SelectedIndex);
				}
			};
		}

		private void MostrarDetallePorIndice(int index)
		{
			if (index < 0 || index >= catalogoCompleto.Count) return;

			DatosPersonaje heroe = catalogoCompleto[index];

			lblNombreHeroe.Text = heroe.Nombre.ToUpper();
			lblNombreHeroe.ForeColor = heroe.ColorElemento;
			lblDetallesHeroe.Text = heroe.Descripcion;

			CargarImagenPersonaje(heroe);
		}

		private void CargarImagenPersonaje(DatosPersonaje heroe)
		{
			string carpetaPersonajes = @"C:\Users\BOOM\Documents\SharpDevelop Projects\proyecto_final_iujo\proyecto_final_iujo\Personajes";

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

			// Búsqueda flexible con y sin tilde
			string claveMin = heroe.Clave.ToLower();
			string claveSinTilde = claveMin.Replace("ó", "o").Replace("é", "e").Replace("á", "a").Replace("í", "i").Replace("ú", "u");

			string rutaPng = Path.Combine(carpetaPersonajes, claveMin + ".png");
			if (!File.Exists(rutaPng)) rutaPng = Path.Combine(carpetaPersonajes, claveSinTilde + ".png");

			string rutaJpg = Path.Combine(carpetaPersonajes, claveMin + ".jpg");
			if (!File.Exists(rutaJpg)) rutaJpg = Path.Combine(carpetaPersonajes, claveSinTilde + ".jpg");

			if (imagenActualHeroe != null)
			{
				imagenActualHeroe.Dispose();
				imagenActualHeroe = null;
			}

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

			colorActualBorde = heroe.ColorElemento;
			picHeroe.Invalidate();
		}

		private void PicHeroePaint(object sender, PaintEventArgs e)
		{
			Graphics g = e.Graphics;
			g.SmoothingMode = SmoothingMode.AntiAlias;
			g.PixelOffsetMode = PixelOffsetMode.HighQuality;

			int diametro = Math.Min(picHeroe.Width, picHeroe.Height) - 8;
			int posX = (picHeroe.Width - diametro) / 2;
			int posY = (picHeroe.Height - diametro) / 2;
			Rectangle rectCirculo = new Rectangle(posX, posY, diametro, diametro);

			// Relleno base oscuro dentro del círculo
			using (SolidBrush brochaFondo = new SolidBrush(Color.FromArgb(16, 20, 16)))
			{
				g.FillEllipse(brochaFondo, rectCirculo);
			}

			if (imagenActualHeroe != null)
			{
				// Recorte circular estricto (elimina cualquier esquina cuadrada)
				using (GraphicsPath clipPath = new GraphicsPath())
				{
					clipPath.AddEllipse(rectCirculo);
					g.SetClip(clipPath);

					float escala = Math.Max((float)diametro / imagenActualHeroe.Width, (float)diametro / imagenActualHeroe.Height);
					int anchoEscalado = (int)(imagenActualHeroe.Width * escala);
					int altoEscalado = (int)(imagenActualHeroe.Height * escala);
					int x = posX + (diametro - anchoEscalado) / 2;
					int y = posY + (diametro - altoEscalado) / 2;

					g.DrawImage(imagenActualHeroe, new Rectangle(x, y, anchoEscalado, altoEscalado));
					g.ResetClip();
				}
			}
			else
			{
				// Avatar de respaldo si no hay imagen en la carpeta
				using (Font f = new Font("Impact", 36))
				using (SolidBrush brochaTexto = new SolidBrush(Color.White))
				{
					StringFormat sf = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
					g.DrawString("?", f, brochaTexto, rectCirculo, sf);
				}
			}

			// Borde circular brillante del color del elemento
			using (Pen penBorde = new Pen(colorActualBorde, 4))
			{
				g.DrawEllipse(penBorde, rectCirculo);
			}
		}

		void BtnConfirmarClick(object sender, EventArgs e)
		{
			List<string> seleccionados = new List<string>();
			foreach (var item in clbPersonajes.CheckedItems)
			{
				seleccionados.Add(item.ToString());
			}

			if (seleccionados.Count != 5)
			{
				MessageBox.Show("Debes seleccionar exactamente 5 personajes para armar tu equipo según las reglas de la Arena (seleccionados: " + seleccionados.Count + ").", "Regla de Mazo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}

			FormPartida partida = new FormPartida(seleccionados);
			partida.Show();
			this.Close();
		}

		void BtnVolverClick(object sender, EventArgs e)
		{
			if (imagenActualHeroe != null)
			{
				imagenActualHeroe.Dispose();
				imagenActualHeroe = null;
			}

			MainForm menu = new MainForm();
			menu.Show();
			this.Close();
		}

		protected override void OnFormClosing(FormClosingEventArgs e)
		{
			if (imagenActualHeroe != null)
			{
				imagenActualHeroe.Dispose();
				imagenActualHeroe = null;
			}
			base.OnFormClosing(e);
		}
	}
}