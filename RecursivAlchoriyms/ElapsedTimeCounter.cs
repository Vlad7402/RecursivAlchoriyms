using System;
using System.Diagnostics;
using System.IO;
using System.Collections.Generic;
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
    class ElapsedTimeCounter
    {
        public static void Count()
        {
            int smoothnes = 3;
            int step = 1;
            int slidingAvarage = 1;
            int RunTimes = 15;
            List<long> timersResultsTriangle = new(RunTimes);
            List<long> timersResultsSquare = new(RunTimes);
            for (int i = step; i <= (RunTimes + slidingAvarage) * step; i += step)
            {
                long[] resultsTriangle = new long[smoothnes];
                long[] resultsSquare = new long[smoothnes];
                for (int j = 0; j < smoothnes; j++)
                {
                    resultsTriangle[j] = GetAllapsedTime1(i);
                    resultsSquare[j] = GetAllapsedTime2(i);
                }                 

                RemoveWrongValues1(resultsTriangle, i, smoothnes);
                RemoveWrongValues2(resultsSquare, i, smoothnes);

                long smoothResult = resultsTriangle[0];
                for (int j = 1; j < smoothnes; j++)
                    smoothResult = (smoothResult + resultsTriangle[j]) / 2;

                timersResultsTriangle.Add(smoothResult);

                smoothResult = resultsSquare[0];
                for (int j = 1; j < smoothnes; j++)
                    smoothResult = (smoothResult + resultsSquare[j]) / 2;

                timersResultsSquare.Add(smoothResult);
            }
            WriteToCSV1(SlidingAvarageFilter(slidingAvarage, timersResultsTriangle).ToArray());
            WriteToCSV2(SlidingAvarageFilter(slidingAvarage, timersResultsSquare).ToArray());
        }
        private static long GetAllapsedTime1(int array)
        {
            Stopwatch stopwatch = new();
            stopwatch.Start();
            List<Point> points = new(3);
            points.Add(new Point(1000 / 2, 0));
            points.Add(new Point(1000, 1000));
            points.Add(new Point(0, 1000));
            Triangle triangle = new(points);
            TriangleSerpinskogo(triangle, 1, array);
            stopwatch.Stop();
            return stopwatch.ElapsedTicks / (TimeSpan.TicksPerMillisecond / 1000);
        }
        private static long GetAllapsedTime2(int array)
        {
            Stopwatch stopwatch = new();
            stopwatch.Start();
            List<Point> points = new(4);
            points.Add(new Point(0, 0));
            points.Add(new Point(1000, 0));
            points.Add(new Point(0, 1000));
            points.Add(new Point(1000, 1000));
            Square square = new(points);
            CarpetSerpinskogo(square, 1, array);
            stopwatch.Stop();
            return stopwatch.ElapsedTicks / (TimeSpan.TicksPerMillisecond / 1000);
        }
        private static void RemoveWrongValues1(long[] values, int array, int smoothnes)
        {
            for (int j = 1; j < smoothnes; j++)
            {
                if (values[j - 1] != 0L)
                {
                    while (values[j] / values[j - 1] > 2f)
                    {
                        values[j] = GetAllapsedTime1(array);
                        if (values[j - 1] == 0L)
                            break;
                    }
                }
                if (values[j] != 0L)
                {
                    while (values[j - 1] / values[j] > 2f)
                    {
                        values[j - 1] = GetAllapsedTime1(array);
                        if (values[j] == 0L)
                            break;
                    }
                }
            }
        }
        private static void RemoveWrongValues2(long[] values, int array, int smoothnes)
        {
            for (int j = 1; j < smoothnes; j++)
            {
                if (values[j - 1] != 0L)
                {
                    while (values[j] / values[j - 1] > 2f)
                    {
                        values[j] = GetAllapsedTime2(array);
                        if (values[j - 1] == 0L)
                            break;
                    }
                }
                if (values[j] != 0L)
                {
                    while (values[j - 1] / values[j] > 2f)
                    {
                        values[j - 1] = GetAllapsedTime2(array);
                        if (values[j] == 0L)
                            break;
                    }
                }
            }
        }
        private static List<decimal> SlidingAvarageFilter(int slidingAvarage, List<long> values)
        {
            List<decimal> result = new(values.Count);
            for (int i = slidingAvarage; i < values.Count; i++)
            {
                long avarage = 0L;
                for (int j = slidingAvarage; j > 0; j--)
                    avarage += values[i - j];

                result.Add((decimal)avarage / (decimal)slidingAvarage);
            }
            return result;
        }
        private static void WriteToCSV1(decimal[] timersResults)
        {
            string[] values = new string[timersResults.Length];
            for (int i = 0; i < timersResults.Length; i++)
                values[i] = Convert.ToString(timersResults[i]);

            File.WriteAllLines("ResultTr.csv", values);
        }
        private static void WriteToCSV2(decimal[] timersResults)
        {
            string[] values = new string[timersResults.Length];
            for (int i = 0; i < timersResults.Length; i++)
                values[i] = Convert.ToString(timersResults[i]);

            File.WriteAllLines("ResultSq.csv", values);
        }
        private static void TriangleSerpinskogo(Triangle triangle, int step, int deepnes)
        {
            List<Triangle> triangles = new(3);
            triangles.Add(new(triangle.point1, triangle.MiddlePoint12, triangle.MiddlePoint13));
            triangles.Add(new(triangle.point2, triangle.MiddlePoint12, triangle.MiddlePoint23));
            triangles.Add(new(triangle.point3, triangle.MiddlePoint13, triangle.MiddlePoint23));
            Brush brush = getBrush(step);

            //foreach (var figer in triangles)
            //    AddTriangleOnFild(figer.GetPolygon(brush));

            if (step != deepnes)
            {
                foreach (var figer in triangles)
                    TriangleSerpinskogo(figer, step + 1, deepnes);
            }
        }
        private static Brush getBrush(int i)
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
            if (i > 9)           
                return Brushes.Red;
            
            return brushes[i - 1];
        }
        private static void CarpetSerpinskogo(Square square, int step, int deepnes)
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

            if (step != deepnes)
            {
                foreach (var figer in squares)
                    CarpetSerpinskogo(figer, step + 1, deepnes);
            }
        }
    }
}
