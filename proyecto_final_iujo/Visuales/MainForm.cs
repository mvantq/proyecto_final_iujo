using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Windows.Forms;
using proyecto_final_iujo.Logica;
namespace proyecto_final_iujo.Visuales
	
{
	public partial class MainForm : Form
	{
		public MainForm()
		{
			InitializeComponent();
			ConfigurarFondo();
			AplicarSombraATitulo();
			
			// 1. Nos suscribimos al evento de cambio de idioma (¡Tema de 2do Semestre!)
			Idioma.Cambiado += (s, e) => ActualizarTextosInterfaz();
			
			AsignarOpcionesConSombraYMovimiento();
			ConfigurarAccesoLore();
			ActualizarTextosInterfaz(); // Cargar idioma inicial
		}

		private void ActualizarTextosInterfaz()
		{
			// Actualizamos los textos usando el diccionario centralizado
			lblTitulo.Text = Idioma.T("inicio.titulo");
			this.Text = Idioma.T("app.titulo") + " - Menu Principal";
			
			// Forzamos redibujado de las opciones interactivas
			lblJugar.Invalidate();
			lblIdioma.Text = Idioma.T("inicio.idioma");
			lblIdioma.Invalidate();
			lblSalir.Text = Idioma.T("inicio.salir");
			lblSalir.Invalidate();
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
			// Opción JUGAR -> Abre el Lore del Mundo
			ConfigurarOpcionInteractiva(lblJugar, () => Idioma.T("inicio.jugar").Replace("▶  ", "").Replace("▶  ", ""), () => {
				FormLoreDinamico lore = new FormLoreDinamico();
				lore.Show();
				this.Hide();
			});

			// Opción CAMBIAR IDIOMA -> Alterna el idioma globalmente con un clic
			ConfigurarOpcionInteractiva(lblIdioma, () => Idioma.T("inicio.idioma"), () => {
				Idioma.Alternar(); // Esto cambia entre ES y EN y dispara el evento Cambiado
			});

			// Opción SALIR
			ConfigurarOpcionInteractiva(lblSalir, () => Idioma.T("inicio.salir"), () => {
				Application.Exit();
			});
		}

		// Modificamos ligeramente para aceptar funciones de texto dinámico
		private void ConfigurarOpcionInteractiva(Label opcion, Func<string> obtenerTexto, Action accionClick)
		{
			int yOriginal = opcion.Location.Y;
			opcion.Text = obtenerTexto();

			// Actualizar texto cada vez que cambie el idioma
			Idioma.Cambiado += (s, e) => {
				opcion.Text = obtenerTexto();
				opcion.Invalidate();
			};

			opcion.Paint += (s, e) => {
				e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
				StringFormat sf = new StringFormat();
				sf.Alignment = StringAlignment.Center;
				sf.LineAlignment = StringAlignment.Center;

				using (SolidBrush sombra = new SolidBrush(Color.FromArgb(245, 10, 10, 10)))
				{
					Rectangle rSombra = new Rectangle(2, 2, opcion.Width, opcion.Height);
					e.Graphics.DrawString(opcion.Text, opcion.Font, sombra, rSombra, sf);
				}

				using (SolidBrush frente = new SolidBrush(opcion.ForeColor))
				{
					Rectangle rFrente = new Rectangle(0, 0, opcion.Width, opcion.Height);
					e.Graphics.DrawString(opcion.Text, opcion.Font, frente, rFrente, sf);
				}
			};

			opcion.MouseEnter += (s, e) => {
				opcion.ForeColor = Color.FromArgb(255, 230, 90);
				opcion.Invalidate();
			};

			opcion.MouseLeave += (s, e) => {
				opcion.ForeColor = Color.White;
				opcion.Top = yOriginal;
				opcion.Invalidate();
			};

			opcion.MouseDown += (s, e) => { opcion.Top = yOriginal + 3; };
			opcion.MouseUp += (s, e) => { opcion.Top = yOriginal; };
			opcion.Click += (s, e) => { accionClick(); };
		}

		private void ConfigurarAccesoLore()
		{
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