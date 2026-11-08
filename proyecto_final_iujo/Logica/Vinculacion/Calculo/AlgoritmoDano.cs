using System;
using System.Collections.Generic;
using proyecto_final_iujo; // Necesario para acceder a Personaje y Elemento

namespace proyecto_final_iujo.Logica.Vinculacion.Calculo
{
	public class ResultadoTurno
	{
		public int DanoRecibido { get; set; }
		public int VidaRestante { get; set; }
		public bool TerremotoOcurrido { get; set; }
		public string MensajeDetalle { get; set; }
	}

	public static class AlgoritmoDano
	{
		// 1. Suma la salud inicial real de los 5 personajes (usando VidaMaxima)
		public static int CalcularVidaInicialEquipo(List<Personaje> equipo)
		{
			if (equipo == null || equipo.Count == 0) return 100;
			
			int sumaHP = 0;
			foreach (var p in equipo)
			{
				sumaHP += p.VidaMaxima;
			}
			return sumaHP;
		}

		// 2. Calcula la mitigación pasiva del equipo basándose en Defensa (Escudo) y Empatía
		public static float CalcularFactorMitigacionPasiva(List<Personaje> equipo)
		{
			if (equipo == null || equipo.Count == 0) return 0f;

			int defTotal = 0;
			int empatiaTotal = 0;

			foreach (var p in equipo)
			{
				defTotal += p.Defensa;
				empatiaTotal += p.Empatia;
			}

			float defPromedio = defTotal / (float)equipo.Count;
			float bonoEmpatia = (empatiaTotal / (float)equipo.Count) * 0.05f;

			// Tope de reducción pasiva del 40%
			return Math.Min(0.40f, (defPromedio / 100f) + bonoEmpatia);
		}

		// 3. Procesa el daño ambiental del turno y los eventos de terremoto
		public static ResultadoTurno ProcesarTurno(
			int vidaActual,
			int numeroTurno,
			string tipoEscenario,
			bool poderFuego,
			bool poderAgua,
			bool poderTierra,
			bool poderAire,
			bool terremotoActivado,
			List<Personaje> equipo = null)
		{
			ResultadoTurno resultado = new ResultadoTurno();
			int danoBaseEscenario = 0;
			int danoSismico = 0;
			string explicacion = "";

			float factorMitigacion = (equipo != null) ? CalcularFactorMitigacionPasiva(equipo) : 0f;

			// Evaluación del escenario ambiental
			switch (tipoEscenario.ToLower())
			{
				case "volcán ardiente":
				case "volcan":
					if (!poderAgua) {
						danoBaseEscenario = 20;
						explicacion += "Calor sofocante (Falta Poder de Agua). ";
					} else {
						explicacion += "El Poder de Agua neutralizó el calor volcánico. ";
					}
					break;

				case "ciénaga helada":
				case "cienaga":
					if (!poderFuego) {
						danoBaseEscenario = 15;
						explicacion += "Frío y niebla extrema (Falta Poder de Fuego). ";
					} else {
						explicacion += "El Poder de Fuego disipó la ciénaga. ";
					}
					break;

				case "desfiladero rocoso":
				case "desfiladero":
					if (!poderTierra) {
						danoBaseEscenario = 15;
						explicacion += "Derrumbe de piedras (Falta Poder de Tierra). ";
					} else {
						explicacion += "El Poder de Tierra absorbió el derrumbe. ";
					}
					break;

				case "vórtice huracanado":
				case "vortice":
					if (!poderAire) {
						danoBaseEscenario = 15;
						explicacion += "Vendavales cortantes (Falta Poder de Aire). ";
					} else {
						explicacion += "El Poder de Aire amortiguó el vórtice. ";
					}
					break;
			}

			// Evaluación de amenaza sísmica
			if (terremotoActivado)
			{
				resultado.TerremotoOcurrido = true;
				if (!poderTierra) {
					danoSismico = 25;
					explicacion += "¡Terremoto sin anclaje de Tierra! (+25 daño). ";
				} else {
					explicacion += "¡Terremoto esquivado gracias al Poder de Tierra! ";
				}
			}

			int danoAmbientalFinal = (int)(danoBaseEscenario * (1f - factorMitigacion));
			int danoTotal = danoAmbientalFinal + danoSismico;

			resultado.DanoRecibido = danoTotal;
			resultado.VidaRestante = Math.Max(0, vidaActual - danoTotal);
			resultado.MensajeDetalle = explicacion.Trim();

			return resultado;
		}
	}
}