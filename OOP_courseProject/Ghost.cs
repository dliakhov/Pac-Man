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
    class Ghost : Sprite
    {
        public event Action ConsumePlayer = delegate { }; 

        BitmapImage playerBitmap;
        private int _coeficient;

        protected int CoeficientDifficult
        {
            get { return _coeficient; }
            set { _coeficient = value; }
        }
        int _numStepsHunt;
        int _numStepsRest;

        protected int NumStepsHunt
        {
            get { return _numStepsHunt; }
            set { _numStepsHunt = value; }
        }

        protected int NumStepsRest
        {
            get { return _numStepsRest; }
            set { _numStepsRest = value; }
        }
        private readonly int PACMAN = 0;
        private int GHOST;
        int posGameElementX;
        int posGameElementY;

        protected readonly int COEFICIENT_DIFF = 100;

        int posGhostX;
        int posGhostY;

        public Ghost(int posx, int posy)
            : base(posx, posy)
        {
            PositionZ = 2;
        }

        protected void CountSteps()
        {
            NumStepsHunt = (int)COEFICIENT_DIFF + CoeficientDifficult;
            NumStepsRest = 200 - (int)COEFICIENT_DIFF;
        }


        protected override Geometry LoadGeometry()
        {
            return new RectangleGeometry(new Rect(0, 0, playerBitmap.Width, playerBitmap.Height));
        }

        public bool ConsumePacman(Player pacman, Canvas gamefield)
        {
            double minPacmanPositionX = pacman.PositionX - CELL_SIZE / 2;
            double maxPacmanPositionX = pacman.PositionX + CELL_SIZE / 2;
            double minPacmanPositionY = pacman.PositionY - CELL_SIZE / 2;
            double maxPacmanPositionY = pacman.PositionY + CELL_SIZE / 2;

            if ((PositionX <= maxPacmanPositionX) && (PositionX >= minPacmanPositionX)
                && (PositionY <= maxPacmanPositionY) && (PositionY >= minPacmanPositionY))
            {
                gamefield.Children.Remove(pacman);
                pacman = null;
                return true;
            }
            return false;
        }

        protected void MoveGhost(GameElement gameElement, Map.Map map, int[,] tempMap)
        {
            int min = map.Size.Height * map.Size.Width;
            int x = posGhostX, y = posGhostY;
            if (tempMap[x - 1, y] < min)
            {
                MoveUp();
            }
            else if (tempMap[x + 1, y] < min)
            {
                MoveDown();
            }
            else if (tempMap[x, y - 1] < min)
            {
                MoveLeft();
            }
            else if (tempMap[x, y + 1] < min)
            {
                MoveRight();
            }
            ConsumePlayer();
            Renderer.SetElement(this);
        }

        protected double GetLengthX(GameElement gameElementFrom, GameElement gameElementTo)
        {
            return Math.Abs((gameElementFrom.PositionX - gameElementTo.PositionX) / CELL_SIZE);
        }

        protected double GetLengthY(GameElement gameElementFrom, GameElement gameElementTo)
        {
            return Math.Abs((gameElementFrom.PositionY - gameElementTo.PositionY) / CELL_SIZE);
        }

        protected double GetFullLength(GameElement gameElementFrom, GameElement gameElementTo)
        {
            var deltaX = GetLengthX(gameElementFrom, gameElementTo);
            var deltaY = GetLengthY(gameElementFrom, gameElementTo);
            return Math.Floor(Math.Sqrt(deltaX * deltaX + deltaY * deltaY)); 
        }

        protected int[,] FindPathes(Map.Map map, GameElement gameElement)
        {
            int[,] tempMap = MapForFindPath(map, gameElement);
            bool findOrNotFind = false;
            int step = 0;
            int maxStep = map.Size.Height * map.Size.Width;
            
            while (!findOrNotFind)
            {
                for (int x = 0; x < map.Size.Height; x++)
                {
                    for (int y = 0; y < map.Size.Width; y++)
                    {
                        if (tempMap[x, y] == step)
                        {
                            if (((y - 1) > 0) && (tempMap[x, y - 1] != maxStep + 2))
                            {
                                if (tempMap[x, y - 1] == GHOST)
                                    findOrNotFind = true;
                                else if(!(tempMap[x, y - 1] == step - 1))
                                    tempMap[x, y - 1] = step + 1;
                            }
                            if (((x - 1) > 0) && (tempMap[x - 1, y] != maxStep + 2))
                            {
                                if (tempMap[x - 1, y] == GHOST)
                                    findOrNotFind = true;
                                else if (!(tempMap[x -1 , y] == step - 1))
                                    tempMap[x - 1, y] = step + 1;
                            }
                            if (((x + 1) < map.Size.Height) && (tempMap[x + 1, y] != maxStep + 2))
                            {
                                if (tempMap[x + 1, y] == GHOST)
                                    findOrNotFind = true;
                                else if (!(tempMap[x + 1, y] == step - 1))
                                    tempMap[x + 1, y] = step + 1;
                            }
                            if (((y + 1) < map.Size.Width) && (tempMap[x, y + 1] != maxStep + 2))
                            {
                                if (tempMap[x, y + 1] == GHOST)
                                    findOrNotFind = true;
                                else if (!(tempMap[x, y + 1] == step - 1))
                                    tempMap[x, y + 1] = step + 1;
                            }
                        }
                    }
                }
                step++;
                if ((step - 1) > (map.Size.Height * map.Size.Width))
                    findOrNotFind = true;
            }
            return tempMap;
        }

        private int[,] MapForFindPath(Map.Map map, GameElement gameElement)
        {
            posGameElementX = Convert.ToInt32(gameElement.PositionY / CELL_SIZE);
            posGameElementY = Convert.ToInt32(gameElement.PositionX / CELL_SIZE);
            GHOST = map.Size.Height * map.Size.Width;
            posGhostX = Convert.ToInt32(this.PositionY / CELL_SIZE);
            posGhostY = Convert.ToInt32(this.PositionX / CELL_SIZE);

            int[,] tempMap = new int[map.Size.Height, map.Size.Width];
            for (int i = 0; i < map.Size.Height; i++)
            {
                for (int j = 0; j < map.Size.Width; j++)
                {
                    if ((i == posGameElementX) && (j == posGameElementY))
                        tempMap[i, j] = PACMAN;
                    else if ((i == posGhostX) && (j == posGhostY))
                        tempMap[i, j] = GHOST;
                    else
                    {
                        switch (map[i, j])
                        {
                            case TypeGameElement.Block:
                                tempMap[i, j] = map.Size.Height * map.Size.Width + 2;
                                break;
                            case TypeGameElement.Empty:
                            case TypeGameElement.Pell:
                                tempMap[i, j] = map.Size.Height * map.Size.Width + 1;
                                break;
                            case TypeGameElement.Blynki:
                            case TypeGameElement.Clyde:
                            case TypeGameElement.Inky:
                            case TypeGameElement.Pynki:
                                if(!((i == posGhostX) && (j == posGhostY)))
                                    tempMap[i, j] = map.Size.Height * map.Size.Width + 1;
                                break;
                            case TypeGameElement.Pacman:
                                if(!((i == posGameElementX) && (j == posGameElementY)))
                                    tempMap[i, j] = map.Size.Height * map.Size.Width + 1;
                                break;
                        }
                    }
                }
            }
            return tempMap;
        }
    }
}
