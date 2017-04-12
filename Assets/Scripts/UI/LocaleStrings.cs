using UnityEngine;
using System.Collections;
using System;

public static partial class LocaleStrings
{
	private static string[] CurrentLocale;

	public static readonly string[] en_EN = {
		"test",
		"Press to activate ultimate power!",
		"ULTIMATE POWER!",
		"Completion Time",
		"Hits Taken",
		"Final Clear Time",
		""
	};

	public static readonly string[] fi_FI = {
		"testi",
		"Paina nappulaa :P",
		"Nabbula baineddu :DD",
		"Suoritusaika",
		"Osumia otettu",
		"Yhteenlaskettu aika",
		""
	};

	public static string TestString { get { return CurrentLocale[0]; } }

	public static string UI_LimitBreakPrompt { get { return CurrentLocale[1]; } }

	public static string UI_LimitBreakNotification { get { return CurrentLocale[2]; } }

	public static string UI_SCORE_COMPTIME { get { return CurrentLocale[3]; } }

	public static string UI_SCORE_HITSTAKEN { get { return CurrentLocale[4]; } }

	public static string UI_SCORE_TOTALTIME { get { return CurrentLocale[5]; } }

}