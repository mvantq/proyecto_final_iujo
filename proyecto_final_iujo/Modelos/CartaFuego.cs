using System;

namespace proyecto_final_iujo
{
    // Arquetipo: Homo Volens (Nietzsche / Heraclito). Voluntad de poder, devenir igneo y colera tragica.
    public class CartaFuego : Personaje
    {
        public CartaFuego(string id, string nom, int vida, int esc, int atk, int emp, string esp, string desc, string arquet)
            : base(id, nom, Elemento.Fuego, vida, esc, atk, emp, esp, desc, arquet) { }

        // Entropia termodinamica no lineal: E(T) = atk * (1 + ln(1 + e))
        private double EntropiaTermica(double energia)
        {
            return Math.Log(1.0 + Math.Max(0.0, energia));
        }

        public override int CalcularDanio(Personaje objetivo, Escenario esc)
        {
            double mult = 1.0;
            if (objetivo.Elemento == Elemento.Aire) mult = 1.5;
            else if (objetivo.Elemento == Elemento.Agua) mult = 0.75;

            double bonoEsc = (esc != null && esc.ElementoDominante == Elemento.Fuego) ? 1.2 : 1.0;
            double factorTermico = 1.0 + EntropiaTermica(this.Energia);
            
            int danioCalculado = (int)Math.Max(5, (this.Ataque * factorTermico * mult * bonoEsc) - objetivo.Defensa);
            return danioCalculado;
        }

        public override void HabilidadEspecial(Personaje objetivo)
        {
            if (objetivo != null)
            {
                int danioExplosivo = (int)(this.Ataque * 1.8);
                objetivo.RecibirDanoPuro(danioExplosivo);
            }
            this.CargaEspecial = 0;
        }
    }
}