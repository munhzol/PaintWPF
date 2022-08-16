
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