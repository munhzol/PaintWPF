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
    /// Interaction logic for UCPolygon.xaml
    /// </summary>
    public partial class UCPolygon : UserControl
    {
        public static readonly DependencyProperty 
            PointsProperty = DependencyProperty.Register("Points", typeof(PointCollection), 
                typeof(UCPolygon), new PropertyMetadata());

        public static readonly DependencyProperty
            FillProperty = DependencyProperty.Register("Fill", typeof(Brush), 
                typeof(UCPolygon), new PropertyMetadata());


        public UCPolygon()
        {
            InitializeComponent();
        }

        public PointCollection Points
        {
            get { return (PointCollection)GetValue(PointsProperty); }
            set { SetValue(PointsProperty, value); }
        }

        public Brush Fill
        {
            get { return (Brush)GetValue(FillProperty); }
            set { SetValue(FillProperty, value); }
        }

        public void GetSelected()
        {
            int cnt = polygon.Points.Count;
            for(int i = 0; i<cnt; i++)
            {
                Rectangle rect = new Rectangle()
                {
                    Width = 8,
                    Height = 8,
                    Fill = Brushes.Blue
                };
                canvasPolygon.Children.Add(rect);

                rect.SetValue(Canvas.LeftProperty, (double)polygon.Points[i].X - 4);
                rect.SetValue(Canvas.TopProperty, (double)polygon.Points[i].Y - 4);
                //rect.MouseDown += new MouseButtonEventHandler(Point_MouseDown);
                //rect.MouseMove += new MouseEventHandler(Point_MouseMove);

                //polygon.MouseMove += new MouseEventHandler(Drag_MouseMove);
                //polygon.MouseDown += new MouseButtonEventHandler(Polygon_MouseDown);
            }
            
        }

        public void GetUnSelected()
        {
            Polygon p = polygon;
            canvasPolygon.Children.Clear();
            canvasPolygon.Children.Add(p);
        }

    }
}
