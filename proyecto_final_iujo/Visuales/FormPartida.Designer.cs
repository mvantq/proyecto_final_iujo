namespace proyecto_final_iujo.Visuales
{
	partial class FormPartida
	{
		private System.ComponentModel.IContainer components = null;
		private System.Windows.Forms.Label lblEscenario;
		private System.Windows.Forms.Label lblTurno;
		private System.Windows.Forms.Label lblVida;
		private System.Windows.Forms.ListBox lstHistorial;
		private System.Windows.Forms.Button btnAvanzar;
		private System.Windows.Forms.Button btnFuego;
		private System.Windows.Forms.Button btnAgua;
		private System.Windows.Forms.Button btnTierra;
		private System.Windows.Forms.Button btnAire;
		private System.Windows.Forms.Label lblCartas;

		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}

		private void InitializeComponent()
		{
			this.lblEscenario = new System.Windows.Forms.Label();
			this.lblTurno = new System.Windows.Forms.Label();
			this.lblVida = new System.Windows.Forms.Label();
			this.lstHistorial = new System.Windows.Forms.ListBox();
			this.btnAvanzar = new System.Windows.Forms.Button();
			this.lblCartas = new System.Windows.Forms.Label();
			this.btnFuego = new System.Windows.Forms.Button();
			this.btnAgua = new System.Windows.Forms.Button();
			this.btnTierra = new System.Windows.Forms.Button();
			this.btnAire = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// lblEscenario
			// 
			this.lblEscenario.Font = new System.Drawing.Font("Arial", 11F, System.Drawing.FontStyle.Bold);
			this.lblEscenario.Location = new System.Drawing.Point(20, 15);
			this.lblEscenario.Name = "lblEscenario";
			this.lblEscenario.Size = new System.Drawing.Size(350, 23);
			this.lblEscenario.TabIndex = 0;
			this.lblEscenario.Text = "Escenario: Frontera Tranquila";
			// 
			// lblTurno
			// 
			this.lblTurno.Font = new System.Drawing.Font("Arial", 10F);
			this.lblTurno.Location = new System.Drawing.Point(20, 42);
			this.lblTurno.Name = "lblTurno";
			this.lblTurno.Size = new System.Drawing.Size(150, 20);
			this.lblTurno.TabIndex = 1;
			this.lblTurno.Text = "Turno: 1";
			// 
			// lblVida
			// 
			this.lblVida.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
			this.lblVida.ForeColor = System.Drawing.Color.ForestGreen;
			this.lblVida.Location = new System.Drawing.Point(200, 42);
			this.lblVida.Name = "lblVida";
			this.lblVida.Size = new System.Drawing.Size(180, 20);
			this.lblVida.TabIndex = 2;
			this.lblVida.Text = "Vida Equipo: 100";
			// 
			// lstHistorial
			// 
			this.lstHistorial.Font = new System.Drawing.Font("Consolas", 9F);
			this.lstHistorial.FormattingEnabled = true;
			this.lstHistorial.HorizontalScrollbar = true;
			this.lstHistorial.ItemHeight = 14;
			this.lstHistorial.Location = new System.Drawing.Point(20, 68);
			this.lstHistorial.Name = "lstHistorial";
			this.lstHistorial.Size = new System.Drawing.Size(550, 172);
			this.lstHistorial.TabIndex = 3;
			// 
			// btnAvanzar
			// 
			this.btnAvanzar.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
			this.btnAvanzar.Location = new System.Drawing.Point(20, 252);
			this.btnAvanzar.Name = "btnAvanzar";
			this.btnAvanzar.Size = new System.Drawing.Size(140, 40);
			this.btnAvanzar.TabIndex = 4;
			this.btnAvanzar.Text = "Avanzar Turno";
			this.btnAvanzar.UseVisualStyleBackColor = true;
			this.btnAvanzar.Click += new System.EventHandler(this.BtnAvanzarClick);
			// 
			// lblCartas
			// 
			this.lblCartas.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
			this.lblCartas.Location = new System.Drawing.Point(180, 248);
			this.lblCartas.Name = "lblCartas";
			this.lblCartas.Size = new System.Drawing.Size(390, 16);
			this.lblCartas.TabIndex = 5;
			this.lblCartas.Text = "Usar Cartas de Poder para Sobrevivir al Entorno:";
			// 
			// btnFuego
			// 
			this.btnFuego.Location = new System.Drawing.Point(180, 266);
			this.btnFuego.Name = "btnFuego";
			this.btnFuego.Size = new System.Drawing.Size(90, 26);
			this.btnFuego.TabIndex = 6;
			this.btnFuego.Text = "Poder Fuego";
			this.btnFuego.UseVisualStyleBackColor = true;
			this.btnFuego.Click += new System.EventHandler(this.BtnFuegoClick);
			// 
			// btnAgua
			// 
			this.btnAgua.Location = new System.Drawing.Point(276, 266);
			this.btnAgua.Name = "btnAgua";
			this.btnAgua.Size = new System.Drawing.Size(90, 26);
			this.btnAgua.TabIndex = 7;
			this.btnAgua.Text = "Poder Agua";
			this.btnAgua.UseVisualStyleBackColor = true;
			this.btnAgua.Click += new System.EventHandler(this.BtnAguaClick);
			// 
			// btnTierra
			// 
			this.btnTierra.Location = new System.Drawing.Point(372, 266);
			this.btnTierra.Name = "btnTierra";
			this.btnTierra.Size = new System.Drawing.Size(90, 26);
			this.btnTierra.TabIndex = 8;
			this.btnTierra.Text = "Poder Tierra";
			this.btnTierra.UseVisualStyleBackColor = true;
			this.btnTierra.Click += new System.EventHandler(this.BtnTierraClick);
			// 
			// btnAire
			// 
			this.btnAire.Location = new System.Drawing.Point(468, 266);
			this.btnAire.Name = "btnAire";
			this.btnAire.Size = new System.Drawing.Size(90, 26);
			this.btnAire.TabIndex = 9;
			this.btnAire.Text = "Poder Aire";
			this.btnAire.UseVisualStyleBackColor = true;
			this.btnAire.Click += new System.EventHandler(this.BtnAireClick);
			// 
			// FormPartida
			// 
			this.ClientSize = new System.Drawing.Size(590, 310);
			this.Controls.Add(this.btnAire);
			this.Controls.Add(this.btnTierra);
			this.Controls.Add(this.btnAgua);
			this.Controls.Add(this.btnFuego);
			this.Controls.Add(this.lblCartas);
			this.Controls.Add(this.btnAvanzar);
			this.Controls.Add(this.lstHistorial);
			this.Controls.Add(this.lblVida);
			this.Controls.Add(this.lblTurno);
			this.Controls.Add(this.lblEscenario);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.Name = "FormPartida";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Arena Elemental - Partida";
			this.ResumeLayout(false);
		}
	}
}