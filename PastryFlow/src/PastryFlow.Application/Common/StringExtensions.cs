using System.Globalization;

namespace PastryFlow.Application.Common;

public static class StringExtensions
{
    private static readonly CultureInfo TurkishCulture = new("tr-TR");
    
    public static string ToTurkishLower(this string str)
        => str.ToLower(TurkishCulture);
    
    public static bool TurkishEquals(this string str, string other)
        => string.Equals(str, other, StringComparison.CurrentCultureIgnoreCase);
    
    public static bool TurkishContains(this string str, string value)
        => str.Contains(value, StringComparison.CurrentCultureIgnoreCase);
}
