using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HAI.Pages.Helpers
{
    public static class SourceHelper
    {
        private static readonly string _path = "/Resources/BoardImages/";

        public static (string Empty, string Ai, string Player, string Selected, string AiSelected, string PlayerSelected) Source
        {
            get
            {
                var currentTheme = ThemeManager.GetCurrentTheme();

                var color = "Blue";
                if (currentTheme == ThemeColor.RED)
                    color = "Red";
                else if (currentTheme == ThemeColor.GREEN)
                    color = "Green";

                var Empty = $"{_path}Empty{color}.png";
                var Ai = $"{_path}Ai{color}.png";
                var Player = $"{_path}Player{color}.png";
                var Selected = $"{_path}Selected{color}.png";
                var AiSelected = $"{_path}AiSelected{color}.png";
                var PlayerSelected = $"{_path}PlayerSelected{color}.png";

                return (Empty, Ai, Player, Selected, AiSelected, PlayerSelected);
            }
        }
    }
}
