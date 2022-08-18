using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace PaintWPF
{
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
}
