using HAI.Extensions;
using MaterialDesignColors;
using MaterialDesignThemes.Wpf;
using System;
using System.Windows.Media;

namespace HAI.Pages.Helpers
{
    public static class ThemeManager
    {
        public static (Color Primary, Color Secondary, Color Accent) BlueColor
        {
            get
            {
                var primary = SwatchHelper.Lookup[MaterialDesignColor.Blue500];
                var secondary = SwatchHelper.Lookup[MaterialDesignColor.Blue200];
                var accent = SwatchHelper.Lookup[MaterialDesignColor.BlueA700];

                return (primary, secondary, accent);
            }
        }

        public static (Color Primary, Color Secondary, Color Accent) RedColor
        {
            get
            {
                var primary = SwatchHelper.Lookup[MaterialDesignColor.Red500];
                var secondary = SwatchHelper.Lookup[MaterialDesignColor.Red200];
                var accent = SwatchHelper.Lookup[MaterialDesignColor.RedA700];

                return (primary, secondary, accent);
            }
        }

        public static (Color Primary, Color Secondary, Color Accent) GreenColor
        {
            get
            {
                var primary = SwatchHelper.Lookup[MaterialDesignColor.Green500];
                var secondary = SwatchHelper.Lookup[MaterialDesignColor.Green200];
                var accent = SwatchHelper.Lookup[MaterialDesignColor.GreenA700];

                return (primary, secondary, accent);
            }
        }


        public static void ApplyBase(bool isDark)
            => ModifyTheme(theme => theme.SetBaseTheme(isDark ? Theme.Dark : Theme.Light));

        public static void ApplyPrimary(Color primary, Color secondary)
            => ModifyTheme(theme => theme.SetPrimaryColor(primary, secondary));

        public static void ApplyAccent(Color color)
            => ModifyTheme(theme => theme.SetSecondaryColor(color));


        private static void ModifyTheme(Action<ITheme> modificationAction)
        {
            var paletteHelper = new PaletteHelper();
            var theme = paletteHelper.GetTheme();

            modificationAction?.Invoke(theme);

            paletteHelper.SetTheme(theme);
        }

        public static ThemeColor GetCurrentTheme()
        {
            var paletteHelper = new PaletteHelper();
            var currentColor = paletteHelper.GetTheme().PrimaryMid.Color;

            if(currentColor == GreenColor.Primary)
                return ThemeColor.GREEN;
            if(currentColor == RedColor.Primary)
                return ThemeColor.RED;
            
            return ThemeColor.BLUE;
        }
    }
}
