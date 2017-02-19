using UnityEngine;
using System.Collections;
using System;

public static class LocaleStrings
{
	public static string[] CurrentLocale;

	public static readonly string[] en_EN = {
		"test",
		"Press to activate ultimate power!",
		"ULTIMATE POWER!",
		""
	};

	public static readonly string[] fi_FI = {
		"testi",
		"Paina nappulaa :P",
		"Nabbula baineddu :DD",
		""
	};

	public static string TestString { get { return CurrentLocale[0]; } }

	public static string UI_LimitBreakPrompt { get { return CurrentLocale[1]; } }

	public static string UI_LimitBreakNotification { get { return CurrentLocale[2]; } }

}