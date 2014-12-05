using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_courseProject.Map
{
    class MySize
    {
        private  int _width;
        private  int _height;
        public  int Width
        {
            get { return _width; }
            set { _width = value; }
        }
        public  int Height
        {
            get { return _height; }
            set { _height = value; }
        }
        public MySize(int height, int width)
        {
            Width = width;
            Height = height;
        }
    }
}
