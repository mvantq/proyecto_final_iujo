using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Windows.Forms;

namespace proyecto_final_iujo.Visuales
{
	public partial class FormFinCombate : Form
	{
		private bool esVictoria;
		private string heroePrincipal;
		private int turnosTotales;
		private Timer timerAnimacion;
		private float escalaAparicion = 0.1f;
		private Image imgFondoCaptura = null;

		public FormFinCombate(bool esVictoria, string heroe, int turnos)
		{
			InitializeComponent();
			this.esVictoria = esVictoria;
			this.heroePrincipal = string.IsNullOrEmpty(heroe) ? "Héroe" : heroe;
			this.turnosTotales = turnos;

			this.DoubleBuffered = true;
			this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer, true);

			CargarFondo();
			IniciarAnimacionImpacto();
		}

		private void CargarFondo()
		{
			string[] posibles = new string[] {
				Path.Combine(Application.StartupPath, "Gemini_Generated_Image.jpeg"),
				Path.Combine(Application.StartupPath, "aldea.png"),
				Path.Combine(Application.StartupPath, "Visuales", "aldea.png"),
				Path.Combine(Application.StartupPath, "fondo.jpg"),
				Path.GetFullPath(Path.Combine(Application.StartupPath, @"..\..\fondo.jpg"))
			};

			foreach (string r in posibles)
			{
				if (File.Exists(r))
				{
					using (FileStream fs = new FileStream(r, FileMode.Open, FileAccess.Read))
					{
						imgFondoCaptura = new Bitmap(Image.FromStream(fs));
					}
					break;
				}
			}
		}

