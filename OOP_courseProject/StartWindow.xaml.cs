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
    /// Interaction logic for StartWindow.xaml
    /// </summary>
    public partial class StartWindow : Window
    {
        private bool _start;

        public bool Start
        {
            get { return _start; }
            private set { _start = value; }
        }

        private double _volume;
        public double Volume
        {
            get { return _volume; }
            private set { _volume = value; }
        }

        private string _name;
        public string UserName
        {
            get { return _name; }
            private set { _name = value; }
        }

        public StartWindow()
        {
            InitializeComponent();
            txtBox_name.Focus();
        }

        private void btn_Exit_Click(object sender, RoutedEventArgs e)
        {
            Start = false;
            this.Close();
        }

        private void btn_newGame_Click(object sender, RoutedEventArgs e)
        {
            Start = true;
            Volume = sldr_musicVolume.Value / 10;
            if (txtBox_name.Text.Length == 0)
                MessageBox.Show("Введите своё имя", "Предупреждение");
            else
            {
                UserName = txtBox_name.Text;
                this.Visibility = Visibility.Hidden;
            }
        }

        private void btn_showRaitings_Click(object sender, RoutedEventArgs e)
        {
            RatingsWindow raitingWindow = new RatingsWindow();
            raitingWindow.Show();
            raitingWindow.ShowRaitings();
        }

        private void Window_MouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
    }
}
