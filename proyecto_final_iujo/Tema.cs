using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace proyecto_final_iujo
{
    /// <summary>
    /// Paleta visual y utilidades de estilo compartidas por todas las pantallas de Arena Elemental.
    /// </summary>
    public static class Tema
    {
        public static readonly Color Fondo = Color.FromArgb(14, 15, 22);
        public static readonly Color Panel = Color.FromArgb(26, 28, 40);
        public static readonly Color PanelClaro = Color.FromArgb(40, 43, 60);
        public static readonly Color Borde = Color.FromArgb(64, 69, 94);
        public static readonly Color Oro = Color.FromArgb(232, 193, 90);
        public static readonly Color Texto = Color.FromArgb(232, 230, 227);
        public static readonly Color TextoSuave = Color.FromArgb(150, 156, 178);
        public static readonly Color Exito = Color.FromArgb(92, 201, 138);
        public static readonly Color Peligro = Color.FromArgb(230, 90, 90);
        public static readonly Color Advertencia = Color.FromArgb(240, 170, 60);

        public static readonly Color Fuego = Color.FromArgb(232, 85, 43);
        public static readonly Color Agua = Color.FromArgb(47, 143, 230);
        public static readonly Color Tierra = Color.FromArgb(176, 132, 62);
        public static readonly Color Aire = Color.FromArgb(127, 211, 199);

        public static readonly Color Escudo = Color.FromArgb(110, 150, 220);
        public static readonly Color Energia = Color.FromArgb(240, 200, 60);
        public static readonly Color Carga = Color.FromArgb(170, 110, 230);

        public const string FuenteTitulo = "Palatino Linotype";
        public const string FuenteUI = "Segoe UI";
        public const string FuenteConsola = "Consolas";

        public static Color ColorElemento(Elemento e)
        {
            switch (e)
            {
                case Elemento.Fuego: return Fuego;
                case Elemento.Agua: return Agua;
                case Elemento.Tierra: return Tierra;
                default: return Aire;
            }
        }

        public static Color Mezclar(Color a, Color b, float t)
        {
            t = Math.Max(0f, Math.Min(1f, t));
            return Color.FromArgb(
                (int)(a.A + (b.A - a.A) * t),
                (int)(a.R + (b.R - a.R) * t),
                (int)(a.G + (b.G - a.G) * t),
                (int)(a.B + (b.B - a.B) * t));
        }

        public static Color Aclarar(Color c, float t) { return Mezclar(c, Color.White, t); }
        public static Color Oscurecer(Color c, float t) { return Mezclar(c, Color.Black, t); }

        /// <summary>
        /// Aplica el tema oscuro de forma recursiva a todos los controles estándar de un contenedor.
        /// Cada pantalla puede luego personalizar controles puntuales.
        /// </summary>
        public static void Aplicar(Control raiz)
        {
            foreach (Control c in raiz.Controls)
            {
                AplicarControl(c);
               if (c.HasChildren) Aplicar(c);
            }
        }

        static void AplicarControl(Control c)
        {
            if (c is Button)
            {
                EstilizarBoton((Button)c, PanelClaro, Borde);
            }
            else if (c is RichTextBox)
            {
                c.BackColor = Panel;
                c.ForeColor = Texto;
                ((RichTextBox)c).BorderStyle = BorderStyle.None;
            }
            else if (c is TextBox)
            {
                c.BackColor = PanelClaro;
                c.ForeColor = Texto;
                ((TextBox)c).BorderStyle = BorderStyle.FixedSingle;
            }
            else if (c is ListView)
            {
                EstilizarListView((ListView)c);
            }
            else if (c is ListBox)
            {
                c.BackColor = Panel;
                c.ForeColor = Texto;
                ((ListBox)c).BorderStyle = BorderStyle.None;
            }
            else if (c is GroupBox)
            {
                c.ForeColor = Oro;
            }
            else if (c is RadioButton || c is CheckBox)
            {
                c.ForeColor = Texto;
            }
            else if (c is Label)
            {
                c.ForeColor = Texto;
            }
            else if (c is Panel || c is FlowLayoutPanel)
            {
                if (c.BackColor != Color.Transparent) c.BackColor = Panel;
            }
        }

        public static void EstilizarBoton(Button b, Color fondo, Color borde)
        {
            EstilizarBoton(b, fondo, borde, Texto);
        }

        public static void EstilizarBoton(Button b, Color fondo, Color borde, Color texto)
        {
            b.FlatStyle = FlatStyle.Flat;
            b.UseVisualStyleBackColor = false;
            b.FlatAppearance.BorderColor = borde;
            b.FlatAppearance.BorderSize = 1;
            b.FlatAppearance.MouseOverBackColor = Aclarar(fondo, 0.15f);
            b.FlatAppearance.MouseDownBackColor = Oscurecer(fondo, 0.2f);
            b.BackColor = fondo;
            b.ForeColor = texto;
            b.Cursor = Cursors.Hand;
        }

        /// <summary>Botón dorado destacado (acción principal de la pantalla).</summary>
        public static void EstilizarBotonPrimario(Button b)
        {
            EstilizarBoton(b, Oro, Aclarar(Oro, 0.35f), Fondo);
        }

        /// <summary>Botón coloreado según un elemento o acción.</summary>
        public static void EstilizarBotonAccion(Button b, Color color)
        {
            EstilizarBoton(b, Oscurecer(color, 0.45f), color, Texto);
        }

        public static void EstilizarListView(ListView lv)
        {
            lv.BackColor = Panel;
            lv.ForeColor = Texto;
            lv.BorderStyle = BorderStyle.None;
            lv.FullRowSelect = true;
            lv.HideSelection = false;
            lv.OwnerDraw = true;
            lv.DrawColumnHeader -= DibujarEncabezado;
            lv.DrawColumnHeader += DibujarEncabezado;
            lv.DrawItem -= DibujarItemPorDefecto;
            lv.DrawItem += DibujarItemPorDefecto;
        }

        static void DibujarEncabezado(object sender, DrawListViewColumnHeaderEventArgs e)
        {
            using (SolidBrush b = new SolidBrush(PanelClaro)) e.Graphics.FillRectangle(b, e.Bounds);
            using (Pen p = new Pen(Borde)) e.Graphics.DrawLine(p, e.Bounds.Right - 1, e.Bounds.Top + 4, e.Bounds.Right - 1, e.Bounds.Bottom - 4);
            Rectangle r = new Rectangle(e.Bounds.X + 4, e.Bounds.Y, e.Bounds.Width - 6, e.Bounds.Height);
            TextRenderer.DrawText(e.Graphics, e.Header.Text, e.Font, r, Oro,
                TextFormatFlags.Left | TextFormatFlags.VerticalCenter | TextFormatFlags.EndEllipsis);
        }

        static void DibujarItemPorDefecto(object sender, DrawListViewItemEventArgs e)
        {
            e.DrawDefault = true;
        }

        public static GraphicsPath RectanguloRedondeado(RectangleF r, float radio)
        {
            GraphicsPath p = new GraphicsPath();
            float d = radio * 2f;
            if (d <= 0f) { p.AddRectangle(r); return p; }
            p.AddArc(r.X, r.Y, d, d, 180, 90);
            p.AddArc(r.Right - d, r.Y, d, d, 270, 90);
            p.AddArc(r.Right - d, r.Bottom - d, d, d, 0, 90);
            p.AddArc(r.X, r.Bottom - d, d, d, 90, 90);
            p.CloseFigure();
            return p;
        }
    }
}
