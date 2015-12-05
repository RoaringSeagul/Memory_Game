using System;
using System.Collections.Generic;
using System.IO;
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
using System.Diagnostics;
using System.Threading;

namespace Memory_Game
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        byte TourdeJeu;
        byte size;
        logique.Theme theme;
        Carte[,] aCartes;

        public logique logique;
        public MainWindow()
        {
            InitializeComponent();
            size = 8;
            theme = logique.Theme.voitures;
            logique = new logique(size, theme);
            aCartes = new Carte[size, size];
            TourdeJeu = 0;
            Plateau.Rows = size;
            Plateau.Columns = size;
            for (int x = 0; x < size; x++)
            {
                for (int y = 0; y < size; y++)
                {
                    aCartes[x, y] = new Carte(x, y, size, this);
                    Plateau.Children.Add(aCartes[x, y]);
                }
            }
        }

        internal void update(int x, int y)
        {
            TourdeJeu++;

            if (logique.GetCurrentPlayer() == logique.GetLastPlayerPlayed())
            {
                MessageBox.Show("Ce n'est pas votre tour");
                logique.ChangeCurrentPlayer();
            }
            else
            {
                if (!aCartes[x, y].clicked)
                {
                    aCartes[x, y].clicked = true;
                    var brush = new ImageBrush();
                    brush.ImageSource = new BitmapImage(new Uri((Directory.GetCurrentDirectory() + @"\Image\" + logique.GetImage(x, y).imageName + ".png"), UriKind.Relative));
                    aCartes[x, y].SetBackground(brush);

                    Thread.Sleep(3000);

                    TurnAllCards();

                    if (logique.GetImage(x, y).imageName.Contains("parking") || logique.GetImage(x, y).imageName.Contains("melange") )
                    {
                        logique.ReshuffleCards();
                    }
                }
                else
                {
                    aCartes[x, y].SetBackground(Brushes.Aqua);
                    aCartes[x, y].clicked = false;
                }
            }

            if (TourdeJeu >= 2)
            {
                TourdeJeu = 0;
                logique.ChangeCurrentPlayer();
                TurnAllCards();
            }
        }

        void TurnAllCards()
        {
            for (int x = 0; x < size; x++)
            {
                for (int y = 0; y < size; y++)
                {
                    aCartes[x, y].SetBackground(Brushes.Aqua);
                }
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //Window login = new Login();
            //login.ShowDialog();
            //this.Show();
        }
    }
}
