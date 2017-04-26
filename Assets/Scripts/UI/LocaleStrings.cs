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
		"Next Rank\n{0}",
		"Automatoned Diner",
		"Olde Columns",
		"Vanilla Room",
		"The Iron Chef",
		"Apprentice's Cellar",
		"Column Storage",
		"Wet Treasures",
		"The Puddler",
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
		"Paused",
		"Fireball",
		"Teleport",
		"Sounds",
		"Options",
		"Gamepad",
		"Keyboard",
		"Effects",
		"Music",
		"Quit",
		"Level select",
		"Game Over",
		"Credits",
		"Ultimate power!",
		"Return",
		"Press any key!",
		""
	};

	public static readonly string[] fi_FI = {
		"Paina nappulaa :P",
		"Nabbula baineddu :DD",
		"Suoritusaika",
		"Osumia otettu",
		"Yhteenlaskettu aika",
		"Seuraava taso\n{0}",
		"Koneiden kahvila",
		"Vanha Tolppa",
		"Perushuone",
		"Iron Chef",
		"Oppipojan kellari",
		"Pilarivarasto",
		"Sitä märkää",
		"Läikyttäjä",
		"Vaarojen Kirjasto",
		"Laservaara",
		"Kirjapolttamo",
		"Kirjastonhoitajan kuula",
		"Sinisilmä",
		"Opintohalli",
		"Uudestaan!!!",
		"Yhteisaika",
		"Päihitetty",
		"Jatka",
		"Pysäytetty",
		"Tulipallo",
		"Hyppy",
		"Äänet",
		"Asetukset",
		"Padi",
		"Näppäimistö",
		"Efektit",
		"Musiikki",
		"Poistu",
		"Tasonvalinta",
		"Ohi on!",
		"Höbönasut",
		"VOIMAAAAAA!",
		"Palaa",
		"Paina nappulaa :P",
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

	public static string UI_ROOM_CELLAR_3 { get { return CurrentLocale[11]; } }

	public static string UI_ROOM_CELLAR_2 { get { return CurrentLocale[12]; } }

	public static string UI_ROOM_CELLAR_BOSS { get { return CurrentLocale[13]; } }

	public static string UI_ROOM_LIBRARY_1 { get { return CurrentLocale[14]; } }

	public static string UI_ROOM_LIBRARY_2 { get { return CurrentLocale[15]; } }

	public static string UI_ROOM_LIBRARY_3 { get { return CurrentLocale[16]; } }

	public static string UI_ROOM_LIBRARY_BOSS { get { return CurrentLocale[17]; } }

	public static string UI_ROOM_CHAMBERS_BOSS { get { return CurrentLocale[18]; } }

	public static string UI_ROOM_LEVELSELECTHUB { get { return CurrentLocale[19]; } }

	public static string UI_MENU_RETRY { get { return CurrentLocale[20]; } }

	public static string UI_HIGHSCORE_TOTALTIME { get { return CurrentLocale[21]; } }

	public static string UI_HIGHSCORE_COMPLETIONPERCENT { get { return CurrentLocale[22]; } }

	public static string UI_MENU_CONTINUE { get { return CurrentLocale[23]; } }

	public static string UI_MENU_PAUSE { get { return CurrentLocale[24]; } }

	public static string UI_MENU_FIRE { get { return CurrentLocale[25]; } }

	public static string UI_MENU_DASH { get { return CurrentLocale[26]; } }

	public static string UI_MENU_SOUND { get { return CurrentLocale[27]; } }

	public static string UI_MENU_OPTIONS { get { return CurrentLocale[28]; } }

	public static string UI_MENU_GAMEPAD { get { return CurrentLocale[29]; } }

	public static string UI_MENU_KEYBOARD { get { return CurrentLocale[30]; } }

	public static string UI_MENU_SFX { get { return CurrentLocale[31]; } }

	public static string UI_MENU_MUSIC { get { return CurrentLocale[32]; } }

	public static string UI_MENU_QUIT { get { return CurrentLocale[33]; } }

	public static string UI_MENU_LEVELSELECT { get { return CurrentLocale[34]; } }

	public static string UI_MENU_GAMEOVER { get { return CurrentLocale[35]; } }

	public static string UI_MENU_CREDITS { get { return CurrentLocale[36]; } }

	public static string UI_MENU_LIMIT { get { return CurrentLocale[37]; } }

	public static string UI_MENU_RETURN { get { return CurrentLocale[38]; } }

	public static string UI_MENU_ANYKEY { get { return CurrentLocale[39]; } }

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
		UI_ROOM_CELLAR_3=11,
		UI_ROOM_CELLAR_2=12,
		UI_ROOM_CELLAR_BOSS=13,
		UI_ROOM_LIBRARY_1=14,
		UI_ROOM_LIBRARY_2=15,
		UI_ROOM_LIBRARY_3=16,
		UI_ROOM_LIBRARY_BOSS=17,
		UI_ROOM_CHAMBERS_BOSS=18,
		UI_ROOM_LEVELSELECTHUB=19,
		UI_MENU_RETRY=20,
		UI_HIGHSCORE_TOTALTIME=21,
		UI_HIGHSCORE_COMPLETIONPERCENT=22,
		UI_MENU_CONTINUE=23,
		UI_MENU_PAUSE=24,
		UI_MENU_FIRE=25,
		UI_MENU_DASH=26,
		UI_MENU_SOUND=27,
		UI_MENU_OPTIONS=28,
		UI_MENU_GAMEPAD=29,
		UI_MENU_KEYBOARD=30,
		UI_MENU_SFX=31,
		UI_MENU_MUSIC=32,
		UI_MENU_QUIT=33,
		UI_MENU_LEVELSELECT=34,
		UI_MENU_GAMEOVER=35,
		UI_MENU_CREDITS=36,
		UI_MENU_LIMIT=37,
		UI_MENU_RETURN=38,
		UI_MENU_ANYKEY=39,
	}
}
