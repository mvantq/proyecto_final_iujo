using System;
using System.Collections.Generic;

namespace proyecto_final_iujo
{
    public enum Lenguaje
    {
        ES = 0,
        EN = 1
    }

    public static partial class Idioma
    {
        static Lenguaje actual = Lenguaje.ES;

        public static event EventHandler Cambiado;

        public static Lenguaje Actual
        {
            get { return actual; }
            set
            {
                if (actual == value) return;
                actual = value;
                EventHandler h = Cambiado;
                if (h != null) h(null, EventArgs.Empty);
            }
        }

        public static bool EsIngles { get { return actual == Lenguaje.EN; } }

        public static void Alternar()
        {
            Actual = (actual == Lenguaje.ES) ? Lenguaje.EN : Lenguaje.ES;
        }

        public static string T(string clave)
        {
            string[] v;
            if (textos.TryGetValue(clave, out v)) return v[(int)actual];
            return "[" + clave + "]";
        }

        public static string T(string clave, params object[] args)
        {
            string plantilla = T(clave);
            if (args == null || args.Length == 0) return plantilla;
            return string.Format(plantilla, args);
        }

        static readonly Dictionary<string, string[]> textos = new Dictionary<string, string[]>
        {
            { "app.titulo", new[] { "Arena Elemental", "Elemental Arena" } },
            { "jugador.n", new[] { "Jugador {0}", "Player {0}" } },
            { "abr.j", new[] { "J{0}", "P{0}" } },
            { "tipo.personaje", new[] { "Personaje", "Character" } },
            { "tipo.escenario", new[] { "Escenario", "Scenario" } },
            { "abr.stats", new[] { "V{0} D{1} A{2} E{3}", "HP{0} D{1} A{2} E{3}" } },
            { "abr.escenario", new[] { "+{0} ATQ · +{1} DEF", "+{0} ATK · +{1} DEF" } },

            { "inicio.titulo", new[] { "ARENA ELEMENTAL", "ELEMENTAL ARENA" } },
            { "inicio.subtitulo", new[] { "Fuego · Agua · Tierra · Aire  —  el pacto de la Aldea se ha roto",
                                          "Fire · Water · Earth · Air  —  the Village pact has been broken" } },
            { "inicio.jugar", new[] { "JUGAR", "PLAY" } },
            { "inicio.salir", new[] { "Salir", "Exit" } },
            { "inicio.idioma", new[] { "Idioma: ES  |  EN", "Language: EN  |  ES" } },
            { "inicio.creditos", new[] { "Proyecto Final IUJO  ·  Combate por turnos con Colas, Cálculo y POO",
                                         "IUJO Final Project  ·  Turn-based combat with Queues, Calculus and OOP" } },

            { "lore.titulo", new[] { "Crónicas de la Aldea", "Chronicles of the Village" } },
            { "lore.pagina", new[] { "Capítulo {0} de {1}", "Chapter {0} of {1}" } },
            { "lore.anterior", new[] { "Anterior", "Previous" } },
            { "lore.siguiente", new[] { "Siguiente", "Next" } },
            { "lore.continuar", new[] { "Continuar a Selección", "Next: Team Selection" } },

            { "sel.titulo", new[] { "Preparación del Combate", "Battle Preparation" } },
            { "sel.usuario", new[] { "Usuario", "Username" } },
            { "sel.clave", new[] { "Clave", "Password" } },
            { "sel.ingresar", new[] { "Ingresar", "Log in" } },
            { "sel.registrar", new[] { "Registrar", "Sign up" } },
            { "sel.cerrarsesion", new[] { "Cerrar sesión", "Log out" } },
            { "sel.estado.desconectado", new[] { "Sin sesión. Ingresa o regístrate.", "Not logged in. Log in or sign up." } },
            { "sel.estado.conectado", new[] { "Conectado como {0}", "Logged in as {0}" } },
            { "sel.err.formato", new[] { "Usuario de 3-16 letras/números y clave de mínimo 3 caracteres.",
                                         "Username: 3-16 letters/digits; password: at least 3 characters." } },
            { "sel.err.mismo", new[] { "Ese usuario ya está conectado como el otro jugador.", "That user is already logged in as the other player." } },
            { "sel.err.clave", new[] { "Clave incorrecta.", "Wrong password." } },
            { "sel.err.noexiste", new[] { "El usuario no existe. Pulsa Registrar.", "User not found. Press Sign up." } },
            { "sel.err.existe", new[] { "Ese usuario ya está registrado. Pulsa Ingresar.", "That user already exists. Press Log in." } },
            { "sel.err.io", new[] { "Error de almacenamiento: {0}", "Storage error: {0}" } },
            { "sel.catalogo", new[] { "Catálogo · 16 cartas (12 personajes + 4 escenarios)", "Catalog · 16 cards (12 characters + 4 scenarios)" } },
            { "sel.detalle", new[] { "Vista previa de la carta", "Card preview" } },
            { "sel.agregar", new[] { "+ Equipo {0}", "+ Team {0}" } },
            { "sel.quitar", new[] { "Quitar", "Remove" } },
            { "sel.vaciar", new[] { "Vaciar", "Clear" } },
            { "sel.aleatorio", new[] { "Azar", "Random" } },
            { "sel.equipo", new[] { "Equipo de {0}  —  {1}/{2} cartas", "{0}'s team  —  {1}/{2} cards" } },
            { "sel.col.carta", new[] { "Carta", "Card" } },
            { "sel.col.tipo", new[] { "Tipo", "Type" } },
            { "sel.col.vida", new[] { "Vida", "HP" } },
            { "sel.col.def", new[] { "Def", "Def" } },
            { "sel.col.atq", new[] { "Atq", "Atk" } },
            { "sel.col.emp", new[] { "Emp", "Emp" } },
            { "sel.col.habilidad", new[] { "Habilidad especial", "Special ability" } },
            { "sel.modalidad", new[] { "Modalidad", "Mode" } },
            { "sel.local", new[] { "Local (misma PC)", "Local (same PC)" } },
            { "sel.red", new[] { "Red LAN / Web", "LAN / Web" } },
            { "sel.red.aviso", new[] { "El modo Red LAN/Web aún no tiene servidor implementado.\nLa partida se jugará localmente en esta PC.",
                                       "LAN/Web mode has no server implemented yet.\nThe match will be played locally on this PC." } },
            { "sel.iniciar", new[] { "¡Iniciar Batalla!", "Start Battle!" } },
            { "sel.volver", new[] { "Menú", "Menu" } },
            { "sel.req", new[] { "{0}: sesión {1} · equipo {2}/5 {3}\n{4}: sesión {5} · equipo {6}/5 {7}",
                                 "{0}: login {1} · team {2}/5 {3}\n{4}: login {5} · team {6}/5 {7}" } },
            { "sel.ayuda", new[] { "Cada equipo: 4 personajes + 1 escenario.", "Each team: 4 characters + 1 scenario." } },
            { "sel.err.sel", new[] { "Selecciona primero una carta del catálogo.", "Select a card from the catalog first." } },
            { "sel.err.duplicado", new[] { "Esa carta ya está en el equipo.", "That card is already in the team." } },
            { "sel.err.lleno", new[] { "El equipo ya tiene 4 personajes.", "The team already has 4 characters." } },
            { "sel.err.escenario", new[] { "El equipo ya tiene un escenario. Quítalo primero.", "The team already has a scenario. Remove it first." } },
            { "sel.err.quitar", new[] { "Selecciona una fila del equipo para quitarla.", "Select a team row to remove it." } },
            { "sel.ok.agregada", new[] { "{0} se unió al equipo de {1}.", "{0} joined {1}'s team." } },
            { "sel.det.personaje", new[] {
                "Vida: {0}     Defensa: {1}     Ataque: {2}\nEmpatía: {3}     Σ base = {4} {5}\nPosición en la arena: {6}\n\nHabilidad especial: {7}\n{8}",
                "HP: {0}     Defense: {1}     Attack: {2}\nEmpathy: {3}     Σ base = {4} {5}\nArena position: {6}\n\nSpecial ability: {7}\n{8}" } },
            { "sel.det.escenario", new[] {
                "Bono de ataque: +{0}\nBono de defensa: +{1}\nPotencia a los personajes de {2}.\n\n{3}",
                "Attack bonus: +{0}\nDefense bonus: +{1}\nEmpowers {2} characters.\n\n{3}" } },

            { "bat.titulo", new[] { "Tablero de Batalla", "Battle Board" } },
            { "bat.turno", new[] { "Turno {0}  ·  Juega: {1}", "Turn {0}  ·  Playing: {1}" } },
            { "bat.info", new[] { "Modo: {0}   ·   Santuarios: {1} vs {2}", "Mode: {0}   ·   Sanctuaries: {1} vs {2}" } },
            { "bat.modo.local", new[] { "Local", "Local" } },
            { "bat.modo.red", new[] { "Red LAN/Web (simulado en local)", "LAN/Web (simulated locally)" } },
            { "bat.atacar", new[] { "Atacar", "Attack" } },
            { "bat.defender", new[] { "Defender", "Defend" } },
            { "bat.especial", new[] { "Habilidad Especial", "Special Ability" } },
            { "bat.relevar", new[] { "Relevar", "Swap" } },
            { "bat.rendirse", new[] { "Rendirse", "Surrender" } },
            { "bat.acciones", new[] { "Acciones de {0}", "{0}'s actions" } },
            { "bat.vida", new[] { "Vida", "Health" } },
            { "bat.escudo", new[] { "Escudo", "Shield" } },
            { "bat.energia", new[] { "Energía", "Energy" } },
            { "bat.carga", new[] { "Carga especial", "Special charge" } },
            { "bat.lista", new[] { "LISTA", "READY" } },
            { "bat.banca", new[] { "Banca de relevos · Queue<Personaje>", "Relief bench · Queue<Personaje>" } },
            { "bat.bancavacia", new[] { "(sin relevos disponibles)", "(no substitutes left)" } },
            { "bat.consola", new[] { "Consola de Operaciones · Bitácora logarítmica", "Operations Console · Logarithmic log" } },
            { "bat.enturno", new[] { "EN TURNO", "ON TURN" } },
            { "bat.arena.info", new[] { "d = {0:0.00} u   ·   ƒ({1}) = 1 + 0.15·ln({1}) = {2:0.000}",
                                        "d = {0:0.00} u   ·   ƒ({1}) = 1 + 0.15·ln({1}) = {2:0.000}" } },
            { "bat.confirmar.rendirse", new[] { "¿{0} desea rendirse?", "Does {0} want to surrender?" } },
            { "bat.confirmar.salir", new[] { "La batalla está en curso. ¿Abandonar y cerrar el juego?", "The battle is in progress. Quit and close the game?" } },

            { "log.inicio", new[] { "=== Comienza la batalla en la Arena Elemental ===", "=== The battle in the Elemental Arena begins ===" } },
            { "log.santuario", new[] { "{0} consagra el santuario {1} ({2}): +{3} ATQ a sus aliados de ese elemento, +{4} DEF al entrar.",
                                       "{0} consecrates the {1} sanctuary ({2}): +{3} ATK for allies of that element, +{4} DEF on entry." } },
            { "log.sorteo", new[] { "Sorteo de los Ancianos: comienza {0}.", "Elders' draw: {0} goes first." } },
            { "log.entra", new[] { "{0} envía a {1} a la arena.", "{0} sends {1} into the arena." } },
            { "log.relevo.forzado", new[] { "Relevo forzado: {0} envía a {1} desde la banca.", "Forced swap: {0} sends {1} from the bench." } },
            { "log.bono.santuario", new[] { "   {0} resuena con {2}: +{1} DEF.", "   {0} resonates with {2}: +{1} DEF." } },
            { "log.turno", new[] { "── Turno {0} · {1} con {2} ──", "── Turn {0} · {1} with {2} ──" } },
            { "log.ataque", new[] { "{0} ataca a {1} (d = {3:0.00} u). Daño base por derivadas/límites: {2}.",
                                    "{0} attacks {1} (d = {3:0.00} u). Base damage from derivatives/limits: {2}." } },
            { "log.formula", new[] { "   ƒ(t) = 1 + 0.15·ln(t) → ƒ({0}) = {1:0.000} | ƒ'(t) = 0.15/t = {2:0.000} | daño = round(base·ƒ) = {3}",
                                     "   ƒ(t) = 1 + 0.15·ln(t) → ƒ({0}) = {1:0.000} | ƒ'(t) = 0.15/t = {2:0.000} | damage = round(base·ƒ) = {3}" } },
            { "log.impacto", new[] { "   {0} pierde {1} HP; su escudo absorbe {2}.", "   {0} loses {1} HP; shield absorbs {2}." } },
            { "log.defensa", new[] { "{0} adopta postura defensiva: +{1} DEF, +1 energía.", "{0} takes a defensive stance: +{1} DEF, +1 energy." } },
            { "log.especial", new[] { "¡{0} desata [{1}]!", "{0} unleashes [{1}]!" } },
            { "log.especial.efecto", new[] { "   Efecto → {0}: {1:+0;-0;0} HP, {2:+0;-0;0} DEF | {3}: {4:+0;-0;0} HP, {5:+0;-0;0} DEF",
                                             "   Effect → {0}: {1:+0;-0;0} HP, {2:+0;-0;0} DEF | {3}: {4:+0;-0;0} HP, {5:+0;-0;0} DEF" } },
            { "log.especial.no", new[] { "{0} no puede usar su habilidad (carga {1}/100, energía {2}/2).", "{0} cannot use the ability (charge {1}/100, energy {2}/2)." } },
            { "log.relevo", new[] { "{0} retira a {1} al final de la cola de relevos.", "{0} sends {1} to the back of the relief queue." } },
            { "log.sinrelevo", new[] { "{0} no tiene personajes en la banca.", "{0} has no characters on the bench." } },
            { "log.tiempo", new[] { "[TIEMPO] {0} agotó su tiempo: pierde el turno ({1} queda inmóvil).", "[TIME] {0} ran out of time: turn lost ({1} stands still)." } },
            { "log.rendicion", new[] { "{0} se rinde ante los Ancianos.", "{0} surrenders before the Elders." } },
            { "log.caido", new[] { "[KO] {0} ({1}) ha caído.", "[KO] {0} ({1}) has fallen." } },
            { "log.terremoto", new[] { "[!] ¡TERREMOTO EN LA ALDEA! La tierra despierta su memoria...", "[!] EARTHQUAKE IN THE VILLAGE! The earth awakens its memory..." } },
            { "log.terremoto.formula", new[] { "   M(t) = 2 + ln(t) → M({0}) = {1:0.00} | dM/dt = 1/t = {2:0.000} | daño sísmico = round(4·M) = {3}",
                                               "   M(t) = 2 + ln(t) → M({0}) = {1:0.00} | dM/dt = 1/t = {2:0.000} | seismic damage = round(4·M) = {3}" } },
            { "log.cambio", new[] { "   {0} (EMP {1}): {2:+0;-0;0} HP, {3:+0;-0;0} DEF", "   {0} (EMP {1}): {2:+0;-0;0} HP, {3:+0;-0;0} DEF" } },
            { "log.campana", new[] { "La campana de los Ancianos resuena: ambos campeones ganan +1 energía y +15 carga.",
                                     "The Elders' bell rings: both champions gain +1 energy and +15 charge." } },
            { "log.victoria", new[] { "*** {0} conquista la Arena Elemental ***", "*** {0} conquers the Elemental Arena ***" } },

            { "vic.titulo", new[] { "¡VICTORIA!", "VICTORY!" } },
            { "vic.ganador", new[] { "{0} conquista la Arena Elemental", "{0} conquers the Elemental Arena" } },
            { "vic.rendicion", new[] { "(su rival se rindió)", "(the rival surrendered)" } },
            { "vic.felicitacion", new[] { "Los Ancianos de la Aldea honran tu nombre. El equilibrio de los cuatro elementos ha sido restaurado y el bosque vuelve a guardar silencio.",
                                          "The Village Elders honor your name. The balance of the four elements has been restored and the woods fall silent once more." } },
            { "vic.resumen", new[] { "Resumen de la partida  ·  {0} turnos  ·  duración {1}  ·  modo {2}",
                                     "Match summary  ·  {0} turns  ·  duration {1}  ·  mode {2}" } },
            { "vic.col.estadistica", new[] { "Estadística", "Statistic" } },
            { "vic.fila.danio", new[] { "Daño total infligido", "Total damage dealt" } },
            { "vic.fila.turnos", new[] { "Turnos jugados", "Turns played" } },
            { "vic.fila.relevos", new[] { "Relevos realizados", "Swaps performed" } },
            { "vic.fila.especiales", new[] { "Habilidades especiales", "Special abilities" } },
            { "vic.fila.caidos", new[] { "Personajes caídos", "Fallen characters" } },
            { "vic.fila.santuario", new[] { "Santuario (escenario)", "Sanctuary (scenario)" } },
            { "vic.guardado.ok", new[] { "Estadísticas guardadas en BD y Log .txt exportado", "Stats saved to DB and .txt log exported" } },
            { "vic.guardado.error", new[] { "No se pudo guardar: {0}", "Could not save: {0}" } },
            { "vic.sindatos", new[] { "No hay resultados de partida.", "No match results." } },
            { "vic.ruta", new[] { "Log: {0}", "Log: {0}" } },
            { "vic.mvp", new[] { "Último en pie:\n{0}", "Last one standing:\n{0}" } },
            { "vic.menu", new[] { "Volver al Menú Principal", "Back to Main Menu" } },
            { "vic.salir", new[] { "Salir", "Exit" } },

            { "txt.titulo", new[] { "ARENA ELEMENTAL — BITÁCORA DE PARTIDA", "ELEMENTAL ARENA — MATCH LOG" } },
            { "txt.fecha", new[] { "Fecha: {0}", "Date: {0}" } },
            { "txt.jugadores", new[] { "Jugadores: {0} vs {1}   (modo {2})", "Players: {0} vs {1}   (mode {2})" } },
            { "txt.ganador", new[] { "Ganador: {0} {1}", "Winner: {0} {1}" } },
            { "txt.turnos", new[] { "Turnos jugados: {0}   Duración: {1}", "Turns played: {0}   Duration: {1}" } },
            { "txt.stats", new[] { "{0}: daño {1}, relevos {2}, especiales {3}, caídos {4}", "{0}: damage {1}, swaps {2}, specials {3}, fallen {4}" } },
            { "txt.bitacora", new[] { "---- Bitácora de operaciones ----", "---- Operations log ----" } },

            { "partida.inicio", new[] { "¡COMIENZA EL COMBATE POR EL DESTINO DE LA ALDEA!", "THE BATTLE FOR THE DESTINY OF THE VILLAGE BEGINS!" } },
            { "partida.vic.total", new[] { "¡VICTORIA TOTAL! Has derrotado a los 5 personajes del rival. La frontera de La Aldea está a salvo.", "TOTAL VICTORY! You have defeated all 5 rival characters. The Village border is safe." } },
            { "partida.vic.magna", new[] { "Victoria Magna", "Magna Victory" } },
            { "partida.ko.rival", new[] { "¡K.O.! El rival {0} ha caído. El rival debe enviar a su siguiente combatiente.", "K.O.! Rival {0} has fallen. The rival must send their next combatant." } },
            { "partida.baja.enemiga", new[] { "Baja Enemiga", "Enemy Casualty" } },
            { "partida.atk.fuego", new[] { "Golpe Ígneo", "Igneous Strike" } },
            { "partida.def.tierra", new[] { "Escudo Rocoso", "Rocky Shield" } },
            { "partida.esp.agua", new[] { "Marea (+{0} HP)", "Tide (+{0} HP)" } },
            { "partida.rel.aire", new[] { "Ráfaga de Relevo", "Relay Gust" } },
            { "partida.sismo", new[] { "¡SISMO EN LA ALDEA! {0} causó {1}. Recibes {2} daño sísmico.", "EARTHQUAKE IN THE VILLAGE! {0} dealt {1}. You take {2} seismic damage." } },
            { "partida.log.turno", new[] { "[T{0}] {1} ({2} daño | mult x{3:0.0}). Recibes {4} de daño (Fatiga x{5:0.0}).", "[T{0}] {1} ({2} damage | mult x{3:0.0}). You take {4} damage (Fatigue x{5:0.0})." } },
            { "partida.ko.heroe", new[] { "¡TU HÉROE HA CAÍDO! {0} ya no puede combatir. Relevo forzado automático.", "YOUR HERO HAS FALLEN! {0} can no longer fight. Automatic forced relay." } },
            { "partida.caido.combate", new[] { "Caído en Combate", "Fallen in Combat" } },
            { "partida.relevo.tactico", new[] { "¡RELEVO TÁCTICO! Entra {0} ({1}). Descansas y disipas fatiga.", "TACTICAL RELAY! {0} ({1}) enters. You rest and dissipate fatigue." } },
            { "partida.nom.jugador", new[] { "{0} [{1}/5]", "{0} [{1}/5]" } },
            { "partida.nom.rival", new[] { "RIVAL: {0} [{1}/5]", "RIVAL: {0} [{1}/5]" } },
            { "partida.nom.jugador.hp", new[] { "{0} ({1} HP) [{2} en banca]", "{0} ({1} HP) [{2} on bench]" } },
            { "partida.nom.rival.hp", new[] { "RIVAL: {0} ({1} HP) [{2} en banca]", "RIVAL: {0} ({1} HP) [{2} on bench]" } },
            { "partida.energia", new[] { "ENERGÍA: 5\nTURNO {0}", "ENERGY: 5\nTURN {0}" } },
            { "fin.derrota", new[] { "DERROTA", "DEFEAT" } },
            { "fin.sub.victoria", new[] { "EL PACTO DE LA ALDEA HA SIDO PRESERVADO POR {0} (TURNO {1})", "THE VILLAGE PACT HAS BEEN PRESERVED BY {0} (TURN {1})" } },
            { "fin.sub.derrota", new[] { "TU ESCUADRÓN HA CAÍDO ANTE LA ESPESURA DEL BOSQUE (TURNO {0})", "YOUR SQUAD HAS FALLEN TO THE THICKET OF THE FOREST (TURN {0})" } },

            // Botones de acción traducibles en FormPartida
            { "btn.atacar", new[] { "ATACAR (2 ENER)", "ATTACK (2 ENER)" } },
            { "btn.defender", new[] { "DEFENDER (1 ENER)", "DEFEND (1 ENER)" } },
            { "btn.especial", new[] { "ESPECIAL (2 ENER)", "SPECIAL (2 ENER)" } },
            { "btn.relevo", new[] { "RELEVO / PASAR", "SWAP / PASS" } }
        };
    }
}