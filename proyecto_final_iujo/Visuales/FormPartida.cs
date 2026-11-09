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
		private int vidaRival = 100;
		private int turnoTerremoto;
		private bool terremotoOcurrido = false;
		private Random random = new Random();

		private bool poderFuegoActivo = false;
		private bool poderAguaActivo = false;
		private bool poderTierraActivo = false;
		private bool poderAireActivo = false;

		public FormPartida(List<string> personajesCargados)
		{
			InitializeComponent();
			this.equipo = personajesCargados;

			// Sismo aleatorio en el turno 4 o 5
			this.turnoTerremoto = random.Next(4, 6);

			if (equipo != null && equipo.Count > 0)
			{
				lblNombreJugador.Text = equipo[0];
			}

			ConfigurarFondoYTransparencia();
			lblLogTurno.Text = "¡COMIENZA EL COMBATE!";
			ActualizarInterfaz();
		}

		private void ConfigurarFondoYTransparencia()
		{
			this.BackgroundImageLayout = ImageLayout.Stretch;
			lblNombreJugador.BackColor = Color.Transparent;
			lblEnergiaJugador.BackColor = Color.Transparent;
		}

		private void ProcesarAccionTurno(string tipoAccion)
		{
			// 1. EVALUAR EVENTO SÍSMICO (Módulo 2 de Cálculo I)
			bool esTurnoTerremoto = (!terremotoOcurrido && turno == turnoTerremoto);

			string[] escenarios = { "Vórtice Huracanado", "Volcán Ardiente", "Ciénaga Helada", "Desfiladero Rocoso" };
			string escenarioActual = escenarios[turno % 4];

			ResultadoTurno res = AlgoritmoDano.ProcesarTurno(
				vida, turno, escenarioActual,
				poderFuegoActivo, poderAguaActivo, poderTierraActivo, poderAireActivo,
				esTurnoTerremoto
			);

			if (esTurnoTerremoto) terremotoOcurrido = true;

			int danoInfligido = 0;
			int curacion = 0;
			string descripcionAccion = "";

			// 2. FASE 1: ACCIÓN Y ATAQUE DEL JUGADOR
			if (tipoAccion == "Ataque de Fuego")
			{
				danoInfligido = random.Next(24, 34);
				descripcionAccion = string.Format("🔥 ¡Ataque Fuego! Infliges {0} de daño al rival.", danoInfligido);
			}
			else if (tipoAccion == "Defensa de Tierra")
			{
				danoInfligido = random.Next(5, 10);
				descripcionAccion = "🛡️ ¡Escudo de Tierra! Te preparas para mitigar el impacto.";
			}
			else if (tipoAccion == "Especial de Agua")
			{
				danoInfligido = random.Next(10, 16);
				curacion = random.Next(15, 23);
				vida = Math.Min(100, vida + curacion);
				descripcionAccion = string.Format("🌊 ¡Agua Curativa! Sanas +{0} HP e infliges {1} de daño.", curacion, danoInfligido);
			}
			else if (tipoAccion == "Relevo de Aire")
			{
				danoInfligido = random.Next(8, 14);
				descripcionAccion = string.Format("💨 ¡Relevo de Aire! Das un golpe rápido de {0} daño y esquivas.", danoInfligido);
			}

			// Aplicar daño al rival
			vidaRival = Math.Max(0, vidaRival - danoInfligido);

			// EVALUAR SI EL RIVAL FUE DERROTADO ANTES DE CONTRAATACAR
			if (vidaRival <= 0)
			{
				ActualizarInterfaz();
				MessageBox.Show("¡VICTORIA! Has derrotado al rival antes de que pudiera responder.", "Victoria", MessageBoxButtons.OK, MessageBoxIcon.Information);
				VolverAlMenu();
				return;
			}

			// 3. FASE 2: CONTRAATAQUE DEL RIVAL Y ENTORNO (Solo si el rival sigue vivo)
			int danoAtaqueRival = random.Next(14, 22);
			int danoFinalRecibido = 0;

			if (tipoAccion == "Defensa de Tierra")
			{
				// Mitigación del 85%: solo recibes 15% del daño total
				danoFinalRecibido = (int)((danoAtaqueRival + res.DanoRecibido) * 0.15);
			}
			else if (tipoAccion == "Relevo de Aire")
			{
				// Esquivas al rival por completo, solo recibes roce ambiental
				danoFinalRecibido = (int)(res.DanoRecibido * 0.2);
			}
			else
			{
				// Daño completo
				danoFinalRecibido = danoAtaqueRival + res.DanoRecibido;
			}

			vida = Math.Max(0, vida - danoFinalRecibido);

			// Incrementar turno
			turno++;
			ActualizarInterfaz();

			// 4. REGISTRO EN PANTALLA Y VENTANA DE GRÁFICA SÍSMICA
			if (esTurnoTerremoto)
			{
				lblLogTurno.ForeColor = Color.OrangeRed;
				lblLogTurno.Text = string.Format("⚠️ ¡TERREMOTO! {0} | Recibes: {1} daño sístmico", descripcionAccion, danoFinalRecibido);

				FormGraficaTerremoto grafica = new FormGraficaTerremoto();
				grafica.ShowDialog();
			}
			else
			{
				lblLogTurno.ForeColor = Color.FromArgb(255, 220, 90);
				lblLogTurno.Text = string.Format("[Turno {0}] {1} (Recibes: {2} daño)", turno - 1, descripcionAccion, danoFinalRecibido);
			}

			// 5. EVALUAR SI EL JUGADOR FUE DERROTADO
			if (vida <= 0)
			{
				MessageBox.Show("DERROTA: Tu equipo ha caído ante el contraataque rival. ¡Usa Defensa o Curación a tiempo!", "Derrota", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				VolverAlMenu();
				return;
			}

			poderFuegoActivo = poderAguaActivo = poderTierraActivo = poderAireActivo = false;
		}

		private void VolverAlMenu()
		{
			MainForm inicio = new MainForm();
			inicio.Show();
			this.Close();
		}

		private void ActualizarInterfaz()
		{
			pbHpJugador.Value = Math.Max(0, Math.Min(100, vida));
			pbHpRival.Value = Math.Max(0, Math.Min(100, vidaRival));
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