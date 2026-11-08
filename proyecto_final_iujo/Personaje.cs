
using System;

namespace proyecto_final_iujo
{
    public abstract class Personaje
    {
        // Atributos privados encapsulados
        private string _idPersonaje;
        private string _nombre;
        private Elemento _elemento;
        private int _vidaMaxima;
        private int _vidaActual;
        private int _escudoBase;
        private int _escudo;
        private int _escudoMaximo = 100;
        private int _ataqueBase;
        private int _energia;
        private int _energiaMaxima = 10;
        private int _cargaEspecial;
        private int _empatia;
        private string _nombreEspecial;
        private string _descripcionEspecial;
        private string _arquetipoFilosofico;

        #region Propiedades Publicas
        public string IdPersonaje { get { return _idPersonaje; } set { _idPersonaje = value ?? ""; } }
        public string Nombre { get { return _nombre; } set { _nombre = string.IsNullOrWhiteSpace(value) ? "SinNombre" : value.Trim(); } }
        public Elemento Elemento { get { return _elemento; } protected set { _elemento = value; } }
        public Punto2D Posicion { get; set; }

        public int VidaMaxima
        {
            get { return _vidaMaxima; }
            protected set { _vidaMaxima = Math.Max(1, value); }
        }

        public int VidaActual
        {
            get { return _vidaActual; }
            set { _vidaActual = Math.Max(0, Math.Min(_vidaMaxima, value)); }
        }

        public int EscudoBase { get { return _escudoBase; } protected set { _escudoBase = Math.Max(0, value); } }

        public int Escudo
        {
            get { return _escudo; }
            set { _escudo = Math.Max(0, Math.Min(_escudoMaximo, value)); }
        }

        public int AtaqueBase { get { return _ataqueBase; } protected set { _ataqueBase = Math.Max(0, value); } }

        public int Energia
        {
            get { return _energia; }
            set { _energia = Math.Max(0, Math.Min(_energiaMaxima, value)); }
        }

        public int CargaEspecial
        {
            get { return _cargaEspecial; }
            set { _cargaEspecial = Math.Max(0, Math.Min(100, value)); }
        }

        public int Empatia { get { return _empatia; } set { _empatia = Math.Max(0, value); } }
        public string NombreEspecial { get { return _nombreEspecial; } protected set { _nombreEspecial = value; } }
        public string DescripcionEspecial { get { return _descripcionEspecial; } protected set { _descripcionEspecial = value; } }
        public string ArquetipoFilosofico { get { return _arquetipoFilosofico; } protected set { _arquetipoFilosofico = value; } }
        public bool EstaVivo { get { return _vidaActual > 0; } }
        public bool PuedeUsarEspecial { get { return _cargaEspecial >= 100 && _energia >= 2; } }
        #endregion

        // Constructor que encaja exactamente con tus 4 cartas
        protected Personaje(string id, string nom, Elemento elem, int vida, int escudo, int ataque, int empatia, string espNom, string espDesc, string arquetipo, Punto2D pos)
        {
            int suma = vida + escudo + ataque;
            if (suma != 100)
            {
                throw new ArgumentException(string.Format("Balance invalido para {0}: HP({1}) + DEF({2}) + ATK({3}) = {4}. La suma debe ser 100.",
                    nom, vida, escudo, ataque, suma));
            }

            IdPersonaje = id;
            Nombre = nom;
            Elemento = elem;
            VidaMaxima = vida;
            VidaActual = vida;
            EscudoBase = escudo;
            Escudo = escudo;
            AtaqueBase = ataque;
            Empatia = empatia;
            Energia = 2;
            CargaEspecial = 0;
            NombreEspecial = espNom;
            DescripcionEspecial = espDesc;
            ArquetipoFilosofico = arquetipo;
            Posicion = pos;
        }

        #region Metodos Polimorficos
        public abstract int CalcularDanio(Personaje objetivo, Escenario escenario);
        public abstract string UsarHabilidadEspecial(Personaje objetivo, Escenario escenario);

        public virtual int RecibirDanio(int danioBruto)
        {
            if (danioBruto <= 0) return 0;
            int remanente = danioBruto;
            int danioVida = 0;

            if (_escudo > 0)
            {
                if (remanente <= _escudo) { _escudo -= remanente; remanente = 0; }
                else { remanente -= _escudo; _escudo = 0; }
            }

            if (remanente > 0)
            {
                danioVida = remanente;
                VidaActual -= danioVida;
            }
            return danioVida;
        }

        public virtual void Defender()
        {
            Escudo += 15;
        }

        public virtual bool ConsumirEnergia(int costo)
        {
            if (_energia >= costo) { _energia -= costo; return true; }
            return false;
        }

        public virtual string ProcesarEmpatiaSismica(int danioBase)
        {
            Escudo = Escudo / 2;
            if (Empatia >= 60)
            {
                VidaActual += danioBase;
                Escudo += 10;
                return string.Format("[INVERSION POR EMPATIA] {0} ({1}): sintoniza con la tierra, sana +{2} HP y gana +10 DEF.",
                    Nombre, ArquetipoFilosofico, danioBase);
            }
            else
            {
                int danio = RecibirDanio(danioBase);
                return string.Format("[COLAPSO EXISTENCIAL] {0} ({1}): sufre {2} danio sismico.",
                    Nombre, ArquetipoFilosofico, danio);
            }
        }

        public abstract Personaje Clonar();

        public override string ToString()
        {
            return string.Format("{0} [{1}] - {2} | HP:{3}/{4} DEF:{5} ATK:{6} EMP:{7}",
                Nombre, Elemento, ArquetipoFilosofico, VidaActual, VidaMaxima, Escudo, AtaqueBase, Empatia);
        }
        #endregion
    }
}