using UnityEngine;
using System.Collections;
using System;

public static partial class LocaleStrings
{
	private static string[] CurrentLocale;

	public static readonly string[] en_EN = {
		"Press to activate ultimate power!",
		"ULTIMATE POWER!",
		"Completion Time",
		"Hits Taken",
		"Final Clear Time",
		"{0}: {1}",
		"Automatoned Diner",
		"Clockwork Express",
		"Hall of Inconvenience",
		"The Iron Chef",
		"Apprentice's Cellar",
		"Wet Treasures",
		"Slime of Death",
		"Dangerous Library",
		"Laser Danger",
		"Book Burnery",
		"Librarian's Sphere",
		"The Blue Eyes Brown Belurker",
		"The Study",
		"Retry",
		"Total Time",
		"Completed",
		"Continue",
		"Pause",
		"Fireball",
		"Meteor Dash",
		"Sounds",
		"Options",
		"Gamepad",
		"Keyboard",
		"Effects",
		"Music",
		"Exit Game",
		"Level Select",
		"Game Over",
		"Credits",
		"Forge Fury",
		"Return",
		"Press any key!",
		"Column Storage",
		"Metal Wizard",
		"Developer",
		"Contributor",
		"Archmage",
		"Battle Mage",
		"Wizard",
		"Apprentice",
		"Novice",
		"Cellar Wing",
		"Kitchen Wing",
		"Library Wing",
		"Start",
		"Quit to Title",
		"Big Daddy Belurker",
		"Monster Closet",
		"Dance Dance Incineration",
		"Laser Revolution",
		"Chambers Wing",
		"Language",
		"English",
		"Spanish",
		"Finnish",
		"Your progress is saved automatically",
		"So the Archmage is finally dead. \n\nYears ago I arrived on his doorstep, full of hope, brimming with potential. Now, I can scarcely remember life outside of this place... my every breath carries the smell of old books, polished brass, and the heat of the steamworks. Every step is haunted by some clockwork horror he \"invented\" in one of his dreams. Now he's gone, and the manor has turned against me! I can hear his clockwork beasts roaming the steam pipes above, and most of the doors have locked themselves. \n\nIt seems as though I'm about to get some exercise at last... if nothing else, I'll use all this fire and metal magic to forge myself a path out of here.",
		"Apprentice,\n\nThe manor will turn against you here first. The automatons will try to stop you. Yet if there is anywhere to gain a foothold, it must be here in the dark and humid cellars, because it is at least free of my more ingenious security devices. But tread carefully! Many years ago, a colleague gifted me a most peculiar beast. No eyes, no mouth, yet there was no warmth it could not detect and nothing it could not consume. It grew until I could no longer contain it in the laboratory, and has remained sealed in the deepest reaches of the cellars since. It must be very hungry.",
		"Apprentice,\n\nNever have I regretted my prowess as an inventor, but if I still lived, I would regret it today. My greatest idea struck when my head was emptiest; over a late night snack. In my private dining room, one of my greatest siege works lays dormant. It was at first a marvel... but as the fevered memory of inspiration faded, weeks after its creation, it became a watchful, foreboding presence. It never stirred when I was in the room, but I began to find increasingly more piles of ash, and decidedly fewer of my cats. I've taken my meals in my chambers since.",
		"Apprentice,\n\nThe secret I now pass on to you may be your doom. Deep in the library, in my private archives, there is a wellspring of infinite knowledge. For decades I have communed with it, my fingertips running over its surface, my mind fed by its mysteries and whispered propositions. At first it meant to consume my thoughts from within, but over the decades I subdued the urges it seeded in my subconscious. Most of them at least. You have no such time to spare. Whether demon, god, or spirit, you must destroy it. Though, the thought bears down on my soul with unsettling weight...",
		"Apprentice,\n\nI know not what awaits you in my private chambers, but it may well have been the agent of my fate... some powerful entity with a grudge, or simply a lust for what is mine. I have prepared you as well as I can for what's to come, but was it enough to succeed where I failed?",
		"Apprentice,",
		"German",
		"Portuguese",
		"French",
		"Italian",
		""
	};

