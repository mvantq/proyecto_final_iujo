using System;

namespace proyecto_final_iujo.Logica.Vinculacion.Calculo
{
	public class ResultadoTurno
	{
		public int VidaRestante { get; set; }
		public int DanoRecibido { get; set; }
		public bool TerremotoOcurrido { get; set; }
		public string MensajeDetalle { get; set; }
	}

	public static class AlgoritmoDano
	{
		private static Random random = new Random();

		/// <summary>
		/// MÓDULO DE CÁLCULO I: Procesa el daño ambiental y eventos sísmicos.
		/// </summary>
		public static ResultadoTurno ProcesarTurno(
			int vidaActual, 
			int turno, 
			string escenario,
			bool escudoFuego, 
			bool escudoAgua, 
			bool escudoTierra, 
			bool escudoAire,
			bool esTurnoTerremoto)
		{
			ResultadoTurno resultado = new ResultadoTurno();
			resultado.TerremotoOcurrido = esTurnoTerremoto;

			// =========================================================================
			// UNIDAD 1: SISTEMA DE COORDENADAS Y DISTANCIA ENTRE DOS PUNTOS
			// d = √[(x₂ - x₁)² + (y₂ - y₁)²]
			// =========================================================================
			double epicentroX = 0.0;
			double epicentroY = 0.0;
			
			// Posición del personaje según el escenario en el plano cartesiano
			double jugadorX = random.NextDouble() * 4.0 + 1.0; // Distancia entre 1.0 y 5.0 km
			double jugadorY = random.NextDouble() * 3.0 + 1.0;

			double distancia = Math.Sqrt(Math.Pow(jugadorX - epicentroX, 2) + Math.Pow(jugadorY - epicentroY, 2));

			// =========================================================================
			// UNIDAD 2: FUNCIONES EXPONENCIALES, TRIGONOMÉTRICAS Y LÍMITES AL INFINITO
			// f(d) = A · e^(-k·d) · sin(ω·d)  |  lim (d->∞) f(d) = 0 (Asíntota Horizontal y=0)
			// =========================================================================
			int danoBase = 0;

			if (esTurnoTerremoto)
			{
				double amplitud = 45.0;  // Magnitud inicial A
				double k = 0.35;          // Coeficiente de atenuación
				double omega = 2.5;       // Frecuencia angular de la onda

				// Cálculo amortiguado exponencial/trigonométrico
				double intensidad = Math.Abs(amplitud * Math.Exp(-k * distancia) * Math.Sin(omega * distancia));
				danoBase = (int)Math.Round(intensidad);

				resultado.MensajeDetalle = string.Format(
					"Sismo a {0:F2}km de distancia (Epicentro [0,0]). Intensidad f(d)={1} HP", 
					distancia, danoBase);
			}
			else
			{
				// Daño ambiental moderado por desgaste de escenario
				danoBase = random.Next(5, 12);
				resultado.MensajeDetalle = string.Format("Efecto de zona: {0}", escenario);
			}

			// Aplicar mitigación elemental de escudos
			if (escudoTierra) danoBase = 0; // Cancelación total por protección
			else if (escudoAire) danoBase = (int)(danoBase * 0.5);

			resultado.DanoRecibido = danoBase;
			resultado.VidaRestante = Math.Max(0, vidaActual - danoBase);

			return resultado;
		}
	}
}