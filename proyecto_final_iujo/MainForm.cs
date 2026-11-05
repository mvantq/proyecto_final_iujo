using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace proyecto_final_iujo
{
    public partial class MainForm : Form
    {
        // Declaración de variables vinculadas al Designer
        private Label lblTitulo;
        private Label btnJugar;
        private Label btnCambiarIdioma;
        private Label btnCargarMazo;
        private Label btnSalir;

        // Textos base para restaurar tras la animación
        private string txtJugar = "JUGAR";
        private string txtIdioma = "CAMBIAR IDIOMA";
        private string txtMazo = "CARGAR MAZO";
        private string txtSalir = "SALIR";

        public MainForm()
        {
            InitializeComponent();
            ConfigurarEventos();

            // Suscripción al evento global de idioma
            Idioma.Cambiado += (s, e) => ActualizarTextosIdioma();
            ActualizarTextosIdioma();
        }

        private void ConfigurarEventos()
        {
            // Cargar imagen de fondo si existe en bin/Debug
            try
            {
                string ruta = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "fondo.jpg");
                if (File.Exists(ruta))
                {
                    this.BackgroundImage = Image.FromFile(ruta);
                    this.BackgroundImageLayout = ImageLayout.Stretch;
                }
            }
            catch { }

            // Conectar el efecto interactivo (subir 5px y mostrar ▶)
            VincularHoverAnimado(btnJugar, () => txtJugar);
            VincularHoverAnimado(btnCambiarIdioma, () => txtIdioma);
            VincularHoverAnimado(btnCargarMazo, () => txtMazo);
            VincularHoverAnimado(btnSalir, () => txtSalir);

            // 1. Clic en JUGAR: Abre el formulario de selección de escenario
            btnJugar.Click += (s, e) =>
            {
                FormSeleccionEscenario seleccion = new FormSeleccionEscenario(1); // Nivel inicial 1
                seleccion.Show();
                this.Hide();
            };

            // 2. Clic en CAMBIAR IDIOMA: Alterna entre ES e EN
            btnCambiarIdioma.Click += (s, e) =>
            {
                if (Idioma.Actual == Lenguaje.ES)
                    Idioma.Actual = Lenguaje.EN;
                else
                    Idioma.Actual = Lenguaje.ES;
            };

            // 3. Clic en CARGAR MAZO
            btnCargarMazo.Click += (s, e) =>
            {
                string mensaje = Idioma.Actual == Lenguaje.ES 
                    ? "Abriendo gestión de mazos..." 
                    : "Opening deck management...";
                MessageBox.Show(mensaje, "Arena Elemental");
            };

            // 4. Clic en SALIR
            btnSalir.Click += (s, e) =>
            {
                Application.Exit();
            };
        }

        private void ActualizarTextosIdioma()
        {
            if (Idioma.Actual == Lenguaje.ES)
            {
                this.Text = "Arena Elemental - Menu Principal";
                lblTitulo.Text = "ARENA ELEMENTAL";
                txtJugar = "JUGAR";
                txtIdioma = "CAMBIAR IDIOMA (ES)";
                txtMazo = "CARGAR MAZO";
                txtSalir = "SALIR";
            }
            else
            {
                this.Text = "Elemental Arena - Main Menu";
                lblTitulo.Text = "ELEMENTAL ARENA";
                txtJugar = "PLAY";
                txtIdioma = "CHANGE LANGUAGE (EN)";
                txtMazo = "LOAD DECK";
                txtSalir = "EXIT";
            }

            if (btnJugar != null) btnJugar.Text = txtJugar;
            if (btnCambiarIdioma != null) btnCambiarIdioma.Text = txtIdioma;
            if (btnCargarMazo != null) btnCargarMazo.Text = txtMazo;
            if (btnSalir != null) btnSalir.Text = txtSalir;
        }

        private void VincularHoverAnimado(Label lbl, Func<string> obtenerTexto)
        {
            if (lbl == null) return;

            lbl.Cursor = Cursors.Hand;

            lbl.MouseEnter += (s, e) =>
            {
                lbl.Location = new Point(lbl.Location.X, lbl.Location.Y - 5);
                lbl.Text = "▶  " + obtenerTexto();
                lbl.ForeColor = Color.FromArgb(255, 230, 90);
                lbl.BackColor = Color.FromArgb(235, 45, 60, 45);
            };

            lbl.MouseLeave += (s, e) =>
            {
                lbl.Location = new Point(lbl.Location.X, lbl.Location.Y + 5);
                lbl.Text = obtenerTexto();
                lbl.ForeColor = Color.White;
                lbl.BackColor = Color.FromArgb(190, 20, 25, 20);
            };
        }
    }
}