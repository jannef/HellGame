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
		"Next rank\n{0}",
		"Kitchen Of Doom",
		"Gordon Ramsay's",
		"Hearths of Hell",
		"Wall of Death",
		"Noob Cellar",
		"Round the Wine",
		"Slime of Death",
		"Silent Room",
		"Eye Of Laser",
		"Fire and Flames",
		"Disco Ball Of Death",
		"Eye Of The Tiger",
		"The Study",
		"Retry",
		""
	};

	public static readonly string[] fi_FI = {
		"Paina nappulaa :P",
		"Nabbula baineddu :DD",
		"Suoritusaika",
		"Osumia otettu",
		"Yhteenlaskettu aika",
		"Seuraava taso\n{0}",
		"Kuolon Keittiö",
		"KauhuKeittiö",
		"Tosi Kuuma Huone",
		"Kuolon Muuri",
		"Aloitus Kellari",
		"ViiniKellari",
		"Kuoleman Lima",
		"Hyllyjen Välissä",
		"LaserSilmä",
		"LiekkiMeri",
		"Kuoleman Discopallo",
		"Kissansilmä",
		"Opintohalli",
		"Uudestaan!!!",
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
	}
}
