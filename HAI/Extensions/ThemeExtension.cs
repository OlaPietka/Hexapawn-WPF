using MaterialDesignColors.ColorManipulation;
using MaterialDesignThemes.Wpf;
using System;
using System.Windows.Media;

namespace HAI.Extensions
{
    public static class ThemeExtension
    {
        public static void SetPrimaryColor(this ITheme theme, Color primaryColor, Color secondaryColor)
        {
            if (theme == null) throw new ArgumentNullException(nameof(theme));
            theme.PrimaryLight = secondaryColor;
            theme.PrimaryMid = primaryColor;
            theme.PrimaryDark = primaryColor.Darken();
        }
    }
}
