using System;

namespace proyecto_final_iujo
{
    // clase para los personajes y cartas de la faccion agua
    public class CartaAgua : Personaje
    {
        // constructor que manda todos los datos a la clase base de personaje
        public CartaAgua(string id, string nom, int vida, int esc, int atk, int emp, string esp, string desc, string arquet)
            : base(id, nom, Elemento.Agua, vida, esc, atk, emp, esp, desc, arquet) { }

        // calculo de amortiguacion con la formula de limites segun la defensa del enemigo
        private double LimiteAmortiguacionFluidos(double defensa)
        {
            // si no tiene defensa no reduce nada
            if (defensa <= 0) return 0.0;
            return defensa / (defensa + 40.0);
        }

        // formula de derivada para calcular cuanta vida regenera con la energia
        private double DerivadaFlujoVital(double energia)
        {
            // multiplicamos 4 por la raiz cuadrada de la energia
            return 4.0 * Math.Sqrt(Math.Max(1.0, energia));
        }

        // funcion para calcular cuanto golpea al rival tomando en cuenta ventajas y terreno
        public override int CalcularDanio(Personaje objetivo, Escenario esc)
        {
            double mult = 1.0;

            // agua tiene ventaja contra fuego y desventaja contra tierra
            if (objetivo.Elemento == Elemento.Fuego){
            	mult = 1.5;
            }
            else if (objetivo.Elemento == Elemento.Tierra) {
            	mult = 0.75;
            }

            // bono extra de ataque si el escenario actual es de agua
            double bonoEsc = (esc != null && esc.ElementoDominante == Elemento.Agua) ? 1.2 : 1.0;

            // calculamos la reduccion de dano y el ataque base
            double amortiguacion = LimiteAmortiguacionFluidos(objetivo.Defensa);
            double danioBase = this.Ataque * mult * bonoEsc;

            // aplica la reduccion por defensa pero asegura minimo 5 de golpe
            int danioFinal = (int)Math.Max(5, danioBase * (1.0 - amortiguacion));

            return danioFinal;
        }

        // poder especial de agua para curarse segun la energia que tenga
        public override void HabilidadEspecial(Personaje objetivo)
        {
            // calcula la curacion, se recupera vida y reinicia la barra especial a cero
            int curacion = (int)DerivadaFlujoVital(this.Energia);
            this.RecuperarVida(curacion);
            this.CargaEspecial = 0;
        }
    }
}