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
        // Drawing indicator
        bool drawing = false;

        UIElement seldShape = new UIElement();

        Point clickV;

        public UCCanvas()
        {
            InitializeComponent();
        }

        
        public void StartDraw()
        {
            //Change cursor
            mainCanvas.Cursor = Cursors.Cross;

            drawing = true;

            // init painter Canvas
            painter.Clear();
        }

        
        private void DrawCanvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (drawing)
            {
                Point point = e.GetPosition(mainCanvas);
                painter.AddPoint(point);
            }

        }

        private void DrawCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (drawing)
            {
                Point point = e.GetPosition(mainCanvas);
                painter.DrawLineOnMouseMove(point);
            }
        }


        // To Cancel drawing Click Mouse right button while drawing
        private void DrawCanvas_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (drawing)
            {
                painter.Clear();
                mainCanvas.Cursor = Cursors.Arrow;
                drawing = false;
            }
        }





        private void DrawCanvas_DragOver(object sender, DragEventArgs e)
        {
            Point dropPosition = e.GetPosition(mainCanvas);
            Canvas.SetLeft(seldShape, dropPosition.X - clickV.X);
            Canvas.SetTop(seldShape, dropPosition.Y - clickV.Y);
        }

        private void DrawCanvas_Drop(object sender, DragEventArgs e)
        {
            /*
           Point dropPosition = e.GetPosition(DrawCanvas);
           Canvas.SetLeft(redRectangle, dropPosition.X - clickV.X);
           Canvas.SetTop(redRectangle, dropPosition.Y - clickV.Y);
           */
        }

        private void Drag_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                seldShape = (UIElement)sender;
                clickV = e.GetPosition(seldShape);  
                DragDrop.DoDragDrop(seldShape, seldShape, DragDropEffects.Move);
            }
        }


        public void Clear()
        {
            //mainCanvas.Children.Clear();
            //Console.WriteLine("Blah");
        }


        
        // Draw Finish Event handler
        private void painter_DrawFinished(object sender, RoutedEventArgs e)
        {
            Polygon polygon = new Polygon();

            polygon.Points = painter.points;

            painter.Clear();
            mainCanvas.Cursor = Cursors.Arrow;
            drawing = false;

            polygon.Fill = new SolidColorBrush(Colors.Transparent);
            polygon.Stroke = new SolidColorBrush(Colors.Black);
            polygon.StrokeThickness = 1;
            polygon.MouseMove += new MouseEventHandler(Drag_MouseMove);

            mainCanvas.Children.Add(polygon);

            

        }
    }
}
