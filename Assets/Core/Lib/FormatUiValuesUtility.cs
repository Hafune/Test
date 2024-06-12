using System;

namespace Core
{
    //https://learn.microsoft.com/en-us/dotnet/api/system.string.format?view=net-8.0#code-try-20
    //https://learn.microsoft.com/ru-ru/dotnet/standard/base-types/standard-numeric-format-strings
    //https://stackoverflow.com/questions/17527847/how-would-i-separate-thousands-with-space-in-c-sharp
    /*
        double value = 1211916432;

        var f = new NumberFormatInfo {NumberGroupSeparator = " "};
        string output = value.ToString("n0",f);
        Console.WriteLine(output);//1 211 916 432    
     */
    public static class FormatUiValuesUtility
    {
        public static string ToIntString(float value) => ((int)value).ToString();
        public static string ToPercentInt(float value) => $"{(int)Math.Round(value * 100)}";
        public static string WeaponDamagePerSecond(float value) => $"{value,12:# ###.0}";
        public static string FloatDim1(float value) => $"{value,18:0.0}";
        public static string FloatDim2(float value) => $"{value,18:0.00}";
    }
}