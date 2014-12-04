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
using System.ComponentModel;
using System.IO;
using OOP_courseProject.Map;
using OOP_courseProject.Enums;

namespace OOP_courseProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Game game;
        private MediaPlayer musicDeath;
        private MediaPlayer musicWin;
        StartWindow startWindow;
        private readonly string STR_WIN = "  YOU WIN!";
        private readonly string STR_LOSE = "GAME OVER";
        private bool _death = false;
        private string _userName;
        public static readonly string PATH_TO_RATINGS = @"Ratings\ratings.r";
        private bool exitGood = false;

        public MainWindow()
        {
            try
            {
                InitializeComponent();
                ShowStartWindow();
            }
            catch (FileNotFoundException ex)
            {
                MessageBox.Show(ex.Message, "Критическая ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                exitGood = true;
                Application.Current.Shutdown();
            }
            catch (GhostsNotFoundException ex)
            {
                MessageBox.Show(ex.Message, "Критическая ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                exitGood = true;
                Application.Current.Shutdown();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Критическая ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                exitGood = true;
                Application.Current.Shutdown();
            }
            
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.F2)
                RestartGame();
            else if(e.Key == Key.P)
                game.PauseGame();
            else
                game.KeyDown(e.Key);
        }

        private void ShowStartWindow()
        {
            startWindow = new StartWindow();
            startWindow.ShowDialog();
            if (startWindow.Start == true)
            {
                if (_death == false)
                {
                    _death = true;
                    StartGame(startWindow.Volume);
                }
                else
                    RestartGame();
                _userName = startWindow.UserName;
                txtBlock_userName.Text = _userName;
            }
            else
            {
                exitGood = true;
                Application.Current.Shutdown();
            }
        }

        private void game_Lose()
        {
            musicDeath = new System.Windows.Media.MediaPlayer();
            musicDeath.Volume = startWindow.Volume;
            musicDeath.Open(new Uri(@"Audio\die.mp3", UriKind.Relative));
            musicDeath.Play();
            RatingsWindow.WriteRatintg(_userName, Convert.ToInt32(txtBlock_score.Text));
            ShowWinner(false);
            RestartWindow restarWnd = new RestartWindow();
            restarWnd.InitTextBoxNameAndScore(_userName, txtBlock_score.Text);
            restarWnd.ShowDialog();
            if (restarWnd.Exit == false)
                RestartGame();
            else
            {
                exitGood = true;
                Application.Current.Shutdown();
            }
        }

        private void game_Win()
        {
            musicWin = new System.Windows.Media.MediaPlayer();
            musicWin.Volume = startWindow.Volume;
            musicWin.Open(new Uri(@"Audio\intermission.mp3", UriKind.Relative));
            musicWin.Play();
            RatingsWindow.WriteRatintg(_userName, Convert.ToInt32(txtBlock_score.Text));
            ShowWinner(true);
            ShowStartWindow();
        }

        private void ShowWinner(bool win)
        {
            GameOverWindow gameOverWnd = new GameOverWindow();
            
            if (win)
                gameOverWnd.SetText(STR_WIN);
            else
                gameOverWnd.SetText(STR_LOSE);
            gameOverWnd.Show();
            System.Threading.Thread.Sleep(3000);
            gameOverWnd.Close();
        }

        private void StartGame(double startVolume)
        {
            double speed = 1;
            game = new Game(gameField, speed, startVolume);
            game.Lose += new Action(game_Lose);
            game.Win += new Action(game_Win);
            game.ScoreRecieve += new Action(game_ScoreRecieve);
            game.StartGame();
        }

        private void game_ScoreRecieve()
        {
            txtBlock_score.Text = game.Score.ToString();
        }

        private void RestartGame()
        {
            game.Lose -= game_Lose;
            game = null;
            gameField.Children.Clear();
            txtBlock_score.Text = "0";
            StartGame(startWindow.Volume);
        }


        private void ClickEvent_restartGame (object sender, RoutedEventArgs e)
        {
            RestartGame();
        }

        private void ClickEvent_pauseGame_continueGame(object sender, RoutedEventArgs e)
        {
            game.PauseGame();
        }

        private bool CloseApplication()
        {
            game.PauseGame();
            if (MessageBox.Show("Вы действительно хотите выйти?", "Предупреждение",
                MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                return true;
            }
            else { return false; }
        }

        private void ClickEvent_exitGame(object sender, RoutedEventArgs e)
        {
            if (CloseApplication() == true)
            {
                exitGood = true;
                Application.Current.Shutdown();
            }
            else
                game.PauseGame();
        }

        private void closeApplication(object sender, CancelEventArgs e)
        {
            if (exitGood == true)
            {
                exitGood = false;
                return;
            }
            else
            {
                if (CloseApplication() == true)
                    Application.Current.Shutdown();
                else
                {
                    game.PauseGame();
                }
            }
        }
    }
}
