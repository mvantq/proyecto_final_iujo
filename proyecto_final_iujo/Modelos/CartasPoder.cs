using System;

namespace proyecto_final_iujo
{
    public enum ElementoTipo { Fuego, Agua, Tierra, Viento }

    public class CartaPoder
    {
        public string Nombre { get; set; }
        public ElementoTipo Elemento { get; set; }
        public string Descripcion { get; set; }
        public double MitigacionDano { get; set; } // Fracción entre 0.0 y 1.0
        public int CuracionBono { get; set; }

        public CartaPoder(string nombre, ElementoTipo elemento, string descripcion, double mitigacion, int curacion)
        {
            Nombre = nombre;
            Elemento = elemento;
            Descripcion = descripcion;
            MitigacionDano = mitigacion;
            CuracionBono = curacion;
        }

        public void AplicarEfecto(Personaje p)
        {
            if (p != null && CuracionBono > 0)
            {
                p.RecuperarVida(CuracionBono);
            }
        }
    }
}
