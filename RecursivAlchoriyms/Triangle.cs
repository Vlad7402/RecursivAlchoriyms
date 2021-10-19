using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RecursivAlchoriyms
{
    public class Triangle
    {
        public readonly Point point1, point2, point3;
        public Triangle(Point point1, Point point2, Point point3)
        {
            this.point1 = point1;
            this.point2 = point2;
            this.point3 = point3;
        }
        public Triangle(List<Point> points)
        {
            this.point1 = points[0];
            this.point2 = points[1];
            this.point3 = points[2];
        }
        public Point MiddlePoint12 => GetMiddlePoint(point1, point2);
        public Point MiddlePoint23 => GetMiddlePoint(point2, point3);
        public Point MiddlePoint13 => GetMiddlePoint(point1, point3);

        private Point GetMiddlePoint(Point point1, Point point2) => new Point((point1.X + point2.X) / 2d, (point1.Y + point2.Y) / 2d);

        public Polygon GetPolygon(Brush brush)
        {
            Polygon polygon = new Polygon();
            List<Point> points = new(3);
            points.Add(point1);
            points.Add(point2);
            points.Add(point3);
            polygon.Points = new PointCollection(points);
            polygon.Fill = brush;
            return polygon;
        }
    }
}
