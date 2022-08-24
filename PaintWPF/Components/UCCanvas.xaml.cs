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

        Polygon? seldPolygon = null;
        bool draggingPolygon = false;

        bool draggingPointPolygon = false;
        int draggingPointIndex = -1;
        UCVertex? seldPointPolygon = null;
        List<UCVertex> seldPolygonPoints = new List<UCVertex>();

        // Mouse poistion when clicked on Canvas, it helps to move object properly
        Point clickV;

        public UCCanvas()
        {
            InitializeComponent();
        }

        public void DeletePolygon()
        {
            if (mainCanvas.Children.Contains(seldPolygon))
            {
                DeSelectPolygon();
                mainCanvas.Children.Remove(seldPolygon);
            }
               
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

            if(draggingPolygon == true)
            {
                DeSelectPolygon(); 
                Canvas.SetLeft(seldPolygon, dropPosition.X - clickV.X);
                Canvas.SetTop(seldPolygon, dropPosition.Y - clickV.Y);
            }

            if (draggingPointPolygon == true && seldPolygon != null && seldPointPolygon != null)
            {
                
                    double left = Canvas.GetLeft(seldPolygon);
                    double top = Canvas.GetTop(seldPolygon);


                    //double x = dropPosition.X - clickV.X + 4 ;
                    //double y = dropPosition.Y - clickV.Y + 4 ;


                    double x = dropPosition.X - (double.IsNaN(left) ? 0 : left);
                    double y = dropPosition.Y - (double.IsNaN(top) ? 0 : top);


                    // Position Blue marker dot while changing polygon shape
                    Canvas.SetLeft(seldPointPolygon, dropPosition.X - 4);
                    Canvas.SetTop(seldPointPolygon, dropPosition.Y - 4);

                    // Moving Polygon point
                    seldPolygon.Points[index: seldPointPolygon.VertexIndex] = 
                        new Point(x, y);
          
            }

        }


        private void DrawCanvas_Drop(object sender, DragEventArgs e)
        {
            SelectPolygon(seldPolygon);
            draggingPolygon = false;
            draggingPointPolygon = false;

            /*
           Point dropPosition = e.GetPosition(DrawCanvas);
           Canvas.SetLeft(redRectangle, dropPosition.X - clickV.X);
           Canvas.SetTop(redRectangle, dropPosition.Y - clickV.Y);
           */
        }

        private void PolygonDrag_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed && sender is Polygon)
            {
                // Drag Polygon
                seldPolygon = (Polygon)sender;
                clickV = e.GetPosition(seldPolygon);
                DragDrop.DoDragDrop(seldPolygon, seldPolygon, DragDropEffects.Move);
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
            polygon.MouseMove += new MouseEventHandler(PolygonDrag_MouseMove);
            polygon.MouseDown += new MouseButtonEventHandler(Polygon_MouseDown);

            mainCanvas.Children.Add(polygon);

        }

        private void Polygon_MouseDown(object sender, RoutedEventArgs e)
        {
            seldPolygon = (Polygon)sender;
            SelectPolygon(seldPolygon);
            draggingPointPolygon = false;
            draggingPolygon = true;
        }

        public void FillPolygon()
        {
            if (seldPolygon != null)
            {
                Random r = new Random();
                seldPolygon.Fill = new SolidColorBrush(
                    Color.FromRgb((byte)r.Next(1, 255), (byte)r.Next(1, 255), (byte)r.Next(1, 233)));
            }
        }

        public void SelectPolygon()
        {
            
        }

        public void UnSelectPolygon()
        {
            
        }

        private void SelectPolygon(Polygon? polygon)
        {

            DeSelectPolygon();

            if(polygon == null)
            {
                return;
            }

            int cnt = polygon.Points.Count;
            for (int i = 0; i < cnt; i++)
            {

                UCVertex vertex = new UCVertex();

                mainCanvas.Children.Add(vertex);

                vertex.VertexIndex = i;


                double left = Canvas.GetLeft(polygon);
                double top = Canvas.GetTop(polygon);

                vertex.SetValue(Canvas.LeftProperty, (double.IsNaN(left) ? 0 : left) + (double)polygon.Points[i].X - 4);
                vertex.SetValue(Canvas.TopProperty, (double.IsNaN(top) ? 0 : top) + (double)polygon.Points[i].Y - 4);

                vertex.MouseMove += new MouseEventHandler(PointPolygonDrag_MouseMove);
                vertex.MouseDown += new MouseButtonEventHandler(PointPolygon_MouseDown);

                seldPolygonPoints.Add(vertex);

            }
        }

        private void PointPolygonDrag_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed && sender is UCVertex)
            {
                // Drag Polygon
                seldPointPolygon = (UCVertex)sender;
                clickV = e.GetPosition(seldPointPolygon);
                DragDrop.DoDragDrop(seldPointPolygon, seldPointPolygon, DragDropEffects.Move);
            }
        }

        private void PointPolygon_MouseDown(object sender, RoutedEventArgs e)
        {
            seldPointPolygon = (UCVertex)sender;
            draggingPointPolygon = true;
            draggingPolygon = false;
            
        }

        private void DeSelectPolygon()
        {
            int cnt = seldPolygonPoints.Count;

            for(int i=0; i<cnt; i++)
            {
                mainCanvas.Children.Remove(seldPolygonPoints[i]);
            }

            seldPolygonPoints.Clear();
        }

    }
}
