namespace proyecto_final_iujo.Visuales
{
	partial class MainForm
	{
		private System.ComponentModel.IContainer components = null;
		private System.Windows.Forms.PictureBox picFondo;
		private System.Windows.Forms.Label lblTitulo;
		private System.Windows.Forms.Label lblJugar;
		private System.Windows.Forms.Label lblIdioma;
		private System.Windows.Forms.Label lblSalir;

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
			this.lblJugar = new System.Windows.Forms.Label();
			this.lblIdioma = new System.Windows.Forms.Label();
			this.lblSalir = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.picFondo)).BeginInit();
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
			this.lblTitulo.Font = new System.Drawing.Font("Impact", 34F, System.Drawing.FontStyle.Regular);
			this.lblTitulo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(215)))), ((int)(((byte)(0)))));
			this.lblTitulo.Location = new System.Drawing.Point(125, 70);
			this.lblTitulo.Name = "lblTitulo";
			this.lblTitulo.Size = new System.Drawing.Size(600, 70);
			this.lblTitulo.TabIndex = 1;
			this.lblTitulo.Text = "ARENA ELEMENTAL";
			this.lblTitulo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// lblJugar
			// 
			this.lblJugar.BackColor = System.Drawing.Color.Transparent;
			this.lblJugar.Cursor = System.Windows.Forms.Cursors.Hand;
			this.lblJugar.Font = new System.Drawing.Font("Impact", 20F);
			this.lblJugar.ForeColor = System.Drawing.Color.White;
			this.lblJugar.Location = new System.Drawing.Point(225, 185);
			this.lblJugar.Name = "lblJugar";
			this.lblJugar.Size = new System.Drawing.Size(400, 45);
			this.lblJugar.TabIndex = 2;
			this.lblJugar.Text = "JUGAR";
			this.lblJugar.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// lblIdioma
			// 
			this.lblIdioma.BackColor = System.Drawing.Color.Transparent;
			this.lblIdioma.Cursor = System.Windows.Forms.Cursors.Hand;
			this.lblIdioma.Font = new System.Drawing.Font("Impact", 20F);
			this.lblIdioma.ForeColor = System.Drawing.Color.White;
			this.lblIdioma.Location = new System.Drawing.Point(225, 245);
			this.lblIdioma.Name = "lblIdioma";
			this.lblIdioma.Size = new System.Drawing.Size(400, 45);
			this.lblIdioma.TabIndex = 3;
			this.lblIdioma.Text = "CAMBIAR IDIOMA [ES]";
			this.lblIdioma.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// lblSalir
			// 
			this.lblSalir.BackColor = System.Drawing.Color.Transparent;
			this.lblSalir.Cursor = System.Windows.Forms.Cursors.Hand;
			this.lblSalir.Font = new System.Drawing.Font("Impact", 20F);
			this.lblSalir.ForeColor = System.Drawing.Color.White;
			this.lblSalir.Location = new System.Drawing.Point(225, 305);
			this.lblSalir.Name = "lblSalir";
			this.lblSalir.Size = new System.Drawing.Size(400, 45);
			this.lblSalir.TabIndex = 4;
			this.lblSalir.Text = "SALIR";
			this.lblSalir.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(850, 480);
			this.Controls.Add(this.lblSalir);
			this.Controls.Add(this.lblIdioma);
			this.Controls.Add(this.lblJugar);
			this.Controls.Add(this.lblTitulo);
			this.Controls.Add(this.picFondo);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.Name = "MainForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Arena Elemental - Menu Principal";
			((System.ComponentModel.ISupportInitialize)(this.picFondo)).EndInit();
			this.ResumeLayout(false);
		}
	}
}