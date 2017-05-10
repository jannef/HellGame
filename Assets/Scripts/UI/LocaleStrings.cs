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
		"Battle Wizard",
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
		"Höbönasut",
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
		"So the Archmage is finally dead. \nYears ago I arrived on his doorstep, full of hope, brimming with potential. Now, I can scarcely remember life outside of this place... my every breath carries the smell of old books, polished brass, and the heat of the steamworks. Every step is haunted by some clockwork horror he \"invented\" in one of his dreams. Now he's gone, and the manor has turned against me! I can hear his clockwork beasts roaming the steam pipes above, and most of the doors have locked themselves.",
		"Apprentice,The manor will turn against you here first. The automatons will try to stop you. Yet if there is anywhere to gain a foothold, it must be here in the dark and humid cellars, because it is at least free of my more ingenious security devices. But tread carefully! Many years ago, a colleague gifted me a most peculiar beast. No eyes, no mouth, yet there was no warmth it could not detect and nothing it could not consume. It grew until I could no longer contain it in the laboratory, and has remained sealed in the deepest reaches of the cellars since. It must be very hungry.",
		"Apprentice,Never have I regretted my prowess as an inventor, but if I still lived, I would regret it today. My greatest idea struck when my head was emptiest; over a late night snack. In my private dining room, one of my greatest siege works lays dormant. It was at first a marvel... but as the fevered memory of inspiration faded, weeks after its creation, it became a watchful, foreboding presence. It never stirred when I was in the room, but I began to find increasingly more piles of ash, and decidedly fewer of my cats. I've taken my meals in my chambers since.",
		"Apprentice,The secret I now pass on to you may be your doom. Deep in the library, in my private archives, there is a wellspring of infinite knowledge. For decades I have communed with it, my fingertips running over its surface, my mind fed by its mysteries and whispered propositions. At first it meant to consume my thoughts from within, but over the decades I subdued the urges it seeded in my subconscious. Most of them at least. You have no such time to spare. Whether demon, god, or spirit, you must destroy it. Though, the thought bears down on my soul with unsettling weight...",
		"Apprentice,I know not what awaits you in my private chambers, but it may well have been the agent of my fate... some powerful entity with a grudge, or simply a lust for what is mine. I have prepared you as well as I can for what's to come, but was it enough to succeed where I failed?",
		"Apprentice,",
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
		"So the Archmage is finally dead.Years ago I arrived on his doorstep, full of hope, brimming with potential. Now, I can scarcely remember life outside of this place... my every breath carries the smell of old books, polished brass, and the heat of the steamworks. Every step is haunted by some clockwork horror he \"invented\" in one of his dreams. Now he's gone, and the manor has turned against me! I can hear his clockwork beasts roaming the steam pipes above, and most of the doors have locked themselves.",
		"Apprentice,The manor will turn against you here first. The automatons will try to stop you. Yet if there is anywhere to gain a foothold, it must be here in the dark and humid cellars, because it is at least free of my more ingenious security devices. But tread carefully! Many years ago, a colleague gifted me a most peculiar beast. No eyes, no mouth, yet there was no warmth it could not detect and nothing it could not consume. It grew until I could no longer contain it in the laboratory, and has remained sealed in the deepest reaches of the cellars since. It must be very hungry.",
		"Apprentice,Never have I regretted my prowess as an inventor, but if I still lived, I would regret it today. My greatest idea struck when my head was emptiest; over a late night snack. In my private dining room, one of my greatest siege works lays dormant. It was at first a marvel... but as the fevered memory of inspiration faded, weeks after its creation, it became a watchful, foreboding presence. It never stirred when I was in the room, but I began to find increasingly more piles of ash, and decidedly fewer of my cats. I've taken my meals in my chambers since.",
		"Apprentice,The secret I now pass on to you may be your doom. Deep in the library, in my private archives, there is a wellspring of infinite knowledge. For decades I have communed with it, my fingertips running over its surface, my mind fed by its mysteries and whispered propositions. At first it meant to consume my thoughts from within, but over the decades I subdued the urges it seeded in my subconscious. Most of them at least. You have no such time to spare. Whether demon, god, or spirit, you must destroy it. Though, the thought bears down on my soul with unsettling weight...",
		"Apprentice,I know not what awaits you in my private chambers, but it may well have been the agent of my fate... some powerful entity with a grudge, or simply a lust for what is mine. I have prepared you as well as I can for what's to come, but was it enough to succeed where I failed?",
		"Apprentice,",
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
	}
}
