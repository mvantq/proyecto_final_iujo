
namespace proyecto_final_iujo
{
	partial class MainForm
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		
		/// <summary>
		/// Disposes resources used by the form.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent()
{
			this.lblTitulo = new System.Windows.Forms.Label();
			this.btnJugar = new System.Windows.Forms.Label();
			this.btnCambiarIdioma = new System.Windows.Forms.Label();
			this.btnCargarMazo = new System.Windows.Forms.Label();
			this.btnSalir = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// lblTitulo
			// 
			this.lblTitulo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(15)))), ((int)(((byte)(20)))), ((int)(((byte)(15)))));
			this.lblTitulo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.lblTitulo.Font = new System.Drawing.Font("Impact", 28F, System.Drawing.FontStyle.Bold);
			this.lblTitulo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(215)))), ((int)(((byte)(60)))));
			this.lblTitulo.Location = new System.Drawing.Point(170, 70);
			this.lblTitulo.Name = "lblTitulo";
			this.lblTitulo.Size = new System.Drawing.Size(460, 55);
			this.lblTitulo.TabIndex = 4;
			this.lblTitulo.Text = "ARENA ELEMENTAL";
			this.lblTitulo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// btnJugar
			// 
			this.btnJugar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(190)))), ((int)(((byte)(20)))), ((int)(((byte)(25)))), ((int)(((byte)(20)))));
			this.btnJugar.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.btnJugar.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btnJugar.Font = new System.Drawing.Font("Impact", 15F);
			this.btnJugar.ForeColor = System.Drawing.Color.White;
			this.btnJugar.Location = new System.Drawing.Point(270, 160);
			this.btnJugar.Name = "btnJugar";
			this.btnJugar.Size = new System.Drawing.Size(260, 38);
			this.btnJugar.TabIndex = 3;
			this.btnJugar.Text = "JUGAR";
			this.btnJugar.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// btnCambiarIdioma
			// 
			this.btnCambiarIdioma.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(190)))), ((int)(((byte)(20)))), ((int)(((byte)(25)))), ((int)(((byte)(20)))));
			this.btnCambiarIdioma.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.btnCambiarIdioma.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btnCambiarIdioma.Font = new System.Drawing.Font("Impact", 15F);
			this.btnCambiarIdioma.ForeColor = System.Drawing.Color.White;
			this.btnCambiarIdioma.Location = new System.Drawing.Point(270, 210);
			this.btnCambiarIdioma.Name = "btnCambiarIdioma";
			this.btnCambiarIdioma.Size = new System.Drawing.Size(260, 38);
			this.btnCambiarIdioma.TabIndex = 2;
			this.btnCambiarIdioma.Text = "CAMBIAR IDIOMA";
			this.btnCambiarIdioma.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// btnCargarMazo
			// 
			this.btnCargarMazo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(190)))), ((int)(((byte)(20)))), ((int)(((byte)(25)))), ((int)(((byte)(20)))));
			this.btnCargarMazo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.btnCargarMazo.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btnCargarMazo.Font = new System.Drawing.Font("Impact", 15F);
			this.btnCargarMazo.ForeColor = System.Drawing.Color.White;
			this.btnCargarMazo.Location = new System.Drawing.Point(270, 260);
			this.btnCargarMazo.Name = "btnCargarMazo";
			this.btnCargarMazo.Size = new System.Drawing.Size(260, 38);
			this.btnCargarMazo.TabIndex = 1;
			this.btnCargarMazo.Text = "CARGAR MAZO";
			this.btnCargarMazo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// btnSalir
			// 
			this.btnSalir.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(190)))), ((int)(((byte)(20)))), ((int)(((byte)(25)))), ((int)(((byte)(20)))));
			this.btnSalir.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.btnSalir.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btnSalir.Font = new System.Drawing.Font("Impact", 15F);
			this.btnSalir.ForeColor = System.Drawing.Color.White;
			this.btnSalir.Location = new System.Drawing.Point(270, 310);
			this.btnSalir.Name = "btnSalir";
			this.btnSalir.Size = new System.Drawing.Size(260, 38);
			this.btnSalir.TabIndex = 0;
			this.btnSalir.Text = "SALIR";
			this.btnSalir.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// MainForm
			// 
			this.ClientSize = new System.Drawing.Size(800, 450);
			this.Controls.Add(this.btnSalir);
			this.Controls.Add(this.btnCargarMazo);
			this.Controls.Add(this.btnCambiarIdioma);
			this.Controls.Add(this.btnJugar);
			this.Controls.Add(this.lblTitulo);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.Name = "MainForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Arena Elemental - Menu Principal";
			this.Load += new System.EventHandler(this.MainFormLoad);
			this.ResumeLayout(false);
}

		
		void MainFormLoad(object sender, System.EventArgs e)
		{
			
		}
	}
	}
