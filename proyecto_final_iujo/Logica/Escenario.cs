using System;

namespace proyecto_final_iujo
{
    // clase para manejar los escenarios donde pelean los personajes
    public class Escenario
    {
        private string _idEscenario;
        private string _nombre;
        private Elemento _elemento;
        private int _bonoAtaque;
        private int _bonoDefensa;
        private string _loreFilosofico;

        // propiedades de validacion por si es nulo
        public string IdEscenario { get { return _idEscenario; } set { _idEscenario = value ?? ""; } }
        public string Nombre { get { return _nombre; } set { _nombre = value ?? ""; } }
        public Elemento Elemento { get { return _elemento; } set { _elemento = value; } }

        // alias para que no se rompa la compilacion al llamarlo desde las cartas
        public Elemento ElementoDominante { get { return _elemento; } set { _elemento = value; } }

        // bonos de stats que da el terreno al pelear
        public int BonoAtaque { get { return _bonoAtaque; } set { _bonoAtaque = value; } }
        public int BonoDefensa { get { return _bonoDefensa; } set { _bonoDefensa = value; } }
        public string LoreFilosofico { get { return _loreFilosofico; } set { _loreFilosofico = value ?? ""; } }

        // constructor para inicializar el escenario con todos sus datos
        public Escenario(string id, string nombre, Elemento elemento, int bonoAtk, int bonoDef, string lore)
        {
            IdEscenario = id;
            Nombre = nombre;
            Elemento = elemento;
            BonoAtaque = bonoAtk;
            BonoDefensa = bonoDef;
            LoreFilosofico = lore;
        }

        // metodo tostring para mostrar el resumen del escenario y sus buffs
        public override string ToString()
        {
            return string.Format("{0} [{1}] - ATK+{2} DEF+{3}", Nombre, Elemento, BonoAtaque, BonoDefensa);
        }
    }
}