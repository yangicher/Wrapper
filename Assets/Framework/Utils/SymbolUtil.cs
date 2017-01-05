using System;
using UnityEngine;

namespace Assets.Framework.Utils
{
    public static class SymbolUtil
    {
        public const string Seconds2Miliseconds1 = "00.0";
        public const string Minutes1Seconds2 = "#0:00";
        public const string Minutes2Seconds2 = "#00:00";

        public const string Minutes1Seconds2Miliseconds1 = "0:00.0";
        public const string Minutes2Seconds2Miliseconds1 = "#0:00.0";
        public const string Minutes1Seconds2Miliseconds2 = "0:00.00";
        public const string Minutes2Seconds2Miliseconds2 = "#0:00.00";

        public static string ToRoman(int number)
        {
            if ((number < 0) || (number > 3999))
                throw new ArgumentOutOfRangeException("insert value betwheen 1 and 3999");
            if (number < 1) return string.Empty;
            if (number >= 1000) return "M" + ToRoman(number - 1000);
            if (number >= 900) return "CM" + ToRoman(number - 900); //EDIT: i've typed 400 instead 900
            if (number >= 500) return "D" + ToRoman(number - 500);
            if (number >= 400) return "CD" + ToRoman(number - 400);
            if (number >= 100) return "C" + ToRoman(number - 100);
            if (number >= 90) return "XC" + ToRoman(number - 90);
            if (number >= 50) return "L" + ToRoman(number - 50);
            if (number >= 40) return "XL" + ToRoman(number - 40);
            if (number >= 10) return "X" + ToRoman(number - 10);
            if (number >= 9) return "IX" + ToRoman(number - 9);
            if (number >= 5) return "V" + ToRoman(number - 5);
            if (number >= 4) return "IV" + ToRoman(number - 4);
            if (number >= 1) return "I" + ToRoman(number - 1);
            throw new ArgumentOutOfRangeException("something bad happened");
        }

        public static string FloatToTime(float toConvert, string format)
        {
            switch (format)
            {
                case Seconds2Miliseconds1:
                    return string.Format("{0:00}:{1:0}",
                        Mathf.Floor(toConvert) % 60,//seconds
                        Mathf.Floor((toConvert * 10) % 10));//miliseconds

                case Minutes1Seconds2:
                    return string.Format("{0:#0}:{1:00}",
                        Mathf.Floor(toConvert / 60),//minutes
                        Mathf.Floor(toConvert) % 60);//seconds

                case Minutes2Seconds2:
                    return string.Format("{0:#00}:{1:00}",
                        Mathf.Floor(toConvert / 60),//minutes
                        Mathf.Floor(toConvert) % 60);//seconds

                case Minutes1Seconds2Miliseconds1:
                    return string.Format("{0:0}:{1:00}.{2:0}",
                        Mathf.Floor(toConvert / 60),//minutes
                        Mathf.Floor(toConvert) % 60,//seconds
                        Mathf.Floor((toConvert * 10) % 10));//miliseconds

                case Minutes2Seconds2Miliseconds1:
                    return string.Format("{0:#0}:{1:00}.{2:0}",
                        Mathf.Floor(toConvert / 60),//minutes
                        Mathf.Floor(toConvert) % 60,//seconds
                        Mathf.Floor((toConvert * 10) % 10));//miliseconds

                case Minutes1Seconds2Miliseconds2:
                    return string.Format("{0:0}:{1:00}.{2:00}",
                        Mathf.Floor(toConvert / 60),//minutes
                        Mathf.Floor(toConvert) % 60,//seconds
                        Mathf.Floor((toConvert * 100) % 100));//miliseconds

                case Minutes2Seconds2Miliseconds2:
                    return string.Format("{0:#0}:{1:00}.{2:00}",
                        Mathf.Floor(toConvert / 60),//minutes
                        Mathf.Floor(toConvert) % 60,//seconds
                        Mathf.Floor((toConvert * 100) % 100));//miliseconds

            }
            return "error";
        }

        public static string ForGameAnalytics(string from)
        {
            if (string.IsNullOrEmpty(from))
                return "No";

            if (from.Length == 1)
                return "000" + from;
            else if (from.Length == 2)
                return "00" + from;
            else if (from.Length == 3)
                return "0" + from;

            return from;
        }

        public static string FormatNumber(int numberCount)
        {
            if (numberCount >= 1000000)
            {
                return Math.Round(numberCount / 1000000f, 1) + "M";
            }
            if (numberCount >= 1000)
            {
                return Math.Round(numberCount / 1000f, 1) + "K";
            }

            return numberCount.ToString();
        }
    }
}