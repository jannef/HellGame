    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using UnityEngine;

public static partial class LocaleStrings
{
    /// <summary>
    /// PlayerPrefs key for language locale.
    /// </summary>
    private const string PlayerLocaleKey = "PlayerLocale";

    /// <summary>
    /// Reads field infos and thus finds available locales.
    /// </summary>
    static LocaleStrings()
    {
        LocaleMap = new Dictionary<string, string[]>();
        PopulateLocaleDictionary();
        LoadLocale();
    }

    /// <summary>
    /// Populates dictionary of available locales. Called from static constructor.
    /// </summary>
    private static void PopulateLocaleDictionary()
    {
        var locales = typeof(LocaleStrings).GetFields().Where(x => x.Name != "CurrentLocale");
        foreach (var info in locales)
        {
            LocaleMap.Add(info.Name, info.GetValue(null) as string[]);
        }
    }

    /// <summary>
    /// Lists available locales.
    /// <example>
    /// Return values are formatted like so: { 'en_EN', 'en_US', 'fi_FI' }
    /// </example>
    /// </summary>
    /// <returns>Available locales as a string array. Returned values can be used as keys for SetCurrentLocale(string)</returns>
    public static string[] ListAvailableLocales()
    {
        return LocaleMap.Select(x => x.Key).ToArray();
    }

    /// <summary>
    /// Changes current locale and saves the change to player prefs.
    /// </summary>
    /// <param name="selectLocale">Which locale to set. Get lsit of available locales from string[] ListAvailableLocales()</param>
    public static void SetCurrentLocale(string selectLocale)
    {
        if (LocaleMap.ContainsKey(selectLocale))
        {
            CurrentLocale = LocaleMap[selectLocale];
            SaveLocale(selectLocale);
        }
        else
        {
            var locs = ListAvailableLocales().Aggregate("", (current, locale) => current + (" '" + locale + "' "));
            throw new UnityException(string.Format("No such locale '{0}'. Available locales are: {1}", selectLocale, locs));
        }
    }

    /// <summary>
    /// Called bu SetCurrentLocale(string) to save the selection into PlayerPrefs.
    /// </summary>
    /// <param name="whichLocale">Which locale to save</param>
    private static void SaveLocale(string whichLocale)
    {
        PlayerPrefs.SetString(PlayerLocaleKey, whichLocale);
        PlayerPrefs.Save();
    }

    /// <summary>
    /// Loads locale data from PlayerPrefs.
    /// </summary>
    private static void LoadLocale()
    {
        SetCurrentLocale(PlayerPrefs.HasKey(PlayerLocaleKey) ? PlayerPrefs.GetString(PlayerLocaleKey) : "en_EN");
    }

    /// <summary>
    /// Lookup dictionary to convert string identifiers for locales into actual string arrays that contain the localized
    /// data.
    /// </summary>
    private static readonly Dictionary<string, string[]> LocaleMap;
}