	public static readonly string[] fi_FI = {
		"Paina nappulaa :P",
		"Nabbula baineddu :DD",
		"Suoritusaika",
		"Osumia otettu",
		"Yhteenlaskettu aika",
		"{0}: {1}",
		"Koneiden kahvila",
		"Ratahuone",
		"Logistinen Painajainen",
		"Iron Chef",
		"Oppipojan kellari",
		"Sitä märkää",
		"Kuoleman Lima",
		"Vaarojen Kirjasto",
		"Laservaara",
		"Kirjapolttamo",
		"Kirjastonhoitajan kuula",
		"Sinisilmä",
		"Opintohalli",
		"Uudestaan!",
		"Yhteisaika",
		"Päihitetty",
		"Jatka",
		"Pysäytä",
		"Tulipallo",
		"Meteorirykäisy",
		"Äänet",
		"Asetukset",
		"Padi",
		"Näppäimistö",
		"Efektit",
		"Musiikki",
		"Sulje peli",
		"Tasonvalinta",
		"Ohi on!",
		"Tekijät",
		"Säkenöivä voima",
		"Palaa",
		"Paina nappulaa",
		"Pilarivarasto",
		"Metal Wizar",
		"Kehittäjä",
		"Apustaja",
		"Arkkimaagi",
		"Sotavelho",
		"Velho",
		"Oppipoika",
		"Noviisi",
		"Kellarisiipi",
		"Keittiösiipi",
		"Kirjastosiipi",
		"Aloita",
		"Päävalikkoon",
		"Big Daddy Belurker",
		"Hirviökomero",
		"Palavan Kissan Tango",
		"Karulaaseriselli",
		"Asuinsiipi",
		"Kieli",
		"Englanti",
		"Espanja",
		"Suomi",
		"Peli tallentaa automaattisesti",
		"Vanha arkkimaagi on siis vihdoing kuollut. \n\nTämä kaameiden kojeiden ja kummallisten kokeiden pesä on ollut kotini niin kauan kuin muistan. Nyt vanhuksen keksinnöt ovat vapautuneet kahleistaan. Tutut käytävät ovat eittämättä nyt vaarallisemmat kuin koskaan. \n\n Ehkä vanhusta ehtii vielä tulla ikävä.",
		"Oppilaani,\n\nJok'ikinen keksintö, koje ja kellopeli tässä kartanossa janoaa vertasi. Tämä kellari on siivoton ja unohdettu, mutta sen vallanneet olennot pitävät sitä kotonaan. Toivon että selviät tästä ensimmäisestä koetuksestasi. Varo eritoten vanhaa kellarin vartijaa - kasvotonta ja kylmää hirviötä jonka ikeen ovat monet seikkailijat kaatuneet. En ole muistanut ruokkia kyseistä hirviötä kuuteen vuoteen, ja sen nälän oletan olevan mahtava.",
		"Oppilaani,\n\nNäin jälkeenpäin kadun kiintymystäni kuolettaviin kojeisiin. Vannoutuneiden yönaposteluhetkieni kulussa päädyin kehittämään hirvittävän sotakojeen, jonka jätin keittiön kaappien pohjalle. Juuston lailla uskon koneen kehittyneen näiden vuosien aikana, ja mitä kuolleiden kissojeni määrästä voin päätellä, uskon kehityksen olleen turmiollista. Olen sittemmin siirtynyt aterioimaan makuuhuoneeni puolella.",
		"Oppilaani,\n\nOstin nuorena oppilaana kiertelevältä kauppiaalta ikuisen tiedon kuulan. Mitä tulee tämän suunnattoman kirjaston järjestelemiseen, se on ollut erittäin hyvä apuri. Epäilen sen kuitenkin olevan kyltymättömän tiedon demoni, joka on kirjaston tiedon avulla kasvattanut mahtiaan. Olen pahoillani, mutta jätän tämän olennon sinun vastuullesi. En raaskinut sitä surmata, sillä huolimatta sen pahasta sisimmästä, sen kantama viisaus on kaunimpaa kuin yhdeksän ihmiselämää.",
		"Rakas oppilaani\n\nEn tiedä mikä hirviö odottaa makuutilassani, mutta se on varmasti ollut surmaajani. Olen valmistellut sinua tätä viimeistä koitosta varten, mutta lopulta teräksen voima nousee palavasta sielustasi. Toivon koko sydämestäni, että onnistut siinä mikä minut kaatoi.",
		"Oppilaani,",
		"Saksa",
		"Portugali",
		"Ranska",
		"Italia",
		""
	};

	public static readonly string[] es_ES = {
		"presiona el botón",
		"Poder ultimo",
		"Tiempo De Finalización",
		"Hits Tomados",
		"Tiempo Final Para Borrar",
		"{0}: {1}",
		"Café de Machcines",
		"Cogexpress",
		"Hall Inconveniente",
		"El Chef De Hierro",
		"Bodega Del Aprendiz",
		"Tesoros Húmedos",
		"Limo De La Muerte",
		"Biblioteca Peligrosa",
		"Peligro Láser",
		"Incinerador De Libros",
		"Esfera Del Bibliotecario",
		"Ojos Azules, Fantasma Marrón",
		"El Estudio",
		"Rever",
		"Tiempo Total",
		"Terminado",
		"Continuar",
		"Pausa",
		"Bola De Fuego",
		"Embestida Meteorito",
		"Sonidos",
		"Opciones",
		"Gamepad",
		"Teclado",
		"Efectos",
		"Música",
		"Dejar",
		"Nivel Seleccionado",
		"Juego Terminado",
		"Créditos",
		"Forjar La Furia",
		"Regreso",
		"Presiona Cualquier Tecla",
		"Almacenamiento De Columnas",
		"Metal Wizard",
		"Desarrollador",
		"Contribuyente",
		"Archimago",
		"Mago De Batalla",
		"Mago",
		"Aprendiz",
		"Principiante",
		"Bodegas",
		"Cocina",
		"Biblioteca",
		"Comienzo",
		"Al menú principal",
		"Gran Papá Fantasma",
		"Armario Monstruo",
		"Dance Dance Incineración",
		"Revolución Láser",
		"Cámaras",
		"Lenguaje",
		"Inglés",
		"Español",
		"Finlandés",
		"Tu progreso se guarda automáticamente",
		"Así que el Archimago está finalmente muerto. \n\nHace años llegué a su puerta, lleno de esperanza, rebosante de potencial. Ahora, apenas puedo recordar la vida fuera de este lugar ... cada respiración lleva el olor de los libros viejos, bronce pulido, y el calor de los steamworks. Cada paso está atormentado por un horror de relojería que él \"inventó\" en uno de sus sueños. Ahora se ha ido, y la casa se ha vuelto contra mí! Puedo oír a sus bestias de relojería recorrer las tuberías de vapor por encima, y ​​la mayoría de las puertas se han bloqueado. \n\nParece como si estuviera a punto de hacer algún ejercicio por fin ... si nada más, usaré toda esta magia de fuego y metal para forjarme un camino fuera de aquí.",
		"Aprendiz, \n\nLa mansión se volverá contra ti aquí primero. Los autómatas intentarán detenerte. Sin embargo, si hay algún lugar para ganar un punto de apoyo, debe estar aquí en las bodegas oscuras y húmedas, porque al menos está libre de mis ingeniosos dispositivos de seguridad. ¡Pero pise con cuidado! Hace muchos años, un colega me regaló una bestia muy peculiar. Sin ojos, sin boca, pero no había calor que no pudiera detectar y nada que no pudiera consumir. Creció hasta que ya no podía contenerla en el laboratorio, y ha permanecido sellada en los alcances más profundos de las bodegas desde entonces. Debe estar muy hambriento.",
		"Aprendiz, \n\nNunca me he arrepentido de mi destreza como inventor, pero si todavía vivía, me arrepentiría hoy. Mi idea más grande golpeó cuando mi cabeza estaba más vacía; Sobre un bocado de la noche. En mi comedor privado, una de mis mayores obras de asedio permanece inactiva. Fue al principio una maravilla ... pero a medida que el febril recuerdo de la inspiración se desvaneció, semanas después de su creación, se convirtió en una presencia vigilante y presagiosa. Nunca se movió cuando estaba en la habitación, pero empecé a encontrar cada vez más pilas de cenizas, y decididamente menos de mis gatos. He tomado mis comidas en mis cámaras desde entonces.",
		"Aprendiz, \n\nEl secreto que ahora te transmito puede ser tu perdición. Profundamente en la biblioteca, en mis archivos privados, hay una fuente de conocimiento infinito. Durante décadas he hablado con él, con las yemas de mis dedos corriendo sobre su superficie, mi mente alimentada por sus misterios y proposiciones susurradas. Al principio significaba consumir mis pensamientos desde dentro, pero durante las décadas subjugué los impulsos que sembraba en mi subconsciente. La mayoría de ellos al menos. No tienes tiempo de sobra. Ya sea demonio, dios o espíritu, debes destruirlo. Sin embargo, el pensamiento se apaga en mi alma con un peso inquietante ...",
		"Aprendiz, \n\nNo sé lo que os espera en mis cámaras privadas, pero bien pudo haber sido el agente de mi destino ... una entidad poderosa con rencor, o simplemente una lujuria por lo que es mío. Te he preparado lo mejor que he podido para lo que vendrá, pero ¿fue suficiente para tener éxito donde fracasé?",
		"Aprendiz,",
		"Alemán",
		"Portugués",
		"Francés",
		"Italiano",
		""
	};

