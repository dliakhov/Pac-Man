using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;
using System.Windows.Input;
using System.Diagnostics;
using OOP_courseProject.Map;
using OOP_courseProject.Enums;

namespace OOP_courseProject
{
    class Game
    {
        private int _score = 0;
        private DispatcherTimer gameTimer;
        private Player player;

        private Pynki _pynki;
        private Clyde _clyde;
        private Inky _inky;
        private Blynki _blynki;

        private readonly int SCORE_FOR_POINT = 10;

        private List<Pellet> _pells;
        private List<Block> _blocks;
        private Canvas _gameField;
        private Renderer renderer;
        private Direction direction;
        private double speed;
        private MediaPlayer musicStart;
        
        private MediaPlayer musicConsume;

        private bool pause;

        private double _volume;

        public static readonly double TIME = 150;

        Map.Map map;

        public int Score
        {
            get { return _score; }
        }

        public List<Block> Blocks
        {
            get { return _blocks; }
        }
        public List<Pellet> Pells
        {
            get { return _pells; }
        }

        public Game(Canvas gameField, double speed, double startVolume)
        {
            _gameField = gameField;
            gameTimer = new DispatcherTimer();
            gameTimer.Interval = TimeSpan.FromMilliseconds(TIME);
            AddEventTimer(gameTimer, new EventHandler(TimerTick));
            this.speed = speed;
            renderer = new Renderer();
            _volume = startVolume;
            PlayStartSound();
        }

        public void PauseGame()
        {
            if (pause)
                return;
            if (gameTimer.IsEnabled == true)
                gameTimer.Stop();
            else
                gameTimer.Start();
        }

        private void AddEventTimer(DispatcherTimer timer, EventHandler ev)
        {
            timer.Tick += ev;
        }

        private void RemoveEventTimer(DispatcherTimer timer, EventHandler ev)
        {
            timer.Tick -= ev;
        }

        private void PlayStartSound()
        {
            musicStart = new System.Windows.Media.MediaPlayer();
            musicStart.Volume = _volume;
            musicStart.Open(new Uri(@"Audio\opening_song.mp3", UriKind.Relative));
            pause = true;
        }

        private void TimerTick(object sender, EventArgs e)
        {
            switch (direction)
            {
                case Direction.Down:
                    player.MoveDown();
                    break;
                case Direction.Left:
                    player.MoveLeft();
                    break;
                case Direction.Up:
                    player.MoveUp();
                    break;
                case Direction.Right:
                    player.MoveRight();
                    break;
            }
            
            DrawPacmanAnimation();
            MoveGhosts();
            CountScore();
        }

        public void StartGame()
        {
            DrawMap();
            if(player != null)
                player.PelletConsumed += new Action(player_PelletConsumed);
            if (_inky != null)
                _inky.ConsumePlayer += new Action(player_PlayerConsumed);
            else if (_pynki != null)
                _pynki.ConsumePlayer += new Action(player_PlayerConsumed);
            else if (_clyde != null)
                _clyde.ConsumePlayer += new Action(player_PlayerConsumed);
            else if (_blynki != null)
                _blynki.ConsumePlayer += new Action(player_PlayerConsumed);
            else if(player == null || _inky == null || _pynki == null || _clyde == null || _blynki == null)
                throw new GhostsNotFoundException("На карте нет всех основных героев!!! Ваша карта некорректна!!!");
        }

        private void player_PelletConsumed()
        {
            musicConsume = new System.Windows.Media.MediaPlayer();
            musicConsume.Volume = _volume;
            musicConsume.Open(new Uri(@"Audio\eating.short.mp3", UriKind.Relative));
            musicConsume.Play();
            if (_pells.Count <= 0)
            {
                gameTimer.Stop();
                Win();
            }
        }

        private void DrawPacmanAnimation()
        {
            renderer.AnimationPacman(player, direction.ToString());
            Renderer.SetElement(player);
        }
        
        private void CountScore()
        {
            bool consumed = false;
            _pells = player.Consume(_pells, _gameField, ref consumed);

            if (consumed == true)
            {
                _score += SCORE_FOR_POINT;
                ScoreRecieve();
            }
        }

