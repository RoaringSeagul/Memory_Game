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
using System.Windows.Threading;
using System.Security.Permissions;

namespace Memory_Game
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int scoreP1;
        int scoreP2;
        int firstCardX;
        int firstCardY;

        byte TourdeJeu;
        byte size;
        logique.Theme theme;
        Carte[,] aCartes;
        Random _rnd = new Random();
        bool finished;
        public logique logique;
        public MainWindow()
        {
            InitializeComponent();
            size = 8;
            scoreP1 = 0;
            scoreP2 = 0;
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
            lblTurn.Content = ("Its player1's turn");
        }

        internal void update(int x, int y)
        {
            if (!aCartes[x, y].clicked)
            {
                aCartes[x, y].ShowBackground(this);

                if (TourdeJeu == 0)
                {
                    firstCardX = x;
                    firstCardY = y;
                }

                ++TourdeJeu;
            }
            else if (logique.GetIsRemoved(x, y))
            {
                MessageBox.Show("Cette carte n'existe plus");
            }
            else
            {
                MessageBox.Show("Carte deja choisie");
            }

            if (TourdeJeu >= 2)
            {
                TourdeJeu = 0;
                logique.ChangeCurrentPlayer();

                var result = MessageBox.Show("Are you ready?", "Ready", MessageBoxButton.YesNo);

                if (result == MessageBoxResult.Yes)
                {
                    if (logique.GetImage(x, y).imageName.Contains("parking") || logique.GetImage(x, y).imageName.Contains("melange") ||
                        logique.GetImage(firstCardX, firstCardY).imageName.Contains("parking") || 
                        logique.GetImage(firstCardX, firstCardY).imageName.Contains("melange"))
                    {
                        logique.ReshuffleCards();
                    }
                    else if (logique.GetImage(x, y).imageName.Contains("maudite1") || logique.GetImage(x, y).imageName.Contains("maudite2") ||
                             logique.GetImage(firstCardX, firstCardY).imageName.Contains("maudite1") || 
                             logique.GetImage(firstCardX, firstCardY).imageName.Contains("maudite2"))
                    {
                        logique.ReshuffleCardsCursed(x, y, logique.GetImage(x, y).imageName);
                    }
                    else if (logique.GetImage(x, y).imageName.Contains("joker") || logique.GetImage(firstCardX, firstCardY).imageName.Contains("joker"))
                    {
                        aCartes[x, y].SetBackground(Brushes.Red);
                        logique.rmJoker(x, y);
                    }
                    else if (logique.GetImage(firstCardX, firstCardY).imageName == logique.GetImage(x, y).imageName)
                    {
                        if (logique.GetCurrentPlayer() == logique.Joueurs.joueur1)
                            scoreP1++;
                        else if (logique.GetCurrentPlayer() == logique.Joueurs.joueur2)
                            scoreP2++;

                        logique.rmCarte(firstCardX, firstCardY, x, y);
                        aCartes[firstCardX, firstCardY].SetBackground(Brushes.Red);
                        aCartes[x, y].SetBackground(Brushes.Red);
                    }

                    TurnAllCards();
                }

                if (logique.GetCurrentPlayer() == logique.Joueurs.joueur1)
                {
                    lblTurn.Content = "Its player1's turn";
                }
                else if (logique.GetCurrentPlayer() == logique.Joueurs.joueur2)
                {
                    lblTurn.Content = "Its player2's turn";

                    int rndX;
                    int rndY;
                    finished = false;

                    do
                    {
                        rndX = _rnd.Next(0, size);
                        rndY = _rnd.Next(0, size);

                        if (!aCartes[rndX, rndY].clicked)
                        {
                            aCartes[rndX, rndY].PerformClick();
                            finished = true;
                        }

                    } while (!finished);

                    finished = false;

                    do
                    {
                        rndX = _rnd.Next(0, size);
                        rndY = _rnd.Next(0, size);

                        if (!aCartes[rndX, rndY].clicked)
                        {
                            aCartes[rndX, rndY].PerformClick();
                            finished = true;
                        }

                    } while (!finished);
                }
            }
            UpdateScore();

            if (logique.CheckIfFinished())
            {
                MessageBox.Show("C'est termine!");
            }
        }

        void TurnAllCards()
        {
            for (int x = 0; x < size; x++)
            {
                for (int y = 0; y < size; y++)
                {
                    if (!logique.GetIsRemoved(x, y))
                    {
                        aCartes[x, y].SetBackground(Brushes.Aqua);
                        aCartes[x, y].clicked = false;
                    }
                }
            }
        }

        void UpdateScore()
        {
            txtPlayer1.Text = scoreP1.ToString();
            txtPlayer2.Text = scoreP2.ToString();
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
                    if (!logique.GetIsRemoved(x, y))
                    {
                        aCartes[x, y].ShowBackground(this);
                        aCartes[x, y].clicked = false;
                    }
                }
            }
        }

        private void btnHideAll_Click(object sender, RoutedEventArgs e)
        {
            for (int x = 0; x < size; x++)
            {
                for (int y = 0; y < size; y++)
                {
                    if (!logique.GetIsRemoved(x, y))
                    {
                        aCartes[x, y].SetBackground(Brushes.Aqua);
                        aCartes[x, y].clicked = false;
                    }
                }
            }
        }
    }
}