	public static readonly string[] de_DE = {
		"Drücken Sie, um die ultimative Kraft zu aktivieren!",
		"ULTIMATIVE KRAFT!",
		"Fertigstellungszeit",
		"Hits Getroffen",
		"Endgültige Zeit",
		"{0}: {1}",
		"Automatisierte Diner",
		"Uhrwerk Ausdrücklich",
		"Hall of Unbequemlichkeit",
		"Der Eiserne Chef",
		"Lehrlingskeller",
		"Nasse Schätze",
		"Schleim des Todes",
		"Gefährliche Bibliothe",
		"Laser-Gefahr",
		"Buchbrenner",
		"Bibliothekarsphäre",
		"Die Blauen Augen Braunes Monster",
		"Die Studium",
		"Wiederholen",
		"Gesamtzeit",
		"Abgeschlossen",
		"Fortsetzen",
		"Pause",
		"Feuerball",
		"Meteorschlag",
		"Geräusche",
		"Optionen",
		"Spielcontroller",
		"Tastatur",
		"Auswirkungen",
		"Musik",
		"Spiel Verlassen",
		"Ebene Auswählen",
		"Spiel Ist Aus",
		"Gutschriften",
		"Schmeißen",
		"Rückkehr",
		"Drücke Irgendeine Taste",
		"Säulenlagerung",
		"Metall-Zauberer",
		"Entwickler",
		"Mitwirkender",
		"Erzmagier",
		"Schlacht Magier",
		"Zauberer",
		"Lehrling",
		"Anfänger",
		"Kellerflügel",
		"Küchenflügel",
		"Bibliothek flügel",
		"Anfang",
		"Zurück Zum Titel",
		"Großes Vatermonster",
		"Monster Schrank",
		"Tanzverbrennung",
		"Laser-Revolution",
		"Kammern Flügel",
		"Sprache",
		"Englisch",
		"Spanisch",
		"Finnisch",
		"Ihr fortschritt wird automatisch gespeichert",
		"So ist der Erzmagier endlich tot. \n\nVor Jahren kam ich vor der Haustür, voller Hoffnung, voller Potenzial. Jetzt kann ich mich kaum an das Leben außerhalb dieses Ortes erinnern ... mein Atem trägt den Geruch von alten Büchern, poliertem Messing und die Hitze der Dampfwerke. Jeder Schritt wird von einem Uhrwerk Horror verfolgt er \"erfunden\" in einem seiner Träume. Jetzt ist er weg, und das Herrenhaus hat sich gegen mich gewandt! Ich höre seine Uhrwerk-Tiere, die die Dampfpfeifen überstreifen, und die meisten Türen haben sich verschlossen. \n\nEs scheint, als ob ich endlich etwas üben würde ... wenn nichts anderes, werde ich all dieses Feuer und Metall Magie verwenden, um mir einen Weg von hier zu schmieden.",
		"Lehrling, \n\nDas Herrenhaus wird sich hier zuerst gegen dich wenden. Die Automaten werden versuchen, Sie zu stoppen. Doch wenn es irgendwo einen Fuß zu fassen gibt, muss es hier in den dunklen und feuchten Kellern sein, denn es ist wenigstens frei von meinen genialeren Sicherheitsgeräten. Aber vorsichtig vorgehen! Vor vielen Jahren hat mir ein Kollege ein eigenartiges Tier geschenkt. Keine Augen, kein Mund, aber es gab keine Wärme, die es nicht erkennen konnte und nichts, was es nicht verbrauchen konnte. Es wuchs, bis ich es nicht mehr im Labor aufnehmen konnte und seitdem in den tiefsten Wegen der Keller versiegelt ist. Es muss sehr hungrig sein.",
		"Lehrling, \n\nNiemals habe ich meine Tapferkeit als Erfinder bereut, aber wenn ich noch lebte, würde ich es heute bereuen. Meine größte Idee schlug, als mein Kopf leer war; Über einen späten Nacht-Snack. In meinem privaten Speisesaal legt eine meiner größten Belagerungsarbeiten still. Es war zuerst ein Wunder ... aber als die fieberhafte Erinnerung an die Inspiration verblaßte, Wochen nach ihrer Schöpfung wurde es zu einer wachsamen, vorzeitigen Gegenwart. Es war niemals gerührt, als ich im Zimmer war, aber ich fing an, immer mehr Haufen von Asche zu finden, und entschieden weniger von meinen Katzen. Ich habe meine Mahlzeiten in meinen Kammern seitdem genommen.",
		"Lehrling, \n\nDas Geheimnis, das ich jetzt weitergebe, kann dein Schicksal sein. Tief in der Bibliothek, in meinem privaten Archiv, gibt es eine Quelle von unendlichem Wissen. Seit Jahrzehnten habe ich mit ihm kommuniziert, meine Fingerspitzen über seine Oberfläche, mein Geist von seinen Mysterien gefüttert und flüsterte Sätze. Zuerst bedeutete es, meine Gedanken von innen zu verbrauchen, aber über die Jahrzehnte unterwarf ich die Triebe, die es in meinem Unterbewusstsein seeded Die meisten von ihnen zumindest. Sie haben keine solche Zeit zu ersparen. Ob Dämon, Gott oder Geist, du musst es zerstören. Obwohl der Gedanke auf meine Seele mit beunruhigendem Gewicht ...",
		"Lehrling, \n\nIch weiß nicht, was dich in meinen privaten Kammern erwartet, aber es ist wohl der Agent meines Schicksals gewesen ... eine mächtige Einheit mit einem Groll oder einfach nur eine Lust auf das, was mir gehört. Ich habe dich so gut vorbereitet wie ich kann, was kommt, aber war es genug, um erfolgreich zu sein, wo ich versagte?",
		"Lehrling,",
		"Deutsche",
		"Portugiesisch",
		"Französisch",
		"Italienisch",
		""
	};

