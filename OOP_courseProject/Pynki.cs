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
    class Pynki : Ghost
    {
        ImageBrush myBrush;
        BitmapImage playerBitmap;

        public Pynki(int posx, int posy)
            : base(posx, posy)
        {
            myBrush = new ImageBrush();
            playerBitmap = new BitmapImage(new Uri(@"Images\Ghosts\pinky.png", UriKind.Relative));
            myBrush.ImageSource = playerBitmap;
            Fill = myBrush;
        }
        protected override Geometry LoadGeometry()
        {
            return new RectangleGeometry(new Rect(0, 0, playerBitmap.Width, playerBitmap.Height));
        }

        public void MovePynki(Map.Map map, GameElement gameElement, Direction direction)
        {
            int posX;
            int posY;
            switch (direction)
            {
                case Direction.Down:
                    posX = Convert.ToInt32(gameElement.PositionX + 4 * CELL_SIZE);
                    posY = Convert.ToInt32(gameElement.PositionY);
                    break;
                case Direction.Up:
                    posX = Convert.ToInt32(gameElement.PositionX - 4 * CELL_SIZE);
                    posY = Convert.ToInt32(gameElement.PositionY);
                    break;
                case Direction.Left:
                    posX = Convert.ToInt32(gameElement.PositionX);
                    posY = Convert.ToInt32(gameElement.PositionY - 4 * CELL_SIZE);
                    break;
                case Direction.Right:
                    posX = Convert.ToInt32(gameElement.PositionX);
                    posY = Convert.ToInt32(gameElement.PositionY + 4 * CELL_SIZE);
                    break;
                default:
                    posX = Convert.ToInt32(gameElement.PositionX + 4 * CELL_SIZE);
                    posY = Convert.ToInt32(gameElement.PositionY + 4 * CELL_SIZE);
                    break;
            }

            GameElement gameElementSearching = new Pellet(posX, posY);
            int[,] tempMap = FindPathes(map, gameElementSearching);
            MoveGhost(gameElementSearching, map, tempMap);
        }
    }
}
