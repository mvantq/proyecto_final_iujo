using System;

namespace proyecto_final_iujo
{
    // Arquetipo: Homo Volens (Nietzsche / Heráclito). Pasión activa, devenir y aceleración térmica.
    public class CartaFuego : Personaje
    {
        public CartaFuego(string id, string nom, int vida, int esc, int atk, int emp, string esp, string desc, string arquetipo, Punto2D pos)
            : base(id, nom, Elemento.Fuego, vida, esc, atk, emp, esp, desc, arquetipo, pos) { }

        // Derivada de combustión: C(e) = 10*e + 2.5*e^2 => C'(e) = 10 + 5*e
        private double CalcularDerivadaCombustion(double energia)
        {
            return 10.0 + (5.0 * energia);
        }

        // Límite asintótico de fusión del rival: lim(def -> inf) [def / (def + 30)] = 1.0 (100%)
        private double LimitePerforacionTermica(double escudoRival)
        {
            return escudoRival / (escudoRival + 30.0);
        }

        public override int CalcularDanio(Personaje objetivo, Escenario escenario)
        {
            double mult = (objetivo != null && objetivo.Elemento == Elemento.Aire) ? 1.5 :
                          (objetivo != null && objetivo.Elemento == Elemento.Agua) ? 0.75 : 1.0;
            int bono = (escenario != null && escenario.Elemento == Elemento.Fuego) ? escenario.BuffAtaque : 0;

            // Coordenadas: radiación térmica que se disipa con la distancia euclidiana
            double d = this.Posicion.DistanciaA(objetivo.Posicion);
            double atenuacion = 12.0 / (12.0 + d);

            // Derivada: tasa instantánea de combustión según el impulso vital invertido
            double impulsoCombustion = CalcularDerivadaCombustion(this.Energia);

            double danio = ((AtaqueBase + impulsoCombustion) * mult + bono) * atenuacion;
            return Math.Max(1, (int)Math.Round(danio));
        }

        public override string UsarHabilidadEspecial(Personaje objetivo, Escenario escenario)
        {
            if (!ConsumirEnergia(2) || CargaEspecial < 100) return "Recursos insuficientes.";
            CargaEspecial = 0;

            // Quiebre de escudo calculado por el límite asintótico de fusión
            double factor = LimitePerforacionTermica(objetivo.Escudo);
            int roto = (int)Math.Round(objetivo.Escudo * factor);
            objetivo.Escudo -= roto;

            int danio = objetivo.RecibirDanio((int)(CalcularDanio(objetivo, escenario) * 1.8));
            return string.Format("¡{0} [{1}] desata [{2}]! Fusión térmica quiebra {3} DEF e inflige {4} daño a {5}.", 
                Nombre, ArquetipoFilosofico, NombreEspecial, roto, danio, objetivo.Nombre);
        }

        public override Personaje Clonar()
        {
            return new CartaFuego(IdPersonaje, Nombre, VidaMaxima, EscudoBase, AtaqueBase, Empatia, NombreEspecial, DescripcionEspecial, ArquetipoFilosofico, Posicion);
        }
    }
}