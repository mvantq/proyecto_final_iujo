using System;
using System.Windows.Forms;


namespace proyecto_final_iujo.Logica
{
    public enum EstadoPartida
    {
        EsperandoInicio,
        TurnoJugador1,
        TurnoJugador2,
        PartidaFinalizada
    }

    public class MotorTurnos
    {
        public EstadoPartida EstadoActual { get; private set; }
        public int TiempoRestanteTurno { get; private set; }
        public int DuracionMaximaTurno { get; set; }
        public int NumeroTurnoGlobal { get; private set; }

        public Personaje CartaActivaJ1 { get; set; }
        public Personaje CartaActivaJ2 { get; set; }

        public GestorEstructurasYBitacora Estructuras { get; private set; }

        private Timer _timerTurno;

        // Eventos para enlazar con FormPartida sin romper su código actual
        public event Action<int> AlActualizarReloj;
        public event Action<EstadoPartida, int> AlCambiarTurno;
        public event Action<string, string> AlFinalizarPartida;

        public MotorTurnos(int segundosPorTurno = 30)
        {
            DuracionMaximaTurno = segundosPorTurno;
            TiempoRestanteTurno = segundosPorTurno;
            EstadoActual = EstadoPartida.EsperandoInicio;
            NumeroTurnoGlobal = 0;

            Estructuras = new GestorEstructurasYBitacora();

            _timerTurno = new Timer();
            _timerTurno.Interval = 1000; // 1 segundo
            _timerTurno.Tick += TimerTurno_Tick;
        }

        public void IniciarCombate(Personaje inicialJ1, Personaje inicialJ2)
        {
            CartaActivaJ1 = inicialJ1;
            CartaActivaJ2 = inicialJ2;
            NumeroTurnoGlobal = 1;
            EstadoActual = EstadoPartida.TurnoJugador1;
            TiempoRestanteTurno = DuracionMaximaTurno;

            Estructuras.RegistrarJugada("¡Comienza la partida en La Aldea! Turno 1 para el Jugador 1.");
            _timerTurno.Start();

            if (AlCambiarTurno != null)
                AlCambiarTurno(EstadoActual, NumeroTurnoGlobal);
        }

        private void TimerTurno_Tick(object sender, EventArgs e)
        {
            if (EstadoActual == EstadoPartida.PartidaFinalizada || EstadoActual == EstadoPartida.EsperandoInicio)
                return;

            TiempoRestanteTurno--;

            if (AlActualizarReloj != null)
                AlActualizarReloj(TiempoRestanteTurno);

            if (TiempoRestanteTurno <= 0)
            {
                string jugadorAgotado = (EstadoActual == EstadoPartida.TurnoJugador1) ? "Jugador 1" : "Jugador 2";
                Estructuras.RegistrarJugada(jugadorAgotado + " agotó su tiempo de turno. Cambio automático.");
                AlternarTurno();
            }
        }

        public void AlternarTurno()
        {
            if (EstadoActual == EstadoPartida.PartidaFinalizada) return;

            if (EstadoActual == EstadoPartida.TurnoJugador1)
            {
                EstadoActual = EstadoPartida.TurnoJugador2;
            }
            else if (EstadoActual == EstadoPartida.TurnoJugador2)
            {
                EstadoActual = EstadoPartida.TurnoJugador1;
                NumeroTurnoGlobal++;
            }

            TiempoRestanteTurno = DuracionMaximaTurno;
            string jugadorActivo = (EstadoActual == EstadoPartida.TurnoJugador1) ? "Jugador 1" : "Jugador 2";
            Estructuras.RegistrarJugada(string.Format("Cambio de estado -> Ahora es el turno del {0} (Turno #{1}).", jugadorActivo, NumeroTurnoGlobal));

            if (AlCambiarTurno != null)
                AlCambiarTurno(EstadoActual, NumeroTurnoGlobal);
        }

        public void VerificarRelevoOBaja(int jugadorAfectado, bool cartaDerrotada)
        {
            if (!cartaDerrotada) return;

            Personaje nuevoRelevo = Estructuras.SacarSiguienteRelevo(jugadorAfectado);
            if (nuevoRelevo != null)
            {
                if (jugadorAfectado == 1)
                    CartaActivaJ1 = nuevoRelevo;
                else
                    CartaActivaJ2 = nuevoRelevo;
            }
            else
            {
                // Si no quedan cartas en la Queue<Personaje>, termina la partida
                string ganador = (jugadorAfectado == 1) ? "Jugador 2" : "Jugador 1";
                FinalizarPartida(ganador);
            }
        }

        public void FinalizarPartida(string nombreGanador)
        {
            _timerTurno.Stop();
            EstadoActual = EstadoPartida.PartidaFinalizada;
            Estructuras.RegistrarJugada("PARTIDA FINALIZADA. Victoria para: " + nombreGanador);

            string rutaTxt = Estructuras.ExportarBitacoraTxt(nombreGanador);

            if (AlFinalizarPartida != null)
                AlFinalizarPartida(nombreGanador, rutaTxt);
        }

        public void PausarReloj()
        {
            _timerTurno.Stop();
        }

        public void ReanudarReloj()
        {
            if (EstadoActual == EstadoPartida.TurnoJugador1 || EstadoActual == EstadoPartida.TurnoJugador2)
                _timerTurno.Start();
        }
    }
}