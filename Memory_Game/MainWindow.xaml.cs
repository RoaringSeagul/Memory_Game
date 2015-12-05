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
        private int[] clicked1;
        private int[] clicked2;
        private bool bclicked1 = false;
        private bool bclicked2 = false;

        public logique logique;
        public MainWindow()
        {
            InitializeComponent();
            clicked1 = new int[2];
            clicked2 = new int[2];
            size = 8;
            theme = logique.Theme.fruits;
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
            if (!bclicked1)
            {
                clicked1[0] = x;
                clicked1[1] = y;
            }

            if (!bclicked2)
            {
                clicked2[0] = x;
                clicked2[1] = y;
            }
            
            if (logique.GetCurrentPlayer() == logique.GetLastPlayerPlayed())
            {
                MessageBox.Show("Ce n'est pas votre tour");
                logique.ChangeCurrentPlayer();
            }
            else
            {
                if (!aCartes[x, y].clicked)
                {
                    aCartes[x, y].ShowBackground(this);
                    if (logique.GetImage(x, y).imageName.Contains("parking") || logique.GetImage(x, y).imageName.Contains("melange"))
                    {
                        TurnAllCards();
                        logique.ReshuffleCards();
                    }
                    else if (logique.GetImage(x, y).imageName.Contains("maudite1") || logique.GetImage(x, y).imageName.Contains("maudite2"))
                    {
                        TurnAllCards();
                        logique.ReshuffleCardsCursed(x, y, logique.GetImage(x, y).imageName);
                    }
                    else if (logique.GetImage(x, y).imageName.Contains("joker"))
                    {
                        TurnAllCards();
                        aCartes[x, y].SetBackground(Brushes.Red);
                        logique.rmJoker(x, y);
                    }
                }
                else
                {
                    aCartes[x, y].SetBackground(Brushes.Aqua);
                    aCartes[x, y].clicked = false;
                }
            }

            aCartes[clicked1[0], clicked1[1]].ShowBackground(this);
            aCartes[clicked2[0], clicked2[1]].ShowBackground(this);

            if (TourdeJeu >= 2)
            {
                TourdeJeu = 0;
                logique.ChangeCurrentPlayer();

                var stopWatch = Stopwatch.StartNew();
                stopWatch.Start();
                stopWatch.Stop();

                TurnAllCards();
            }
        }

        void TurnAllCards()
        {
            for (int x = 0; x < size; x++)
            {
                for (int y = 0; y < size; y++)
                {
                    if (!logique.GetIsUsed(x, y))
                    {
                        aCartes[x, y].SetBackground(Brushes.Aqua);
                    }
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

        private void btnShowAll_Click(object sender, RoutedEventArgs e)
        {
            for (int x = 0; x < size; x++)
            {
                for (int y = 0; y < size; y++)
                {
                    if (!logique.GetIsUsed(x, y))
                    {
                        aCartes[x, y].ShowBackground(this);
                    }
                }
            }
        }
    }
}
