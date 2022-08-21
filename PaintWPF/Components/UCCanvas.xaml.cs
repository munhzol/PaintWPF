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

namespace PaintWPF.Components
{
    /// <summary>
    /// Interaction logic for UCCanvas.xaml
    /// </summary>

    public partial class UCCanvas : UserControl
    {
        
        bool drawing = false;
        PointCollection points = new PointCollection();

        Rectangle startPointMarker = new Rectangle();

        Polyline polyLine = new Polyline();

        Line line = new Line();

        UIElement seldShape = new UIElement();

        public UCCanvas()
        {
            InitializeComponent();

        }

        private void StartPoint_MouseDown(object sender, MouseButtonEventArgs e)
        {
            FinishDraw();
        }


        private void DrawCanvas_DragOver(object sender, DragEventArgs e)
        {
            Point dropPosition = e.GetPosition(DrawCanvas);
            Canvas.SetLeft(seldShape, dropPosition.X);
            Canvas.SetTop(seldShape, dropPosition.Y);
        }

        private void DrawCanvas_Drop(object sender, DragEventArgs e)
        {
            /*
           Point dropPosition = e.GetPosition(DrawCanvas);
           Canvas.SetLeft(redRectangle, dropPosition.X);
           Canvas.SetTop(redRectangle, dropPosition.Y);
           */
        }

        private void Drag_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                seldShape = (UIElement)sender;
                DragDrop.DoDragDrop(seldShape, seldShape, DragDropEffects.Move);
            }
        }


        public void Clear()
        {
            DrawCanvas.Children.Clear();
            //Console.WriteLine("Blah");
        }

        public void StartDraw()
        {
            DrawCanvas.Cursor = Cursors.Cross;

            drawing = true;

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

            startPointMarker = new Rectangle()
            {
                Stroke = Brushes.Black,
                Fill = Brushes.Blue,
                StrokeThickness = 1,
                Width = 8,
                Height = 8,
            };
            DrawCanvas.Children.Add(startPointMarker);
            startPointMarker.Visibility = Visibility.Hidden;
            startPointMarker.MouseDown += new MouseButtonEventHandler(StartPoint_MouseDown);
            Panel.SetZIndex(startPointMarker, 1);

        }



        private void DrawCanvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
          if(drawing)
            {
                AddPoint(e.GetPosition(DrawCanvas));
            }

        }


        private void AddPoint(Point newPoint)
        {
            if (points.Count == 0)
            {
                startPointMarker.SetValue(Canvas.LeftProperty, (double)newPoint.X - 4);
                startPointMarker.SetValue(Canvas.TopProperty, (double)newPoint.Y - 4);

                if (!DrawCanvas.Children.Contains(startPointMarker))
                    DrawCanvas.Children.Add(startPointMarker);
                 else
                    startPointMarker.Visibility = Visibility.Visible;
            }
            else
            {
                
                if (DrawCanvas.Children.Contains(polyLine))
                {
                    DrawCanvas.Children.Remove(polyLine);
                }

                polyLine.Points = points;
                DrawCanvas.Children.Add(polyLine);

            }


            points.Add(newPoint);

            line.X1 = newPoint.X;
            line.Y1 = newPoint.Y;

        }

        private void DrawCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (drawing && points.Count > 0)
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
        }

        private void CancelDraw()
        {
            DrawCanvas.Children.Remove(polyLine);
            drawing = false;
            DrawCanvas.Cursor = Cursors.Arrow;
            DrawCanvas.Children.Remove(line);
            startPointMarker.Visibility = Visibility.Hidden;
        }

        private void DrawCanvas_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            CancelDraw();
        }

        private void StartPoint_MouseButtonUp(object sender, MouseEventArgs e)
        {
            FinishDraw();
        }

        private void FinishDraw()
        {
            if (drawing && points.Count>2)
            {
                Polygon myPoly = new Polygon();

                myPoly.Points = points;

                myPoly.Fill = new SolidColorBrush(Colors.Blue);

                myPoly.MouseMove += new MouseEventHandler(Drag_MouseMove);

                DrawCanvas.Children.Add(myPoly);

                CancelDraw();



            }
        }

        private void StartPoint_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Ok");
        }

        private void RectangleA_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Ok");
        }

        private void startPointMarker_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Ok");
        }

 
    }
}
