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
    class Square
    {
        public readonly Point pointTopLeft, pointTopRight, pointDownLeft, pointDownRight;
        public Square(Point pointTopLeft, Point pointTopRight, Point pointDownLeft, Point pointDownRight)
        {
            this.pointTopLeft = pointTopLeft;
            this.pointTopRight = pointTopRight;
            this.pointDownLeft = pointDownLeft;
            this.pointDownRight = pointDownRight;
        }
        public Square(List<Point> points)
        {
            this.pointTopLeft = points[0];
            this.pointTopRight = points[1];
            this.pointDownLeft = points[2];
            this.pointDownRight = points[3];
        }

        public Point CentralTopLeftPoint => 
            new(((pointTopRight.X - pointTopLeft.X) / 3d) + pointTopLeft.X, ((pointDownLeft.Y - pointTopLeft.Y) / 3d) + pointTopLeft.Y);
        public Point CentralTopRightPoint =>
            new(((pointTopRight.X - pointTopLeft.X) / 3d * 2d) + pointTopLeft.X, ((pointDownLeft.Y - pointTopLeft.Y) / 3d) + pointTopLeft.Y);
        public Point CentralDownLeftPoint =>
            new(((pointTopRight.X - pointTopLeft.X) / 3d) + pointTopLeft.X, ((pointDownLeft.Y - pointTopLeft.Y) / 3d * 2d) + pointTopLeft.Y);
        public Point CentralDownRightPoint =>
            new(((pointTopRight.X - pointTopLeft.X) / 3d * 2d) + pointTopLeft.X, ((pointDownLeft.Y - pointTopLeft.Y) / 3d * 2d) + pointTopLeft.Y);

        public Polygon GetPolygon(Brush brush)
        {
            Polygon polygon = new Polygon();
            List<Point> points = new(3);
            points.Add(pointTopLeft);
            points.Add(pointTopRight);
            points.Add(pointDownRight);
            points.Add(pointDownLeft);
            polygon.Points = new PointCollection(points);
            polygon.Fill = brush;
            return polygon;
        }
    }
}
