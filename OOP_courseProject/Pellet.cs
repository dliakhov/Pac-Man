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
    class Pellet : GameElement
    {
        ImageBrush myBrush;
        BitmapImage playerBitmap;
        public Pellet(int posx, int posy)
            : base(posx, posy)
        {
            PositionZ = 0;
            myBrush = new ImageBrush();
            playerBitmap = new BitmapImage(new Uri(@"Images\dot.png", UriKind.Relative));
            myBrush.ImageSource = playerBitmap;
            Fill = myBrush;
        }
        protected override Geometry LoadGeometry()
        {
            return new RectangleGeometry(new Rect(0, 0, playerBitmap.Width, playerBitmap.Height));
        }
    }
}
