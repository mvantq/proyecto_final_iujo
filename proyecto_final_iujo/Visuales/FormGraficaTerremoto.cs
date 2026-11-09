using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace proyecto_final_iujo.Visuales
{
	public class FormGraficaTerremoto : Form
	{
		public FormGraficaTerremoto()
		{
			this.Text = "Alerta Sísmica - Análisis de Atenuación";
			this.Size = new Size(720, 480);
			this.StartPosition = FormStartPosition.CenterParent;
			this.FormBorderStyle = FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.BackColor = Color.FromArgb(15, 18, 24);
			this.Paint += new PaintEventHandler(FormGraficaTerremoto_Paint);
		}

		private void FormGraficaTerremoto_Paint(object sender, PaintEventArgs e)
		{
			Graphics g = e.Graphics;
			g.SmoothingMode = SmoothingMode.AntiAlias;

			Pen penEjes = new Pen(Color.Gray, 2);
			Pen penAsintota = new Pen(Color.Yellow, 1) { DashStyle = DashStyle.Dash };
			Pen penCurva = new Pen(Color.Crimson, 3);

			Font fontTitulo = new Font("Arial", 11, FontStyle.Bold);
			Font fontSub = new Font("Arial", 9, FontStyle.Italic);
			Font fontTexto = new Font("Arial", 8, FontStyle.Regular);

			int origenX = 80;
			int origenY = 250;

			// Ejes Cartesianos
			g.DrawLine(penEjes, origenX, 50, origenX, 380);       // Eje Y: Intensidad
			g.DrawLine(penEjes, origenX, origenY, 660, origenY);  // Eje X: Distancia (km)

			// Interfaz de juego
			g.DrawString("MONITOR SÍSMICO: PROPAGACIÓN Y ATENUACIÓN DE ONDA", fontTitulo, Brushes.Gold, 120, 15);
			g.DrawString("Función de Onda: f(d) = A · e^(-k·d) · sin(ω·d)", fontSub, Brushes.Cyan, 180, 40);
			g.DrawString("Asíntota (y = 0)", fontTexto, Brushes.Yellow, 530, 235);

			// Línea de asíntota horizontal
			g.DrawLine(penAsintota, origenX, origenY, 660, origenY);

			// Trazado de la onda decreciente
			PointF[] puntos = new PointF[500];
			for (int i = 0; i < puntos.Length; i++)
			{
				float d = i * 0.022f;
				float y = (float)(130.0 * Math.Exp(-0.42 * d) * Math.Sin(3.0 * d));

				float px = origenX + (i * 1.15f);
				float py = origenY - y;
				puntos[i] = new PointF(px, py);
			}

			g.DrawLines(penCurva, puntos);

			// Etiquetas
			g.DrawString("Intensidad (Y)", fontTexto, Brushes.White, 5, 50);
			g.DrawString("Distancia Epicentro (km)", fontTexto, Brushes.White, 500, 260);
		}
	}
}