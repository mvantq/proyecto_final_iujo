namespace proyecto_final_iujo.Visuales
{
	partial class FormPartida
	{
		private System.ComponentModel.IContainer components = null;
		private System.Windows.Forms.PictureBox picEscenario;
		private System.Windows.Forms.Panel pnlHUD;
		private System.Windows.Forms.ProgressBar pbHpJugador;
		private System.Windows.Forms.ProgressBar pbHpRival;
		private System.Windows.Forms.Label lblNombreJugador;
		private System.Windows.Forms.Label lblNombreRival;
		private System.Windows.Forms.Label lblEnergiaJugador;
		private System.Windows.Forms.Panel pnlAcciones;
		private System.Windows.Forms.Button btnAtacar;
		private System.Windows.Forms.Button btnDefender;
		private System.Windows.Forms.Button btnEspecial;
		private System.Windows.Forms.Button btnRelevo;
		private System.Windows.Forms.Label lblLogTurno;

		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void InitializeComponent()
		{
			this.picEscenario = new System.Windows.Forms.PictureBox();
			this.pnlHUD = new System.Windows.Forms.Panel();
			this.lblEnergiaJugador = new System.Windows.Forms.Label();
			this.lblNombreRival = new System.Windows.Forms.Label();
			this.lblNombreJugador = new System.Windows.Forms.Label();
			this.pbHpRival = new System.Windows.Forms.ProgressBar();
			this.pbHpJugador = new System.Windows.Forms.ProgressBar();
			this.pnlAcciones = new System.Windows.Forms.Panel();
			this.lblLogTurno = new System.Windows.Forms.Label();
			this.btnRelevo = new System.Windows.Forms.Button();
			this.btnEspecial = new System.Windows.Forms.Button();
			this.btnDefender = new System.Windows.Forms.Button();
			this.btnAtacar = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.picEscenario)).BeginInit();
			this.pnlHUD.SuspendLayout();
			this.pnlAcciones.SuspendLayout();
			this.SuspendLayout();
			// 
			// picEscenario
			// 
			this.picEscenario.Dock = System.Windows.Forms.DockStyle.Fill;
			this.picEscenario.Location = new System.Drawing.Point(0, 0);
			this.picEscenario.Name = "picEscenario";
			this.picEscenario.Size = new System.Drawing.Size(960, 540);
			this.picEscenario.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.picEscenario.TabIndex = 0;
			this.picEscenario.TabStop = false;
			// 
			// pnlHUD
			// 
			this.pnlHUD.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(15)))), ((int)(((byte)(18)))), ((int)(((byte)(15)))));
			this.pnlHUD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.pnlHUD.Controls.Add(this.lblEnergiaJugador);
			this.pnlHUD.Controls.Add(this.lblNombreRival);
			this.pnlHUD.Controls.Add(this.lblNombreJugador);
			this.pnlHUD.Controls.Add(this.pbHpRival);
			this.pnlHUD.Controls.Add(this.pbHpJugador);
			this.pnlHUD.Location = new System.Drawing.Point(50, 15);
			this.pnlHUD.Name = "pnlHUD";
			this.pnlHUD.Size = new System.Drawing.Size(860, 65);
			this.pnlHUD.TabIndex = 1;
			// 
			// lblEnergiaJugador
			// 
			this.lblEnergiaJugador.Font = new System.Drawing.Font("Impact", 11F);
			this.lblEnergiaJugador.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(215)))), ((int)(((byte)(0)))));
			this.lblEnergiaJugador.Location = new System.Drawing.Point(340, 10);
			this.lblEnergiaJugador.Name = "lblEnergiaJugador";
			this.lblEnergiaJugador.Size = new System.Drawing.Size(180, 42);
			this.lblEnergiaJugador.TabIndex = 4;
			this.lblEnergiaJugador.Text = "ENERGÍA: 2\nTURNO 1";
			this.lblEnergiaJugador.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// lblNombreRival
			// 
			this.lblNombreRival.Font = new System.Drawing.Font("Impact", 12F);
			this.lblNombreRival.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(140)))), ((int)(((byte)(200)))), ((int)(((byte)(255)))));
			this.lblNombreRival.Location = new System.Drawing.Point(540, 10);
			this.lblNombreRival.Name = "lblNombreRival";
			this.lblNombreRival.Size = new System.Drawing.Size(300, 20);
			this.lblNombreRival.TabIndex = 3;
			this.lblNombreRival.Text = "RIVAL";
			this.lblNombreRival.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// lblNombreJugador
			// 
			this.lblNombreJugador.Font = new System.Drawing.Font("Impact", 12F);
			this.lblNombreJugador.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(160)))), ((int)(((byte)(90)))));
			this.lblNombreJugador.Location = new System.Drawing.Point(20, 10);
			this.lblNombreJugador.Name = "lblNombreJugador";
			this.lblNombreJugador.Size = new System.Drawing.Size(300, 20);
			this.lblNombreJugador.TabIndex = 2;
			this.lblNombreJugador.Text = "JUGADOR";
			// 
			// pbHpRival
			// 
			this.pbHpRival.Location = new System.Drawing.Point(540, 34);
			this.pbHpRival.Name = "pbHpRival";
			this.pbHpRival.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			this.pbHpRival.RightToLeftLayout = true;
			this.pbHpRival.Size = new System.Drawing.Size(300, 18);
			this.pbHpRival.TabIndex = 1;
			this.pbHpRival.Value = 100;
			// 
			// pbHpJugador
			// 
			this.pbHpJugador.Location = new System.Drawing.Point(20, 34);
			this.pbHpJugador.Name = "pbHpJugador";
			this.pbHpJugador.Size = new System.Drawing.Size(300, 18);
			this.pbHpJugador.TabIndex = 0;
			this.pbHpJugador.Value = 100;
			// 
			// pnlAcciones
			// 
			this.pnlAcciones.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(15)))), ((int)(((byte)(18)))), ((int)(((byte)(15)))));
			this.pnlAcciones.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.pnlAcciones.Controls.Add(this.lblLogTurno);
			this.pnlAcciones.Controls.Add(this.btnRelevo);
			this.pnlAcciones.Controls.Add(this.btnEspecial);
			this.pnlAcciones.Controls.Add(this.btnDefender);
			this.pnlAcciones.Controls.Add(this.btnAtacar);
			this.pnlAcciones.Location = new System.Drawing.Point(50, 430);
			this.pnlAcciones.Name = "pnlAcciones";
			this.pnlAcciones.Size = new System.Drawing.Size(860, 90);
			this.pnlAcciones.TabIndex = 2;
			// 
			// lblLogTurno
			// 
			this.lblLogTurno.Font = new System.Drawing.Font("Arial", 9.5F, System.Drawing.FontStyle.Bold);
			this.lblLogTurno.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(220)))), ((int)(((byte)(90)))));
			this.lblLogTurno.Location = new System.Drawing.Point(15, 8);
			this.lblLogTurno.Name = "lblLogTurno";
			this.lblLogTurno.Size = new System.Drawing.Size(830, 20);
			this.lblLogTurno.TabIndex = 4;
			this.lblLogTurno.Text = "¡COMIENZA EL COMBATE!";
			this.lblLogTurno.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// btnRelevo
			// 
			this.btnRelevo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(35)))), ((int)(((byte)(30)))));
			this.btnRelevo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnRelevo.Font = new System.Drawing.Font("Impact", 11F);
			this.btnRelevo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(220)))), ((int)(((byte)(90)))));
			this.btnRelevo.Location = new System.Drawing.Point(650, 36);
			this.btnRelevo.Name = "btnRelevo";
			this.btnRelevo.Size = new System.Drawing.Size(180, 38);
			this.btnRelevo.TabIndex = 3;
			this.btnRelevo.Text = "RELEVO / PASAR ▶";
			this.btnRelevo.UseVisualStyleBackColor = false;
			this.btnRelevo.Click += new System.EventHandler(this.BtnRelevoClick);
			// 
			// btnEspecial
			// 
			this.btnEspecial.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(25)))), ((int)(((byte)(75)))));
			this.btnEspecial.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnEspecial.Font = new System.Drawing.Font("Impact", 11F);
			this.btnEspecial.ForeColor = System.Drawing.Color.White;
			this.btnEspecial.Location = new System.Drawing.Point(440, 36);
			this.btnEspecial.Name = "btnEspecial";
			this.btnEspecial.Size = new System.Drawing.Size(180, 38);
			this.btnEspecial.TabIndex = 2;
			this.btnEspecial.Text = "ESPECIAL (2 ENER)";
			this.btnEspecial.UseVisualStyleBackColor = false;
			this.btnEspecial.Click += new System.EventHandler(this.BtnEspecialClick);
			// 
			// btnDefender
			// 
			this.btnDefender.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(50)))), ((int)(((byte)(80)))));
			this.btnDefender.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnDefender.Font = new System.Drawing.Font("Impact", 11F);
			this.btnDefender.ForeColor = System.Drawing.Color.White;
			this.btnDefender.Location = new System.Drawing.Point(230, 36);
			this.btnDefender.Name = "btnDefender";
			this.btnDefender.Size = new System.Drawing.Size(180, 38);
			this.btnDefender.TabIndex = 1;
			this.btnDefender.Text = "DEFENDER (1 ENER)";
			this.btnDefender.UseVisualStyleBackColor = false;
			this.btnDefender.Click += new System.EventHandler(this.BtnDefenderClick);
			// 
			// btnAtacar
			// 
			this.btnAtacar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(95)))), ((int)(((byte)(25)))), ((int)(((byte)(20)))));
			this.btnAtacar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnAtacar.Font = new System.Drawing.Font("Impact", 11F);
			this.btnAtacar.ForeColor = System.Drawing.Color.White;
			this.btnAtacar.Location = new System.Drawing.Point(20, 36);
			this.btnAtacar.Name = "btnAtacar";
			this.btnAtacar.Size = new System.Drawing.Size(180, 38);
			this.btnAtacar.TabIndex = 0;
			this.btnAtacar.Text = "ATACAR (2 ENER)";
			this.btnAtacar.UseVisualStyleBackColor = false;
			this.btnAtacar.Click += new System.EventHandler(this.BtnAtacarClick);
			// 
			// FormPartida
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(960, 540);
			this.Controls.Add(this.pnlAcciones);
			this.Controls.Add(this.pnlHUD);
			this.Controls.Add(this.picEscenario);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.Name = "FormPartida";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Arena Elemental - Batalla en la Aldea";
			((System.ComponentModel.ISupportInitialize)(this.picEscenario)).EndInit();
			this.pnlHUD.ResumeLayout(false);
			this.pnlAcciones.ResumeLayout(false);
			this.ResumeLayout(false);
		}
	}
}