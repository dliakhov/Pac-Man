using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows;
using System.Windows.Threading;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace OOP_courseProject
{
    class Sprite : GameElement
    {
        private readonly double _defaultX;
        private readonly double _defaultY;

        private DispatcherTimer animationTimer;

        public Sprite(int posx, int posy)
            : base(posx, posy)
        {
            _defaultX = posx;
            _defaultY = posy;
            animationTimer = new DispatcherTimer();
            double time = Math.Floor(Game.TIME / 5);
            animationTimer.Interval = TimeSpan.FromMilliseconds(time);
            animationTimer.Tick += new EventHandler(MotionAnimation);
        }

        public void Reset()
        {
            PositionX = _defaultX;
            PositionY = _defaultY;
        }

        public bool MoveRight()
        {
            return Move(CELL_SIZE, 0);
        }
        public bool MoveLeft()
        {
            return Move(-CELL_SIZE, 0);
        }
        public bool MoveUp()
        {
            return Move(0, -CELL_SIZE);
        }
        public bool MoveDown()
        {
            return Move(0, CELL_SIZE);
        }
        double _newx;
        double _newy;

        double numRepeats = 0;
        double addAdvancedx;
        double addAdvancedy;

        private bool Move(double addx, double addy)
        {
            if (numRepeats > 0)
                return false;
            _newx = PositionX + addx;
            _newy = PositionY + addy;
            addAdvancedx = addx / 8;
            addAdvancedy = addy / 8;
            if (AllowedToMove(_newx, _newy))
            {
                numRepeats = 7;
                animationTimer.Start();
                return true;
            }
            return false;
        }
        
        private void MotionAnimation(object sender, EventArgs e)
        {
            PositionX += addAdvancedx;
            PositionY += addAdvancedy;
            if ((numRepeats--) <= 0)
            {
                animationTimer.Stop();
                numRepeats = 0;
                PositionX = _newx;
                PositionY = _newy;
            }
            Renderer.SetElement(this);
        }

        private bool AllowedToMove(double _newx, double _newy)
        {
            var blocks = Container.onCreate().Blocks;
            foreach (Block block in blocks)
            {
                if ((block.PositionX == _newx) && (block.PositionY == _newy))
                    return false;
            }
            return true;
        }

        protected override Geometry LoadGeometry()
        {
            return new RectangleGeometry(new Rect(0, 0, 0, 0));
        }
    }
}
