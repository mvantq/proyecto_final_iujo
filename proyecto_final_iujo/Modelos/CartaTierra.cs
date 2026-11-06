using System;

namespace proyecto_final_iujo
{
    // Arquetipo: Homo Radicatus (Heidegger / Zubiri). Arraigo ontologico, finitud, firmeza y memoria.
    public class CartaTierra : Personaje
    {
        public CartaTierra(string id, string nom, int vida, int esc, int atk, int emp, string esp, string desc, string arquet)
            : base(id, nom, Elemento.Tierra, vida, esc, atk, emp, esp, desc, arquet) { }

        // Deformacion plastica del material: sigma = E * epsilon
        private double ModuloElasticidadTectonica(double defensa)
        {
            return defensa * 1.35;
        }

        public override int CalcularDanio(Personaje objetivo, Escenario esc)
        {
            double mult = 1.0;
            if (objetivo.Elemento == Elemento.Agua) mult = 1.5;
            else if (objetivo.Elemento == Elemento.Aire) mult = 0.75;

            double bonoEsc = (esc != null && esc.ElementoDominante == Elemento.Tierra) ? 1.2 : 1.0;
            int danioNeto = (int)Math.Max(5, (this.Ataque * mult * bonoEsc) - (objetivo.Defensa * 0.7));
            return danioNeto;
        }

        public override void HabilidadEspecial(Personaje objetivo)
        {
            // Fortalece la defensa y recupera escudo en base a su modulo elastico
            this.Defensa += (int)(ModuloElasticidadTectonica(this.Defensa) * 0.2);
            this.CargaEspecial = 0;
        }
    }
}