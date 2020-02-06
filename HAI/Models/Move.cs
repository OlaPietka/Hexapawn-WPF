namespace HAI.Models
{
    public class Move
    {
        public Point From;
        public Point To;

        public Move()
        {
            From = new Point();
            To = new Point();
        }

        public Move(Point from, Point to)
        {
            From = from;
            To = to;
        }
        public static bool operator== (Move obj1, Move obj2)
        {
            return (obj1.From == obj2.From && obj1.To == obj2.To);
        }

        public static bool operator!= (Move obj1, Move obj2)
        {
            return (obj1.From != obj2.From || obj1.To != obj2.To);
        }
    }
}