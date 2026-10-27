using System;

namespace proyecto_final_iujo
{
    // Arquetipo: Homo Viator (Marcel / Kant). Trascendencia metafísica, libertad pura y superación de límites corporales.
    public class CartaAire : Personaje
    {
        public CartaAire(string id, string nom, int vida, int esc, int atk, int emp, string esp, string desc, string arquetipo, Punto2D pos)
            : base(id, nom, Elemento.Aire, vida, esc, atk, emp, esp, desc, arquetipo, pos) { }

        // Derivada de velocidad instantánea de ráfaga: s(t) = 15*t + 4*t^2 => v(t) = s'(t) = 15 + 8*t
        private double DerivadaVelocidadInstantanea(double energia)
        {
            return 15.0 + (8.0 * Math.Min(3.0, energia));
        }

        // Límite de arrastre: coeficiente aerodinámico que apenas pierde fuerza con la distancia
        private double LimiteResistenciaAerodinamica(double d)
        {
            return 10.0 / (10.0 + (d * 0.4));
        }

        public override int CalcularDanio(Personaje objetivo, Escenario escenario)
        {
            double mult = (objetivo != null && objetivo.Elemento == Elemento.Tierra) ? 1.5 :
                          (objetivo != null && objetivo.Elemento == Elemento.Fuego) ? 0.75 : 1.0;
            int bono = (escenario != null && escenario.Elemento == Elemento.Aire) ? escenario.BuffAtaque : 0;

            // Coordenadas: trayectoria balística rectilínea
            double d = this.Posicion.DistanciaA(objetivo.Posicion);
            double factor = LimiteResistenciaAerodinamica(d);

            double velocidadSonica = DerivadaVelocidadInstantanea(this.Energia) * 0.4;
            double danio = ((AtaqueBase + velocidadSonica) * mult + bono) * factor;
            return Math.Max(1, (int)Math.Round(danio));
        }

        public override string UsarHabilidadEspecial(Personaje objetivo, Escenario escenario)
        {
            if (!ConsumirEnergia(2) || CargaEspecial < 100) return "Recursos insuficientes.";
            CargaEspecial = 0;

            // Por su derivada sónica y arquetipo de trascendencia, elude el 100% del escudo rival
            int danioDirecto = (int)(CalcularDanio(objetivo, escenario) * 1.4);
            objetivo.VidaActual -= danioDirecto;

            return string.Format("¡{0} [{1}] ejecuta [{2}]! Ráfaga trascendente elude el escudo e inflige {3} daño directo a los signos vitales.", 
                Nombre, ArquetipoFilosofico, NombreEspecial, danioDirecto);
        }

        public override Personaje Clonar()
        {
            return new CartaAire(IdPersonaje, Nombre, VidaMaxima, EscudoBase, AtaqueBase, Empatia, NombreEspecial, DescripcionEspecial, ArquetipoFilosofico, Posicion);
        }
    }
}