using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Windows.Forms;

namespace proyecto_final_iujo.Visuales
{
    // ventana para mostrar la historia del juego con efecto de maquina de escribir
    public partial class FormLoreDinamico : Form
    {
        // clase para estructurar el contenido de cada pagina de la historia
        private class PaginaLore
        {
            public string Subtitulo { get; set; }
            public string TextoPrincipal { get; set; }
            public Color TinteColor { get; set; }
            public Color ColorTitulo { get; set; }
        }

        // lista de capitulos y control de la pagina actual
        private List<PaginaLore> paginas = new List<PaginaLore>();
        private int indiceActual = 0;

        // variables para el efecto de escribir letra por letra
        private Timer timerTexto;
        private string textoObjetivo = "";
        private int caracterActual = 0;

        // variables para la transicion suave de color sobre el fondo
        private Timer timerFundido;
        private float factorFundido = 0f;
        private Color colorAnterior = Color.Black;
        private Color colorDestino = Color.Black;
        private Image imagenBaseOriginal = null;

        // constructor que inicializa la interfaz, capas y timers
        public FormLoreDinamico()
        {
            InitializeComponent();
            ConfigurarCapasIniciales();
            CargarCapitulos();
            InicializarTimers();

            // actualiza los textos si cambia el idioma
            Idioma.Cambiado += (s, e) => ActualizarTextosFormulario();
            ActualizarTextosFormulario();

            MostrarPaginaActual();
        }

        // cambia los textos de los controles segun el idioma activo
        private void ActualizarTextosFormulario()
        {
            lblTitulo.Text = Idioma.T("lore.titulo");
            btnVolver.Text = Idioma.T("sel.volver");
        }

        // configura posiciones, sombra del titulo y carga la imagen base
        private void ConfigurarCapasIniciales()
        {
            lblTitulo.Parent = picFondo;
            lblTitulo.Left = (this.ClientSize.Width - lblTitulo.Width) / 2;
            pnlFondoTexto.Left = (this.ClientSize.Width - pnlFondoTexto.Width) / 2;

            // carga el fondo una sola vez desde la carpeta del proyecto
            string rutaFondoBase = Path.Combine(Application.StartupPath, "fondo.jpg");
            if (!File.Exists(rutaFondoBase))
            {
                rutaFondoBase = Path.Combine(Directory.GetCurrentDirectory(), "fondo.jpg");
            }

            if (File.Exists(rutaFondoBase))
            {
                imagenBaseOriginal = Image.FromFile(rutaFondoBase);
            }

            // dibuja la sombra y el texto del titulo principal
            lblTitulo.Paint += (s, e) => {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                StringFormat sf = new StringFormat();
                sf.Alignment = StringAlignment.Center;
                sf.LineAlignment = StringAlignment.Center;

                // sombra oscura atras
                using (SolidBrush sombra = new SolidBrush(Color.FromArgb(240, 10, 10, 10)))
                {
                    Rectangle rSombra = new Rectangle(3, 3, lblTitulo.Width, lblTitulo.Height);
                    e.Graphics.DrawString(lblTitulo.Text, lblTitulo.Font, sombra, rSombra, sf);
                }

                // texto frontal
                using (SolidBrush frente = new SolidBrush(lblTitulo.ForeColor))
                {
                    Rectangle rFrente = new Rectangle(0, 0, lblTitulo.Width, lblTitulo.Height);
                    e.Graphics.DrawString(lblTitulo.Text, lblTitulo.Font, frente, rFrente, sf);
                }
            };

            // si haces clic en el texto lo muestra completo de golpe
            rtbLoreJuego.Click += (s, e) => CompletarTextoInmediato();
        }

