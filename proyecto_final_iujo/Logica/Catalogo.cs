using System.Collections.Generic;

namespace proyecto_final_iujo
{
    
	public static class CatalogoCartas
    {

        public static List<Personaje> ObtenerPersonajes()
        {
            List<Personaje> listaPersonajes = new List<Personaje>();

            // FUEGO
            
            // Personaje 1: Ares (El dios de la contienda belica)
            // Balance: HP: 45 + DEF: 20 + ATK: 35 = 100 pts | Empatia: 30
            // Concepto: El choque directo, la pulsion violenta y la angustia canalizada en combate.
            listaPersonajes.Add(new CartaFuego("FUE-01", "Ares el Beligerante", 45, 20, 35, 30, 
                "Furia de Maraton", 
                "Quiebra escudo por impacto igneo directo", 
                "Homo Volens: Pasion y Lucha Tragica"
            ));

            // Personaje 2: Hefesto (El dios herrero y artifice de la tecnica)
            // Balance: HP: 50 + DEF: 30 + ATK: 20 = 100 pts | Empatia: 25
            // Concepto: El "Homo Faber", la capacidad del hombre de transformar la materia mediante el trabajo y el dolor.
            listaPersonajes.Add(new CartaFuego("FUE-02", "Hefesto de la Fragua", 50, 30, 20, 25, 
                "Martillo de Lemnos", 
                "Funde armaduras con calor primordial", 
                "Homo Faber: El Sentido del Trabajo"
            ));

            // Personaje 3: Hestia (La guardiana del fuego interior del hogar)
            // Balance: HP: 55 + DEF: 25 + ATK: 20 = 100 pts | Empatia: 45
            // Concepto: La cohesion social y el limite moral que resguarda a la comunidad frente a la intemperie.
           listaPersonajes.Add(new CartaFuego("FUE-03", "Hestia la Custodia", 55, 25, 20, 45, 
                "Llama Sacrificial", 
                "Deflagracion defensiva que consume hostilidades", 
                "Voluntad Ignea: Comunidad y Limite"
            ));

            // AGUA: 
            
            // Personaje 4: Poseidon (Senor de las aguas turbulentas y abisales)
            // Balance: HP: 50 + DEF: 30 + ATK: 20 = 100 pts | Empatia: 65
            // Concepto: Los abismos psiquicos del hombre y la memoria inconsciente que no se puede dominar.
            listaPersonajes.Add(new CartaAgua("AGU-01", "Poseidon Senor del Abismo", 50, 30, 20, 65, 
                "Colera del Egeo", 
                "Impacto desestabilizador de marea envolvente", 
                "Homo Compassivus: La Profundidad del Ser"
            ));

            // Personaje 5: Anfitrite (Reina del mar y simbolo de serenidad)
            // Balance: HP: 55 + DEF: 30 + ATK: 15 = 100 pts | Empatia: 80 
            // Concepto: La alteridad levinasiana: la mirada del otro que exige cuidado, empatia y restitucion de vida.
            listaPersonajes.Add(new CartaAgua("AGU-02", "Anfitrite la Conciliadora", 55, 30, 15, 80, 
                "Marea Restauradora", 
                "Sana puntos de vida y disuelve la agresividad rival", 
                "Alteridad: La Mirada del Otro"
            ));

            // Personaje 6: Proteo (El dios metamorfico de las costas)
            // Balance: HP: 45 + DEF: 25 + ATK: 30 = 100 pts | Empatia: 70
            // Concepto: El "Homo Fluxus", la naturaleza humana maleable que aprende a adaptarse a cada adversidad.
            listaPersonajes.Add(new CartaAgua("AGU-03", "Proteo el Cambiante", 45, 25, 30, 70, 
                "Fluidez Inmutable", 
                "Amortiguacion hidrostatica que neutraliza ataques continuos", 
                "Homo Fluxus: Metamorfosis Existencial"
            ));

          	// TIERRA:
          	
            // Personaje 7: Gea (La madre tierra primigenia)
            // Balance: HP: 60 + DEF: 30 + ATK: 10 = 100 pts | Empatia: 85
            // Concepto: La matriz fundante que sostiene la vida; transfigura la catastrofe telurica en absorcion vital.
            listaPersonajes.Add(new CartaTierra("TIE-01", "Gea Matriz Telurica", 60, 30, 10, 85, 
                "Bastion de Raices", 
                "Comunion con el suelo que bloquea fracturas y sismos", 
                "Homo Radicatus: Arraigo Ontologico"
            ));

            // Personaje 8: Hades (El custodio de las profundidades y del reposo final)
            // Balance: HP: 50 + DEF: 35 + ATK: 15 = 100 pts | Empatia: 75
            // Concepto: El recordatorio heideggeriano del "ser-para-la-muerte": la finitud que dota de valor a la vida.
            listaPersonajes.Add(new CartaTierra("TIE-02", "Hades Guardian del Umbral", 50, 35, 15, 75, 
                "Juicio del Aqueronte", 
                "Presion tectonica inamovible que desgasta al atacante", 
                "Finitud: La Angustia ante la Muerte"
            ));

            // Personaje 9: Demeter (Diosa de los ciclos agricolas y las estaciones)
            // Balance: HP: 55 + DEF: 25 + ATK: 20 = 100 pts | Empatia: 80
            // Concepto: La tradicion y el retorno: el aprendizaje colectivo que sobrevive a los inviernos de la historia.
            listaPersonajes.Add(new CartaTierra("TIE-03", "Demeter de la Espiga", 55, 25, 20, 80, 
                "Muralla Fertil", 
                "Impacto denso por absorcion de nutrientes terrestres", 
                "Tradicion: El Legado que Permanece"
            ));

			// AIRE:

            // Personaje 10: Zeus (Soberano de las alturas y la justicia cosmica)
            // Balance: HP: 45 + DEF: 20 + ATK: 35 = 100 pts | Empatia: 45
            // Concepto: La aspiracion a un orden superior (Logos) que busca estructurar el sentido etico del mundo.
            listaPersonajes.Add(new CartaAire("AIR-01", "Zeus Soberano del Olimpo", 45, 20, 35, 45, 
                "Rayo Celestial", 
                "Aceleracion sonica perforante que traspasa defensas base", 
                "Homo Viator: La Busqueda del Logos"
            ));

            // Personaje 11: Hermes (El dios mensajero, viajero y cruzador de umbrales)
            // Balance: HP: 40 + DEF: 20 + ATK: 40 = 100 pts | Empatia: 60
            // Concepto: El caminante incansable: la transgresion de los limites de la aldea y el rechazo al aislamiento dogmatico.
            listaPersonajes.Add(new CartaAire("AIR-02", "Hermes el Caminante", 40, 20, 40, 60, 
                "Paso de la Frontera", 
                "Rafaga eolica que elude el bloqueo convencional", 
                "El Nomada: Ruptura de Empalizadas"
            ));

            // Personaje 12: Atenea (Diosa de la sabiduria lucida y la estrategia)
            // Balance: HP: 50 + DEF: 25 + ATK: 25 = 100 pts | Empatia: 55
            // Concepto: La razon prudente: la capacidad reflexiva del ser humano para no sucumbir a impulsos destructivos.
            listaPersonajes.Add(new CartaAire("AIR-03", "Atenea de la Sabiduria", 50, 25, 25, 55, 
                "Viento de la Prudencia", 
                "Corte certero estructurado por analisis tactico", 
                "Trascendencia: La Razon Lucida"
            ));

            return listaPersonajes;
        }

   
        // Genera el catalogo de los 4 escenarios elementales correspondientes a cada faccion.
        // Los escenarios otorgan bonificaciones (Buffs) o penalizaciones (Nerfs) matematicaS,, y representan el marco ambiental donde se manifiestan los dilemas filosoficos del juego
        public static List<Escenario> ObtenerEscenarios()
        {
            List<Escenario> esc = new List<Escenario>();

            // Escenario Fuego: Aumenta la agresividad fisica a expensas de la defensa
            esc.Add(new Escenario("ESC-01", "Volcan Activo", Elemento.Fuego, 6, 0, 
                "El Devenir Heracliteo: El fuego consume la estabilidad material; el ataque prevalece sobre la forma."
            ));

            // Escenario Agua: Otorga mitigacion y fluidez compartida
            esc.Add(new Escenario("ESC-02", "Abismo Oceanico", Elemento.Agua, 5, 5, 
                "La Fluidez Intersubjetiva: El mar disipa la violencia egoica; fortalece el escudo y la resistencia."
            ));

            // Escenario Tierra: Bonifica fuertemente el blindaje y el arraigo defensivo
            esc.Add(new Escenario("ESC-03", "Canion Rocoso", Elemento.Tierra, 0, 10, 
                "El Arraigo Ontologico: La roca impone la finitud de las fuerzas; la defensa se vuelve inquebrantable."
            ));

            // Escenario Aire/Viento: Maximiza la velocidad ofensiva y la ruptura de escudos
            esc.Add(new Escenario("ESC-04", "Cumbre de Viento", Elemento.Aire, 7, 0, 
            "La Trascendencia del Espiritu: El viento ignora las ataduras terrestres; la iniciativa y el impacto se disparan."
            ));

            return esc;
        }
    }
}