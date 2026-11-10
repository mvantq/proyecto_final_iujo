using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Windows.Forms;
using proyecto_final_iujo.Logica;

namespace proyecto_final_iujo.Visuales
{
    // ventana del menu principal del juego
    public partial class MainForm : Form
    {
        // constructor que inicializa los componentes y estilos de la interfaz
        public MainForm()
        {
            InitializeComponent();
            ConfigurarFondo();
            AplicarSombraATitulo();

            // suscripcion al evento que detecta cambios de idioma
            Idioma.Cambiado += (s, e) => ActualizarTextosInterfaz();

            AsignarOpcionesConSombraYMovimiento();
            ConfigurarAccesoLore();
            ActualizarTextosInterfaz(); // carga el idioma seleccionado
        }

        // actualiza el texto de los botones y etiquetas segun el idioma activo
        private void ActualizarTextosInterfaz()
        {
            // asigna los textos desde la clase de idiomas
            lblTitulo.Text = Idioma.T("inicio.titulo");
            this.Text = Idioma.T("app.titulo") + " - Menu Principal";

            // redibuja las opciones para aplicar cambios
            lblJugar.Invalidate();
            lblIdioma.Text = Idioma.T("inicio.idioma");
            lblIdioma.Invalidate();
            lblSalir.Text = Idioma.T("inicio.salir");
            lblSalir.Invalidate();
        }

        // busca y carga la imagen de fondo centrando los textos
        private void ConfigurarFondo()
        {
            // busca fondo.jpg en la carpeta debug o en la raiz del proyecto
            string rutaFondo = Path.Combine(Application.StartupPath, "fondo.jpg");
            if (!File.Exists(rutaFondo))
            {
                rutaFondo = Path.Combine(Directory.GetCurrentDirectory(), "fondo.jpg");
            }

            if (File.Exists(rutaFondo))
            {
                picFondo.Image = Image.FromFile(rutaFondo);
            }

            // emparenta los controles con la imagen para quitar fondos grises
            lblTitulo.Parent = picFondo;
            lblJugar.Parent = picFondo;
            lblIdioma.Parent = picFondo;
            lblSalir.Parent = picFondo;

            // centra horizontalmente los textos en pantalla
            CentrarHorizontal(lblTitulo);
            CentrarHorizontal(lblJugar);
            CentrarHorizontal(lblIdioma);
            CentrarHorizontal(lblSalir);
        }

        // calcula el centrado horizontal de un control
        private void CentrarHorizontal(Control ctrl)
        {
            ctrl.Left = (this.ClientSize.Width - ctrl.Width) / 2;
        }

        // dibuja el texto del titulo con sombra oscura retro
        private void AplicarSombraATitulo()
        {
            lblTitulo.Paint += (s, e) => {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                string texto = lblTitulo.Text;
                Font fuente = lblTitulo.Font;

                StringFormat sf = new StringFormat();
                sf.Alignment = StringAlignment.Center;
                sf.LineAlignment = StringAlignment.Center;

                // sombra negra desplazada
                using (SolidBrush sombra = new SolidBrush(Color.FromArgb(240, 10, 10, 10)))
                {
                    Rectangle rSombra = new Rectangle(3, 3, lblTitulo.Width, lblTitulo.Height);
                    e.Graphics.DrawString(texto, fuente, sombra, rSombra, sf);
                }

                // texto dorado superior
                using (SolidBrush frente = new SolidBrush(lblTitulo.ForeColor))
                {
                    Rectangle rFrente = new Rectangle(0, 0, lblTitulo.Width, lblTitulo.Height);
                    e.Graphics.DrawString(texto, fuente, frente, rFrente, sf);
                }
            };
        }

        // configura los eventos y acciones para los botones del menu
        private void AsignarOpcionesConSombraYMovimiento()
        {
            // boton jugar: abre la pantalla de lore y oculta el menu
            ConfigurarOpcionInteractiva(lblJugar, () => Idioma.T("inicio.jugar").Replace("▶  ", "").Replace("▶  ", ""), () => {
                FormLoreDinamico lore = new FormLoreDinamico();
                lore.Show();
                this.Hide();
            });

            // boton idioma: alterna entre espanol e ingles
            ConfigurarOpcionInteractiva(lblIdioma, () => Idioma.T("inicio.idioma"), () => {
                Idioma.Alternar();
            });

            // boton salir: cierra toda la aplicacion
            ConfigurarOpcionInteractiva(lblSalir, () => Idioma.T("inicio.salir"), () => {
                Application.Exit();
            });
        }

        // aplica efectos de sombra, hover, click y actualizacion de texto dinámico
        private void ConfigurarOpcionInteractiva(Label opcion, Func<string> obtenerTexto, Action accionClick)
        {
            int yOriginal = opcion.Location.Y;
            opcion.Text = obtenerTexto();

            // actualiza el texto si cambia el idioma
            Idioma.Cambiado += (s, e) => {
                opcion.Text = obtenerTexto();
                opcion.Invalidate();
            };

            // dibuja la sombra y el texto del boton
            opcion.Paint += (s, e) => {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                StringFormat sf = new StringFormat();
                sf.Alignment = StringAlignment.Center;
                sf.LineAlignment = StringAlignment.Center;

                // sombra oscura
                using (SolidBrush sombra = new SolidBrush(Color.FromArgb(245, 10, 10, 10)))
                {
                    Rectangle rSombra = new Rectangle(2, 2, opcion.Width, opcion.Height);
                    e.Graphics.DrawString(opcion.Text, opcion.Font, sombra, rSombra, sf);
                }

                // texto frontal
                using (SolidBrush frente = new SolidBrush(opcion.ForeColor))
                {
                    Rectangle rFrente = new Rectangle(0, 0, opcion.Width, opcion.Height);
                    e.Graphics.DrawString(opcion.Text, opcion.Font, frente, rFrente, sf);
                }
            };

            // cambia el color a amarillo al pasar el mouse por encima
            opcion.MouseEnter += (s, e) => {
                opcion.ForeColor = Color.FromArgb(255, 230, 90);
                opcion.Invalidate();
            };

            // vuelve al color blanco original cuando el mouse sale
            opcion.MouseLeave += (s, e) => {
                opcion.ForeColor = Color.White;
                opcion.Top = yOriginal;
                opcion.Invalidate();
            };

            // efecto de presion al presionar y soltar el boton
            opcion.MouseDown += (s, e) => { opcion.Top = yOriginal + 3; };
            opcion.MouseUp += (s, e) => { opcion.Top = yOriginal; };
            opcion.Click += (s, e) => { accionClick(); };
        }

        // mata por completo el proceso al cerrar la ventana principal
        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            base.OnFormClosed(e);
            Environment.Exit(0);
        }

        // atajo con la tecla l para saltar directo a la pantalla de lore
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