		private void IniciarAnimacionImpacto()
		{
			timerAnimacion = new Timer();
			timerAnimacion.Interval = 15;
			timerAnimacion.Tick += (s, e) => {
				if (escalaAparicion < 1.0f)
				{
					escalaAparicion += 0.08f;
					if (escalaAparicion >= 1.0f)
					{
						escalaAparicion = 1.0f;
						timerAnimacion.Stop();
						btnContinuar.Visible = true;
					}
					this.Invalidate();
				}
			};
			timerAnimacion.Start();
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
			Graphics g = e.Graphics;
			g.SmoothingMode = SmoothingMode.AntiAlias;
			g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;

			int w = this.ClientSize.Width;
			int h = this.ClientSize.Height;

			if (imgFondoCaptura != null)
			{
				g.DrawImage(imgFondoCaptura, 0, 0, w, h);
			}
			else
			{
				using (SolidBrush b = new SolidBrush(Color.FromArgb(20, 20, 24)))
					g.FillRectangle(b, 0, 0, w, h);
			}

			Color tinteBase = esVictoria ? Color.FromArgb(160, 45, 15, 65) : Color.FromArgb(190, 40, 10, 10);
			using (SolidBrush bTinte = new SolidBrush(tinteBase))
			{
				g.FillRectangle(bTinte, 0, 0, w, h);
			}

			using (GraphicsPath pathResplandor = new GraphicsPath())
			{
				pathResplandor.AddEllipse(w / 4, h / 4, w / 2, h / 2);
				using (PathGradientBrush pgb = new PathGradientBrush(pathResplandor))
				{
					pgb.CenterColor = esVictoria ? Color.FromArgb(200, 255, 230, 120) : Color.FromArgb(200, 255, 60, 60);
					pgb.SurroundColors = new Color[] { Color.Transparent };
					g.FillRectangle(pgb, 0, 0, w, h);
				}
			}

			Color colorBandaFrente = esVictoria ? Color.FromArgb(255, 235, 59) : Color.FromArgb(215, 30, 40);
			Color colorBandaBorde = esVictoria ? Color.FromArgb(255, 179, 0) : Color.FromArgb(130, 10, 20);

			int centerY = h / 2;
			float factor = escalaAparicion;

			PointF[] puntosFranja = new PointF[] {
				new PointF(-20, centerY + (55 * factor)),
				new PointF(w * 0.35f, centerY + (15 * factor)),
				new PointF(w * 0.38f, centerY + (35 * factor)),
				new PointF(w + 30, centerY - (60 * factor)),
				new PointF(w + 30, centerY - (125 * factor)),
				new PointF(w * 0.62f, centerY - (45 * factor)),
				new PointF(w * 0.58f, centerY - (70 * factor)),
				new PointF(-20, centerY - (25 * factor))
			};

			using (Matrix mSombra = new Matrix())
			{
				mSombra.Translate(0, 10);
				using (GraphicsPath pathSombra = new GraphicsPath())
				{
					pathSombra.AddPolygon(puntosFranja);
					pathSombra.Transform(mSombra);
					using (SolidBrush bSombraRayo = new SolidBrush(Color.FromArgb(120, 0, 0, 0)))
					{
						g.FillPath(bSombraRayo, pathSombra);
					}
				}
			}

			using (GraphicsPath pathRayo = new GraphicsPath())
			{
				pathRayo.AddPolygon(puntosFranja);
				using (LinearGradientBrush lgb = new LinearGradientBrush(
					new Point(0, centerY - 80), new Point(w, centerY + 80),
					colorBandaFrente, esVictoria ? Color.FromArgb(255, 248, 170) : Color.FromArgb(255, 90, 80)))
				{
					g.FillPath(lgb, pathRayo);
				}

				using (Pen penBorde = new Pen(colorBandaBorde, 4))
				{
					g.DrawPath(penBorde, pathRayo);
				}
			}

			string textoBanner = esVictoria ? "VICTORIA" : "DERROTA";
			using (Font fuenteBanner = new Font("Impact", 68f * factor, FontStyle.Italic, GraphicsUnit.Pixel))
			{
				SizeF tam = g.MeasureString(textoBanner, fuenteBanner);
				float posX = (w - tam.Width) / 2f;
				float posY = centerY - (tam.Height / 2f) - 15;

				GraphicsState estado = g.Save();
				g.TranslateTransform(w / 2f, centerY - 15);
				g.RotateTransform(-5f);
				g.TranslateTransform(-w / 2f, -(centerY - 15));

				for (int i = 8; i >= 1; i--)
				{
					Color colSombra3D = esVictoria ? Color.FromArgb(180, 50, 20) : Color.FromArgb(60, 0, 0);
					using (SolidBrush b3D = new SolidBrush(colSombra3D))
					{
						g.DrawString(textoBanner, fuenteBanner, b3D, posX + (i * 1.5f), posY + (i * 2.2f));
					}
				}

				using (GraphicsPath pathTexto = new GraphicsPath())
				{
					pathTexto.AddString(textoBanner, fuenteBanner.FontFamily, (int)fuenteBanner.Style, fuenteBanner.Size,
						new PointF(posX, posY), StringFormat.GenericDefault);

					using (Pen penTexto = new Pen(Color.FromArgb(25, 25, 25), 8))
					{
						penTexto.LineJoin = LineJoin.Round;
						g.DrawPath(penTexto, pathTexto);
					}

					Color c1 = Color.White;
					Color c2 = esVictoria ? Color.FromArgb(255, 240, 130) : Color.FromArgb(210, 210, 215);

					using (LinearGradientBrush lgbTexto = new LinearGradientBrush(
						new PointF(posX, posY), new PointF(posX, posY + tam.Height), c1, c2))
					{
						g.FillPath(lgbTexto, pathTexto);
					}
				}

				g.Restore(estado);
			}

			if (escalaAparicion >= 1.0f)
			{
				string subtexto = esVictoria
					? string.Format("EL PACTO DE LA ALDEA HA SIDO PRESERVADO POR {0} (TURNO {1})", heroePrincipal.ToUpper(), turnosTotales)
					: string.Format("TU ESCUADRÓN HA CAÍDO ANTE LA ESPESURA DEL BOSQUE (TURNO {0})", turnosTotales);

				using (Font fSub = new Font("Arial", 11f, FontStyle.Bold))
				using (SolidBrush bSubSombra = new SolidBrush(Color.Black))
				using (SolidBrush bSub = new SolidBrush(esVictoria ? Color.FromArgb(255, 220, 90) : Color.FromArgb(255, 140, 140)))
				{
					SizeF tamSub = g.MeasureString(subtexto, fSub);
					float subX = (w - tamSub.Width) / 2f;
					float subY = centerY + 95;

					g.DrawString(subtexto, fSub, bSubSombra, subX + 2, subY + 2);
					g.DrawString(subtexto, fSub, bSub, subX, subY);
				}
			}
		}

		void BtnContinuarClick(object sender, EventArgs e)
		{
			if (timerAnimacion != null) timerAnimacion.Dispose();
			if (imgFondoCaptura != null) imgFondoCaptura.Dispose();

			MainForm menu = new MainForm();
			menu.Show();
			this.Close();
		}

		protected override void OnFormClosing(FormClosingEventArgs e)
		{
			if (timerAnimacion != null) timerAnimacion.Dispose();
			if (imgFondoCaptura != null) imgFondoCaptura.Dispose();
			base.OnFormClosing(e);
		}
	}
}