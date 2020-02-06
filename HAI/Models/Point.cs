namespace HAI.Models
{
    public struct Point
    {
        public int X;
        public int Y;

        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        public static bool operator== (Point obj1, Point obj2)
        {
            return (obj1.Y == obj2.Y && obj1.X == obj2.X);
        }

        public static bool operator !=(Point obj1, Point obj2)
        {
            return (obj1.Y != obj2.Y || obj1.X != obj2.X);
        }

    }
}