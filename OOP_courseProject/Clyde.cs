using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;
using System.Windows.Media.Imaging;
using OOP_courseProject.Enums;
using OOP_courseProject.Map;


namespace OOP_courseProject
{
    class Clyde : Ghost
    {
        ImageBrush myBrush;
        BitmapImage playerBitmap;
        private MediaPlayer musicDeath;

        public Clyde(int posx, int posy)
            : base(posx, posy)
        {
            myBrush = new ImageBrush();
            playerBitmap = new BitmapImage(new Uri(@"Images\Ghosts\clyde.png", UriKind.Relative));
            myBrush.ImageSource = playerBitmap;
            Fill = myBrush;
        }
        protected override Geometry LoadGeometry()
        {
            return new RectangleGeometry(new Rect(0, 0, playerBitmap.Width, playerBitmap.Height));
        }

        public void MoveClyde(Map.Map map, GameElement gameElement)
        {
            GameElement gameElementSearching;
            int[,] tempMap;
            if (GetFullLength(this, gameElement) > 4)
            {
                tempMap = FindPathes(map, gameElement);
                MoveGhost(gameElement, map, tempMap);
            }
            else
            {
                gameElementSearching = new Pellet(CELL_SIZE, CELL_SIZE);
                tempMap = FindPathes(map, gameElementSearching);
                MoveGhost(gameElementSearching, map, tempMap);
            }
        }
    }
}