        private void player_PlayerConsumed()
        {
            bool lose;
            lose = _inky.ConsumePacman(player, _gameField);
            if(lose == false)
                lose = _pynki.ConsumePacman(player, _gameField);
            if (lose == false)
                lose = _blynki.ConsumePacman(player, _gameField);
            if (lose == false)
                lose = _clyde.ConsumePacman(player, _gameField);
            if (lose == true)
            {
                gameTimer.Stop();
                RemoveEventTimer(gameTimer, TimerTick);
                Lose();
            }
        }

        private void MoveGhosts()
        {
            if(_blynki != null)
                _blynki.MoveBlynki(map, player);
            if(_pynki != null)
                _pynki.MovePynki(map, player, direction);
            if(_inky != null)
                _inky.MoveInky(_blynki, map, player, direction);
            if(_clyde != null)
                _clyde.MoveClyde(map, player);
        }

        private void DrawMap()
        {
            map = MapLoader.LoadMap();
            _pells = new List<Pellet>();
            _blocks = new List<Block>();
            for (int i = 0; i < map.Size.Height; i++)
            {
                for (int j = 0; j < map.Size.Width; j++)
                {
                    RenderMapElement(_gameField, map, i, j);
                }
            }
            musicStart.MediaEnded += musicStart_MediaEnded;
            musicStart.Play();
        }

        private void musicStart_MediaEnded(object sender, EventArgs e)
        {
            pause = false;
            gameTimer.Start();
        }

        public void RenderMapElement(Canvas gameField, Map.Map map, int x, int y)
        {
            switch (map[x, y])
            {
                case TypeGameElement.Pacman:
                    player = new Player(x * GameElement.CELL_SIZE, y * GameElement.CELL_SIZE);
                    AddElementToGameField(gameField, player);
                    break;
                case TypeGameElement.Block:
                    Block block = new Block(x * GameElement.CELL_SIZE, y * GameElement.CELL_SIZE);
                    _blocks.Add(block);
                    AddElementToGameField(gameField, block);
                    break;
                case TypeGameElement.Pynki:
                    _pynki = new Pynki(x * GameElement.CELL_SIZE, y * GameElement.CELL_SIZE);
                    AddElementToGameField(gameField, _pynki);
                    break;
                case TypeGameElement.Clyde:
                    _clyde = new Clyde(x * GameElement.CELL_SIZE, y * GameElement.CELL_SIZE);
                    AddElementToGameField(gameField, _clyde);
                    break;
                case TypeGameElement.Blynki:
                    _blynki = new Blynki(x * GameElement.CELL_SIZE, y * GameElement.CELL_SIZE);
                    AddElementToGameField(gameField, _blynki);
                    break;
                case TypeGameElement.Inky:
                    _inky = new Inky(x * GameElement.CELL_SIZE, y * GameElement.CELL_SIZE);
                    AddElementToGameField(gameField, _inky);
                    break;
                case TypeGameElement.Pell:
                    Pellet pell = new Pellet(x * GameElement.CELL_SIZE, y * GameElement.CELL_SIZE);
                    _pells.Add(pell);
                    AddElementToGameField(gameField, pell);
                    break;
            }
            Container.onCreate().Blocks = _blocks;
            Container.onCreate().Pells = _pells;
        }
        
        private void AddElementToGameField (Canvas gameField, GameElement element)
        {
            gameField.Children.Add(element);
            Renderer.SetElement(element);
        }

        public void KeyDown(Key key)
        {
            if (!pause)
            {
                switch (key)
                {
                    case Key.Down:
                        direction = Direction.Down;
                        break;
                    case Key.Up:
                        direction = Direction.Up;
                        break;
                    case Key.Right:
                        direction = Direction.Right;
                        break;
                    case Key.Left:
                        direction = Direction.Left;
                        break;
                    default:
                        break;
                }
            }
        }

        public event Action Win = delegate { };
        public event Action Lose = delegate { };
        public event Action ScoreRecieve = delegate { }; 
    }
}
[Serializable]
public class GhostsNotFoundException : Exception
{
    public GhostsNotFoundException() { }
    public GhostsNotFoundException(string message) : base(message) { }
    public GhostsNotFoundException(string message, Exception inner) : base(message, inner) { }
    protected GhostsNotFoundException(
      System.Runtime.Serialization.SerializationInfo info,
      System.Runtime.Serialization.StreamingContext context)
        : base(info, context) { }
}
