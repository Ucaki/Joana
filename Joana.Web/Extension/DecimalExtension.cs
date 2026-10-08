using System.Globalization;

namespace Joana.Web.Extension;

public static class DecimalExtension
{
    private static readonly NumberFormatInfo RsdFormat = new NumberFormatInfo
    {
        NumberGroupSeparator = ".",
        NumberDecimalSeparator = ",",
        NumberDecimalDigits = 2
    };

    public static string ToRsd(this decimal value)
    {
        return $"{value.ToString("N2", RsdFormat)} RSD";
    }
}