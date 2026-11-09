namespace proyecto_final_iujo.Visuales
{
	partial class FormFinCombate
	{
		private System.ComponentModel.IContainer components = null;
		private System.Windows.Forms.Button btnContinuar;

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
			this.btnContinuar = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// btnContinuar
			// 
			this.btnContinuar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(30)))));
			this.btnContinuar.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btnContinuar.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(215)))), ((int)(((byte)(0)))));
			this.btnContinuar.FlatAppearance.BorderSize = 2;
			this.btnContinuar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnContinuar.Font = new System.Drawing.Font("Impact", 14F);
			this.btnContinuar.ForeColor = System.Drawing.Color.White;
			this.btnContinuar.Location = new System.Drawing.Point(370, 455);
			this.btnContinuar.Name = "btnContinuar";
			this.btnContinuar.Size = new System.Drawing.Size(220, 45);
			this.btnContinuar.TabIndex = 0;
			this.btnContinuar.Text = "CONTINUAR ▶";
			this.btnContinuar.UseVisualStyleBackColor = false;
			this.btnContinuar.Visible = false;
			this.btnContinuar.Click += new System.EventHandler(this.BtnContinuarClick);
			// 
			// FormFinCombate
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.Black;
			this.ClientSize = new System.Drawing.Size(960, 540);
			this.Controls.Add(this.btnContinuar);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormFinCombate";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Arena Elemental - Resultado";
			this.ResumeLayout(false);
		}
	}
}