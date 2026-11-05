using System;
using System.Drawing;
using System.Windows.Forms;

namespace proyecto_final_iujo
{
    public partial class FormSeleccionEscenario : Form
    {
        private Label lblTitulo;
        private Label lblSubtitulo;
        private Panel pnlContenedor;
        private Button btnIniciarBatalla;
        private Button btnVolver;
        
        // Elemento y dificultad seleccionados
        private Elemento elementoSeleccionado = Elemento.Tierra;
        private int nivelActual = 1;
        private Panel tarjetaSeleccionada = null;

        // Datos balanceados de los 4 escenarios elementales
        private readonly EscenarioData[] escenarios = new EscenarioData[]
        {
            new EscenarioData { Elemento = Elemento.Tierra, Nombre = "Cañón Rocoso (Tierra)", Dificultad = "Fácil (Nivel 1)", ProbTerremoto = 45, Estrellas = "★☆☆ (1 Estrella)", Buff = "Tierra +20% DEF / Mitiga Sismos", ColorTema = Color.FromArgb(176, 132, 62) },
            new EscenarioData { Elemento = Elemento.Agua, Nombre = "Abismo Oceánico (Agua)", Dificultad = "Normal (Nivel 2)", ProbTerremoto = 25, Estrellas = "★★☆ (2 Estrellas)", Buff = "Agua +20% HP / Vulnerable a Tierra", ColorTema = Color.FromArgb(47, 143, 230) },
            new EscenarioData { Elemento = Elemento.Fuego, Nombre = "Volcán Activo (Fuego)", Dificultad = "Difícil (Nivel 3)", ProbTerremoto = 35, Estrellas = "★★★ (3 Estrellas)", Buff = "Fuego +25% ATK / Vulnerable a Agua", ColorTema = Color.FromArgb(232, 85, 43) },
            new EscenarioData { Elemento = Elemento.Aire, Nombre = "Cumbre Celeste (Aire)", Dificultad = "Experto (Nivel 4)", ProbTerremoto = 15, Estrellas = "★★★★ (4 Estrellas)", Buff = "Aire +30% Velocidad y Daño", ColorTema = Color.FromArgb(127, 211, 199) }
        };

        public FormSeleccionEscenario(int nivelProgreso = 1)
        {
            this.nivelActual = nivelProgreso;
            ConfigurarVentana();
            ConstruirInterfaz();
        }

        private void ConfigurarVentana()
        {
            this.Text = "Arena Elemental - Selección de Escenario";
            this.ClientSize = new Size(880, 520);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.BackColor = Color.FromArgb(20, 24, 28);
        }

