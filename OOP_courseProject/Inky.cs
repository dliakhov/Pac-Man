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
    class Inky : Ghost
    {
        ImageBrush myBrush;
        BitmapImage playerBitmap;

        public Inky(int posx, int posy)
            : base(posx, posy)
        {
            myBrush = new ImageBrush();
            playerBitmap = new BitmapImage(new Uri(@"Images\Ghosts\inky.png", UriKind.Relative));
            myBrush.ImageSource = playerBitmap;
            Fill = myBrush;
            CoeficientDifficult = 30;
            CountSteps();
        }

        protected override Geometry LoadGeometry()
        {
            return new RectangleGeometry(new Rect(0, 0, playerBitmap.Width, playerBitmap.Height));
        }

        int count = 0;
        public void MoveInky(Blynki blynki, Map.Map map, GameElement gameElement, Direction direction)
        {
            int posX;
            int posY;

            count++;
            int[,] tempMap;

            if (count < NumStepsHunt)
            {
                tempMap = FindPathes(map, gameElement);
                MoveGhost(gameElement, map, tempMap);
            }
            else if ((count >= NumStepsHunt) && (count < NumStepsRest + NumStepsHunt))
            {
                switch (direction)
                {
                    case Direction.Down:
                        posX = Convert.ToInt32(gameElement.PositionX + 2 * GameElement.CELL_SIZE);
                        posY = Convert.ToInt32(gameElement.PositionY);
                        break;
                    case Direction.Up:
                        posX = Convert.ToInt32(gameElement.PositionX - 2 * GameElement.CELL_SIZE);
                        posY = Convert.ToInt32(gameElement.PositionY);
                        break;
                    case Direction.Left:
                        posX = Convert.ToInt32(gameElement.PositionX);
                        posY = Convert.ToInt32(gameElement.PositionY - 2 * GameElement.CELL_SIZE);
                        break;
                    case Direction.Right:
                        posX = Convert.ToInt32(gameElement.PositionX);
                        posY = Convert.ToInt32(gameElement.PositionY + 2 * GameElement.CELL_SIZE);
                        break;
                    default:
                        posX = Convert.ToInt32(gameElement.PositionX);
                        posY = Convert.ToInt32(gameElement.PositionY);
                        break;
                }
                GameElement tempGameElement = new Pellet(posX, posY);

                int posGoalX = Convert.ToInt32(2 * GetLengthX(blynki, tempGameElement));
                int posGoalY = Convert.ToInt32(2 * GetLengthY(blynki, tempGameElement));

                GameElement gameElementSearching = new Pellet(posGoalX * GameElement.CELL_SIZE, posGoalY * GameElement.CELL_SIZE);
                tempMap = FindPathes(map, gameElementSearching);
                MoveGhost(gameElementSearching, map, tempMap);
            }
            else { count = 0; }
        }
    }
}
