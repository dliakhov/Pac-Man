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
    class Blynki : Ghost
    {
        ImageBrush myBrush;
        BitmapImage playerBitmap;

        public Blynki(int posx, int posy)
            : base(posx, posy)
        {
            myBrush = new ImageBrush();
            playerBitmap = new BitmapImage(new Uri(@"Images\Ghosts\blynki.png", UriKind.Relative));
            myBrush.ImageSource = playerBitmap;
            Fill = myBrush;
            CoeficientDifficult = 50;
            CountSteps();
        }

        protected override Geometry LoadGeometry()
        {
            return new RectangleGeometry(new Rect(0, 0, playerBitmap.Width, playerBitmap.Height));
        }
        int counter = 0;

        public void MoveBlynki(Map.Map map, GameElement gameElement)
        {
            int[,] tempMap;
            GameElement gameElementSearching;
            counter++;
            if (counter < NumStepsHunt)
            {
                tempMap = FindPathes(map, gameElement);
                MoveGhost(gameElement, map, tempMap);
            }
            else if (counter >= NumStepsHunt && counter < NumStepsRest + NumStepsHunt)
            {
                gameElementSearching = new Pellet(1 * CELL_SIZE, 5 * CELL_SIZE);
                tempMap = FindPathes(map, gameElementSearching);
                MoveGhost(gameElementSearching, map, tempMap);
            }
            else { counter = 0; }
        }
    }
}
