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
using System.Windows.Shapes;

namespace OOP_courseProject
{
    /// <summary>
    /// Interaction logic for RestartWindow.xaml
    /// </summary>
    public partial class RestartWindow : Window
    {
        public RestartWindow()
        {
            InitializeComponent();
        }
        private bool _exit;
        public bool Exit
        {
            get { return _exit; }
            set { _exit = value; }
        }

        private string _score;
        public string Score
        {
            get { return _score; }
            set { value = _score; }
        }
        public void InitTextBoxNameAndScore(string name, string score)
        {
            txtBlock_userName.Text = name + "! " + "Вы набрали " + score + " очков.";
        }
        private void btn_ExitApp_Click(object sender, RoutedEventArgs e)
        {
            Exit = true;
            this.Close();
        }

        private void btn_Continue_Click(object sender, RoutedEventArgs e)
        {
            Exit = false;
            this.Close();
        }

        private void Window_MouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
        
    }
}
