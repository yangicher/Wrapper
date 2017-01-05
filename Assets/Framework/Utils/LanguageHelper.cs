using UnityEngine;

namespace Assets.Framework.Utils
{
    public class LanguageHelper
    {
        public static string Get2LetterIsoCodeFromSystemLanguage()
        {
            SystemLanguage lang = Application.systemLanguage;
            string res = "EN";
            switch (lang)
            {
                case SystemLanguage.Afrikaans: res = "AF"; break;
                case SystemLanguage.Arabic: res = "AR"; break;
                case SystemLanguage.Basque: res = "EU"; break;
                case SystemLanguage.Belarusian: res = "BY"; break;
                case SystemLanguage.Bulgarian: res = "BG"; break;
                case SystemLanguage.Catalan: res = "CA"; break;
                case SystemLanguage.Chinese: res = "ZH"; break;
                case SystemLanguage.Czech: res = "CS"; break;
                case SystemLanguage.Danish: res = "DA"; break;
                case SystemLanguage.Dutch: res = "NL"; break;
                case SystemLanguage.English: res = "EN"; break;
                case SystemLanguage.Estonian: res = "ET"; break;
                case SystemLanguage.Faroese: res = "FO"; break;
                case SystemLanguage.Finnish: res = "FI"; break;
                case SystemLanguage.French: res = "FR"; break;
                case SystemLanguage.German: res = "DE"; break;
                case SystemLanguage.Greek: res = "EL"; break;
                case SystemLanguage.Hebrew: res = "IW"; break;
                case SystemLanguage.Hungarian: res = "HU"; break;
                case SystemLanguage.Icelandic: res = "IS"; break;
                case SystemLanguage.Indonesian: res = "IN"; break;
                case SystemLanguage.Italian: res = "IT"; break;
                case SystemLanguage.Japanese: res = "JA"; break;
                case SystemLanguage.Korean: res = "KO"; break;
                case SystemLanguage.Latvian: res = "LV"; break;
                case SystemLanguage.Lithuanian: res = "LT"; break;
                case SystemLanguage.Norwegian: res = "NO"; break;
                case SystemLanguage.Polish: res = "PL"; break;
                case SystemLanguage.Portuguese: res = "PT"; break;
                case SystemLanguage.Romanian: res = "RO"; break;
                case SystemLanguage.Russian: res = "RU"; break;
                case SystemLanguage.SerboCroatian: res = "SH"; break;
                case SystemLanguage.Slovak: res = "SK"; break;
                case SystemLanguage.Slovenian: res = "SL"; break;
                case SystemLanguage.Spanish: res = "ES"; break;
                case SystemLanguage.Swedish: res = "SV"; break;
                case SystemLanguage.Thai: res = "TH"; break;
                case SystemLanguage.Turkish: res = "TR"; break;
                case SystemLanguage.Ukrainian: res = "UA"; break;
                case SystemLanguage.Unknown: res = "EN"; break;
                case SystemLanguage.Vietnamese: res = "VI"; break;
            }

            return res;
        }