	public static readonly string[] pt_PT = {
		"Prima para activar a potência máxima!",
		"PODER FINAL!",
		"Tempo de Conclusão",
		"Hits Tomados",
		"Tempo Final de Limpeza",
		"{0}: {1}",
		"Jantar Automatizado",
		"Relógio Expresso",
		"Hall da Inconveniência",
		"O Cozinheiro Chefe de Ferro",
		"Adega do Aprendiz",
		"Tesouros Molhados",
		"Slime da Morte",
		"Biblioteca Perigosa",
		"Perigo de laser",
		"Gravação de livros",
		"Esfera do Bibliotecário",
		"Os Olhos Azuis Marrom Belurker",
		"O Estudo",
		"Repetir",
		"Tempo Total",
		"Concluído",
		"Continuar",
		"Pausa",
		"Bola Fogo",
		"Colisão de Meteoros",
		"Sons",
		"Opções",
		"Controle de Vídeo Game",
		"Teclado",
		"Efeitos",
		"Música",
		"Sair do Jogo",
		"Seleção de Nível",
		"Fim de Jogo",
		"Créditos",
		"Forjar Fúria",
		"Retorna",
		"Pressione Qualquer Tecla",
		"Armazenamento de Coluna",
		"Assistente de Metal",
		"Desenvolvedor",
		"Contribuinte",
		"Arquimago",
		"Mago de Batalha",
		"Bruxo",
		"Aprendiz",
		"Novato",
		"Adega",
		"Asa da Cozinha",
		"Ala da Biblioteca",
		"Começar",
		"Sair Para o Título",
		"Grande Paizinho Belurker",
		"Armário do Monstro",
		"Dance, Dance, Incineração",
		"Revolução laser",
		"Asa das Câmaras",
		"Língua",
		"Inglês",
		"Espanhol",
		"Finlandês",
		"Seu progresso é salvo automaticamente",
		"Então o Archimago finalmente morreu. \n\nAnos atrás eu cheguei à sua porta, cheio de esperança, repleto de potencial. Agora, eu mal consigo me lembrar da vida fora deste lugar ... cada respiração carrega o cheiro de livros antigos, latão polido, eo calor do steamworks. Cada passo é assombrado por algum horror de relógio que ele \"inventou\" em um de seus sonhos. Agora ele se foi, e a mansão se virou contra mim! Eu posso ouvir suas bestas de relógio vagando pelas tubulações de vapor acima, ea maioria das portas se trancaram. \n\nParece que estou prestes a fazer algum exercício, afinal ... se nada mais, vou usar toda essa magia de fogo e metal para me forjar um caminho para fora daqui.",
		"Aprendiz, \n\nA mansão se voltará contra você aqui primeiro. Os autômatos vão tentar pará-lo. No entanto, se há algum lugar para ganhar uma posição, deve estar aqui nas adegas escuras e úmidas, porque é pelo menos livre de meus dispositivos de segurança mais engenhosos. Mas pise com cuidado! Muitos anos atrás, um colega me deu uma besta mais peculiar. Sem olhos, sem boca, mas não havia calor que não pudesse detectar e nada que não pudesse consumir. Cresceu até que eu não podia mais contê-lo em laboratório, e permaneceu selado nos alcances mais profundos das adegas desde então. Deve estar com muita fome.",
		"Aprendiz, \n\nNunca me arrependi das minhas proezas como inventor, mas se eu ainda vivesse, eu me arrependeria hoje. Minha maior idéia atingiu quando minha cabeça estava mais vazia; Sobre um lanche tarde da noite. Na minha sala de jantar privada, um dos meus maiores trabalhos de cerco fica adormecido. Foi no início uma maravilha ... mas como a memória febril de inspiração desbotada, semanas após a sua criação, tornou-se uma presença vigilante, presunçoso. Nunca me mexia quando eu estava na sala, mas eu comecei a encontrar cada vez mais pilhas de cinzas, e decididamente menos de meus gatos. Eu tenho tomado minhas refeições em minhas câmaras desde então.",
		"Aprendiz, \n\nO segredo que agora passo para você pode ser seu destino. No fundo da biblioteca, em meus arquivos particulares, há uma fonte de conhecimento infinito. Por décadas eu comuniquei com ele, minhas pontas do dedo que funcionam sobre sua superfície, minha mente alimentada por seus mistérios e proposições whispered. No início, isso significava consumir meus pensamentos de dentro, mas ao longo das décadas eu subjuguei os impulsos que semeavam no meu subconsciente. A maioria deles pelo menos. Você não tem tempo de sobra. Seja demônio, deus ou espírito, você deve destruí-lo. Embora, o pensamento carrega para baixo em minha alma com peso inquietante ...",
		"Aprendiz, \n\nEu não sei o que o espera em minhas câmaras privadas, mas pode muito bem ter sido o agente do meu destino ... alguma entidade poderosa com rancor, ou simplesmente um desejo pelo que é meu. Eu preparei você o melhor que posso para o que está para vir, mas foi o suficiente para ter sucesso onde eu falhei?",
		"Aprendiz,",
		"Alemão",
		"Português",
		"Francês",
		"Italiano",
		""
	};

