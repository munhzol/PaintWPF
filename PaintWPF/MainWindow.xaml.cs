using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace PaintWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {

        bool drawingShape = false;
        PointCollection points = new PointCollection();

        Polyline polyLine = new Polyline()
        {
            Stroke = Brushes.Black,
            StrokeThickness = 1,
        };

        Point startPoint = new Point();
        Rectangle startPointMarker = new Rectangle()
        {
            Stroke = Brushes.Black,
            StrokeThickness = 1,
            Width = 8,
            Height = 8,
        };

        Line line = new Line()
        {
            Stroke = Brushes.Black,
            StrokeThickness = 1,
        };

        

        Line seldLine = new Line();
        Boolean drawing = false;
        Shape seldShape;
        bool dragging = false;
        Point clickV;

        public event PropertyChangedEventHandler? PropertyChanged;
        PointCollection imagePoints;

        public MainWindow()
        {
            InitializeComponent();

            seldLine.StrokeThickness = 1;
            seldLine.Stroke = Brushes.Red;

            this.ImagePoints = new PointCollection(
                new[] { new Point(10, 10), new Point(80, 80), new Point(50, 60) }
                );
            this.DataContext = this;

            /*
            Polygon myPoly = new Polygon();
            myPoly.Points.Add(new Point(10, 10));
            myPoly.Points.Add(new Point(85, 350));
            myPoly.Points.Add(new Point(15, 80));
            myPoly.Points.Add(new Point(85, 20));
            myPoly.Fill = new SolidColorBrush(Colors.Blue);

            

            myPoly.MouseDown += new MouseButtonEventHandler(myPoly_MouseDown);
            DrawCanvas.MouseMove += new MouseEventHandler(DrawCanvas_MouseMove);
            DrawCanvas.MouseUp += new MouseButtonEventHandler(DrawCanvas_MouseUp);

            DrawCanvas.Children.Add(myPoly);
            */

        }


        public PointCollection ImagePoints
        {
            get { return imagePoints; }
            set
            {
                if (this.imagePoints != value)
                {
                    this.imagePoints = value;
                    if (this.PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("ImagePoints"));
                    }
                }
            }
        }

        private void myPoly_MouseDown(object sender, MouseButtonEventArgs e)
        {
            dragging = true;
            seldShape = (Shape)sender;
            clickV = e.GetPosition(seldShape);
        }


        private void DrawCanvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            /*
            drawing = true;

            Point point = e.GetPosition(DrawCanvas);

            line = new Line();
            line.StrokeThickness = 1;
            line.Stroke = Brushes.Black;
            line.X1 = point.X;
            line.Y1 = point.Y;
            */

            if (drawingShape)
            {
                AddPoint(e.GetPosition(DrawCanvas));
            }

        }

        private void AddPoint(Point newPoint)
        {
            if (points.Count() == 0)
            {
                if (!DrawCanvas.Children.Contains(startPointMarker)) { 
                    DrawCanvas.Children.Add(startPointMarker);
                } else
                {
                    startPointMarker.Visibility = Visibility.Visible;
                }

                startPoint = newPoint;

                startPointMarker.SetValue(Canvas.LeftProperty, (double)newPoint.X-4);
                startPointMarker.SetValue(Canvas.TopProperty, (double)newPoint.Y-4);
            } else
            {
                if (DrawCanvas.Children.Contains(polyLine)){
                    DrawCanvas.Children.Remove(polyLine);
                }

                polyLine.Points = points;
                DrawCanvas.Children.Add(polyLine);

            }

            
            points.Add(newPoint);

            line.X1 = newPoint.X;
            line.Y1 = newPoint.Y;

        }

        private void DrawCanvas_MouseUp(object sender, MouseButtonEventArgs e)
        {

            if (drawingShape)
            {

            }

            /*
            if (drawing)
            {
                line.MouseEnter += new MouseEventHandler(LineMouse_Enter);
                line.MouseLeave += new MouseEventHandler(LineMouse_Leave);
                line.MouseDown += new MouseButtonEventHandler(LineMouse_Down);
                drawing = false;
            }

            dragging = false;
           */
        }

        private void DrawCanvas_MouseMove(object sender, MouseEventArgs e)
        {

            if(drawingShape && points.Count>0)
            {
                if (DrawCanvas.Children.Contains(line))
                {
                DrawCanvas.Children.Remove(line);
                }

                Point point = e.GetPosition(DrawCanvas);
                line.X2 = point.X;
                line.Y2 = point.Y;

                DrawCanvas.Children.Add(line);
            }

            /*
           

            
            Polygon p = (Polygon)seldShape;
            if (dragging)
            {
                Canvas.SetLeft(p, e.GetPosition(DrawCanvas).X - clickV.X);
                Canvas.SetTop(p, e.GetPosition(DrawCanvas).Y - clickV.Y);

            }
            */
        }

        private void LineMouse_Enter(object sender, RoutedEventArgs e)
        {
            if (sender is Line) { 
                Line aLine = (Line)sender;

                if (seldLine != aLine)
                    aLine.Stroke = new SolidColorBrush(Colors.LimeGreen);

            }
        }

        private void LineMouse_Down(object sender, RoutedEventArgs e)
        {
            if (sender is Line)
            {
                if (seldLine!=null)
                {
                    seldLine.Stroke = Brushes.Black;
                }

                Line aLine = (Line)sender;
                aLine.Stroke = new SolidColorBrush(Colors.Red);

                seldLine = aLine;

            }
        }


        private void LineMouse_Leave(object sender, RoutedEventArgs e)
        {
            if (sender is Line)
            {
                Line aLine = (Line)sender;

                if(seldLine != aLine)
                 aLine.Stroke = Brushes.Black;

            }

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(seldLine is Line)
            {
                DrawCanvas.Children.Remove(seldLine);
            }
        }


        private void BtnDraw_Click(object sender, RoutedEventArgs e)
        {
            DrawCanvas.Cursor = Cursors.Cross;
            drawingShape = true;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Random rnd = new Random();
            this.ImagePoints = new PointCollection(
                new[] { 
                    new Point((int)rnd.Next(1,500), (int)rnd.Next(1, 500)),
                    new Point((int)rnd.Next(1,500), (int)rnd.Next(1, 500)),
                    new Point((int)rnd.Next(1,500), (int)rnd.Next(1, 500)),
                }
                );
        }
    }
}