        // configura los timers para el texto progresivo y el fundido de color
        private void InicializarTimers()
        {
            // agrega un caracter cada 20 milisegundos
            timerTexto = new Timer();
            timerTexto.Interval = 20;
            timerTexto.Tick += (s, e) => {
                if (caracterActual < textoObjetivo.Length)
                {
                    rtbLoreJuego.AppendText(textoObjetivo[caracterActual].ToString());
                    caracterActual++;
                    rtbLoreJuego.ScrollToCaret();
                }
                else
                {
                    timerTexto.Stop();
                }
            };

            // interpola el color del fondo en 20 pasos
            timerFundido = new Timer();
            timerFundido.Interval = 25;
            timerFundido.Tick += (s, e) => {
                factorFundido += 0.05f;
                if (factorFundido >= 1f)
                {
                    factorFundido = 1f;
                    timerFundido.Stop();
                }
                AplicarFundidoVisual();
            };
        }

        // llena la lista de capitulos en espanol o ingles segun el idioma seleccionado
        private void CargarCapitulos()
        {
            switch (Idioma.Actual) {
                case Lenguaje.ES:
                    // capitulo 1: la aldea y las reglas
                    paginas.Add(new PaginaLore
                    {
                        Subtitulo = "PARTE I: EL PACTO Y LA FRONTERA CERRADA (LA ALDEA)",
                        TextoPrincipal = "En lo profundo del valle yace una comunidad nacida de la renuncia al dolor. Para resguardarse de la violencia del mundo moderno, los ancianos fundaron La Aldea y sellaron un pacto tácito con la espesura: nadie traspasa la línea de los árboles centinelas, pues allí moran 'Aquellos de Quienes No Hablamos'.\n\nEl color rojo está terminantemente prohibido por considerarse la chispa de la transgresión y el caos; solo las capas amarillas garantizan una tregua precaria frente a lo desconocido.",
                        TinteColor = Color.FromArgb(40, 70, 30),
                        ColorTitulo = Color.FromArgb(255, 215, 90)
                    });

                    // capitulo 2: dioses griegos y elementos
                    paginas.Add(new PaginaLore
                    {
                        Subtitulo = "PARTE II: EL PANTEÓN PRIMORDIAL Y LAS FUERZAS GRIEGAS",
                        TextoPrincipal = "Bajo los mitos del bosque yacen las fuerzas elementales arquetípicas del panteón griego:\n\n• HEFESTO Y PROMETEO: Encarnan la llama de la curiosidad técnica y la rebelión que busca la medicina de la verdad.\n• POSEIDÓN: Rige los pantanos infranqueables y las aguas subterráneas de la memoria reprimida.\n• HADES Y GEA: Gobiernan el subsuelo y las fallas tectónicas que sacuden la falsa utopía.\n• HERMES: Guía veloz y silenciosa de los senderos prohibidos que cruzan la oscuridad.",
                        TinteColor = Color.FromArgb(25, 45, 80),
                        ColorTitulo = Color.FromArgb(120, 220, 255)
                    });

                    // capitulo 3: mecanica sismica y naturaleza
                    paginas.Add(new PaginaLore
                    {
                        Subtitulo = "PARTE III: LA ARENA ELEMENTAL Y LA AMENAZA SÍSMICA",
                        TextoPrincipal = "El entorno exterior no es pacífico ni estático: transmuta progresivamente por la furia elemental de volcanes, ciénagas y ventiscas.\n\nCada 3 o 4 turnos, la tierra se fractura en un terremoto inevitable que destruye las defensas de quienes no portan protección terrenal. Los expedicionarios no combaten contra bestias inventadas, sino contra la crudeza de la naturaleza, viéndose forzados a canalizar cartas de poder elemental para resistir cada transición.",
                        TinteColor = Color.FromArgb(85, 30, 20),
                        ColorTitulo = Color.FromArgb(255, 140, 100)
                    });

                    // capitulo 4: dilema antropologico
                    paginas.Add(new PaginaLore
                    {
                        Subtitulo = "PARTE IV: CONDICIÓN HUMANA Y ANTROPOLOGÍA FILOSÓFICA",
                        TextoPrincipal = "¿Es legítimo asegurar la inocencia y la calma mediante el engaño sistemático y la privación de la libertad?\n\nLa frontera de Arena Elemental representa el dilema antropológico: permanecer a salvo dentro de la caverna del miedo comunal o cruzar la frontera hacia lo inhóspito para salvar la vida de los demás. Como la heroína ciega que se interna en el bosque guiada solo por el afecto, sobrevivir exige vencer el terror mitológico con valor racional.",
                        TinteColor = Color.FromArgb(55, 25, 65),
                        ColorTitulo = Color.FromArgb(210, 170, 255)
                    });
                    break;

                case Lenguaje.EN:
                    // chapter 1: the village rules
                    paginas.Add(new PaginaLore
                    {
                        Subtitulo = "PART I: THE PACT AND THE CLOSED BORDER (THE VILLAGE)",
                        TextoPrincipal = "Deep in the valley lies a community born from the renunciation of pain. To protect themselves from the violence of the modern world, the elders founded The Village and sealed a tacit pact with the thicket: no one crosses the line of the sentinel trees, for 'Those We Don't Speak Of' dwell there.\n\nThe color red is strictly forbidden as it is considered the spark of transgression and chaos; only yellow cloaks guarantee a precarious truce against the unknown.",
                        TinteColor = Color.FromArgb(40, 70, 30),
                        ColorTitulo = Color.FromArgb(255, 215, 90)
                    });

                    // chapter 2: greek archetypes
                    paginas.Add(new PaginaLore
                    {
                        Subtitulo = "PART II: THE PRIMORDIAL PANTHEON AND THE GREEK FORCES",
                        TextoPrincipal = "Beneath the forest myths lie the archetypal elemental forces of the Greek pantheon:\n\n• HEPHAESTUS AND PROMETHEUS: They embody the flame of technical curiosity and the rebellion that seeks the medicine of truth.\n• POSEIDON: He rules the impassable swamps and the underground waters of repressed memory.\n• HADES AND GAIA: They govern the underground and the tectonic faults that shake the false utopia.\n• HERMES: Swift and silent guide of the forbidden paths that cross the darkness.",
                        TinteColor = Color.FromArgb(25, 45, 80),
                        ColorTitulo = Color.FromArgb(120, 220, 255)
                    });

                    // chapter 3: seismic arena
                    paginas.Add(new PaginaLore
                    {
                        Subtitulo = "PART III: THE ELEMENTAL ARENA AND THE SEISMIC THREAT",
                        TextoPrincipal = "The external environment is neither peaceful nor static: it progressively transmutes through the elemental fury of volcanoes, bogs, and blizzards.\n\nEvery 3 or 4 turns, the earth fractures in an inevitable earthquake that destroys the defenses of those who do not carry earthly protection. The expeditionaries do not fight against invented beasts, but against the harshness of nature, being forced to channel elemental power cards to resist each transition.",
                        TinteColor = Color.FromArgb(85, 30, 20),
                        ColorTitulo = Color.FromArgb(255, 140, 100)
                    });

                    // chapter 4: philosophical dilema
                    paginas.Add(new PaginaLore
                    {
                        Subtitulo = "PART IV: THE HUMAN CONDITION AND PHILOSOPHICAL ANTHROPOLOGY",
                        TextoPrincipal = "Is it legitimate to ensure innocence and calm through systematic deception and the deprivation of freedom?\n\nThe border of Elemental Arena represents the anthropological dilemma: staying safe inside the cave of communal fear or crossing the border into the inhospitable to save the lives of others. Like the blind heroine who ventures into the forest guided only by affection, survival requires overcoming mythological terror with rational courage.",
                        TinteColor = Color.FromArgb(55, 25, 65),
                        ColorTitulo = Color.FromArgb(210, 170, 255)
                    });
                    break;
            }
        }

