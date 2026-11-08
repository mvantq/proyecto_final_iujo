using System;

namespace proyecto_final_iujo
{
    // Arquetipo: Homo Compassivus (Scheler / Levinas). Fluidez, escucha, acogida del Otro y disipación.
    public class CartaAgua : Personaje
    {
        public CartaAgua(string id, string nom, int vida, int esc, int atk, int emp, string esp, string desc, string arquetipo, Punto2D pos)
            : base(id, nom, Elemento.Agua, vida, esc, atk, emp, esp, desc, arquetipo, pos) { }

        // Límite de amortiguación hidráulica: lim(def -> inf) [def / (def + 40)] = 1.0 (100%)
        private double LimiteAmortiguacionFluidos(double defensa)
        {
            if (defensa <= 0) return 0.0;
            return defensa / (defensa + 40.0);
        }

        // Derivada de flujo laminar y regeneración: dV/de = 4*sqrt(e)
        private double DerivadaFlujoVital(double energia)
        {
            return 4.0 * Math.Sqrt(Math.Max(1.0, energia));
        }

        public override int CalcularDanio(Personaje objetivo, Escenario escenario)
        {
            double mult = (objetivo != null && objetivo.Elemento == Elemento.Fuego) ? 1.5 :
                          (objetivo != null && objetivo.Elemento == Elemento.Tierra) ? 0.75 : 1.0;
            int bono = (escenario != null && escenario.Elemento == Elemento.Agua) ? escenario.BuffAtaque : 0;

            // Coordenadas: rozamiento hidrodinámico por distancia
            double d = this.Posicion.DistanciaA(objetivo.Posicion);
            double atenuacion = 10.0 / (10.0 + (d * 0.8));

            double danio = (AtaqueBase * mult + bono) * atenuacion;
            return Math.Max(1, (int)Math.Round(danio));
        }

        public override string UsarHabilidadEspecial(Personaje objetivo, Escenario escenario)
        {
            if (!ConsumirEnergia(2) || CargaEspecial < 100) return "Recursos insuficientes.";
            CargaEspecial = 0;

            int danio = objetivo.RecibirDanio((int)(CalcularDanio(objetivo, escenario) * 1.3));

            // Curación sustentada en la derivada de flujo vital
            int cura = 15 + (int)Math.Round(DerivadaFlujoVital(this.Energia));
            VidaActual += cura;

            if (objetivo.Energia > 0) { objetivo.ConsumirEnergia(1); Energia += 1; }

            return string.Format("¡{0} [{1}] invoca [{2}]! Flujo laminar causa {3} daño, regenera +{4} HP y drena 1 energía.", 
                Nombre, ArquetipoFilosofico, NombreEspecial, danio, cura);
        }

        public override Personaje Clonar()
        {
            return new CartaAgua(IdPersonaje, Nombre, VidaMaxima, EscudoBase, AtaqueBase, Empatia, NombreEspecial, DescripcionEspecial, ArquetipoFilosofico, Posicion);
        }
    }
}