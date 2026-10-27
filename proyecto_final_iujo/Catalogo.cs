using System.Collections.Generic;

namespace proyecto_final_iujo
{
    public static class CatalogoCartas
    {
        public static List<Personaje> ObtenerPersonajes()
        {
            List<Personaje> l = new List<Personaje>();

            // Fuego (Homo Volens - Voluntad de Poder y Devenir)
            l.Add(new CartaFuego("FUE-01", "Ignis el Temerario", 45, 20, 35, 30, "Explosión Volcánica", "Quiebra escudo por fusión", "Homo Volens", new Punto2D(2, 4)));
            l.Add(new CartaFuego("FUE-02", "Pyra la Maga Ardiente", 40, 25, 35, 45, "Lanza Solar", "Funde armaduras", "Voluntad Ígnea", new Punto2D(2, 5)));
            l.Add(new CartaFuego("FUE-03", "Vulcano el Coloso", 55, 30, 15, 20, "Erupción Primordial", "Onda sísmica", "Fuerza Primitiva", new Punto2D(1, 4)));

            // Agua (Homo Compassivus - Alteridad y Escucha)
            l.Add(new CartaAgua("AGU-01", "Aqua la Sirena Mística", 50, 35, 15, 75, "Marea Restauradora", "Sana vida y drena energía", "Homo Compassivus", new Punto2D(1, 7)));
            l.Add(new CartaAgua("AGU-02", "Tsunami Guerrero Océano", 45, 25, 30, 60, "Golpe de Rompiente", "Impacto desestabilizador", "Guardián de la Empatía", new Punto2D(2, 8)));
            l.Add(new CartaAgua("AGU-03", "Glaciar Guardián de Hielo", 60, 30, 10, 70, "Cero Absoluto", "Amortiguación hidrostática", "Ser de Serenidad", new Punto2D(1, 9)));

            // Tierra (Homo Radicatus - Arraigo Ontológico y Finitud)
            l.Add(new CartaTierra("TIE-01", "Terran el Druida Telúrico", 55, 35, 10, 80, "Bastión de Raíces", "Comunión cósmica", "Homo Radicatus", new Punto2D(8, 2)));
            l.Add(new CartaTierra("TIE-02", "Golem de Obsidiana", 60, 30, 10, 85, "Fisura Aplastante", "Estructura incompresible", "Monolito del Ser", new Punto2D(9, 2)));
            l.Add(new CartaTierra("TIE-03", "Titán de Granito", 45, 35, 20, 65, "Muralla Inquebrantable", "Impacto por deformación", "Arraigo Inquebrantable", new Punto2D(8, 3)));

            // Aire (Homo Viator - Trascendencia y Libertad Espiritual)
            l.Add(new CartaAire("AIR-01", "Zephyr Arquero Celestial", 40, 20, 40, 50, "Flecha Huracanada", "Aceleración sónica elude escudo", "Homo Viator", new Punto2D(8, 8)));
            l.Add(new CartaAire("AIR-02", "Tempest Chamana del Viento", 45, 25, 30, 40, "Tornado Perforante", "Impacto sónico directo", "Espíritu Trascendente", new Punto2D(9, 7)));
            l.Add(new CartaAire("AIR-03", "Gale Mensajero Eólico", 50, 25, 25, 55, "Vórtice Cortante", "Ráfaga continua", "Nómada de la Libertad", new Punto2D(8, 9)));

            return l;
        }

        public static List<Escenario> ObtenerEscenarios()
        {
            List<Escenario> esc = new List<Escenario>();
            esc.Add(new Escenario("ESC-01", "Volcán Activo", Elemento.Fuego, 6, 0, "El Devenir Heraclíteo: El fuego consume la estabilidad material."));
            esc.Add(new Escenario("ESC-02", "Abismo Oceánico", Elemento.Agua, 5, 5, "La Fluidez Intersubjetiva: El agua disipa la violencia egoica."));
            esc.Add(new Escenario("ESC-03", "Cañón Rocoso", Elemento.Tierra, 0, 10, "El Arraigo Ontológico: La tierra recuerda la finitud del ser."));
            esc.Add(new Escenario("ESC-04", "Cumbre de Viento", Elemento.Aire, 7, 0, "La Trascendencia del Espíritu: El aire supera toda atadura física."));
            return esc;
        }
    }
}
