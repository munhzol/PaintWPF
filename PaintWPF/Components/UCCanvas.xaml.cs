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
        UIElement seldShape = new UIElement();

        public UCCanvas()
        {
            InitializeComponent();
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


        public void Blah()
        {
            DrawCanvas.Children.Clear();
            //Console.WriteLine("Blah");
        }

    }
}
