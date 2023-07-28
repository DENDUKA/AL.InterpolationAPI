namespace AL.Interpolation.Entities.Interpolation
{
    public class Surface
    {
        public Surface()
            : this(new List<Point>())
        {
        }

        public List<Point> Points { get; set; }
        public int SurfaceCellsX { get; set; }
        public int SurfaceCellsY { get; set; }
        public double SurfaceSizeX { get; set; }
        public double SurfaceSizeY { get; set; }

        public Surface(params Point[] points)
        {
            Points = points.ToList();
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="Surface"/>.
        /// </summary>
        /// <param name="points">Двухмерный массив точек.</param>
        /// <param name="cellSize">Размер ячейка поверхности по X.</param>
        /// <param name="cellSize">Размер ячейка поверхности по Y.</param>
        /// <param name="sizeX">Количество ячеек по горизонтали.</param>
        /// <param name="sizeY">Количество ячеек по вертикали.</param>
        /// <param name="bottomLeftPoint">Координата центра нижней левой ячейки.</param>
        public Surface(double[,] points, int cellSizeX, int cellSizeY, int sizeX, int sizeY, Point bottomLeftPoint)
        {
            Points = new List<Point>();
            Point topRightPoint = Point.NullPoint;

            for (var ii = 0; ii < points.GetLength(0); ii++)
            {
                for (var jj = 0; jj < points.GetLength(1); jj++)
                {
                    double z = points[points.GetLength(0) - ii - 1, points.GetLength(1) - jj - 1];

                    if (topRightPoint == Point.NullPoint)
                    {
                        topRightPoint = new Point(
                            bottomLeftPoint.X + (cellSizeX * sizeX),
                            bottomLeftPoint.Y + (cellSizeY * sizeY),
                            z);
                    }

                    double pointX = topRightPoint.X - (cellSizeX * (ii + 1)),
                           pointY = topRightPoint.Y - (cellSizeY * (jj + 1)),
                           pointZ = z;

                    Points.Add(new Point(pointX, pointY, pointZ));
                }
            }

            SurfaceSizeX = cellSizeX;
            SurfaceSizeY = cellSizeY;
            SurfaceCellsX = sizeX;
            SurfaceCellsY = sizeY;
        }

        public Surface(List<Point> points)
        {
            Points = points;
        }
    }
}
