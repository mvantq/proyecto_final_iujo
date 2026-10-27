
using System;

namespace proyecto_final_iujo
{
    // Arquetipo: Homo Radicatus (Heidegger / Buber). Arraigo a la tierra, solidez del ser y memoria ontológica.
    public class CartaTierra : Personaje
    {
        public CartaTierra(string id, string nom, int vida, int esc, int atk, int emp, string esp, string desc, string arquetipo, Punto2D pos)
            : base(id, nom, Elemento.Tierra, vida, esc, atk, emp, esp, desc, arquetipo, pos) { }

        // Límite de incompresibilidad de la roca: lim(def -> inf) [def / (def + 25)] = 1.0 (100%)
        private double LimiteIncompresible(double escudo)
        {
            return escudo / (escudo + 25.0);
        }

        // Derivada de deformación elástica (furia de contraataque acumulada por vida perdida v):
        // F(v) = 3*v^2 => F'(v) = 6*v
        private double DerivadaResistenciaSismica(double vidaActual, double vidaMaxima)
        {
            double v = Math.Max(0.0, 1.0 - (vidaActual / vidaMaxima));
            return 6.0 * v;
        }

        public override int CalcularDanio(Personaje objetivo, Escenario escenario)
        {
            double mult = (objetivo != null && objetivo.Elemento == Elemento.Agua) ? 1.5 :
                          (objetivo != null && objetivo.Elemento == Elemento.Aire) ? 0.75 : 1.0;
            int bono = (escenario != null && escenario.Elemento == Elemento.Tierra) ? escenario.BuffAtaque : 0;

            // Coordenadas: propagación radial de onda sísmica
            double d = this.Posicion.DistanciaA(objetivo.Posicion);
            double atenuacion = 15.0 / (15.0 + Math.Pow(d, 1.2));

            double impulso = DerivadaResistenciaSismica(this.VidaActual, this.VidaMaxima);
            double danio = ((AtaqueBase + impulso) * mult + bono) * atenuacion;
            return Math.Max(1, (int)Math.Round(danio));
        }

        public override string UsarHabilidadEspecial(Personaje objetivo, Escenario escenario)
        {
            if (!ConsumirEnergia(2) || CargaEspecial < 100) return "Recursos insuficientes.";
            CargaEspecial = 0;

            int refuerzo = 35 + (int)Math.Round(LimiteIncompresible(this.Escudo) * 10);
            Escudo += refuerzo;

            int danio = objetivo.RecibirDanio(AtaqueBase + (int)(Escudo * 0.5));
            return string.Format("¡{0} [{1}] activa [{2}]! Estructura incompresible otorga +{3} DEF e inflige {4} daño contundente.", 
                Nombre, ArquetipoFilosofico, NombreEspecial, refuerzo, danio);
        }

        // Inmunidad natural de la Tierra al sismo
        public override string ProcesarEmpatiaSismica(int danioBase)
        {
            Escudo += 15;
            return string.Format("[COMUNIÓN ONTOLÓGICA] {0} ({1}) entra en resonancia natural con la tierra: ¡Inmune al daño y gana +15 DEF!",
                Nombre, ArquetipoFilosofico);
        }

        public override Personaje Clonar()
        {
            return new CartaTierra(IdPersonaje, Nombre, VidaMaxima, EscudoBase, AtaqueBase, Empatia, NombreEspecial, DescripcionEspecial, ArquetipoFilosofico, Posicion);
        }
    }
}
