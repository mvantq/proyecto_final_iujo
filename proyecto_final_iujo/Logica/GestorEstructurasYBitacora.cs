using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace proyecto_final_iujo.Logica
{
    public class GestorEstructurasYBitacora
    {
        // Colas de relevos para ambos jugadores
        public Queue<Personaje> BancaJugador1 { get; private set; }
        public Queue<Personaje> BancaJugador2 { get; private set; }

        // Pila LIFO para el historial de jugadas de la partida
        public Stack<string> HistorialJugadas { get; private set; }

        public GestorEstructurasYBitacora()
        {
            BancaJugador1 = new Queue<Personaje>();
            BancaJugador2 = new Queue<Personaje>();
            HistorialJugadas = new Stack<string>();
        }

        // --- MÉTODOS DE LA COLA (QUEUE) ---
        public void AgregarABanca(int numeroJugador, Personaje personaje)
        {
            if (personaje == null) return;
            if (numeroJugador == 1)
                BancaJugador1.Enqueue(personaje);
            else if (numeroJugador == 2)
                BancaJugador2.Enqueue(personaje);

            RegistrarJugada( string.Format("Jugador {0} envió a la banca de relevos a {1}.", numeroJugador, personaje.ToString()) );
        }

        public Personaje SacarSiguienteRelevo(int numeroJugador)
        {
            Queue<Personaje> colaObjetivo = (numeroJugador == 1) ? BancaJugador1 : BancaJugador2;

            if (colaObjetivo.Count > 0)
            {
                Personaje relevo = colaObjetivo.Dequeue();
                RegistrarJugada(string.Format("Relevo activado: Jugador {0} saca al combate a {1}.", numeroJugador, relevo.ToString()));
                return relevo;
            }
            return null;
        }

        public Personaje VerProximoEnBanca(int numeroJugador)
        {
            Queue<Personaje> colaObjetivo = (numeroJugador == 1) ? BancaJugador1 : BancaJugador2;
            return colaObjetivo.Count > 0 ? colaObjetivo.Peek() : null;
        }

        // --- MÉTODOS DE LA PILA (STACK) ---
        public void RegistrarJugada(string descripcionJugada)
        {
            string marcaTiempo = DateTime.Now.ToString("HH:mm:ss");
            string entrada = string.Format("[{0}] {1}", marcaTiempo, descripcionJugada);
            HistorialJugadas.Push(entrada);
        }

        public string ObtenerUltimaJugada()
        {
            return HistorialJugadas.Count > 0 ? HistorialJugadas.Peek() : "Sin jugadas registradas.";
        }

        // --- EXPORTACIÓN DE BITÁCORA A ARCHIVO .TXT ---
        public string ExportarBitacoraTxt(string ganador)
        {
            string carpeta = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Bitacoras");
            if (!Directory.Exists(carpeta))
            {
                Directory.CreateDirectory(carpeta);
            }

            string nombreArchivo = string.Format("Bitacora_Batalla_{0}.txt", DateTime.Now.ToString("yyyyMMdd_HHmmss"));
            string rutaCompleta = Path.Combine(carpeta, nombreArchivo);

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("==================================================");
            sb.AppendLine("        BITÁCORA DE BATALLA - ESCENARIO LA ALDEA  ");
            sb.AppendLine("==================================================");
            sb.AppendLine("Fecha y Hora : " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            sb.AppendLine("Ganador      : " + ganador);
            sb.AppendLine("Total Jugadas: " + HistorialJugadas.Count);
            sb.AppendLine("--------------------------------------------------");
            sb.AppendLine("HISTORIAL DE COMBATE (Orden inverso - Pila LIFO):");
            sb.AppendLine("--------------------------------------------------");

            // Recorremos la pila sin vaciarla (de la jugada más reciente a la primera)
            foreach (string jugada in HistorialJugadas)
            {
                sb.AppendLine(jugada);
            }

            sb.AppendLine("==================================================");
            sb.AppendLine("           FIN DEL REGISTRO DE COMBATE            ");
            sb.AppendLine("==================================================");

            File.WriteAllText(rutaCompleta, sb.ToString(), Encoding.UTF8);
            return rutaCompleta;
        }
    }
}