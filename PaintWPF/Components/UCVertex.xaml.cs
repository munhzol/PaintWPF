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
    /// Selected Polygon Vertex - Point
    /// </summary>
    public partial class UCVertex : UserControl
    {

        private int vertexIndex = -1;

        public UCVertex()
        {
            InitializeComponent();
        }

        public int VertexIndex
        {
            get { return vertexIndex; }
            set { vertexIndex = value; }
        }
    }
}
