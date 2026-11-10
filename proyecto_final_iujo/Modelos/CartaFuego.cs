using System;

namespace proyecto_final_iujo
{
    // clase para los personajes y cartas de la faccion fuego
    public class CartaFuego : Personaje
    {
        // constructor que envia los datos a la clase base de personaje
        public CartaFuego(string id, string nom, int vida, int esc, int atk, int emp, string esp, string desc, string arquet)
            : base(id, nom, Elemento.Fuego, vida, esc, atk, emp, esp, desc, arquet) { }

        // formula con logaritmo natural para calcular el incremento por entropia segun la energia
        private double EntropiaTermica(double energia)
        {
            // asegura que la energia no sea negativa antes de calcular el logaritmo
            return Math.Log(1.0 + Math.Max(0.0, energia));
        }

        // calcula el dano del ataque tomando en cuenta energia, ventajas de tipo y terreno
        public override int CalcularDanio(Personaje objetivo, Escenario esc)
        {
            double mult = 1.0;

            // fuego tiene ventaja contra aire y desventaja contra agua
            if (objetivo.Elemento == Elemento.Aire) mult = 1.5;
            else if (objetivo.Elemento == Elemento.Agua) mult = 0.75;

            // bono extra si el escenario actual es de fuego
            double bonoEsc = (esc != null && esc.ElementoDominante == Elemento.Fuego) ? 1.2 : 1.0;

            // sumamos 1 al valor de la entropia para obtener el multiplicador termico
            double factorTermico = 1.0 + EntropiaTermica(this.Energia);

            // aplica la formula restando la defensa rival y garantiza minimo 5 de golpe
            int danioCalculado = (int)Math.Max(5, (this.Ataque * factorTermico * mult * bonoEsc) - objetivo.Defensa);
            return danioCalculado;
        }

        // ataque especial que mete un golpe fuerte ignorando la defensa
        public override void HabilidadEspecial(Personaje objetivo)
        {
            // valida que exista un objetivo antes de golpearlo
            if (objetivo != null)
            {
                // escala el ataque por 1.8 para el dano puro
                int danioExplosivo = (int)(this.Ataque * 1.8);
                objetivo.RecibirDanoPuro(danioExplosivo);
            }

            // reinicia la barra de carga especial
            this.CargaEspecial = 0;
        }
    }
}