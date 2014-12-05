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
using OOP_courseProject.Map;
using OOP_courseProject.Enums;

namespace OOP_courseProject
{
    class Renderer
    {
        int numFrame = 3;
        ImageBrush myBrush;
        BitmapImage playerBitmap;
        public void AnimationPacman(Player pacman, string direction)
        {
            myBrush = new ImageBrush();
            playerBitmap = new BitmapImage(new Uri(@"Images\Pacmans\pacman" + direction + numFrame.ToString() + ".png", UriKind.Relative));
            myBrush.ImageSource = playerBitmap;
            pacman.Fill = myBrush;
            if (numFrame == 5)
                numFrame = 1;
            else
                numFrame++;
        }

        public static void SetElement(GameElement element)
        {
            Canvas.SetTop(element, element.PositionY);
            Canvas.SetLeft(element, element.PositionX);
            Canvas.SetZIndex(element, element.PositionZ);
        }
    }
}
