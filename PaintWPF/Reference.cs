
// Draw a line on a canvas
/*
 * 
 
 //MessageBox.Show("OK");

    Line line = new Line();
    line.StrokeThickness = 1;
    line.Stroke = Brushes.Black;

    line.X1 = 10;
    line.Y1 = 10;
    line.X2 = 100;
    line.Y2 = 100;

    DrawCanvas.Children.Add(line);

 * 
 */


// get mouse position related to the canvas
/*    
    Point point = e.GetPosition(DrawCanvas);
    String msg = $"{point.X} - {point.Y}";
    MessageBox.Show(msg);
 */



// Add a event handler to a new created line
/*
 * 

   private void DrawCanvas_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (drawing)
            {
                line.MouseEnter += new MouseEventHandler(LineMouse_Enter);
                line.MouseLeave += new MouseEventHandler(LineMouse_Leave);
                drawing = false;
            }
           
        }

    private void LineMouse_Enter(object sender, RoutedEventArgs e)
        {
            if (sender is Line) { 
                Line aLine = (Line)sender;

                aLine.Stroke = new SolidColorBrush(Colors.LimeGreen);

            }

        }

        private void LineMouse_Leave(object sender, RoutedEventArgs e)
        {
            if (sender is Line)
            {
                Line aLine = (Line)sender;

                aLine.Stroke = Brushes.Black;

            }

        }

 * 
 */



//Remove from canvas
/*
  DrawCanvas.Children.Remove(seldLine);
 */



//Get sender as a LINE
/*
   private void LineMouse_Leave(object sender, RoutedEventArgs e)
        {
            if (sender is Line)
            {
                Line aLine = (Line)sender;

                if(seldLine != aLine)
                 aLine.Stroke = Brushes.Black;

            }

        }
 */




//Stack elements

/*
 
    <StackPanel DockPanel.Dock="Top" Orientation="Horizontal" Margin="8">
            <Button Content="draw a line" Canvas.Left="40" Canvas.Top="-40" HorizontalAlignment="Left" VerticalAlignment="Center" Width="84"/>
            <Button Content="remove a line" Canvas.Left="150" Canvas.Top="-40" Width="84" HorizontalAlignment="Left" VerticalAlignment="Center" Click="Button_Click"/>
        </StackPanel>
 
 */




// ELLIPSE, RECTANGLE
/*
 
  Ellipse startPoint = new Ellipse()
        {
            Stroke = Brushes.Black,
            StrokeThickness = 1,
            Width = 8,
            Height = 8,
        };

        Rectangle rect = new Rectangle()
        {
            Stroke = Brushes.Black,
            StrokeThickness = 1,
            Width = 8,
            Height = 8,
        };


 */






// Variable Binding

/*

   // MainWindow.xaml.cs

    public partial class MainWindow : Window, INotifyPropertyChanged
    .....

    public event PropertyChangedEventHandler? PropertyChanged;
    PointCollection imagePoints;
 
 
    
    public MainWindow()
    {
        InitializeComponent();

        .......

        this.ImagePoints = new PointCollection(
                new[] { new Point(10, 10), new Point(80, 80), new Point(50, 60) }
                );
        this.DataContext = this;

        ......
    }

    public PointCollection ImagePoints
        {
            get { return imagePoints; }
            set
            {
                if (this.imagePoints != value)
                {
                    this.imagePoints = value;
                    if (this.PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("ImagePoints"));
                    }
                }
            }
        }


       private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Random rnd = new Random();
            this.ImagePoints = new PointCollection(
                new[] { 
                    new Point((int)rnd.Next(1,500), (int)rnd.Next(1, 500)),
                    new Point((int)rnd.Next(1,500), (int)rnd.Next(1, 500)),
                    new Point((int)rnd.Next(1,500), (int)rnd.Next(1, 500)),
                }
                );
        }



   // MainWindow.xaml

     ......
     <Polygon x:Name="imgPolygon" Points="{Binding ImagePoints}" Stretch="Fill" Fill="Black" Opacity="0.8" />

     <Button ToolTip="Set New Points" Click="Button_Click_1">
        <TextBlock>
          Set New Points
         </TextBlock>
     </Button>
    .........

 
 */







/*
   <Canvas Background="LightGray" x:Name="DrawCanvas" Margin="0,0,0,0" MouseDown="DrawCanvas_MouseDown" MouseUp="DrawCanvas_MouseUp" MouseMove="DrawCanvas_MouseMove">
            <Polygon x:Name="imgPolygon" Points="{Binding ImagePoints}" Stretch="Fill" Fill="Black" Opacity="0.8" />
        </Canvas>
 */







// Binding with ShapePointCollection class
/*

    ....
    ShapePointCollection spc = new ShapePointCollection();
    ....

    public MainWindow()
        {
            InitializeComponent();

            spc.ShapePoints = new PointCollection(
                new[] { new Point(10, 10), new Point(80, 80), new Point(50, 60) }
                );

            Binding myBinding = new Binding("ShapePoints");
            myBinding.Source = spc;

            Polygon myPoly = new Polygon();
            myPoly.Fill = new SolidColorBrush(Colors.Blue);
            myPoly.SetBinding(Polygon.PointsProperty, myBinding);
            DrawCanvas.Children.Add(myPoly);

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



//ShapePointCollection.cs

     internal class ShapePointCollection : INotifyPropertyChanged
        {
            public event PropertyChangedEventHandler? PropertyChanged;

            private PointCollection shapePoints;

            public ShapePointCollection() { }

            public PointCollection ShapePoints
            {
                get { return shapePoints; }
                set
                {
                    if (this.shapePoints != value)
                    {
                        this.shapePoints = value;
                        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ShapePoints"));
                    }
                }
            }

        }

 */