using LiveCharts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HAI.Models
{
    public static class StatsData
    {
        private static Random _rnd = new Random();

        public static int AiVictories = 0;
        public static int PlayerVictories = 0;

        public static ChartValues<double> Values = new ChartValues<double> { 0 };

        public static void Add()
        {
            Values.Add(Values.Last() + 10 + _rnd.Next(-5, 3));
        }

        public static double GetAverageWins()
        {
            var sum = PlayerVictories + AiVictories;

            if (sum == 0)
                return 0;

            return (double)PlayerVictories / sum * 100;
        }
}
}
