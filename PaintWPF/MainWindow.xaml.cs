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
    public partial class MainWindow : Window
    {

        ShapePointCollection spc = new ShapePointCollection();
        Polygon myPoly = new Polygon();

        Shape seldShape;

        public MainWindow()
        {
            InitializeComponent();

            spc.ShapePoints = new PointCollection(
                new[] { new Point(10, 10), new Point(80, 80), new Point(50, 60) }
                );

            spc.ShapePoints.Add(new Point(90, 300));

            Binding myBinding = new Binding("ShapePoints");
            myBinding.Source = spc;

            
            myPoly.Fill = new SolidColorBrush(Colors.Blue);
            myPoly.SetBinding(Polygon.PointsProperty, myBinding);
            myPoly.Name = "myPoly";

            DrawCanvas.Children.Add(myPoly);

            /*
            MyPolygon poly = new MyPolygon()
            {
                points = new PointCollection(
                new[] { new Point(10, 10), new Point(80, 80), new Point(50, 60) }
                ),
                Stroke = Brushes.Black,
                StrokeThickness = 1,
            };
            DrawCanvas.Children.Add(poly.Polygon);
            */

            RectangleA recA = new RectangleA();
            recA.Fill = Brushes.Blue;
            DrawCanvas.Children.Add(recA);

        }


        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            
            Random rnd = new Random();
            spc.ShapePoints = new PointCollection(
                new[] { 
                    new Point((int)rnd.Next(1,500), (int)rnd.Next(1, 500)),
                    new Point((int)rnd.Next(1,500), (int)rnd.Next(1, 500)),
                    new Point((int)rnd.Next(1,500), (int)rnd.Next(1, 500)),
                }
                );
           
        }

        private void DrawCanvas_Drop(object sender, DragEventArgs e)
        {
            /*
            Point dropPosition = e.GetPosition(DrawCanvas);
            Canvas.SetLeft(redRectangle, dropPosition.X);
            Canvas.SetTop(redRectangle, dropPosition.Y);
            */
        }

        private void redRectangle_MouseMove(object sender, MouseEventArgs e)
        {
            if(e.LeftButton == MouseButtonState.Pressed)
            {
                seldShape = (Shape)sender;
                DragDrop.DoDragDrop(seldShape, seldShape, DragDropEffects.Move);
            }
        }

        private void DrawCanvas_DragOver(object sender, DragEventArgs e)
        {
            Point dropPosition = e.GetPosition(DrawCanvas);
            Canvas.SetLeft(seldShape, dropPosition.X);
            Canvas.SetTop(seldShape, dropPosition.Y);
        }
    }
}
