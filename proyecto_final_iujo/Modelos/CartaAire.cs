using System;

namespace proyecto_final_iujo
{
    // Arquetipo: Homo Viator (Marcel / Frankl). Trascendencia, libertad de espiritu, logos y superacion.
    public class CartaAire : Personaje
    {
        public CartaAire(string id, string nom, int vida, int esc, int atk, int emp, string esp, string desc, string arquet)
            : base(id, nom, Elemento.Aire, vida, esc, atk, emp, esp, desc, arquet) { }

        // Aceleracion sonica: a(t) = v'(t) = 1.5 * atk
        private double AceleracionSonica(double ataque)
        {
            return ataque * 1.25;
        }

        public override int CalcularDanio(Personaje objetivo, Escenario esc)
        {
            double mult = 1.0;
            if (objetivo.Elemento == Elemento.Tierra) mult = 1.5;
            else if (objetivo.Elemento == Elemento.Fuego) mult = 0.75;

            double bonoEsc = (esc != null && esc.ElementoDominante == Elemento.Aire) ? 1.2 : 1.0;
            double ataqueEfectivo = AceleracionSonica(this.Ataque);
            
            int danioFinal = (int)Math.Max(5, (ataqueEfectivo * mult * bonoEsc) - (objetivo.Defensa * 0.5));
            return danioFinal;
        }

        public override void HabilidadEspecial(Personaje objetivo)
        {
            if (objetivo != null)
            {
                // Danio penetrante que ignora blindaje convencional
                int danioPerforante = (int)(this.Ataque * 1.6);
                objetivo.RecibirDanoPuro(danioPerforante);
            }
            this.CargaEspecial = 0;
        }
    }
}