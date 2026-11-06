using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Windows.Forms;

namespace proyecto_final_iujo.Visuales
{
	public partial class MainForm : Form
	{
		public MainForm()
		{
			InitializeComponent();
			ConfigurarFondo();
			AplicarSombraATitulo();
			AsignarOpcionesConSombraYMovimiento();
			ConfigurarAccesoLore();
		}

		private void ConfigurarFondo()
		{
			// Prioridad: bin\Debug\fondo.jpg y fallback en la ruta raíz del proyecto
			string rutaFondo = Path.Combine(Application.StartupPath, "fondo.jpg");
			if (!File.Exists(rutaFondo))
			{
				rutaFondo = Path.Combine(Directory.GetCurrentDirectory(), "fondo.jpg");
			}

			if (File.Exists(rutaFondo))
			{
				picFondo.Image = Image.FromFile(rutaFondo);
			}

			// Emparienta con el PictureBox para que el fondo no tenga áreas grises
			lblTitulo.Parent = picFondo;
			lblJugar.Parent = picFondo;
			lblIdioma.Parent = picFondo;
			lblSalir.Parent = picFondo;

			// Centrado horizontal exacto en pantalla
			CentrarHorizontal(lblTitulo);
			CentrarHorizontal(lblJugar);
			CentrarHorizontal(lblIdioma);
			CentrarHorizontal(lblSalir);
		}

		private void CentrarHorizontal(Control ctrl)
		{
			ctrl.Left = (this.ClientSize.Width - ctrl.Width) / 2;
		}

		private void AplicarSombraATitulo()
		{
			lblTitulo.Paint += (s, e) => {
				e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
				string texto = lblTitulo.Text;
				Font fuente = lblTitulo.Font;

				StringFormat sf = new StringFormat();
				sf.Alignment = StringAlignment.Center;
				sf.LineAlignment = StringAlignment.Center;

				// Sombra negra retro de 3 píxeles
				using (SolidBrush sombra = new SolidBrush(Color.FromArgb(240, 10, 10, 10)))
				{
					Rectangle rSombra = new Rectangle(3, 3, lblTitulo.Width, lblTitulo.Height);
					e.Graphics.DrawString(texto, fuente, sombra, rSombra, sf);
				}

				// Texto dorado superior
				using (SolidBrush frente = new SolidBrush(lblTitulo.ForeColor))
				{
					Rectangle rFrente = new Rectangle(0, 0, lblTitulo.Width, lblTitulo.Height);
					e.Graphics.DrawString(texto, fuente, frente, rFrente, sf);
				}
			};
		}

		private void AsignarOpcionesConSombraYMovimiento()
		{
			// Opción JUGAR -> Abre primero el Lore del Mundo
			ConfigurarOpcionInteractiva(lblJugar, "JUGAR", () => {
				FormLoreDinamico lore = new FormLoreDinamico();
				lore.Show();
				this.Hide();
			});

			// Opción CAMBIAR IDIOMA
			ConfigurarOpcionInteractiva(lblIdioma, "CAMBIAR IDIOMA [ES]", () => {
				// Reservado para la lógica de traducción a inglés
			});

			// Opción SALIR
			ConfigurarOpcionInteractiva(lblSalir, "SALIR", () => {
				Application.Exit();
			});
		}

		private void ConfigurarOpcionInteractiva(Label opcion, string textoBase, Action accionClick)
		{
			int yOriginal = opcion.Location.Y;

			// Sombra personalizada en cada frame de dibujado
			opcion.Paint += (s, e) => {
				e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
				StringFormat sf = new StringFormat();
				sf.Alignment = StringAlignment.Center;
				sf.LineAlignment = StringAlignment.Center;

				// Sombra negra de fondo
				using (SolidBrush sombra = new SolidBrush(Color.FromArgb(245, 10, 10, 10)))
				{
					Rectangle rSombra = new Rectangle(2, 2, opcion.Width, opcion.Height);
					e.Graphics.DrawString(opcion.Text, opcion.Font, sombra, rSombra, sf);
				}

				// Texto principal (blanco o dorado en hover)
				using (SolidBrush frente = new SolidBrush(opcion.ForeColor))
				{
					Rectangle rFrente = new Rectangle(0, 0, opcion.Width, opcion.Height);
					e.Graphics.DrawString(opcion.Text, opcion.Font, frente, rFrente, sf);
				}
			};

			// Al pasar el cursor: color dorado y punteros retro
			opcion.MouseEnter += (s, e) => {
				opcion.ForeColor = Color.FromArgb(255, 230, 90);
				opcion.Text = "▶ " + textoBase + " ◀";
				opcion.Invalidate();
			};

			// Al retirar el cursor: vuelve al estado original
			opcion.MouseLeave += (s, e) => {
				opcion.ForeColor = Color.White;
				opcion.Text = textoBase;
				opcion.Top = yOriginal;
				opcion.Invalidate();
			};

			// Presionar: desplazamiento táctil hacia abajo
			opcion.MouseDown += (s, e) => {
				opcion.Top = yOriginal + 3;
			};

			// Soltar clic: vuelve a su altura
			opcion.MouseUp += (s, e) => {
				opcion.Top = yOriginal;
			};

			opcion.Click += (s, e) => {
				accionClick();
			};
		}

		private void ConfigurarAccesoLore()
		{
			// Atajo de teclado: pulsa la tecla 'L' para abrir el Lore Dinámico
			this.KeyPreview = true;
			this.KeyDown += (s, e) => {
				if (e.KeyCode == Keys.L)
				{
					FormLoreDinamico formLore = new FormLoreDinamico();
					formLore.Show();
					this.Hide();
				}
			};
		}
	
	
	}
	
	
}