        public static string Get2LetterIsoCode(SystemLanguage lang)
        {
            string res = "EN";
            switch (lang)
            {
                case SystemLanguage.Afrikaans: res = "AF"; break;
                case SystemLanguage.Arabic: res = "AR"; break;
                case SystemLanguage.Basque: res = "EU"; break;
                case SystemLanguage.Belarusian: res = "BY"; break;
                case SystemLanguage.Bulgarian: res = "BG"; break;
                case SystemLanguage.Catalan: res = "CA"; break;
                case SystemLanguage.Chinese: res = "ZH"; break;
                case SystemLanguage.Czech: res = "CS"; break;
                case SystemLanguage.Danish: res = "DA"; break;
                case SystemLanguage.Dutch: res = "NL"; break;
                case SystemLanguage.English: res = "EN"; break;
                case SystemLanguage.Estonian: res = "ET"; break;
                case SystemLanguage.Faroese: res = "FO"; break;
                case SystemLanguage.Finnish: res = "FI"; break;
                case SystemLanguage.French: res = "FR"; break;
                case SystemLanguage.German: res = "DE"; break;
                case SystemLanguage.Greek: res = "EL"; break;
                case SystemLanguage.Hebrew: res = "IW"; break;
                case SystemLanguage.Hungarian: res = "HU"; break;
                case SystemLanguage.Icelandic: res = "IS"; break;
                case SystemLanguage.Indonesian: res = "IN"; break;
                case SystemLanguage.Italian: res = "IT"; break;
                case SystemLanguage.Japanese: res = "JA"; break;
                case SystemLanguage.Korean: res = "KO"; break;
                case SystemLanguage.Latvian: res = "LV"; break;
                case SystemLanguage.Lithuanian: res = "LT"; break;
                case SystemLanguage.Norwegian: res = "NO"; break;
                case SystemLanguage.Polish: res = "PL"; break;
                case SystemLanguage.Portuguese: res = "PT"; break;
                case SystemLanguage.Romanian: res = "RO"; break;
                case SystemLanguage.Russian: res = "RU"; break;
                case SystemLanguage.SerboCroatian: res = "SH"; break;
                case SystemLanguage.Slovak: res = "SK"; break;
                case SystemLanguage.Slovenian: res = "SL"; break;
                case SystemLanguage.Spanish: res = "ES"; break;
                case SystemLanguage.Swedish: res = "SV"; break;
                case SystemLanguage.Thai: res = "TH"; break;
                case SystemLanguage.Turkish: res = "TR"; break;
                case SystemLanguage.Ukrainian: res = "UA"; break;
                case SystemLanguage.Unknown: res = "EN"; break;
                case SystemLanguage.Vietnamese: res = "VI"; break;
            }

            return res;
        }

        public static SystemLanguage GetSystemLanguage(string lang)
        {
            lang = lang.ToUpper();
            SystemLanguage res = SystemLanguage.English;
            switch (lang)
            {
                case "AF": res = SystemLanguage.Afrikaans; break;
                case "AR": res = SystemLanguage.Arabic; break;
                case "EU": res = SystemLanguage.Basque; break;
                case "BY": res = SystemLanguage.Belarusian; break;
                case "BG": res = SystemLanguage.Bulgarian; break;
                case "CA": res = SystemLanguage.Catalan; break;
                case "ZH": res = SystemLanguage.Chinese; break;
                case "CS": res = SystemLanguage.Czech; break;
                case "DA": res = SystemLanguage.Danish; break;
                case "NL": res = SystemLanguage.Dutch; break;
                case "EN": res = SystemLanguage.English; break;
                case "ET": res = SystemLanguage.Estonian; break;
                case "FO": res = SystemLanguage.Faroese; break;
                case "FI": res = SystemLanguage.Finnish; break;
                case "FR": res = SystemLanguage.French; break;
                case "DE": res = SystemLanguage.German; break;
                case "EL": res = SystemLanguage.Greek; break;
                case "IW": res = SystemLanguage.Hebrew; break;
                case "HU": res = SystemLanguage.Hungarian; break;
                case "IS": res = SystemLanguage.Icelandic; break;
                case "IN": res = SystemLanguage.Indonesian; break;
                case "IT": res = SystemLanguage.Italian; break;
                case "JA": res = SystemLanguage.Japanese; break;
                case "KO": res = SystemLanguage.Latvian; break;
                case "LV": res = SystemLanguage.Lithuanian; break;
                case "LT": res = SystemLanguage.Norwegian; break;
                case "NO": res = SystemLanguage.Polish; break;
                case "PL": res = SystemLanguage.Portuguese; break;
                case "RO": res = SystemLanguage.Romanian; break;
                case "RU": res = SystemLanguage.Russian; break;
                case "SH": res = SystemLanguage.SerboCroatian; break;
                case "SK": res = SystemLanguage.Slovak; break;
                case "SL": res = SystemLanguage.Slovenian; break;
                case "ES": res = SystemLanguage.Spanish; break;
                case "SV": res = SystemLanguage.Swedish; break;
                case "TH": res = SystemLanguage.Thai; break;
                case "TR": res = SystemLanguage.Turkish; break;
                case "UA": res = SystemLanguage.Ukrainian; break;
                case "VI": res = SystemLanguage.Vietnamese; break;
            }

            return res;
        }
    }


}