	public static readonly string[] fr_FR = {
		"Appuyez pour activer la puissance ultime!",
		"POUVOIR ULTIME!",
		"Le Temps d'Achèvement",
		"Hits Pris",
		"Temps Clair Final",
		"{0}: {1}",
		"Diner Automatisé",
		"Express Mécanique",
		"Salle des Inconvénients",
		"Le Chef de Fer",
		"Cave Aux Apprentis",
		"Trésors Humides",
		"Boue de Mort",
		"Bibliothèque Dangereuse",
		"Danger Laser",
		"Graveur de Livres",
		"Sphère du Bibliothécaire",
		"Les Yeux Bleus Brun Belurker",
		"L'étude",
		"Recommencez",
		"Temps Total",
		"Terminé",
		"Continuer",
		"Pause",
		"Boule de Feu",
		"Tiret de Météorite",
		"Des Sons",
		"Les Options",
		"Une Manette",
		"Clavier",
		"Effets",
		"La Musique",
		"Quitter le Jeu",
		"Sélection De Niveau",
		"Jeu Terminé",
		"Crédits",
		"Forger la Fureur",
		"Revenir",
		"Appuyez Sur Une Touche",
		"Stockage des Colonnes",
		"Magicien de Métal",
		"Développeur",
		"Donateur",
		"Archimage",
		"Mage de Combat",
		"Sorcier",
		"Apprenti",
		"Novice",
		"Aile de Cave",
		"Aile de Cuisine",
		"Aile de la Bibliothèque",
		"Début",
		"Quitter le Titre",
		"Grand Papa Belurker",
		"Garde-Robe",
		"Dance, Dance, Incinération",
		"Révolution Laser",
		"Aile des Chambres",
		"la Langue",
		"Anglais",
		"Espanol",
		"Finlandais",
		"Votre progression est enregistrée automatiquement",
		"L'archimage est finalement mort. \n\nIl y a des années, je suis arrivé à la porte, plein d'espoir, débordant de potentiel. Maintenant, je peux à peine me souvenir de la vie en dehors de cet endroit ... chaque souffle porte l'odeur des vieux livres, du laiton poli et de la chaleur de la vapeur. Chaque étape est hantée par une horreur de l'horloge qu'il \"a inventé\" dans l'un de ses rêves. Maintenant, il est parti, et le manoir s'est retourné contre moi! Je peux entendre ses bêtes d'horlogerie errant dans les pipes à vapeur ci-dessus, et la plupart des portes se sont enfermées. \n\nIl semble que je suis sur le point de faire de l'exercice enfin ... si rien d'autre, j'utiliserai tout ce feu et la magie des métaux pour me forcer un chemin ici.",
		"Apprenti, \n\nLe manoir se tournera contre vous ici d'abord. Les automates essayeront de vous arrêter. Pourtant, s'il y a un point de vue, il doit être ici dans les caves sombres et humides, car il est au moins libre de mes dispositifs de sécurité plus ingénieux. Mais marchez prudemment! Il y a plusieurs années, un collègue m'a donné une bête très particulière. Pas d'yeux, pas de bouche, mais il n'y avait pas de chaleur qu'il ne pouvait pas détecter et rien qu'il ne pouvait pas consommer. Il a grandi jusqu'à ce que je ne puisse plus le contenir dans le laboratoire, et est resté scellé dans la partie la plus profonde des caves depuis. Il doit être très faim.",
		"Apprenti, \n\nJe n'ai jamais regretté ma prouesse en tant qu'inventeur, mais si je vivais encore, je le regretterais aujourd'hui. Ma plus grande idée a frappé lorsque ma tête était plus vide; Sur une collation tard dans la nuit. Dans ma salle à manger privée, l'un de mes plus grands travaux de siège est dormant. C'était d'abord une merveille ... mais à mesure que le souvenir fougueux de l'inspiration disparaissait, des semaines après sa création, il devenait une présence vigilante et pressante. Il ne s'est jamais agité quand j'étais dans la pièce, mais j'ai commencé à trouver de plus en plus de tas de cendres et de beaucoup moins de mes chats. J'ai pris mes repas dans mes chambres depuis.",
		"Apprenti, \n\nLe secret que je vous transmets peut-être votre sort. Au fond de la bibliothèque, dans mes archives privées, il y a une source de connaissances infinies. Pendant des décennies, je me suis entretenu avec elle, mes doigts dépassant sa surface, mon esprit alimenté par ses mystères et ses propos chuchotés. Au début, cela voulait consommer mes pensées de l'intérieur, mais au cours des décennies, je subissais les impulsions qu'elle faisait naître dans mon subconscient. La plupart d'entre eux au moins. Vous n'avez pas de temps à épargner. Que ce soit démon, dieu ou esprit, vous devez le détruire. Pourtant, la pensée porte sur mon âme avec un poids inquiétant ...",
		"Apprenti, \n\nJe ne sais pas ce qui t'attend dans mes chambres privées, mais il a peut-être été l'agent de mon destin ... une entité puissante avec une rancune, ou simplement une convoitise pour ce qui est à moi. Je vous ai préparé aussi bien que possible pour ce qui est venu, mais a-t-il suffi de réussir là où j'ai échoué?",
		"Apprenti,",
		"Allemand",
		"Portugais",
		"Français",
		"Italien",
		""
	};

