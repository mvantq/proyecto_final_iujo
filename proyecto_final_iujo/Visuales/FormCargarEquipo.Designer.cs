namespace proyecto_final_iujo.Visuales
{
	partial class FormCargarEquipo
	{
		private System.ComponentModel.IContainer components = null;
		private System.Windows.Forms.PictureBox picFondo;
		private System.Windows.Forms.Label lblTitulo;
		private System.Windows.Forms.Panel pnlLista;
		private System.Windows.Forms.Label lblSubLista;
		private System.Windows.Forms.CheckedListBox clbPersonajes;
		private System.Windows.Forms.Panel pnlPreview;
		private System.Windows.Forms.PictureBox picHeroe;
		private System.Windows.Forms.Label lblNombreHeroe;
		private System.Windows.Forms.Label lblDetallesHeroe;
		private System.Windows.Forms.Button btnVolver;
		private System.Windows.Forms.Button btnConfirmar;

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
			this.pnlLista = new System.Windows.Forms.Panel();
			this.clbPersonajes = new System.Windows.Forms.CheckedListBox();
			this.lblSubLista = new System.Windows.Forms.Label();
			this.pnlPreview = new System.Windows.Forms.Panel();
			this.lblDetallesHeroe = new System.Windows.Forms.Label();
			this.lblNombreHeroe = new System.Windows.Forms.Label();
			this.picHeroe = new System.Windows.Forms.PictureBox();
			this.btnVolver = new System.Windows.Forms.Button();
			this.btnConfirmar = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.picFondo)).BeginInit();
			this.pnlLista.SuspendLayout();
			this.pnlPreview.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.picHeroe)).BeginInit();
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
			this.lblTitulo.Font = new System.Drawing.Font("Impact", 24F);
			this.lblTitulo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(215)))), ((int)(((byte)(0)))));
			this.lblTitulo.Location = new System.Drawing.Point(50, 18);
			this.lblTitulo.Name = "lblTitulo";
			this.lblTitulo.Size = new System.Drawing.Size(750, 45);
			this.lblTitulo.TabIndex = 1;
			this.lblTitulo.Text = "CONVOCATORIA DEL ESCUADRÓN ELEMENTAL";
			this.lblTitulo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// pnlLista
			// 
			this.pnlLista.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(18)))), ((int)(((byte)(24)))), ((int)(((byte)(18)))));
			this.pnlLista.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.pnlLista.Controls.Add(this.clbPersonajes);
			this.pnlLista.Controls.Add(this.lblSubLista);
			this.pnlLista.Location = new System.Drawing.Point(50, 75);
			this.pnlLista.Name = "pnlLista";
			this.pnlLista.Size = new System.Drawing.Size(420, 320);
			this.pnlLista.TabIndex = 2;
			// 
			// clbPersonajes
			// 
			this.clbPersonajes.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(20)))), ((int)(((byte)(16)))));
			this.clbPersonajes.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.clbPersonajes.CheckOnClick = true;
			this.clbPersonajes.Cursor = System.Windows.Forms.Cursors.Hand;
			this.clbPersonajes.Font = new System.Drawing.Font("Arial", 10.5F, System.Drawing.FontStyle.Bold);
			this.clbPersonajes.ForeColor = System.Drawing.Color.White;
			this.clbPersonajes.FormattingEnabled = true;
			this.clbPersonajes.Location = new System.Drawing.Point(15, 45);
			this.clbPersonajes.Name = "clbPersonajes";
			this.clbPersonajes.Size = new System.Drawing.Size(388, 249);
			this.clbPersonajes.TabIndex = 1;
			// 
			// lblSubLista
			// 
			this.lblSubLista.Font = new System.Drawing.Font("Impact", 13F);
			this.lblSubLista.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(220)))), ((int)(((byte)(90)))));
			this.lblSubLista.Location = new System.Drawing.Point(15, 12);
			this.lblSubLista.Name = "lblSubLista";
			this.lblSubLista.Size = new System.Drawing.Size(390, 25);
			this.lblSubLista.TabIndex = 0;
			this.lblSubLista.Text = "HÉROES DISPONIBLES (MARCA PARA SELECCIONAR)";
			// 
			// pnlPreview
			// 
			this.pnlPreview.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(18)))), ((int)(((byte)(24)))), ((int)(((byte)(18)))));
			this.pnlPreview.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.pnlPreview.Controls.Add(this.lblDetallesHeroe);
			this.pnlPreview.Controls.Add(this.lblNombreHeroe);
			this.pnlPreview.Controls.Add(this.picHeroe);
			this.pnlPreview.Location = new System.Drawing.Point(490, 75);
			this.pnlPreview.Name = "pnlPreview";
			this.pnlPreview.Size = new System.Drawing.Size(310, 320);
			this.pnlPreview.TabIndex = 3;
			// 
			// lblDetallesHeroe
			// 
			this.lblDetallesHeroe.Font = new System.Drawing.Font("Arial", 9.5F);
			this.lblDetallesHeroe.ForeColor = System.Drawing.Color.Gainsboro;
			this.lblDetallesHeroe.Location = new System.Drawing.Point(15, 228);
			this.lblDetallesHeroe.Name = "lblDetallesHeroe";
			this.lblDetallesHeroe.Size = new System.Drawing.Size(280, 80);
			this.lblDetallesHeroe.TabIndex = 2;
			this.lblDetallesHeroe.Text = "Pasa el mouse sobre la lista para ver su arquetipo mitológico y afinidad elementa" +
			"l.";
			this.lblDetallesHeroe.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// lblNombreHeroe
			// 
			this.lblNombreHeroe.Font = new System.Drawing.Font("Impact", 14F);
			this.lblNombreHeroe.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(220)))), ((int)(((byte)(90)))));
			this.lblNombreHeroe.Location = new System.Drawing.Point(15, 202);
			this.lblNombreHeroe.Name = "lblNombreHeroe";
			this.lblNombreHeroe.Size = new System.Drawing.Size(280, 24);
			this.lblNombreHeroe.TabIndex = 1;
			this.lblNombreHeroe.Text = "SELECCIONA UN HÉROE";
			this.lblNombreHeroe.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// picHeroe
			// 
			this.picHeroe.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(16)))), ((int)(((byte)(12)))));
			this.picHeroe.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.picHeroe.Location = new System.Drawing.Point(55, 15);
			this.picHeroe.Name = "picHeroe";
			this.picHeroe.Size = new System.Drawing.Size(200, 180);
			this.picHeroe.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.picHeroe.TabIndex = 0;
			this.picHeroe.TabStop = false;
			// 
			// btnVolver
			// 
			this.btnVolver.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(25)))), ((int)(((byte)(35)))), ((int)(((byte)(30)))));
			this.btnVolver.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btnVolver.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(80)))), ((int)(((byte)(70)))));
			this.btnVolver.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnVolver.Font = new System.Drawing.Font("Impact", 12F);
			this.btnVolver.ForeColor = System.Drawing.Color.White;
			this.btnVolver.Location = new System.Drawing.Point(170, 412);
			this.btnVolver.Name = "btnVolver";
			this.btnVolver.Size = new System.Drawing.Size(180, 40);
			this.btnVolver.TabIndex = 4;
			this.btnVolver.Text = "VOLVER AL MENÚ";
			this.btnVolver.UseVisualStyleBackColor = false;
			this.btnVolver.Click += new System.EventHandler(this.BtnVolverClick);
			// 
			// btnConfirmar
			// 
			this.btnConfirmar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(25)))), ((int)(((byte)(35)))), ((int)(((byte)(30)))));
			this.btnConfirmar.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btnConfirmar.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(80)))), ((int)(((byte)(70)))));
			this.btnConfirmar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnConfirmar.Font = new System.Drawing.Font("Impact", 13F);
			this.btnConfirmar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(220)))), ((int)(((byte)(90)))));
			this.btnConfirmar.Location = new System.Drawing.Point(500, 412);
			this.btnConfirmar.Name = "btnConfirmar";
			this.btnConfirmar.Size = new System.Drawing.Size(220, 40);
			this.btnConfirmar.TabIndex = 5;
			this.btnConfirmar.Text = "ENTRAR A LA ARENA ▶";
			this.btnConfirmar.UseVisualStyleBackColor = false;
			this.btnConfirmar.Click += new System.EventHandler(this.BtnConfirmarClick);
			// 
			// FormCargarEquipo
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(850, 480);
			this.Controls.Add(this.btnConfirmar);
			this.Controls.Add(this.btnVolver);
			this.Controls.Add(this.pnlPreview);
			this.Controls.Add(this.pnlLista);
			this.Controls.Add(this.lblTitulo);
			this.Controls.Add(this.picFondo);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.Name = "FormCargarEquipo";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Arena Elemental - Selección de Equipo";
			((System.ComponentModel.ISupportInitialize)(this.picFondo)).EndInit();
			this.pnlLista.ResumeLayout(false);
			this.pnlPreview.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.picHeroe)).EndInit();
			this.ResumeLayout(false);
		}
	}
}