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
using System.IO;

namespace OOP_courseProject
{
    /// <summary>
    /// Interaction logic for Raitings.xaml
    /// </summary>
    public partial class RatingsWindow : Window
    {
        private static readonly int MAX_RECORDS = 10;
        List<KeyValuePair<string, int>> rating;
        public RatingsWindow()
        {
            InitializeComponent();
        }

        private void btn_CloseRaintings_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_MouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
        string ratingString;

        public void ShowRaitings()
        {
            rating = ReadRatingFile();
            ClearUnnecessaryRecord(rating);
            int counter = 1;
            foreach (KeyValuePair<string, int> user in rating)
            {
                txtBlock_user_name.Text += string.Format(counter++ + ".  " + user.Key + "\n");
                txtBlock_user_score.Text += string.Format(user.Value + "\n");
            }
        }

        private static List<KeyValuePair<string, int>> ClearUnnecessaryRecord(List<KeyValuePair<string, int>> rating)
        {
            rating.Sort((x, y) => y.Value.CompareTo(x.Value));
            if (rating.Count > MAX_RECORDS)
            {
                rating.RemoveRange(MAX_RECORDS, rating.Count - MAX_RECORDS);
            }
            return rating;
        }
        private static List<KeyValuePair<string, int>> ReadRatingFile()
        {
            List<KeyValuePair<string, int>> rating = new List<KeyValuePair<string, int>>();
            if(File.Exists(MainWindow.PATH_TO_RATINGS) == false)
            {
                return rating;
            }
            using (StreamReader sr = new StreamReader(MainWindow.PATH_TO_RATINGS))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    string[] usersRating = line.Split(' ');
                    string user = usersRating[0];
                    int score = Convert.ToInt32(usersRating[1]);
                    rating.Add(new KeyValuePair<string, int>(user, score));
                }
            }
            return rating;
        }

        public static void WriteRatintg(string userName, int score)
        {
            KeyValuePair<string, int> newRecord = new KeyValuePair<string, int>(userName, score);
            List<KeyValuePair<string, int>> rating = ReadRatingFile();
            if (rating.Count == 0)
            {
                rating.Add(newRecord);
            }
            else
            {
                var min = rating.OrderBy(x => x.Value).First();
                if (min.Value > newRecord.Value && rating.Count > MAX_RECORDS)
                    return;
                rating.Add(newRecord);
                if (System.IO.File.Exists(MainWindow.PATH_TO_RATINGS))
                    rating = ClearUnnecessaryRecord(rating);
            }
            File.WriteAllText(MainWindow.PATH_TO_RATINGS, String.Empty);
            using (StreamWriter writer = new StreamWriter(MainWindow.PATH_TO_RATINGS, true))
            {
                foreach (KeyValuePair<string, int> record in rating)
                    writer.WriteLine(record.Key + " " + record.Value);
            }
        }
    }
}
