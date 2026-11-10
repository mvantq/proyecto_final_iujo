using System;

namespace proyecto_final_iujo
{
    // clase abstracta base para todos los personajes del juego
    public abstract class Personaje
    {
        // atributos privados encapsulados
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

        // getters y setters con validacion para no guardar valores nulos o vacios
        public string IdPersonaje { get { return _idPersonaje; } set { _idPersonaje = value ?? ""; } }
        public string Nombre { get { return _nombre; } set { _nombre = string.IsNullOrWhiteSpace(value) ? "SinNombre" : value.Trim(); } }
        public Elemento Elemento { get { return _elemento; } protected set { _elemento = value; } }

        // vida maxima asegurando minimo 1 punto
        public int VidaMaxima
        {
            get { return _vidaMaxima; }
            protected set { _vidaMaxima = Math.Max(1, value); }
        }

        // vida actual limitada entre cero y la vida maxima
        public int VidaActual
        {
            get { return _vidaActual; }
            set { _vidaActual = Math.Max(0, Math.Min(_vidaMaxima, value)); }
        }

        // alias para acceder a la vida actual
        public int Vida
        {
            get { return _vidaActual; }
            set { VidaActual = value; }
        }

        // escudo inicial del combatiente
        public int EscudoBase { get { return _escudoBase; } protected set { _escudoBase = Math.Max(0, value); } }

        // escudo actual topado a 100 maximo
        public int Escudo
        {
            get { return _escudo; }
            set { _escudo = Math.Max(0, Math.Min(_escudoMaximo, value)); }
        }

        // alias de escudo para las formulas que usan defensa
        public int Defensa
        {
            get { return _escudo; }
            set { Escudo = value; }
        }

        // estadistica de ataque base
        public int AtaqueBase { get { return _ataqueBase; } protected set { _ataqueBase = Math.Max(0, value); } }

        // alias de ataque base
        public int Ataque
        {
            get { return _ataqueBase; }
            set { AtaqueBase = value; }
        }

        // energia para lanzar habilidades limitada de 0 a 10
        public int Energia
        {
            get { return _energia; }
            set { _energia = Math.Max(0, Math.Min(_energiaMaxima, value)); }
        }

        // carga de la barra especial limitada hasta 100
        public int CargaEspecial
        {
            get { return _cargaEspecial; }
            set { _cargaEspecial = Math.Max(0, Math.Min(100, value)); }
        }

        // estadisticas de lore y habilidades
        public int Empatia { get { return _empatia; } set { _empatia = Math.Max(0, value); } }
        public string NombreEspecial { get { return _nombreEspecial; } protected set { _nombreEspecial = value; } }
        public string DescripcionEspecial { get { return _descripcionEspecial; } protected set { _descripcionEspecial = value; } }
        public string ArquetipoFilosofico { get { return _arquetipoFilosofico; } protected set { _arquetipoFilosofico = value; } }

        // banderas de estado del combatiente
        public bool EstaVivo { get { return _vidaActual > 0; } }
        public bool PuedeUsarEspecial { get { return _cargaEspecial >= 100 && _energia >= 2; } }

        #endregion

        // constructor base con regla de balance obligatoria de 100 puntos en total
        protected Personaje(string id, string nom, Elemento elem, int vida, int escudo, int ataque, int empatia, string espNom, string espDesc, string arquetipo)
        {
            // valida que vida mas escudo mas ataque sumen exactamente 100
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
        }

        #region Metodos de Soporte y Combate

        // recupera energia cada turno
        public virtual void RegenerarEnergiaTurno(int cantidad = 2)
        {
            Energia += cantidad;
        }

        // recupera vida sin pasar el tope maximo
        public virtual void RecuperarVida(int cantidad)
        {
            if (cantidad <= 0) return;
            VidaActual += cantidad;
        }

        // dano directo a la vida que ignora escudo
        public virtual void RecibirDanoPuro(int cantidad)
        {
            if (cantidad <= 0) return;
            VidaActual -= cantidad;
        }

        // metodos que implementa cada clase de elemento
        public abstract int CalcularDanio(Personaje objetivo, Escenario escenario);
        public abstract void HabilidadEspecial(Personaje objetivo);

        // procesa el impacto absorbiendo primero con escudo y luego afectando la vida
        public virtual int RecibirDanio(int danioBruto)
        {
            if (danioBruto <= 0) return 0;
            int remanente = danioBruto;
            int danioVida = 0;

            // si tiene escudo absorbe primero el golpe
            if (_escudo > 0)
            {
                if (remanente <= _escudo) { _escudo -= remanente; remanente = 0; }
                else { remanente -= _escudo; _escudo = 0; }
            }

            // lo que sobrepasa el escudo le pega directo a la vida
            if (remanente > 0)
            {
                danioVida = remanente;
                VidaActual -= danioVida;
            }
            return danioVida;
        }

        // accion defensiva que suma 15 al escudo
        public virtual void Defender()
        {
            Escudo += 15;
        }

        // descuenta energia si le alcanza
        public virtual bool ConsumirEnergia(int costo)
        {
            if (_energia >= costo) { _energia -= costo; return true; }
            return false;
        }

        // reaccion al terremoto segun el nivel de empatia
        public virtual string ProcesarEmpatiaSismica(int danioBase)
        {
            // el terremoto parte el escudo a la mitad
            Escudo = Escudo / 2;

            // con alta empatia se cura en vez de recibir dano
            if (Empatia >= 60)
            {
                VidaActual += danioBase;
                Escudo += 10;
                return string.Format("[INVERSION POR EMPATIA] {0} ({1}): sintoniza con la tierra, sana +{2} HP y gana +10 DEF.",
                    Nombre, ArquetipoFilosofico, danioBase);
            }
            else
            {
                // si no tiene empatia sufre todo el golpe sismico
                int danio = RecibirDanio(danioBase);
                return string.Format("[COLAPSO EXISTENCIAL] {0} ({1}): sufre {2} danio sismico.",
                    Nombre, ArquetipoFilosofico, danio);
            }
        }

        // clona la instancia del personaje en memoria
        public virtual Personaje Clonar()
        {
            return (Personaje)this.MemberwiseClone();
        }

        // formato de texto para ver las estadisticas completas
        public override string ToString()
        {
            return string.Format("{0} [{1}] - {2} | HP:{3}/{4} DEF:{5} ATK:{6} EMP:{7}",
                Nombre, Elemento, ArquetipoFilosofico, VidaActual, VidaMaxima, Escudo, AtaqueBase, Empatia);
        }

        #endregion
    }
}