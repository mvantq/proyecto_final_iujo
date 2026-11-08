using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using proyecto_final_iujo.Logica.Vinculacion.Calculo;

namespace proyecto_final_iujo.Visuales
{
	public partial class FormPartida : Form
	{
		private List<string> equipo;
		private int turno = 1;
		private int vida = 100;
		private int turnoTerremoto;
		private bool terremotoOcurrido = false;
		private Random random = new Random();

		// Protección activa por turno
		private bool poderFuegoActivo = false;
		private bool poderAguaActivo = false;
		private bool poderTierraActivo = false;
		private bool poderAireActivo = false;

		public FormPartida(List<string> personajesCargados)
		{
			InitializeComponent();
			this.equipo = personajesCargados;

			// Aleatorio: ocurre en el turno 4 o en el turno 5 (después de 3 o 4 turnos)
			this.turnoTerremoto = random.Next(4, 6);

			lstHistorial.Items.Add("=== Partida Iniciada ===");
			foreach (var p in equipo)
			{
				lstHistorial.Items.Add("Héroe activo: " + p);
			}
			lstHistorial.Items.Add("Usa las cartas elementales para sobrevivir a las transiciones.");
		}

		void BtnAvanzarClick(object sender, EventArgs e)
		{
			turno++;
			lblTurno.Text = "Turno: " + turno;

			string[] escenarios = { "Vórtice Huracanado", "Volcán Ardiente", "Ciénaga Helada", "Desfiladero Rocoso" };
			string escenarioActual = escenarios[turno % 4];
			lblEscenario.Text = "Escenario: " + escenarioActual;

			bool esTurnoTerremoto = (!terremotoOcurrido && turno >= turnoTerremoto);

			// LLAMADA AL MÓDULO 2 (CÁLCULO DE DAÑO Y EVENTOS)
			ResultadoTurno res = AlgoritmoDano.ProcesarTurno(
				vida, turno, escenarioActual,
				poderFuegoActivo, poderAguaActivo, poderTierraActivo, poderAireActivo,
				esTurnoTerremoto
			);

			if (res.TerremotoOcurrido) terremotoOcurrido = true;

			vida = res.VidaRestante;
			lblVida.Text = "Vida Equipo: " + vida;
			lstHistorial.Items.Add("[Turno " + turno + "] " + res.MensajeDetalle + " (Daño sufrido: " + res.DanoRecibido + ")");

			if (vida <= 0)
			{
				lblVida.ForeColor = Color.Red;
				MessageBox.Show("El equipo ha caído ante los elementos de la frontera.", "Fin de la Partida", MessageBoxButtons.OK, MessageBoxIcon.Information);
				MainForm inicio = new MainForm();
				inicio.Show();
				this.Close();
				return;
			}

			// Reiniciar estados de cartas para el siguiente turno
			poderFuegoActivo = poderAguaActivo = poderTierraActivo = poderAireActivo = false;
			lstHistorial.TopIndex = lstHistorial.Items.Count - 1;
		}

		void BtnFuegoClick(object sender, EventArgs e)
		{
			poderFuegoActivo = true;
			lstHistorial.Items.Add("[Carta Activada] Poder de Fuego listo para este turno.");
		}

		void BtnAguaClick(object sender, EventArgs e)
		{
			poderAguaActivo = true;
			lstHistorial.Items.Add("[Carta Activada] Poder de Agua listo para este turno.");
		}

		void BtnTierraClick(object sender, EventArgs e)
		{
			poderTierraActivo = true;
			lstHistorial.Items.Add("[Carta Activada] Poder de Tierra listo para este turno.");
		}

		void BtnAireClick(object sender, EventArgs e)
		{
			poderAireActivo = true;
			lstHistorial.Items.Add("[Carta Activada] Poder de Aire listo para este turno.");
		}
	}
}