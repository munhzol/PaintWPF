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

namespace PaintWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        Line line = new Line();
        Boolean drawing = false;

        public MainWindow()
        {
            InitializeComponent();
        }


        private void DrawCanvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            drawing = true;

            Point point = e.GetPosition(DrawCanvas);

            line = new Line();
            line.StrokeThickness = 1;
            line.Stroke = Brushes.Black;
            line.X1 = point.X;
            line.Y1 = point.Y;

        }

        private void DrawCanvas_MouseUp(object sender, MouseButtonEventArgs e)
        {
            drawing = false;
        }

        private void DrawCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (drawing)
            {
                DrawCanvas.Children.Remove(line);

                Point point = e.GetPosition(DrawCanvas);
                line.X2 = point.X;
                line.Y2 = point.Y;

                DrawCanvas.Children.Add(line);
            }
        }
    }
}
