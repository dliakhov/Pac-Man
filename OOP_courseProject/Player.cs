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
    class Player : Sprite
    {
        private ImageBrush myBrush;
        private BitmapImage playerBitmap;
        
        public Player(int posx, int posy)
            : base(posx, posy)
        {
            PositionZ = 1;
            myBrush = new ImageBrush();
            playerBitmap = new BitmapImage(new Uri(@"Images\Pacmans\pacmanRight3.png", UriKind.Relative));
            myBrush.ImageSource = playerBitmap;
            Fill = myBrush;
        }
        public List<Pellet> Consume(List<Pellet> pells, Canvas gamefield, ref bool consumed)
        {
            consumed = false;
            foreach(Pellet pell in pells)
            {
                if((PositionX == pell.PositionX)&&(PositionY == pell.PositionY))
                {
                    gamefield.Children.Remove(pell);
                    pells.Remove(pell);
                    
                    PelletConsumed();
                    consumed = true;
                    return pells;
                }
            }
            return pells;
        }

        public event Action PelletConsumed = delegate { };

        protected override Geometry LoadGeometry()
        {
            return new RectangleGeometry(new Rect(0, 0, playerBitmap.Width, playerBitmap.Height));
        }
    }
}