        private void ConstruirInterfaz()
        {
            // Título principal
            lblTitulo = new Label();
            lblTitulo.Text = "SELECCIONA TU ESCENARIO";
            lblTitulo.Font = new Font("Impact", 24F, FontStyle.Bold);
            lblTitulo.ForeColor = Color.FromArgb(245, 175, 45);
            lblTitulo.Size = new Size(880, 40);
            lblTitulo.Location = new Point(0, 15);
            lblTitulo.TextAlign = ContentAlignment.MiddleCenter;
            this.Controls.Add(lblTitulo);

            lblSubtitulo = new Label();
            lblSubtitulo.Text = string.Format("Progreso actual: Nivel {0} | Supera cada entorno para desbloquear el siguiente", nivelActual);
            lblSubtitulo.Font = new Font("Segoe UI", 9.5F, FontStyle.Italic);
            lblSubtitulo.ForeColor = Color.Gainsboro;
            lblSubtitulo.Size = new Size(880, 22);
            lblSubtitulo.Location = new Point(0, 55);
            lblSubtitulo.TextAlign = ContentAlignment.MiddleCenter;
            this.Controls.Add(lblSubtitulo);

            // Contenedor de las 4 tarjetas
            pnlContenedor = new Panel();
            pnlContenedor.Size = new Size(840, 330);
            pnlContenedor.Location = new Point(20, 85);
            pnlContenedor.BackColor = Color.Transparent;
            this.Controls.Add(pnlContenedor);

            for (int i = 0; i < escenarios.Length; i++)
            {
                CrearTarjetaEscenario(escenarios[i], i);
            }

            // Botón Volver al Menú
            btnVolver = new Button();
            btnVolver.Text = "◀ VOLVER AL MENÚ";
            btnVolver.Size = new Size(180, 42);
            btnVolver.Location = new Point(40, 440);
            btnVolver.BackColor = Color.FromArgb(45, 50, 60);
            btnVolver.ForeColor = Color.White;
            btnVolver.FlatStyle = FlatStyle.Flat;
            btnVolver.FlatAppearance.BorderSize = 0;
            btnVolver.Cursor = Cursors.Hand;
            btnVolver.Font = new Font("Impact", 12F);
            btnVolver.Click += (s, e) =>
            {
                MainForm menu = new MainForm();
                menu.Show();
                this.Close();
            };
            this.Controls.Add(btnVolver);

            // Botón Iniciar Combate
            btnIniciarBatalla = new Button();
            btnIniciarBatalla.Text = "INICIAR COMBATE ▶";
            btnIniciarBatalla.Size = new Size(240, 42);
            btnIniciarBatalla.Location = new Point(600, 440);
            btnIniciarBatalla.BackColor = Color.FromArgb(200, 140, 25);
            btnIniciarBatalla.ForeColor = Color.Black;
            btnIniciarBatalla.FlatStyle = FlatStyle.Flat;
            btnIniciarBatalla.FlatAppearance.BorderSize = 0;
            btnIniciarBatalla.Cursor = Cursors.Hand;
            btnIniciarBatalla.Font = new Font("Impact", 13F);
            btnIniciarBatalla.Click += (s, e) => IniciarPartida();
            this.Controls.Add(btnIniciarBatalla);
        }

