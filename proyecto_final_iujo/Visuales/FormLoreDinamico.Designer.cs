namespace proyecto_final_iujo.Visuales
{
	partial class FormLoreDinamico
	{
		private System.ComponentModel.IContainer components = null;
		private System.Windows.Forms.PictureBox picFondo;
		private System.Windows.Forms.Label lblTitulo;
		private System.Windows.Forms.Panel pnlFondoTexto;
		private System.Windows.Forms.Label lblCapitulo;
		private System.Windows.Forms.RichTextBox rtbLoreJuego;
		private System.Windows.Forms.Button btnAnterior;
		private System.Windows.Forms.Button btnSiguiente;
		private System.Windows.Forms.Button btnVolver;

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
			this.picFondo = new System.Windows.Forms.PictureBox();
			this.lblTitulo = new System.Windows.Forms.Label();
			this.pnlFondoTexto = new System.Windows.Forms.Panel();
			this.lblCapitulo = new System.Windows.Forms.Label();
			this.rtbLoreJuego = new System.Windows.Forms.RichTextBox();
			this.btnAnterior = new System.Windows.Forms.Button();
			this.btnSiguiente = new System.Windows.Forms.Button();
			this.btnVolver = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.picFondo)).BeginInit();
			this.pnlFondoTexto.SuspendLayout();
			this.SuspendLayout();
			// 
			// picFondo
			// 
			this.picFondo.Dock = System.Windows.Forms.DockStyle.Fill;
			this.picFondo.Location = new System.Drawing.Point(0, 0);
			this.picFondo.Name = "picFondo";
			this.picFondo.Size = new System.Drawing.Size(850, 480);
			this.picFondo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.picFondo.TabIndex = 0;
			this.picFondo.TabStop = false;
			// 
			// lblTitulo
			// 
			this.lblTitulo.BackColor = System.Drawing.Color.Transparent;
			this.lblTitulo.Font = new System.Drawing.Font("Impact", 24F, System.Drawing.FontStyle.Regular);
			this.lblTitulo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(215)))), ((int)(((byte)(0)))));
			this.lblTitulo.Location = new System.Drawing.Point(50, 18);
			this.lblTitulo.Name = "lblTitulo";
			this.lblTitulo.Size = new System.Drawing.Size(750, 45);
			this.lblTitulo.TabIndex = 1;
			this.lblTitulo.Text = "CRÓNICAS DE LA FRONTERA";
			this.lblTitulo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// pnlFondoTexto
			// 
			this.pnlFondoTexto.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(18)))), ((int)(((byte)(22)))), ((int)(((byte)(18)))));
			this.pnlFondoTexto.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.pnlFondoTexto.Controls.Add(this.rtbLoreJuego);
			this.pnlFondoTexto.Controls.Add(this.lblCapitulo);
			this.pnlFondoTexto.Location = new System.Drawing.Point(75, 75);
			this.pnlFondoTexto.Name = "pnlFondoTexto";
			this.pnlFondoTexto.Size = new System.Drawing.Size(700, 315);
			this.pnlFondoTexto.TabIndex = 2;
			// 
			// lblCapitulo
			// 
			this.lblCapitulo.BackColor = System.Drawing.Color.Transparent;
			this.lblCapitulo.Font = new System.Drawing.Font("Impact", 15F);
			this.lblCapitulo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(220)))), ((int)(((byte)(90)))));
			this.lblCapitulo.Location = new System.Drawing.Point(20, 15);
			this.lblCapitulo.Name = "lblCapitulo";
			this.lblCapitulo.Size = new System.Drawing.Size(658, 30);
			this.lblCapitulo.TabIndex = 0;
			this.lblCapitulo.Text = "I. EL PACTO Y LA FRONTERA CERRADA";
			// 
			// rtbLoreJuego
			// 
			this.rtbLoreJuego.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(22)))), ((int)(((byte)(18)))));
			this.rtbLoreJuego.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.rtbLoreJuego.Font = new System.Drawing.Font("Arial", 11F);
			this.rtbLoreJuego.ForeColor = System.Drawing.Color.Gainsboro;
			this.rtbLoreJuego.Location = new System.Drawing.Point(20, 55);
			this.rtbLoreJuego.Name = "rtbLoreJuego";
			this.rtbLoreJuego.ReadOnly = true;
			this.rtbLoreJuego.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
			this.rtbLoreJuego.Size = new System.Drawing.Size(658, 238);
			this.rtbLoreJuego.TabIndex = 1;
			this.rtbLoreJuego.Text = "";
			// 
			// btnAnterior
			// 
			this.btnAnterior.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(25)))), ((int)(((byte)(35)))), ((int)(((byte)(30)))));
			this.btnAnterior.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btnAnterior.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(80)))), ((int)(((byte)(70)))));
			this.btnAnterior.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnAnterior.Font = new System.Drawing.Font("Impact", 12F);
			this.btnAnterior.ForeColor = System.Drawing.Color.White;
			this.btnAnterior.Location = new System.Drawing.Point(75, 410);
			this.btnAnterior.Name = "btnAnterior";
			this.btnAnterior.Size = new System.Drawing.Size(150, 40);
			this.btnAnterior.TabIndex = 3;
			this.btnAnterior.Text = "◀ ANTERIOR";
			this.btnAnterior.UseVisualStyleBackColor = false;
			this.btnAnterior.Click += new System.EventHandler(this.BtnAnteriorClick);
			// 
			// btnVolver
			// 
			this.btnVolver.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(25)))), ((int)(((byte)(35)))), ((int)(((byte)(30)))));
			this.btnVolver.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btnVolver.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(80)))), ((int)(((byte)(70)))));
			this.btnVolver.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnVolver.Font = new System.Drawing.Font("Impact", 12F);
			this.btnVolver.ForeColor = System.Drawing.Color.White;
			this.btnVolver.Location = new System.Drawing.Point(350, 410);
			this.btnVolver.Name = "btnVolver";
			this.btnVolver.Size = new System.Drawing.Size(150, 40);
			this.btnVolver.TabIndex = 4;
			this.btnVolver.Text = "VOLVER";
			this.btnVolver.UseVisualStyleBackColor = false;
			this.btnVolver.Click += new System.EventHandler(this.BtnVolverClick);
			// 
			// btnSiguiente
			// 
			this.btnSiguiente.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(25)))), ((int)(((byte)(35)))), ((int)(((byte)(30)))));
			this.btnSiguiente.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btnSiguiente.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(80)))), ((int)(((byte)(70)))));
			this.btnSiguiente.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnSiguiente.Font = new System.Drawing.Font("Impact", 12F);
			this.btnSiguiente.ForeColor = System.Drawing.Color.White;
			this.btnSiguiente.Location = new System.Drawing.Point(625, 410);
			this.btnSiguiente.Name = "btnSiguiente";
			this.btnSiguiente.Size = new System.Drawing.Size(150, 40);
			this.btnSiguiente.TabIndex = 5;
			this.btnSiguiente.Text = "SIGUIENTE ▶";
			this.btnSiguiente.UseVisualStyleBackColor = false;
			this.btnSiguiente.Click += new System.EventHandler(this.BtnSiguienteClick);
			// 
			// FormLoreDinamico
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(850, 480);
			this.Controls.Add(this.btnSiguiente);
			this.Controls.Add(this.btnVolver);
			this.Controls.Add(this.btnAnterior);
			this.Controls.Add(this.pnlFondoTexto);
			this.Controls.Add(this.lblTitulo);
			this.Controls.Add(this.picFondo);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.Name = "FormLoreDinamico";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Arena Elemental - Lore Dinámico";
			((System.ComponentModel.ISupportInitialize)(this.picFondo)).EndInit();
			this.pnlFondoTexto.ResumeLayout(false);
			this.ResumeLayout(false);
		}
	}
}