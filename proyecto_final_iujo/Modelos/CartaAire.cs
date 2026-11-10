using System;

namespace proyecto_final_iujo
{
    // clase para los personajes y cartas de la faccion aire o viento
    public class CartaAire : Personaje
    {
        // constructor que pasa los atributos a la clase padre personaje
        public CartaAire(string id, string nom, int vida, int esc, int atk, int emp, string esp, string desc, string arquet)
            : base(id, nom, Elemento.Aire, vida, esc, atk, emp, esp, desc, arquet) { }

        // calculo de aceleracion para aumentar el ataque base
        private double AceleracionSonica(double ataque)
        {
            // escala el ataque por 1.25 para simular velocidad
            return ataque * 1.25;
        }

        // calcula el golpe al rival evaluando ventaja elemental y reduccion de defensa
        public override int CalcularDanio(Personaje objetivo, Escenario esc)
        {
            double mult = 1.0;

            // aire tiene ventaja contra tierra y desventaja contra fuego
            if (objetivo.Elemento == Elemento.Tierra) mult = 1.5;
            else if (objetivo.Elemento == Elemento.Fuego) mult = 0.75;

            // bono adicional de ataque si el terreno actual es de aire
            double bonoEsc = (esc != null && esc.ElementoDominante == Elemento.Aire) ? 1.2 : 1.0;

            // aplicamos el buff de velocidad sobre el ataque base
            double ataqueEfectivo = AceleracionSonica(this.Ataque);

            // resta la mitad de la defensa del rival y deja como minimo 5 de golpe
            int danioFinal = (int)Math.Max(5, (ataqueEfectivo * mult * bonoEsc) - (objetivo.Defensa * 0.5));
            return danioFinal;
        }

        // ataque especial para ignorar defensas convencionales
        public override void HabilidadEspecial(Personaje objetivo)
        {
            // verifica que el rival exista para pegarle directo
            if (objetivo != null)
            {
                // calcula el dano puro escalado por 1.6
                int danioPerforante = (int)(this.Ataque * 1.6);
                objetivo.RecibirDanoPuro(danioPerforante);
            }

            // resetea la barra de especial
            this.CargaEspecial = 0;
        }
    }
}