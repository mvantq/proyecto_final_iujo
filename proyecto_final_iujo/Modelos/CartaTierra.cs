using System;

namespace proyecto_final_iujo
{
    // clase para los personajes y cartas de la faccion tierra
    public class CartaTierra : Personaje
    {
        // constructor que manda los atributos a la clase padre de personaje
        public CartaTierra(string id, string nom, int vida, int esc, int atk, int emp, string esp, string desc, string arquet)
            : base(id, nom, Elemento.Tierra, vida, esc, atk, emp, esp, desc, arquet) { }

        // formula basada en elasticidad para escalar la defensa
        private double ModuloElasticidadTectonica(double defensa)
        {
            // aumenta la defensa base multiplicandola por 1.35
            return defensa * 1.35;
        }

        // calcula el golpe al rival evaluando ventaja de tipo y resistencia enemiga
        public override int CalcularDanio(Personaje objetivo, Escenario esc)
        {
            double mult = 1.0;

            // tierra tiene ventaja contra agua y desventaja contra aire
            if (objetivo.Elemento == Elemento.Agua) mult = 1.5;
            else if (objetivo.Elemento == Elemento.Aire) mult = 0.75;

            // bono extra de golpe si el terreno actual es de tierra
            double bonoEsc = (esc != null && esc.ElementoDominante == Elemento.Tierra) ? 1.2 : 1.0;

            // resta el 70 por ciento de la defensa rival y garantiza minimo 5 de golpe
            int danioNeto = (int)Math.Max(5, (this.Ataque * mult * bonoEsc) - (objetivo.Defensa * 0.7));
            return danioNeto;
        }

        // habilidad especial para aumentarse la defensa de forma permanente en la pelea
        public override void HabilidadEspecial(Personaje objetivo)
        {
            // sube la defensa usando el modulo elastico y resetea la barra de especial
            this.Defensa += (int)(ModuloElasticidadTectonica(this.Defensa) * 0.2);
            this.CargaEspecial = 0;
        }
    }
}