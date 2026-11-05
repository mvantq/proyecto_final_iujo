using System;

namespace proyecto_final_iujo
{
    public enum Lenguaje
    {
        ES = 0,
        EN = 1
    }

    public static partial class Idioma
    {
        public static event EventHandler Cambiado;
        private static Lenguaje actual = Lenguaje.ES;

        public static Lenguaje Actual
        {
            get { return actual; }
            set
            {
                if (actual != value)
                {
                    actual = value;
                    if (Cambiado != null)
                        Cambiado(null, EventArgs.Empty);
                }
            }
        }
    }
}