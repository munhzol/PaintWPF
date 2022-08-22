using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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

namespace PaintWPF.Components
{
    /// <summary>
    /// Interaction logic for UCPainter.xaml
    /// </summary>
    public partial class UCPainter : UserControl
    {
        public static readonly RoutedEvent DrawFinishedEvent = EventManager.RegisterRoutedEvent(
            "DrawFinished", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(UCPainter));

        public PointCollection points = new PointCollection();

        Rectangle startPointMarker = new Rectangle()
        {
            Stroke = Brushes.Black,
            Fill = Brushes.Blue,
            StrokeThickness = 1,
            Width = 8,
            Height = 8,
        };

        Polyline polyLine = new Polyline();

        Line line = new Line();


        public event RoutedEventHandler DrawFinished
        {
            add { AddHandler(DrawFinishedEvent, value);  }
            remove { RemoveHandler(DrawFinishedEvent, value); }
        }

      
        public UCPainter()
        {
            InitializeComponent();

            painterCanvas.Children.Add(startPointMarker);
            startPointMarker.Visibility = Visibility.Hidden;
            startPointMarker.MouseDown += new MouseButtonEventHandler(StartPoint_MouseDown);
            Panel.SetZIndex(startPointMarker, 1);

        }

        public void Clear()
        {
            // clears all old values
            if (painterCanvas.Children.Contains(polyLine))
            {
                painterCanvas.Children.Remove(polyLine);
            }
            if (painterCanvas.Children.Contains(line))
            {
                painterCanvas.Children.Remove(line);
            }

            startPointMarker.Visibility = Visibility.Hidden;

            polyLine = new Polyline()
            {
                Stroke = Brushes.Black,
                StrokeThickness = 1,
            };

            points = new PointCollection();

            line = new Line()
            {
                Stroke = Brushes.Black,
                StrokeThickness = 1,
            };
        }

        public void AddPoint(Point newPoint)
        {
            if (points.Count == 0)
            {
                startPointMarker.SetValue(Canvas.LeftProperty, (double)newPoint.X - 4);
                startPointMarker.SetValue(Canvas.TopProperty, (double)newPoint.Y - 4);

                if (!painterCanvas.Children.Contains(startPointMarker))
                    painterCanvas.Children.Add(startPointMarker);
                else
                    startPointMarker.Visibility = Visibility.Visible;
            }
            else
            {

                if (painterCanvas.Children.Contains(polyLine))
                {
                    painterCanvas.Children.Remove(polyLine);
                }

                polyLine.Points = points;
                painterCanvas.Children.Add(polyLine);

            }

            points.Add(newPoint);

            line.X1 = newPoint.X;
            line.Y1 = newPoint.Y;

        }

        public void DrawLineOnMouseMove(Point point)
        {
            if (points.Count > 0)
            {
                if (painterCanvas.Children.Contains(line))
                {
                    painterCanvas.Children.Remove(line);
                }

                line.X2 = point.X;
                line.Y2 = point.Y;

                painterCanvas.Children.Add(line);
            }


        }

        private void StartPoint_MouseDown(object sender, MouseButtonEventArgs e)
        {
            startPointMarker.Visibility = Visibility.Hidden;
            if (painterCanvas.Children.Contains(line))
            {
                painterCanvas.Children.Remove(line);
            }
            RaiseEvent(new RoutedEventArgs(DrawFinishedEvent, this));
        }
      
    }
}
