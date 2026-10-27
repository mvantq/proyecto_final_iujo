
namespace proyecto_final_iujo
{
   public class Escenario
    {
        public string IdEscenario { get; set; }
        public string Nombre { get; set; }
        public Elemento Elemento { get; set; }
        public int BuffAtaque { get; set; }
        public int BuffDefensa { get; set; }
        public string ConceptoFilosofico { get; set; }

        public Escenario(string id, string nom, Elemento elem, int bAtk, int bDef, string concepto)
        {
            IdEscenario = id;
            Nombre = nom;
            Elemento = elem;
            BuffAtaque = bAtk;
            BuffDefensa = bDef;
            ConceptoFilosofico = concepto;
        }
    }
}
