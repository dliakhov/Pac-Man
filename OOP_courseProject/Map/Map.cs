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
using OOP_courseProject.Enums;

namespace OOP_courseProject.Map
{
    class Map
    {
        private TypeGameElement[,] _elements;

        private MySize _size;
        public MySize Size
        {
            get { return _size; }
            set { _size = value; }
        }

        public TypeGameElement this[int x, int y] 
        {
            get 
            {
                if(IsValid(x, y))
                {
                    return _elements[x, y];
                }
                throw new IndexOutOfRangeException();
            }
            set {  _elements[x, y] = value; }
        }

        private bool IsValid(int x, int y)
        {
            return ((x >= 0) && (y >= 0) && (x < _size.Height) && (y < _size.Width));
        }

        public Map(MySize size)
        {
            _size = size;
            _elements = new TypeGameElement[size.Height, size.Width];
        }
    }
}
