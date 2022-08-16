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

        Point p1 = new Point();
        Point p2 = new Point();


        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show("OK");

            Line line = new Line();
            line.StrokeThickness = 1;
            line.Stroke = Brushes.Black;

            line.X1 = 10;
            line.Y1 = 10;
            line.X2 = 100;
            line.Y2 = 100;

            DrawCanvas.Children.Add(line);


        }

        private void DrawCanvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            p1 = e.GetPosition(DrawCanvas);
        }

        private void DrawCanvas_MouseUp(object sender, MouseButtonEventArgs e)
        {
            p2 = e.GetPosition(DrawCanvas);
            DrawALine();

        }

        private void DrawALine()
        {
            Line line = new Line();
            line.StrokeThickness = 1;
            line.Stroke = Brushes.Black;

            line.X1 = p1.X;
            line.Y1 = p1.Y;
            line.X2 = p2.X;
            line.Y2 = p2.Y;

            DrawCanvas.Children.Add(line);
        }
    }
}
