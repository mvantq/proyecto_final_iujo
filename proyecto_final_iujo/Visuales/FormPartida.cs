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
    // ventana principal del combate por turnos en la arena
    public partial class FormPartida : Form
    {
        // estructura para los datos de cada combatiente en la cola
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

        // colas de personajes para el relevo del jugador y del rival
        private Queue<Luchador> colaJugador = new Queue<Luchador>();
        private Queue<Luchador> colaRival = new Queue<Luchador>();

        // combatientes activos en el turno actual
        private Luchador heroeJugadorActual = null;
        private Luchador heroeRivalActual = null;

        // imagenes cargadas de los heroes activos
        private Image imgJugador = null;
        private Image imgRival = null;

        // variables de control de turnos y terremoto
        private int turno = 1;
        private int turnoTerremoto;
        private bool terremotoOcurrido = false;
        private Random random = new Random();

        // cartas de poder activas por accion
        private bool poderFuegoActivo = false;
        private bool poderAguaActivo = false;
        private bool poderTierraActivo = false;
        private bool poderAireActivo = false;

        // constructor que inicializa componentes, equipos, fondo y eventos
        public FormPartida(List<string> seleccionados)
        {
            InitializeComponent();

            // activa doble buffer para evitar parpadeos y acelerar repintado
            this.DoubleBuffered = true;
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer, true);

            // define de forma aleatoria en cual turno caera el terremoto
            this.turnoTerremoto = random.Next(5, 8);

            InicializarEquipos(seleccionados);
            CargarFondoEscenarioDifuminado();

            picEscenario.Paint += DibujarPersonajesEnArena;

            Idioma.Cambiado += OnIdiomaCambiado;
            ActualizarTextosIdioma();

            lblLogTurno.Text = Idioma.T("partida.inicio");
            ActualizarInterfaz();
        }

        // evento para refrescar los textos cuando cambia el idioma
        private void OnIdiomaCambiado(object sender, EventArgs e)
        {
            ActualizarTextosIdioma();
            ActualizarInterfaz();
        }

        // actualiza el texto de los botones de accion segun el idioma activo
        private void ActualizarTextosIdioma()
        {
            btnAtacar.Text = Idioma.T("btn.atacar");
            btnDefender.Text = Idioma.T("btn.defender");
            btnEspecial.Text = Idioma.T("btn.especial");
            btnRelevo.Text = Idioma.T("btn.relevo");
        }

        // traduce los nombres de los heroes cuando el juego este en ingles
        private string ObtenerNombreTraducido(string clave, string nombrePorDefecto)
        {
            if (Idioma.Actual == Lenguaje.ES) return nombrePorDefecto;

            switch (clave.ToLower())
            {
                // faccion fuego
                case "ignis": return "Ignis the Blacksmith";
                case "pirox": return "Pirox the Rebel";
                case "vulkan": return "Vulkan the Wrathful";

                // faccion agua
                case "acuaris": return "Acuaris the Abyssal";
                case "nereus": return "Nereus of the Marsh";
                case "glauco": return "Glauco the Torrential";

                // faccion tierra
                case "terran": return "Terran the Guardian";
                case "gaia": return "Gaia the Primordial";
                case "golem":
                case "gólem": return "Telluric Golem";

                // faccion viento
                case "zephyr": return "Zephyr the Herald";
                case "aura": return "Aura the Huntress";
                case "boreas":
                case "bóreas": return "Boreas of the North";

                default: return nombrePorDefecto;
            }
        }

        // traduce el nombre del elemento al ingles
        private string ObtenerElementoTraducido(string elemento)
        {
            if (Idioma.Actual == Lenguaje.ES) return elemento;

            switch (elemento.ToLower())
            {
                case "fuego": return "Fire";
                case "agua": return "Water";
                case "tierra": return "Earth";
                case "viento":
                case "aire": return "Wind";
                default: return elemento;
            }
        }

        // llena las colas de combate con los 5 personajes seleccionados y los del rival
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
                // equipo por defecto por si no vienen seleccionados
                colaJugador.Enqueue(new Luchador("Acuaris el Abisal", "Acuaris", "Agua", 100));
                colaJugador.Enqueue(new Luchador("Vulkan el Colérico", "Vulkan", "Fuego", 100));
                colaJugador.Enqueue(new Luchador("Terran el Guardián", "Terran", "Tierra", 100));
                colaJugador.Enqueue(new Luchador("Zephyr el Heraldo", "Zephyr", "Viento", 100));
                colaJugador.Enqueue(new Luchador("Ignis el Forjador", "Ignis", "Fuego", 100));
            }

            // equipo rival fijo para la batalla
            colaRival.Clear();
            colaRival.Enqueue(new Luchador("Nereus del Pantano", "Nereus", "Agua", 100));
            colaRival.Enqueue(new Luchador("Bóreas del Norte", "Bóreas", "Viento", 100));
            colaRival.Enqueue(new Luchador("Gólem Telúrico", "Gólem", "Tierra", 100));
            colaRival.Enqueue(new Luchador("Pirox el Rebelde", "Pirox", "Fuego", 100));
            colaRival.Enqueue(new Luchador("Glauco el Torrencial", "Glauco", "Agua", 100));

            PrepararHeroesActivos();
        }

        // separa el texto para extraer nombre, clave y elemento del personaje
        private Luchador CrearLuchadorDesdeCadena(string texto)
        {
            string nombre = texto;
            string elemento = "Fuego";
            string clave = texto.Split(' ')[0].Trim();

            if (texto.Contains("[Agua]") || texto.Contains("[Water]")) elemento = "Agua";
            else if (texto.Contains("[Tierra]") || texto.Contains("[Earth]")) elemento = "Tierra";
            else if (texto.Contains("[Viento]") || texto.Contains("[Wind]")) elemento = "Viento";

            return new Luchador(nombre, clave, elemento, 100);
        }

        // toma al primer personaje de cada cola y actualiza sus retratos y nombres
        private void PrepararHeroesActivos()
        {
            if (colaJugador.Count > 0) heroeJugadorActual = colaJugador.Peek();
            if (colaRival.Count > 0) heroeRivalActual = colaRival.Peek();

            CargarImagenesCombatientes();

            if (heroeJugadorActual != null)
            {
                string nom = ObtenerNombreTraducido(heroeJugadorActual.Clave, heroeJugadorActual.Nombre);
                lblNombreJugador.Text = Idioma.T("partida.nom.jugador", nom.ToUpper(), colaJugador.Count);
            }

            if (heroeRivalActual != null)
            {
                string nomRival = ObtenerNombreTraducido(heroeRivalActual.Clave, heroeRivalActual.Nombre);
                lblNombreRival.Text = Idioma.T("partida.nom.rival", nomRival.ToUpper(), colaRival.Count);
            }
        }

        // tabla de ventajas y desventajas elementales
        private double ObtenerMultiplicadorElemental(string elemAtacante, string elemDefensor)
        {
            // ventajas que aumentan 45 por ciento el golpe
            if (elemAtacante == "Fuego" && elemDefensor == "Viento") return 1.45;
            if (elemAtacante == "Viento" && elemDefensor == "Tierra") return 1.45;
            if (elemDefensor == "Tierra" && elemDefensor == "Agua") return 1.45;
            if (elemAtacante == "Agua" && elemDefensor == "Fuego") return 1.45;

            // desventajas que reducen el golpe al 70 por ciento
            if (elemAtacante == "Viento" && elemDefensor == "Fuego") return 0.70;
            if (elemAtacante == "Tierra" && elemDefensor == "Viento") return 0.70;
            if (elemAtacante == "Agua" && elemDefensor == "Tierra") return 0.70;
            if (elemAtacante == "Fuego" && elemDefensor == "Agua") return 0.70;

            return 1.0;
        }

        // procesa los calculos de dano, curacion, contraataque del rival y estados de turno
        private void ProcesarAccionTurno(string tipoAccion)
        {
            if (heroeJugadorActual == null || heroeRivalActual == null) return;

            heroeJugadorActual.TurnosEnArena++;
            heroeRivalActual.TurnosEnArena++;

            bool esTurnoTerremoto = (!terremotoOcurrido && turno == turnoTerremoto);

            // rota los escenarios cada turno
            string[] escenarios = { "Vórtice Huracanado", "Volcán Ardiente", "Ciénaga Helada", "Desfiladero Rocoso" };
            string escenarioActual = escenarios[turno % 4];

            ResultadoTurno res = AlgoritmoDano.ProcesarTurno(
                heroeJugadorActual.VidaActual, turno, escenarioActual,
                poderFuegoActivo, poderAguaActivo, poderTierraActivo, poderAireActivo,
                esTurnoTerremoto
            );

            if (esTurnoTerremoto) terremotoOcurrido = true;

            int danoBaseJugador = 0;
            int curacion = 0;
            string descripcionAccion = "";
            string elemAccion = "Fuego";

            // define estadisticas segun el boton presionado
            if (tipoAccion == "Ataque de Fuego")
            {
                elemAccion = "Fuego";
                danoBaseJugador = random.Next(18, 26);
                descripcionAccion = Idioma.T("partida.atk.fuego");
            }
            else if (tipoAccion == "Defensa de Tierra")
            {
                elemAccion = "Tierra";
                danoBaseJugador = random.Next(6, 12);
                descripcionAccion = Idioma.T("partida.def.tierra");
            }
            else if (tipoAccion == "Especial de Agua")
            {
                elemAccion = "Agua";
                danoBaseJugador = random.Next(10, 16);
                curacion = random.Next(14, 22);
                heroeJugadorActual.VidaActual = Math.Min(heroeJugadorActual.VidaMaxima, heroeJugadorActual.VidaActual + curacion);
                descripcionAccion = Idioma.T("partida.esp.agua", curacion);
            }
            else if (tipoAccion == "Relevo de Aire")
            {
                elemAccion = "Viento";
                danoBaseJugador = random.Next(8, 14);
                descripcionAccion = Idioma.T("partida.rel.aire");
            }

            // calcula el dano final aplicado al rival
            double multJugador = ObtenerMultiplicadorElemental(elemAccion, heroeRivalActual.Elemento);
            int danoInfligidoFinal = (int)(danoBaseJugador * multJugador);
            heroeRivalActual.VidaActual = Math.Max(0, heroeRivalActual.VidaActual - danoInfligidoFinal);

            // comprueba si el rival fue derrotado
            if (heroeRivalActual.VidaActual <= 0)
            {
                colaRival.Dequeue();
                if (colaRival.Count == 0)
                {
                    ActualizarInterfaz();
                    MessageBox.Show(Idioma.T("partida.vic.total"), Idioma.T("partida.vic.magna"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    VolverAlMenu();
                    return;
                }

                MessageBox.Show(Idioma.T("partida.ko.rival", heroeRivalActual.Nombre), Idioma.T("partida.baja.enemiga"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                PrepararHeroesActivos();
                ActualizarInterfaz();
                return;
            }

            // contraataque del enemigo considerando fatiga y defensas
            double factorFatiga = 1.0 + (heroeJugadorActual.TurnosEnArena * 0.10);
            double multRival = ObtenerMultiplicadorElemental(heroeRivalActual.Elemento, heroeJugadorActual.Elemento);

            int danoBaseRival = random.Next(14, 22);
            int danoTotalRival = (int)(danoBaseRival * multRival * factorFatiga) + res.DanoRecibido;

            // mitigacion segun la accion elegida
            if (tipoAccion == "Defensa de Tierra")
            {
                danoTotalRival = (int)(danoTotalRival * 0.25);
            }
            else if (tipoAccion == "Relevo de Aire")
            {
                danoTotalRival = (int)(danoTotalRival * 0.15);
            }

            // aplica el dano al jugador y avanza de turno
            heroeJugadorActual.VidaActual = Math.Max(0, heroeJugadorActual.VidaActual - danoTotalRival);
            turno++;
            ActualizarInterfaz();

            // muestra la grafica si ocurrio el terremoto
            if (esTurnoTerremoto)
            {
                lblLogTurno.ForeColor = Color.OrangeRed;
                lblLogTurno.Text = Idioma.T("partida.sismo", descripcionAccion, danoInfligidoFinal, danoTotalRival);
                FormGraficaTerremoto grafica = new FormGraficaTerremoto();
                grafica.ShowDialog();
            }
            else
            {
                lblLogTurno.ForeColor = Color.FromArgb(255, 220, 90);
                lblLogTurno.Text = Idioma.T("partida.log.turno", turno - 1, descripcionAccion, danoInfligidoFinal, multJugador, danoTotalRival, factorFatiga);
            }

            // comprueba si el heroe actual del jugador cayo
            if (heroeJugadorActual.VidaActual <= 0)
            {
                colaJugador.Dequeue();
                if (colaJugador.Count == 0)
                {
                    emergentBox(
                        "DERROTA TOTAL: Todos tus 5 campeones han caído. La espesura del bosque ha reclamado a tu escuadrón.",
                        "DEFEATED: The thicket of the forest has claimed your squad.",
                        "Derrota",
                        "Defeated"
                    );
                    VolverAlMenu();
                    return;
                }

                MessageBox.Show(Idioma.T("partida.ko.heroe", heroeJugadorActual.Nombre), Idioma.T("partida.caido.combate"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                PrepararHeroesActivos();
                ActualizarInterfaz();
            }

            // resetea los estados de poder
            poderFuegoActivo = poderAguaActivo = poderTierraActivo = poderAireActivo = false;
        }

        // releva al combatiente al final de la cola y le recupera algo de vida
        private void EjecutarRelevoEstrategico()
        {
            if (colaJugador.Count <= 1) {
                emergentBox(
                    "No te quedan personajes en la banca para relevar.",
                    "No more characters on the bench.",
                    "No Relays",
                    "Sin Relevos"
                );    
                return;
            }

            Luchador relevado = colaJugador.Dequeue();
            relevado.TurnosEnArena = 0;
            relevado.VidaActual = Math.Min(relevado.VidaMaxima, relevado.VidaActual + 15);
            colaJugador.Enqueue(relevado);

            // releva al rival si tiene poca vida o lleva muchos turnos peleando
            if (colaRival.Count > 1 && (heroeRivalActual.VidaActual < 35 || heroeRivalActual.TurnosEnArena >= 3))
            {
                Luchador relevadoRival = colaRival.Dequeue();
                relevadoRival.TurnosEnArena = 0;
                relevadoRival.VidaActual = Math.Min(relevadoRival.VidaMaxima, relevadoRival.VidaActual + 10);
                colaRival.Enqueue(relevadoRival);
            }

            PrepararHeroesActivos();
            lblLogTurno.Text = Idioma.T("partida.relevo.tactico", heroeJugadorActual.Nombre, heroeJugadorActual.Elemento);
            picEscenario.Invalidate();
        }

        // actualiza barras de vida, etiquetas bilingues y redibuja la arena
        private void ActualizarInterfaz()
        {
            if (heroeJugadorActual != null)
            {
                pbHpJugador.Value = Math.Max(0, Math.Min(100, heroeJugadorActual.VidaActual));
                
                string nomTraducido = ObtenerNombreTraducido(heroeJugadorActual.Clave, heroeJugadorActual.Nombre);
                string elemTraducido = ObtenerElementoTraducido(heroeJugadorActual.Elemento);

                // formato bilingue para la barra del jugador
                if (Idioma.Actual == Lenguaje.EN)
                {
                    lblNombreJugador.Text = string.Format("{0} [{1}] [{2} HP] [{3} on bench]", 
                        nomTraducido.ToUpper(), elemTraducido.ToUpper(), heroeJugadorActual.VidaActual, colaJugador.Count - 1);
                }
                else
                {
                    lblNombreJugador.Text = string.Format("{0} [{1}] [{2} HP] [{3} en banca]", 
                        nomTraducido.ToUpper(), elemTraducido.ToUpper(), heroeJugadorActual.VidaActual, colaJugador.Count - 1);
                }
            }

            if (heroeRivalActual != null)
            {
                pbHpRival.Value = Math.Max(0, Math.Min(100, heroeRivalActual.VidaActual));

                string nomRivalTraducido = ObtenerNombreTraducido(heroeRivalActual.Clave, heroeRivalActual.Nombre);
                string elemRivalTraducido = ObtenerElementoTraducido(heroeRivalActual.Elemento);

                // formato bilingue para la barra del rival
                if (Idioma.Actual == Lenguaje.EN)
                {
                    lblNombreRival.Text = string.Format("RIVAL: {0} [{1}] [{2} HP] [{3} on bench]", 
                        nomRivalTraducido.ToUpper(), elemRivalTraducido.ToUpper(), heroeRivalActual.VidaActual, colaRival.Count - 1);
                }
                else
                {
                    lblNombreRival.Text = string.Format("RIVAL: {0} [{1}] [{2} HP] [{3} en banca]", 
                        nomRivalTraducido.ToUpper(), elemRivalTraducido.ToUpper(), heroeRivalActual.VidaActual, colaRival.Count - 1);
                }
            }

            lblEnergiaJugador.Text = Idioma.T("partida.energia", turno);
            picEscenario.Invalidate();
        }

        // busca y carga el fondo de la arena aplicandole un difuminado rapido
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

            // aplica difuminado al fondo o crea un degradado oscuro por defecto
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

            // centra los paneles de control sobre el escenario
            pnlHUD.Parent = picEscenario;
            pnlAcciones.Parent = picEscenario;
            pnlHUD.Left = (this.ClientSize.Width - pnlHUD.Width) / 2;
            pnlAcciones.Left = (this.ClientSize.Width - pnlAcciones.Width) / 2;
        }

        // escala la imagen a menor resolucion y la reescala para simular blur rapido
        private Bitmap AplicarDifuminado(Image original, int anchoFinal, int altoFinal)
        {
            if (anchoFinal <= 0) anchoFinal = 960;
            if (altoFinal <= 0) altoFinal = 540;

            int anchoReducido = Math.Max(1, anchoFinal / 12);
            int altoReducido = Math.Max(1, altoFinal / 12);

            // reduce el tamano de la imagen
            Bitmap miniatura = new Bitmap(anchoReducido, altoReducido);
            using (Graphics gMin = Graphics.FromImage(miniatura))
            {
                gMin.InterpolationMode = InterpolationMode.Bilinear;
                gMin.DrawImage(original, 0, 0, anchoReducido, altoReducido);
            }

            // estira la miniatura de vuelta con tinte oscuro
            Bitmap resultado = new Bitmap(anchoFinal, altoFinal);
            using (Graphics gRes = Graphics.FromImage(resultado))
            {
                gRes.InterpolationMode = InterpolationMode.Bilinear;
                gRes.SmoothingMode = SmoothingMode.None;
                gRes.DrawImage(miniatura, 0, 0, anchoFinal, altoFinal);

                using (SolidBrush tinte = new SolidBrush(Color.FromArgb(70, 0, 0, 0)))
                {
                    gRes.FillRectangle(tinte, 0, 0, anchoFinal, altoFinal);
                }
            }

            miniatura.Dispose();
            return resultado;
        }

        // carga las imagenes de los dos heroes activos y voltea al rival
        private void CargarImagenesCombatientes()
        {
            string carpeta = @"C:\Users\BOOM\Documents\SharpDevelop Projects\proyecto_final_iujo\proyecto_final_iujo\Personajes";
            if (!Directory.Exists(carpeta))
            {
                string rel = Path.GetFullPath(Path.Combine(Application.StartupPath, @"..\..\Personajes"));
                carpeta = Directory.Exists(rel) ? rel : Application.StartupPath;
            }

            // limpia las imagenes viejas de memoria
            if (imgJugador != null) { imgJugador.Dispose(); imgJugador = null; }
            if (imgRival != null) { imgRival.Dispose(); imgRival = null; }

            // carga y procesa la imagen del jugador
            if (heroeJugadorActual != null)
            {
                using (Image rawJugador = LeerImagenSegura(carpeta, heroeJugadorActual.Clave))
                {
                    if (rawJugador != null) imgJugador = QuitarFondoNegro(rawJugador);
                }
            }

            // carga, procesa y voltea la imagen del rival
            if (heroeRivalActual != null)
            {
                using (Image rawRival = LeerImagenSegura(carpeta, heroeRivalActual.Clave))
                {
                    if (rawRival != null)
                    {
                        imgRival = QuitarFondoNegro(rawRival);
                        // invertir una sola vez al cargar la imagen, evitando hacerlo en cada paint
                        imgRival.RotateFlip(RotateFlipType.RotateNoneFlipX);
                    }
                }
            }
        }

        // aplica transparencia al color negro sin congelar el hilo principal
        private Bitmap QuitarFondoNegro(Image imgOriginal)
        {
            if (imgOriginal == null) return null;

            // reemplaza el bucle pixel por pixel que congelaba los botones
            Bitmap bmp = new Bitmap(imgOriginal);
            bmp.MakeTransparent(Color.Black);
            return bmp;
        }

        // busca el archivo por clave con o sin acentos en formato png o jpg
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

        // dibuja a ambos heroes con sus auras y sombras en la arena
        private void DibujarPersonajesEnArena(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            int anchoHeroe = 320;
            int altoHeroe = 310;
            int yPiso = pnlAcciones.Top - altoHeroe + 20;

            // dibuja al jugador con su aura correspondiente
            int xJugador = 70;
            if (imgJugador != null)
            {
                Color colorAura = (heroeJugadorActual != null && heroeJugadorActual.Elemento == "Agua") ? Color.FromArgb(90, 60, 160, 255) : Color.FromArgb(90, 255, 120, 30);
                DibujarSombraYResplandor(g, xJugador, yPiso, anchoHeroe, altoHeroe, colorAura);
                g.DrawImage(imgJugador, new Rectangle(xJugador, yPiso, anchoHeroe, altoHeroe));
            }

            // dibuja al rival directamente con la imagen invertida
            int xRival = this.ClientSize.Width - anchoHeroe - 70;
            if (imgRival != null)
            {
                Color colorAuraRival = (heroeRivalActual != null && heroeRivalActual.Elemento == "Viento") ? Color.FromArgb(90, 140, 240, 180) : Color.FromArgb(90, 90, 200, 255);
                DibujarSombraYResplandor(g, xRival, yPiso, anchoHeroe, altoHeroe, colorAuraRival);
                g.DrawImage(imgRival, new Rectangle(xRival, yPiso, anchoHeroe, altoHeroe));
            }
        }

        // dibuja las capas de sombra en la base y el resplandor de fondo
        private void DibujarSombraYResplandor(Graphics g, int x, int y, int ancho, int alto, Color colorAura)
        {
            // traza la sombra eliptica en capas degradadas
            for (int i = 5; i >= 1; i--)
            {
                int alfaSombra = 20 * i;

                using (SolidBrush brochaSombra = new SolidBrush(Color.FromArgb(alfaSombra, 0, 0, 0)))
                {
                    g.FillEllipse(brochaSombra, x + 20 - (i * 4), y + alto - 35, ancho - 40 + (i * 8), 35 + (i * 4));
                }
            }

            // dibuja el aura de energia alrededor del heroe
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

        // regresa a la ventana del menu principal
        private void VolverAlMenu()
        {
            Idioma.Cambiado -= OnIdiomaCambiado;
            MainForm inicio = new MainForm();
            inicio.Show();
            this.Dispose();
        }

        // boton para ejecutar ataque de fuego
        void BtnAtacarClick(object sender, EventArgs e)
        {
            poderFuegoActivo = true;
            ProcesarAccionTurno("Ataque de Fuego");
        }

        // boton para ejecutar defensa de tierra
        void BtnDefenderClick(object sender, EventArgs e)
        {
            poderTierraActivo = true;
            ProcesarAccionTurno("Defensa de Tierra");
        }

        // boton para ejecutar habilidad especial de agua
        void BtnEspecialClick(object sender, EventArgs e)
        {
            poderAguaActivo = true;
            ProcesarAccionTurno("Especial de Agua");
        }

        // boton para ejecutar relevo con carta de aire
        void BtnRelevoClick(object sender, EventArgs e)
        {
            poderAireActivo = true;
            EjecutarRelevoEstrategico();
            ProcesarAccionTurno("Relevo de Aire");
        }

        // libera memoria de imagenes y cierra la app si se cierra la ventana
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            Idioma.Cambiado -= OnIdiomaCambiado;
            if (imgJugador != null) imgJugador.Dispose();
            if (imgRival != null) imgRival.Dispose();
            if (picEscenario != null && picEscenario.Image != null) picEscenario.Image.Dispose();
            base.OnFormClosing(e);

            // mata el proceso al cerrar la ventana con la x
            if (e.CloseReason == CloseReason.UserClosing)
            {
                Application.Exit();
            }
        }

        // muestra cuadro de mensaje emergente en espanol o ingles
        private void emergentBox(string ES, string EN, string NOMBRE, string NAME) {
            switch (Idioma.Actual) {
                case Lenguaje.ES: 
                    MessageBox.Show(ES, NOMBRE, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    break;
                case Lenguaje.EN: 
                    MessageBox.Show(EN, NAME, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    break;
            }
        }
    }
}