	public static readonly string[] it_IT = {
		"Premere per attivare la potenza massima!",
		"Potere ultimo!",
		"Tempo di Completamento",
		"Colpi Effettuati",
		"Tempo Libero Finale",
		"{0}: {1}",
		"Cenone Automatico",
		"Orologio Espresso",
		"Sala di Disagio",
		"Lo Chef Del Ferro",
		"Cantina Apprendista",
		"Tesori Bagnati",
		"La Melma Della Morte",
		"Libreria Pericolosa",
		"Pericolo Laser",
		"Bruciatura del Libro",
		"Sfera dei Bibliotecari",
		"Occhi Azzurri Belurker Marrone",
		"Lo Studio",
		"Riprovare",
		"Tempo Totale",
		"Completato",
		"Continua",
		"Pausa",
		"Bolide",
		"Meteorite Trattino",
		"Suoni",
		"Opzioni",
		"Controller",
		"Tastiera",
		"Effetti",
		"Musica",
		"Uscire dal Gioco",
		"Selezione del Livello",
		"Gioco Finito",
		"Crediti",
		"Forgiare la Furia",
		"Ritorno",
		"Premere un tasto qualsiasi",
		"Memorizzazione Della Colonna",
		"Metallo Guidato",
		"Sviluppatore",
		"Collaboratore",
		"Arcimago",
		"Magia Battaglia",
		"Procedura Guidata",
		"Apprendista",
		"Novizio",
		"Cantina",
		"Ala Cucina",
		"Biblioteca Ala",
		"Inizio",
		"Uscire Dal Titolo",
		"Grande Papà Belurker",
		"Armadio Mostro",
		"Dance, Dance, Incenerimento",
		"Rivoluzione Laser",
		"Ali delle Camere",
		"Lingua",
		"Inglese",
		"Spagnolo",
		"Finlandese",
		"I tuoi progressi vengono salvati automaticamente",
		"Quindi l'Archmage è finalmente morto. \n\nAnni fa sono arrivata sulla soglia, piena di speranza, piena di potenzialità. Ora, non posso ricordare la vita al di fuori di questo posto ... ogni mio respiro porta l'odore di vecchi libri, l'ottone lucido e il caldo dei vapori. Ogni passo è infestato da un orrore di orologeria che ha \"inventato\" in uno dei suoi sogni. Ora è andato, e il castello si è rivolto contro di me! Posso sentire le sue bestie di orologeria che vagano i tubi di vapore sopra, e la maggior parte delle porte si sono bloccate. \n\nSembra che dovrei finalmente fare un esercizio ... se non altro, utilizzerò tutta questa magia di fuoco e metallo per forzarmi un percorso di qui.",
		"Apprendista, \n\nIl castello si rivolge qui prima. Gli automi cercheranno di fermarti. Eppure se c'è da guadagnare un punto d'appoggio, deve essere qui nelle cantine buie e umide, perché è almeno libera dai miei più ingegnosi dispositivi di sicurezza. Ma seguite con attenzione! Molti anni fa, un collega mi ha regalato una bestia peculiare. Nessun occhi, nessuna bocca, ma non c'era calore che non riusciva a rilevare e niente che non potesse consumare. Cresce fino a che non posso più contenerlo in laboratorio, e da allora è rimasto sigillato nelle più profonde strade delle cantine. Deve essere molto affamato.",
		"Apprendista, \n\nNon ho mai dispiaciuto la mia abilità come inventore, ma se ancora vivo, lo dispiacerei oggi. La mia idea più grande colpì quando la mia testa era più svuotata; Su uno spuntino di tarda notte. Nella mia sala da pranzo privata, una delle mie opere d'assedio più grandi si pone in silenzio. Era in un primo momento una meraviglia ... ma come la memoria fonte di ispirazione svanì, settimane dopo la sua creazione, divenne una presenza vigile e preoccupante. Non mi ha mai agitato quando ero in camera, ma ho cominciato a trovare sempre più pile di cenere e decisamente meno dei miei gatti. Da allora ho preso i miei pasti nelle mie camere.",
		"Apprendista, \n\nIl segreto che ora ti passa per te può essere il tuo destino. Profondo nella biblioteca, nei miei archivi privati, c'è una sorgente di conoscenza infinita. Per decenni ho parlato con lui, le dita delle mie mani che corrono sulla sua superficie, la mia mente nutrita dai suoi misteri e dalle proposte bisbigliate. In un primo momento intendeva consumare i miei pensieri dall'interno, ma nel corso dei decenni ho sottomesso le sollecitazioni che sono state seminate nel mio subconscio. La maggior parte di loro almeno. Non hai tempo da risparmiare. Se sia demone, dio o spirito, devi distruggerlo. Sebbene il pensiero abbia la mia anima con un peso inquietante ...",
		"Apprendista, \n\nNon so cosa ti aspetta nelle mie camere private, ma forse è stato l'agente del mio destino ... una potente entità con un rancore, o semplicemente una lussuria per ciò che è mio. Ti ho preparato, come posso per quello che deve venire, ma è stato sufficiente per avere successo quando ho fallito?",
		"Apprendista,",
		"Tedesco",
		"Portoghese",
		"Francese",
		"Italiano",
		""
	};

