using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Windows.Controls;
using System.Windows.Shapes;
using System.Windows.Media;

namespace OOP_courseProject
{
    abstract class GameElement : Shape
    {
        private double _x;
        private double _y;
        private int _z;
        public static readonly int CELL_SIZE = 32;

        protected override Geometry DefiningGeometry
        {
            get { return LoadGeometry(); }
        }

        public double PositionX
        {
            get { return _x; }
            protected set { _x = value; }
        }

        public double PositionY
        {
            get { return _y; }
            protected set { _y = value; }
        }

        public int PositionZ
        {
            get { return _z; }
            protected set { _z = value; }
        }

        public GameElement(int posy, int posx)
        {
            PositionX = posx;
            PositionY = posy;
        }
        
        protected abstract Geometry LoadGeometry();
    }
}
