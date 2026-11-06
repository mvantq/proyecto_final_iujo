using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

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

			// 1. Progresión progresiva y automática del escenario
			string escenarioActual = "";
			int fase = turno % 4;
			int dano = 0;

			switch (fase)
			{
				case 1:
					escenarioActual = "Volcán Ardiente";
					lblEscenario.Text = "Escenario: " + escenarioActual;
					if (!poderAguaActivo) {
						dano = 20;
						lstHistorial.Items.Add("[Turno " + turno + "] Calor sofocante: recibes 20 de daño (Poder de Agua requerido).");
					} else {
						lstHistorial.Items.Add("[Turno " + turno + "] El Poder de Agua sofocó las llamas del volcán.");
					}
					break;

				case 2:
					escenarioActual = "Ciénaga Helada";
					lblEscenario.Text = "Escenario: " + escenarioActual;
					if (!poderFuegoActivo) {
						dano = 15;
						lstHistorial.Items.Add("[Turno " + turno + "] Niebla y frío extremo: recibes 15 de daño (Poder de Fuego requerido).");
					} else {
						lstHistorial.Items.Add("[Turno " + turno + "] El Poder de Fuego calentó la ciénaga.");
					}
					break;

				case 3:
					escenarioActual = "Desfiladero Rocoso";
					lblEscenario.Text = "Escenario: " + escenarioActual;
					if (!poderTierraActivo) {
						dano = 15;
						lstHistorial.Items.Add("[Turno " + turno + "] Derrumbe de piedras: recibes 15 de daño (Poder de Tierra requerido).");
					} else {
						lstHistorial.Items.Add("[Turno " + turno + "] El Poder de Tierra protegió del derrumbe.");
					}
					break;

				case 0:
					escenarioActual = "Vórtice Huracanado";
					lblEscenario.Text = "Escenario: " + escenarioActual;
					if (!poderAireActivo) {
						dano = 15;
						lstHistorial.Items.Add("[Turno " + turno + "] Vendavales cortantes: recibes 15 de daño (Poder de Aire requerido).");
					} else {
						lstHistorial.Items.Add("[Turno " + turno + "] El Poder de Aire canalizó la corriente.");
					}
					break;
			}

			// 2. Evento aleatorio de Terremoto tras 3 o 4 turnos
			if (!terremotoOcurrido && turno >= turnoTerremoto)
			{
				terremotoOcurrido = true;
				lstHistorial.Items.Add("¡¡ALERTA!! ¡UN TERREMOTO SACUDE LA ALDEA!");
				if (poderTierraActivo) {
					lstHistorial.Items.Add("El Poder de Tierra estabilizó el suelo. Sin daño sísmico.");
				} else {
					dano += 25;
					lstHistorial.Items.Add("Sin anclaje de tierra: sufres 25 de daño por el temblor.");
				}
			}

			// Aplicar daño
			vida -= dano;
			if (vida <= 0)
			{
				vida = 0;
				lblVida.Text = "Vida Equipo: 0";
				lblVida.ForeColor = Color.Red;
				MessageBox.Show("El equipo ha caído ante los elementos de la frontera.", "Fin de la Partida", MessageBoxButtons.OK, MessageBoxIcon.Information);
				MainForm inicio = new MainForm();
				inicio.Show();
				this.Close();
				return;
			}

			lblVida.Text = "Vida Equipo: " + vida;

			// Reset de los poderes para el siguiente turno
			poderFuegoActivo = false;
			poderAguaActivo = false;
			poderTierraActivo = false;
			poderAireActivo = false;

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