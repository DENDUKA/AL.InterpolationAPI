namespace AL.Interpolation.Entities.Interpolation
{
    [Serializable]
    public class Point
    {
        public static readonly Point NullPoint = new Point(double.NegativeInfinity, double.NegativeInfinity);

        public Point()
        { }

        public Point(Point point)
            : this(point.X, point.Y, point.Z)
        {
        }

        public Point(double x, double y, double z = 0)
        {
            X = x;
            Y = y;
            Z = z;
        }
 
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }
    }
}