        // actualiza el titulo, activa el timer de escritura y comienza la transicion de fondo
        private void MostrarPaginaActual()
        {
            if (indiceActual < 0 || indiceActual >= paginas.Count) return;

            PaginaLore pagina = paginas[indiceActual];

            lblCapitulo.Text = pagina.Subtitulo;
            lblCapitulo.ForeColor = pagina.ColorTitulo;

            // reinicia y arranca la maquina de escribir
            timerTexto.Stop();
            rtbLoreJuego.Clear();
            textoObjetivo = pagina.TextoPrincipal;
            caracterActual = 0;
            timerTexto.Start();

            // inicia el fundido suave entre los colores de tinte
            colorAnterior = (indiceActual == 0) ? pagina.TinteColor : paginas[indiceActual - 1].TinteColor;
            colorDestino = pagina.TinteColor;
            factorFundido = 0f;
            timerFundido.Start();

            // activa o desactiva el boton anterior segun el indice
            btnAnterior.Enabled = (indiceActual > 0);

            // actualiza textos de navegacion
            btnAnterior.Text = Idioma.T("lore.anterior");
            switch (Idioma.Actual) {
                case Lenguaje.ES:
                    btnSiguiente.Text = (indiceActual == paginas.Count - 1) ? "FINALIZAR ✔" : "SIGUIENTE ▶";
                    break;
                case Lenguaje.EN:
                    btnSiguiente.Text = (indiceActual == paginas.Count - 1) ? "FINISH ✔" : "NEXT ▶";
                    break;
            }
        }

