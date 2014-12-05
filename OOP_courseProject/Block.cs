using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;

namespace OOP_courseProject
{
    class Block : GameElement
    {
        ImageBrush myBrush;
        BitmapImage playerBitmap;
        public Block(int posx, int posy)
            : base(posx, posy)
        {
            myBrush = new ImageBrush();
            playerBitmap = new BitmapImage(new Uri(@"Images\block.png", UriKind.Relative));
            myBrush.ImageSource = playerBitmap;
            Fill = myBrush;
        }
        protected override Geometry LoadGeometry()
        {
            return new RectangleGeometry(new Rect(0, 0, playerBitmap.Width, playerBitmap.Height));
        }
    }
}
