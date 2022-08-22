using PaintWPF.Components;
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

        public MainWindow()
        {
            InitializeComponent();
        }


        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            ucCanwas.Clear();
        }

        private void ButtonStartDraw_Click(object sender, RoutedEventArgs e)
        {
            ucCanwas.StartDraw();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {

        }

        private void ButtonDeletePolygon_Click(object sender, RoutedEventArgs e)
        {
            // add binding on seldShape to disable or enable this button

            MessageBoxResult result = MessageBox.Show("Do you want to delete selected Polygon?",
                "Confirmation", MessageBoxButton.OKCancel);

            if(result == MessageBoxResult.OK)
            {
                ucCanwas.DeletePolygon();
            }

            
        }

        private void ButtonFillPolygon_Click(object sender, RoutedEventArgs e)
        {
            ucCanwas.FillPolygon();
        }
    }
}