        // salta la animacion y pone todo el texto de una vez
        private void CompletarTextoInmediato()
        {
            if (timerTexto.Enabled)
            {
                timerTexto.Stop();
                rtbLoreJuego.Text = textoObjetivo;
                rtbLoreJuego.ScrollToCaret();
            }
        }

        // mezcla la imagen de fondo con el color de tinte usando interpolacion rgb
        private void AplicarFundidoVisual()
        {
            if (imagenBaseOriginal == null) return;

            // calcula el color intermedio segun el factor de fundido
            int r = (int)(colorAnterior.R + (colorDestino.R - colorAnterior.R) * factorFundido);
            int g = (int)(colorAnterior.G + (colorDestino.G - colorAnterior.G) * factorFundido);
            int b = (int)(colorAnterior.B + (colorDestino.B - colorAnterior.B) * factorFundido);

            // crea el bitmap con la capa de color semitransparente
            Bitmap bmp = new Bitmap(imagenBaseOriginal.Width, imagenBaseOriginal.Height);
            using (Graphics gr = Graphics.FromImage(bmp))
            {
                gr.DrawImage(imagenBaseOriginal, 0, 0, imagenBaseOriginal.Width, imagenBaseOriginal.Height);
                using (SolidBrush brocha = new SolidBrush(Color.FromArgb(100, r, g, b)))
                {
                    gr.FillRectangle(brocha, 0, 0, imagenBaseOriginal.Width, imagenBaseOriginal.Height);
                }
            }

            // reemplaza la imagen y libera la anterior para evitar fugas de memoria
            Image imgVieja = picFondo.Image;
            picFondo.Image = bmp;
            if (imgVieja != null && imgVieja != imagenBaseOriginal)
            {
                imgVieja.Dispose();
            }
        }

        // retrocede al capitulo anterior
        void BtnAnteriorClick(object sender, EventArgs e)
        {
            // si esta escribiendo completa el texto primero
            if (timerTexto.Enabled)
            {
                CompletarTextoInmediato();
                return;
            }

            if (indiceActual > 0)
            {
                indiceActual--;
                MostrarPaginaActual();
            }
        }

        // avanza al siguiente capitulo o abre la seleccion de equipo si termino
        void BtnSiguienteClick(object sender, EventArgs e)
        {
            // si esta escribiendo completa el texto primero
            if (timerTexto.Enabled)
            {
                CompletarTextoInmediato();
                return;
            }

            if (indiceActual < paginas.Count - 1)
            {
                indiceActual++;
                MostrarPaginaActual();
            }
            else
            {
                // detiene timers y pasa a la pantalla de seleccion de equipo
                timerTexto.Stop();
                timerFundido.Stop();
                FormCargarEquipo formEquipo = new FormCargarEquipo();
                formEquipo.Show();
                this.Close();
            }
        }

        // regresa al menu principal
        void BtnVolverClick(object sender, EventArgs e)
        {
            timerTexto.Stop();
            timerFundido.Stop();
            MainForm menu = new MainForm();
            menu.Show();
            this.Close();
        }

        // libera los timers al cerrar la ventana
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (timerTexto != null) timerTexto.Dispose();
            if (timerFundido != null) timerFundido.Dispose();
            base.OnFormClosing(e);
        }
    }
}