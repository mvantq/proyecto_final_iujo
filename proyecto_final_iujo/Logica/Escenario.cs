using System;

namespace proyecto_final_iujo
{
    public class Escenario
    {
        private string _idEscenario;
        private string _nombre;
        private Elemento _elemento;
        private int _bonoAtaque;
        private int _bonoDefensa;
        private string _loreFilosofico;

        public string IdEscenario { get { return _idEscenario; } set { _idEscenario = value ?? ""; } }
        public string Nombre { get { return _nombre; } set { _nombre = value ?? ""; } }
        public Elemento Elemento { get { return _elemento; } set { _elemento = value; } }

        // Propiedad alias para que las cartas compilen sin error
        public Elemento ElementoDominante { get { return _elemento; } set { _elemento = value; } }

        public int BonoAtaque { get { return _bonoAtaque; } set { _bonoAtaque = value; } }
        public int BonoDefensa { get { return _bonoDefensa; } set { _bonoDefensa = value; } }
        public string LoreFilosofico { get { return _loreFilosofico; } set { _loreFilosofico = value ?? ""; } }

        public Escenario(string id, string nombre, Elemento elemento, int bonoAtk, int bonoDef, string lore)
        {
            IdEscenario = id;
            Nombre = nombre;
            Elemento = elemento;
            BonoAtaque = bonoAtk;
            BonoDefensa = bonoDef;
            LoreFilosofico = lore;
        }

        public override string ToString()
        {
            return string.Format("{0} [{1}] - ATK+{2} DEF+{3}", Nombre, Elemento, BonoAtaque, BonoDefensa);
        }
    }
}