        private void CrearTarjetaEscenario(EscenarioData data, int indice)
        {
            int ancho = 195;
            int alto = 310;
            int posX = indice * (ancho + 15);
            bool bloqueado = (indice + 1) > nivelActual;

            Panel tarjeta = new Panel();
            tarjeta.Size = new Size(ancho, alto);
            tarjeta.Location = new Point(posX, 10);
            tarjeta.BorderStyle = BorderStyle.FixedSingle;
            tarjeta.Cursor = bloqueado ? Cursors.No : Cursors.Hand;
            tarjeta.BackColor = bloqueado ? Color.FromArgb(30, 30, 35) : Color.FromArgb(32, 36, 46);

            // Franja superior de elemento
            Label lblEncabezado = new Label();
            lblEncabezado.Text = data.Elemento.ToString().ToUpper();
            lblEncabezado.Dock = DockStyle.Top;
            lblEncabezado.Height = 32;
            lblEncabezado.TextAlign = ContentAlignment.MiddleCenter;
            lblEncabezado.Font = new Font("Impact", 13F);
            lblEncabezado.ForeColor = Color.White;
            lblEncabezado.BackColor = bloqueado ? Color.FromArgb(50, 50, 55) : data.ColorTema;
            tarjeta.Controls.Add(lblEncabezado);

            // Nombre
            Label lblNom = new Label();
            lblNom.Text = data.Nombre;
            lblNom.Location = new Point(8, 40);
            lblNom.Size = new Size(ancho - 16, 38);
            lblNom.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblNom.ForeColor = Color.White;
            lblNom.TextAlign = ContentAlignment.TopCenter;
            tarjeta.Controls.Add(lblNom);

            // Dificultad
            Label lblDif = new Label();
            lblDif.Text = "Dificultad:\n" + data.Dificultad;
            lblDif.Location = new Point(8, 85);
            lblDif.Size = new Size(ancho - 16, 34);
            lblDif.Font = new Font("Segoe UI", 8.5F);
            lblDif.ForeColor = bloqueado ? Color.Gray : Color.Gold;
            lblDif.TextAlign = ContentAlignment.MiddleCenter;
            tarjeta.Controls.Add(lblDif);

            // Riesgo de Terremoto
            Label lblSismo = new Label();
            lblSismo.Text = string.Format("Riesgo Terremoto:\n{0}% por turno", data.ProbTerremoto);
            lblSismo.Location = new Point(8, 125);
            lblSismo.Size = new Size(ancho - 16, 36);
            lblSismo.Font = new Font("Segoe UI", 8.5F);
            lblSismo.ForeColor = Color.Salmon;
            lblSismo.TextAlign = ContentAlignment.MiddleCenter;
            tarjeta.Controls.Add(lblSismo);

            // Buffs / Debuffs
            Label lblEfecto = new Label();
            lblEfecto.Text = "Efecto:\n" + data.Buff;
            lblEfecto.Location = new Point(8, 168);
            lblEfecto.Size = new Size(ancho - 16, 48);
            lblEfecto.Font = new Font("Segoe UI", 7.5F);
            lblEfecto.ForeColor = Color.LightGray;
            lblEfecto.TextAlign = ContentAlignment.MiddleCenter;
            tarjeta.Controls.Add(lblEfecto);

            // Recompensa
            Label lblPremio = new Label();
            lblPremio.Text = "Recompensa:\n" + data.Estrellas;
            lblPremio.Location = new Point(8, 222);
            lblPremio.Size = new Size(ancho - 16, 36);
            lblPremio.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            lblPremio.ForeColor = Color.Khaki;
            lblPremio.TextAlign = ContentAlignment.MiddleCenter;
            tarjeta.Controls.Add(lblPremio);

            // Indicador de bloqueo
            if (bloqueado)
            {
                Label lblCandado = new Label();
                lblCandado.Text = "🔒 BLOQUEADO";
                lblCandado.Dock = DockStyle.Bottom;
                lblCandado.Height = 30;
                lblCandado.TextAlign = ContentAlignment.MiddleCenter;
                lblCandado.ForeColor = Color.Red;
                lblCandado.Font = new Font("Impact", 10F);
                lblCandado.BackColor = Color.FromArgb(20, 20, 25);
                tarjeta.Controls.Add(lblCandado);
            }
            else
            {
                tarjeta.Click += (s, e) => SeleccionarEscenario(tarjeta, data.Elemento);
                foreach (Control c in tarjeta.Controls)
                {
                    c.Click += (s, e) => SeleccionarEscenario(tarjeta, data.Elemento);
                }

                if (tarjetaSeleccionada == null)
                {
                    SeleccionarEscenario(tarjeta, data.Elemento);
                }
            }

            pnlContenedor.Controls.Add(tarjeta);
        }

        private void SeleccionarEscenario(Panel tarjeta, Elemento elemento)
        {
            if (tarjetaSeleccionada != null)
            {
                tarjetaSeleccionada.BackColor = Color.FromArgb(32, 36, 46);
            }

            tarjetaSeleccionada = tarjeta;
            tarjetaSeleccionada.BackColor = Color.FromArgb(48, 55, 75);
            elementoSeleccionado = elemento;
        }

        private void IniciarPartida()
        {
            EscenarioData actual = Array.Find(escenarios, x => x.Elemento == elementoSeleccionado);

            MessageBox.Show(string.Format("¡Combate en curso!\nEscenario: {0}\nProbabilidad Sísmica: {1}%\nPremio: {2}",
                actual.Nombre, actual.ProbTerremoto, actual.Estrellas), "Arena Elemental");
        }

        private class EscenarioData
        {
            public Elemento Elemento { get; set; }
            public string Nombre { get; set; }
            public string Dificultad { get; set; }
            public int ProbTerremoto { get; set; }
            public string Estrellas { get; set; }
            public string Buff { get; set; }
            public Color ColorTema { get; set; }
        }
    }
}