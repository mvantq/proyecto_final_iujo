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

			this.turnoTerremoto = random.Next(4, 6);

			if (equipo != null && equipo.Count > 0)
			{
				lblNombreJugador.Text = equipo[0];
			}

			lblLogTurno.Text = "¡COMIENZA EL COMBATE!";
			ActualizarInterfaz();
		}

		private void ProcesarAccionTurno(string tipoAccion)
		{
			turno++;

			string[] escenarios = { "Vórtice Huracanado", "Volcán Ardiente", "Ciénaga Helada", "Desfiladero Rocoso" };
			string escenarioActual = escenarios[turno % 4];

			bool esTurnoTerremoto = (!terremotoOcurrido && turno >= turnoTerremoto);

			// LLAMADA AL MÓDULO 2 (CÁLCULO DE DAÑO Y EVENTOS)
			ResultadoTurno res = AlgoritmoDano.ProcesarTurno(
				vida, turno, escenarioActual,
				poderFuegoActivo, poderAguaActivo, poderTierraActivo, poderAireActivo,
				esTurnoTerremoto
			);

			if (res.TerremotoOcurrido) terremotoOcurrido = true;

			vida = res.VidaRestante;
			ActualizarInterfaz();

			lblLogTurno.Text = string.Format("[Turno {0} - {1}] {2} (Daño: {3})", turno, tipoAccion, res.MensajeDetalle, res.DanoRecibido);

			if (vida <= 0)
			{
				MessageBox.Show("El equipo ha caído ante los elementos de la frontera.", "Fin de la Partida", MessageBoxButtons.OK, MessageBoxIcon.Information);
				MainForm inicio = new MainForm();
				inicio.Show();
				this.Close();
				return;
			}

			// Reiniciar estados de cartas/protecciones para el siguiente turno
			poderFuegoActivo = poderAguaActivo = poderTierraActivo = poderAireActivo = false;
		}

		private void ActualizarInterfaz()
		{
			pbHpJugador.Value = Math.Max(0, Math.Min(100, vida));
			lblEnergiaJugador.Text = string.Format("ENERGÍA: 5\nTURNO {0}", turno);
		}

		void BtnAtacarClick(object sender, EventArgs e)
		{
			poderFuegoActivo = true;
			ProcesarAccionTurno("Ataque de Fuego");
		}

		void BtnDefenderClick(object sender, EventArgs e)
		{
			poderTierraActivo = true;
			ProcesarAccionTurno("Defensa de Tierra");
		}

		void BtnEspecialClick(object sender, EventArgs e)
		{
			poderAguaActivo = true;
			ProcesarAccionTurno("Especial de Agua");
		}

		void BtnRelevoClick(object sender, EventArgs e)
		{
			poderAireActivo = true;
			ProcesarAccionTurno("Relevo de Aire");
		}
	}
}