	public static string UI_LimitBreakPrompt { get { return CurrentLocale[0]; } }

	public static string UI_LimitBreakNotification { get { return CurrentLocale[1]; } }

	public static string UI_SCORE_COMPTIME { get { return CurrentLocale[2]; } }

	public static string UI_SCORE_HITSTAKEN { get { return CurrentLocale[3]; } }

	public static string UI_SCORE_TOTALTIME { get { return CurrentLocale[4]; } }

	public static string UI_SCORE_TEASER { get { return CurrentLocale[5]; } }

	public static string UI_ROOM_KITCHEN_1 { get { return CurrentLocale[6]; } }

	public static string UI_ROOM_KITCHEN_2 { get { return CurrentLocale[7]; } }

	public static string UI_ROOM_KITCHEN_3 { get { return CurrentLocale[8]; } }

	public static string UI_ROOM_KITCHEN_BOSS { get { return CurrentLocale[9]; } }

	public static string UI_ROOM_CELLAR_1 { get { return CurrentLocale[10]; } }

	public static string UI_ROOM_CELLAR_2 { get { return CurrentLocale[11]; } }

	public static string UI_ROOM_CELLAR_BOSS { get { return CurrentLocale[12]; } }

	public static string UI_ROOM_LIBRARY_1 { get { return CurrentLocale[13]; } }

	public static string UI_ROOM_LIBRARY_2 { get { return CurrentLocale[14]; } }

	public static string UI_ROOM_LIBRARY_3 { get { return CurrentLocale[15]; } }

	public static string UI_ROOM_LIBRARY_BOSS { get { return CurrentLocale[16]; } }

	public static string UI_ROOM_CHAMBERS_BOSS { get { return CurrentLocale[17]; } }

	public static string UI_ROOM_LEVELSELECTHUB { get { return CurrentLocale[18]; } }

	public static string UI_MENU_RETRY { get { return CurrentLocale[19]; } }

	public static string UI_HIGHSCORE_TOTALTIME { get { return CurrentLocale[20]; } }

	public static string UI_HIGHSCORE_COMPLETIONPERCENT { get { return CurrentLocale[21]; } }

	public static string UI_MENU_CONTINUE { get { return CurrentLocale[22]; } }

	public static string UI_MENU_PAUSE { get { return CurrentLocale[23]; } }

	public static string UI_MENU_FIRE { get { return CurrentLocale[24]; } }

	public static string UI_MENU_DASH { get { return CurrentLocale[25]; } }

	public static string UI_MENU_SOUND { get { return CurrentLocale[26]; } }

	public static string UI_MENU_OPTIONS { get { return CurrentLocale[27]; } }

	public static string UI_MENU_GAMEPAD { get { return CurrentLocale[28]; } }

	public static string UI_MENU_KEYBOARD { get { return CurrentLocale[29]; } }

	public static string UI_MENU_SFX { get { return CurrentLocale[30]; } }

	public static string UI_MENU_MUSIC { get { return CurrentLocale[31]; } }

	public static string UI_MENU_QUIT { get { return CurrentLocale[32]; } }

	public static string UI_MENU_LEVELSELECT { get { return CurrentLocale[33]; } }

	public static string UI_MENU_GAMEOVER { get { return CurrentLocale[34]; } }

	public static string UI_MENU_CREDITS { get { return CurrentLocale[35]; } }

	public static string UI_MENU_LIMIT { get { return CurrentLocale[36]; } }

	public static string UI_MENU_RETURN { get { return CurrentLocale[37]; } }

	public static string UI_MENU_ANYKEY { get { return CurrentLocale[38]; } }

	public static string UI_ROOM_CELLAR_3 { get { return CurrentLocale[39]; } }

	public static string UI_GAME_GAMETITLE { get { return CurrentLocale[40]; } }

	public static string UI_CREDIT_DEV { get { return CurrentLocale[41]; } }

	public static string UI_CREDIT_CONT { get { return CurrentLocale[42]; } }

	public static string RANK_S { get { return CurrentLocale[43]; } }

	public static string RANK_A { get { return CurrentLocale[44]; } }

	public static string RANK_B { get { return CurrentLocale[45]; } }

	public static string RANK_C { get { return CurrentLocale[46]; } }

	public static string RANK_D { get { return CurrentLocale[47]; } }

	public static string UI_ROOM_CELLAR_LEVELSELECT { get { return CurrentLocale[48]; } }

	public static string UI_ROOM_KITCHEN_LEVELSELECT { get { return CurrentLocale[49]; } }

	public static string UI_ROOM_LIBRARY_LEVELSELECT { get { return CurrentLocale[50]; } }

	public static string UI_MENU_PLAYGAME { get { return CurrentLocale[51]; } }

	public static string UI_MENU_QUITTOTITLE { get { return CurrentLocale[52]; } }

