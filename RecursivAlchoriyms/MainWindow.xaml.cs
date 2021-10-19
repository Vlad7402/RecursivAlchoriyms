using System;
using System.Threading;
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
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int deepnes = 1;

        public MainWindow()
        {
            InitializeComponent();
        }
        private void AddTriangleOnFild(Polygon triangle)
        {
            CanDrowFild.Children.Add(triangle);
        }

        private void SliderDeepnes_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            TBDeepnes.Text = ((int)e.NewValue).ToString();
            deepnes = (int)e.NewValue;
        }

        private void Click_DrowFractal(object sender, RoutedEventArgs e)
        {
            PrepareFild();
            if (RBTriangle.IsChecked == true)
            {
                List<Point> points = new(3);
                points.Add(new Point(CanDrowFild.ActualWidth / 2, 0));
                points.Add(new Point(CanDrowFild.ActualWidth, CanDrowFild.ActualHeight));
                points.Add(new Point(0, CanDrowFild.ActualHeight));
                Triangle triangle = new(points);
                AddTriangleOnFild(triangle.GetPolygon(Brushes.DarkCyan));
                TriangleSerpinskogo(triangle, 1);
            }
            else
            {
                List<Point> points = new(4);
                points.Add(new Point(0, 0));
                points.Add(new Point(CanDrowFild.ActualWidth, 0));
                points.Add(new Point(0, CanDrowFild.ActualHeight));
                points.Add(new Point(CanDrowFild.ActualWidth, CanDrowFild.ActualHeight));
                Square square = new(points);
                AddTriangleOnFild(square.GetPolygon(Brushes.DarkCyan));
                CarpetSerpinskogo(square, 1);
            }       
        }

        private void TriangleSerpinskogo(Triangle triangle, int step)
        {
            List<Triangle> triangles = new(3);
            triangles.Add(new(triangle.point1, triangle.MiddlePoint12, triangle.MiddlePoint13));
            triangles.Add(new(triangle.point2, triangle.MiddlePoint12, triangle.MiddlePoint23));
            triangles.Add(new(triangle.point3, triangle.MiddlePoint13, triangle.MiddlePoint23));
            Brush brush = getBrush(step);

            foreach (var figer in triangles)
                AddTriangleOnFild(figer.GetPolygon(brush));

            if (step != deepnes)
            {
                foreach (var figer in triangles)
                    TriangleSerpinskogo(figer, step + 1);
            }
        }
        private void CarpetSerpinskogo(Square square, int step)
        {
            List<Square> squares = new(8);
            squares.Add(new(square.pointTopLeft, new(square.CentralTopLeftPoint.X, square.pointTopLeft.Y),
                new(square.pointTopLeft.X, square.CentralTopLeftPoint.Y), square.CentralTopLeftPoint));

            squares.Add(new(new(square.CentralTopLeftPoint.X, square.pointTopLeft.Y), new(square.CentralTopRightPoint.X, square.pointTopLeft.Y),
                square.CentralTopLeftPoint, square.CentralTopRightPoint));

            squares.Add(new(new(square.CentralTopRightPoint.X, square.pointTopLeft.Y), square.pointTopRight,
                square.CentralTopRightPoint, new(square.pointTopRight.X, square.CentralTopRightPoint.Y)));

            squares.Add(new(square.CentralTopRightPoint, new(square.pointTopRight.X, square.CentralTopRightPoint.Y),
                square.CentralDownRightPoint, new(square.pointTopRight.X, square.CentralDownRightPoint.Y)));

            squares.Add(new(square.CentralDownRightPoint, new(square.pointTopRight.X, square.CentralDownRightPoint.Y),
                new(square.CentralDownRightPoint.X, square.pointDownRight.Y), square.pointDownRight));

            squares.Add(new(square.CentralDownLeftPoint, square.CentralDownRightPoint,
                new(square.CentralDownLeftPoint.X, square.pointDownRight.Y), new(square.CentralDownRightPoint.X, square.pointDownRight.Y)));

            squares.Add(new(new(square.pointDownLeft.X, square.CentralDownLeftPoint.Y), square.CentralDownLeftPoint,
                square.pointDownLeft, new(square.CentralDownLeftPoint.X, square.pointDownRight.Y)));
            
            squares.Add(new(new(square.pointDownLeft.X, square.CentralTopLeftPoint.Y), square.CentralTopLeftPoint,
                new(square.pointDownLeft.X, square.CentralDownLeftPoint.Y), square.CentralDownLeftPoint));

            Brush brush = getBrush(step);

            foreach (var figer in squares)
                AddTriangleOnFild(figer.GetPolygon(brush));

            if (step != deepnes)
            {
                foreach (var figer in squares)
                    CarpetSerpinskogo(figer, step + 1);
            }
        }
        private void PrepareFild()
        {
            CanDrowFild.Children.Clear();
            Polygon backGround = new Polygon();
            backGround.Points.Add(new Point(CanDrowFild.ActualWidth, CanDrowFild.ActualHeight));
            backGround.Points.Add(new Point(1, CanDrowFild.ActualHeight));
            backGround.Points.Add(new Point(1, 1));
            backGround.Points.Add(new Point(CanDrowFild.ActualWidth, 1));
            backGround.Fill = Brushes.Gray;
            CanDrowFild.Children.Add(backGround);
        }
        private Brush getBrush(int i)
        {
            List<Brush> brushes = new(9);
            brushes.Add(Brushes.Red);
            brushes.Add(Brushes.Orange);
            brushes.Add(Brushes.Yellow);
            brushes.Add(Brushes.Green);
            brushes.Add(Brushes.Blue);
            brushes.Add(Brushes.DarkBlue);
            brushes.Add(Brushes.Violet);
            brushes.Add(Brushes.Aqua);
            brushes.Add(Brushes.DarkOrange);
            return brushes[i - 1];
        }

        private void RBSquare_Checked(object sender, RoutedEventArgs e)
        {
            SliderDeepnes.Value = 1;
            if (((RadioButton)sender).Name == "RBTriangle")
            {
                RBSquare.IsChecked = false;
                SliderDeepnes.Maximum = 9;
            }
            else
            {
                RBTriangle.IsChecked = false;
                SliderDeepnes.Maximum = 5;
            }
        }

        private void CountEllapsedTime(object sender, RoutedEventArgs e)
        {
            Thread Counter = new Thread(new ThreadStart(ElapsedTimeCounter.Count));
            Counter.Start();
        }
    }
}
