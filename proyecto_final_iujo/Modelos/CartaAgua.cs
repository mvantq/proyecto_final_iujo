using System;

namespace proyecto_final_iujo
{
    // Arquetipo: Homo Compassivus (Scheler / Levinas). Fluidez, escucha, acogida del Otro y disipacion.
    public class CartaAgua : Personaje
    {
        public CartaAgua(string id, string nom, int vida, int esc, int atk, int emp, string esp, string desc, string arquet)
            : base(id, nom, Elemento.Agua, vida, esc, atk, emp, esp, desc, arquet) { }

        // Limite de amortiguacion hidraulica: lim(def -> inf) [def / (def + 40)] = 1.0 (100%)
        private double LimiteAmortiguacionFluidos(double defensa)
        {
            if (defensa <= 0) return 0.0;
            return defensa / (defensa + 40.0);
        }

        // Derivada de flujo laminar y regeneracion: dV/de = 4*sqrt(e)
        private double DerivadaFlujoVital(double energia)
        {
            return 4.0 * Math.Sqrt(Math.Max(1.0, energia));
        }

        public override int CalcularDanio(Personaje objetivo, Escenario esc)
        {
            double mult = 1.0;
            if (objetivo.Elemento == Elemento.Fuego) mult = 1.5;
            else if (objetivo.Elemento == Elemento.Tierra) mult = 0.75;

            double bonoEsc = (esc != null && esc.ElementoDominante == Elemento.Agua) ? 1.2 : 1.0;
            double amortiguacion = LimiteAmortiguacionFluidos(objetivo.Defensa);
            double danioBase = this.Ataque * mult * bonoEsc;
            
            int danioFinal = (int)Math.Max(5, danioBase * (1.0 - amortiguacion));
            return danioFinal;
        }

        public override void HabilidadEspecial(Personaje objetivo)
        {
            int curacion = (int)DerivadaFlujoVital(this.Energia);
            this.RecuperarVida(curacion);
            this.CargaEspecial = 0;
        }
    }
}