	public static string UI_ROOM_HEROIC_BELURKER { get { return CurrentLocale[53]; } }

	public static string UI_ROOM_CHAMBERS_1 { get { return CurrentLocale[54]; } }

	public static string UI_ROOM_CHAMBERS_2 { get { return CurrentLocale[55]; } }

	public static string UI_ROOM_CHAMBERS_3 { get { return CurrentLocale[56]; } }

	public static string UI_ROOM_CHAMBERS_LEVELSELECT { get { return CurrentLocale[57]; } }

	public static string UI_LANGUAGE { get { return CurrentLocale[58]; } }

	public static string UI_LANGUAGE_ENGLISH { get { return CurrentLocale[59]; } }

	public static string UI_LANGUAGE_SPANISH { get { return CurrentLocale[60]; } }

	public static string UI_LANGUAGE_FINNISH { get { return CurrentLocale[61]; } }

	public static string UI_MENU_AUTOSAVEREMINDER { get { return CurrentLocale[62]; } }

	public static string UI_LORE_LEVELSELECT { get { return CurrentLocale[63]; } }

	public static string UI_LORE_CELLAR_LEVELSELECT { get { return CurrentLocale[64]; } }

	public static string UI_LORE_KITCHEN_LEVELSELECT { get { return CurrentLocale[65]; } }

	public static string UI_LORE_LIBRARY_LEVELSELECT { get { return CurrentLocale[66]; } }

	public static string UI_LORE_CHAMBERS_LEVELSELECT { get { return CurrentLocale[67]; } }

	public static string UI_LORE_GENERAL_APPRENTICE { get { return CurrentLocale[68]; } }

	public static string UI_LANGUAGE_GERMAN { get { return CurrentLocale[69]; } }

	public static string UI_LANGUAGE_PORTUGESE { get { return CurrentLocale[70]; } }

	public static string UI_LANGUAGE_FRENCH { get { return CurrentLocale[71]; } }

	public static string UI_LANGUAGE_ITALIAN { get { return CurrentLocale[72]; } }

	public enum StringsEnum : int
	{
		UI_LimitBreakPrompt=0,
		UI_LimitBreakNotification=1,
		UI_SCORE_COMPTIME=2,
		UI_SCORE_HITSTAKEN=3,
		UI_SCORE_TOTALTIME=4,
		UI_SCORE_TEASER=5,
		UI_ROOM_KITCHEN_1=6,
		UI_ROOM_KITCHEN_2=7,
		UI_ROOM_KITCHEN_3=8,
		UI_ROOM_KITCHEN_BOSS=9,
		UI_ROOM_CELLAR_1=10,
		UI_ROOM_CELLAR_2=11,
		UI_ROOM_CELLAR_BOSS=12,
		UI_ROOM_LIBRARY_1=13,
		UI_ROOM_LIBRARY_2=14,
		UI_ROOM_LIBRARY_3=15,
		UI_ROOM_LIBRARY_BOSS=16,
		UI_ROOM_CHAMBERS_BOSS=17,
		UI_ROOM_LEVELSELECTHUB=18,
		UI_MENU_RETRY=19,
		UI_HIGHSCORE_TOTALTIME=20,
		UI_HIGHSCORE_COMPLETIONPERCENT=21,
		UI_MENU_CONTINUE=22,
		UI_MENU_PAUSE=23,
		UI_MENU_FIRE=24,
		UI_MENU_DASH=25,
		UI_MENU_SOUND=26,
		UI_MENU_OPTIONS=27,
		UI_MENU_GAMEPAD=28,
		UI_MENU_KEYBOARD=29,
		UI_MENU_SFX=30,
		UI_MENU_MUSIC=31,
		UI_MENU_QUIT=32,
		UI_MENU_LEVELSELECT=33,
		UI_MENU_GAMEOVER=34,
		UI_MENU_CREDITS=35,
		UI_MENU_LIMIT=36,
		UI_MENU_RETURN=37,
		UI_MENU_ANYKEY=38,
		UI_ROOM_CELLAR_3=39,
		UI_GAME_GAMETITLE=40,
		UI_CREDIT_DEV=41,
		UI_CREDIT_CONT=42,
		RANK_S=43,
		RANK_A=44,
		RANK_B=45,
		RANK_C=46,
		RANK_D=47,
		UI_ROOM_CELLAR_LEVELSELECT=48,
		UI_ROOM_KITCHEN_LEVELSELECT=49,
		UI_ROOM_LIBRARY_LEVELSELECT=50,
		UI_MENU_PLAYGAME=51,
		UI_MENU_QUITTOTITLE=52,
		UI_ROOM_HEROIC_BELURKER=53,
		UI_ROOM_CHAMBERS_1=54,
		UI_ROOM_CHAMBERS_2=55,
		UI_ROOM_CHAMBERS_3=56,
		UI_ROOM_CHAMBERS_LEVELSELECT=57,
		UI_LANGUAGE=58,
		UI_LANGUAGE_ENGLISH=59,
		UI_LANGUAGE_SPANISH=60,
		UI_LANGUAGE_FINNISH=61,
		UI_MENU_AUTOSAVEREMINDER=62,
		UI_LORE_LEVELSELECT=63,
		UI_LORE_CELLAR_LEVELSELECT=64,
		UI_LORE_KITCHEN_LEVELSELECT=65,
		UI_LORE_LIBRARY_LEVELSELECT=66,
		UI_LORE_CHAMBERS_LEVELSELECT=67,
		UI_LORE_GENERAL_APPRENTICE=68,
		UI_LANGUAGE_GERMAN=69,
		UI_LANGUAGE_PORTUGESE=70,
		UI_LANGUAGE_FRENCH=71,
		UI_LANGUAGE_ITALIAN=72,
	}
}
