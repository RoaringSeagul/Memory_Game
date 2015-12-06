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
        bool finished;
        bool GameFinished;
        bool gameStarted;
        bool IsCPUTurn;
        bool finishedTurn;

        GameInfo gameInfo;
        List<string> lstPlayed;
        logique.Theme theme;
        Carte[,] aCartes;
        Random _rnd = new Random();
        public logique logique;
        MessageBoxResult newGameChoice;

        public MainWindow()
        {
            InitializeComponent();
            size = 8;
            scoreP1 = 0;
            scoreP2 = 0;
            lstPlayed = new List<string>();
            gameInfo = new GameInfo() { DateGameStart = DateTime.Now, PlayerFirstName = "firstName", PlyerLastName = "lastName", Theme = theme.ToString() };
            IsCPUTurn = false;
            gameStarted = false;
            GameFinished = false;
            finishedTurn = false;
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

            lblTurn.Content = ("Game not started");
        }

        internal void update(int x, int y)
        {
            if (!GameFinished && gameStarted)
            {
                if (!IsCPUTurn)
                {
                    MessageBoxResult resultingOk;
                    do
                    {
                        if (!finishedTurn)
                        {
                            TurnAllCards();
                            resultingOk = MessageBox.Show("Get ready to play", "Ready", MessageBoxButton.OK);
                        }
                        else
                            resultingOk = MessageBoxResult.OK;

                        if (resultingOk == MessageBoxResult.OK)
                        {
                            if (!aCartes[x, y].clicked)
                            {
                                lstPlayed.Add("[" + x + ", " + y + "]");
                                aCartes[x, y].ShowBackground(this);

                                if (TourdeJeu == 0)
                                {
                                    finishedTurn = true;
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
                                finishedTurn = false;

                                if (logique.GetImage(x, y).imageName.Contains("parking") || logique.GetImage(x, y).imageName.Contains("melange") ||
                                    logique.GetImage(firstCardX, firstCardY).imageName.Contains("parking") ||
                                    logique.GetImage(firstCardX, firstCardY).imageName.Contains("melange"))
                                {
                                    logique.ReshuffleCards();
                                }
                                else if (logique.GetImage(x, y).imageName.Contains("maudite1") || logique.GetImage(x, y).imageName.Contains("maudite2"))
                                {
                                    logique.ReshuffleCardsCursed(x, y, logique.GetImage(x, y).imageName);
                                }
                                else if (logique.GetImage(firstCardX, firstCardY).imageName.Contains("maudite1") || logique.GetImage(firstCardX, firstCardY).imageName.Contains("maudite2"))
                                {
                                    logique.ReshuffleCardsCursed(firstCardX, firstCardY, logique.GetImage(firstCardX, firstCardY).imageName);
                                }
                                else if (logique.GetImage(x, y).imageName.Contains("joker"))
                                {
                                    aCartes[x, y].SetBackground(Brushes.Red);
                                    logique.rmJoker(x, y);
                                }
                                else if (logique.GetImage(firstCardX, firstCardY).imageName.Contains("joker"))
                                {
                                    aCartes[firstCardX, firstCardY].SetBackground(Brushes.Red);
                                    logique.rmJoker(firstCardX, firstCardY);
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

                                if (logique.GetCurrentPlayer() == logique.Joueurs.joueur1)
                                {
                                    lblTurn.Content = "Its player1's turn";
                                }

                                else if (logique.GetCurrentPlayer() == logique.Joueurs.joueur2)
                                {
                                    IsCPUTurn = true;
                                    lblTurn.Content = "Its player2's turn";
                                }
                            }
                            UpdateScore();
                        }
                    } while (resultingOk != MessageBoxResult.OK);
                }
                else
                {
                    lblTurn.Content = "It's the CPU turn. \n press te generate button to continue";
                }
            }
            else
            {
                newGameChoice = MessageBox.Show("Do you want to start a new game?", "New Game", MessageBoxButton.YesNo);
            }

            if (newGameChoice == MessageBoxResult.Yes)
            {
                gameStarted = true;
                GameFinished = false;
                logique.StartNewGame();
                lblTurn.Content = ("It's player1's turn");
            }

            if (logique.CheckIfFinished())
            {
                MessageBox.Show("C'est termine!");
                GameFinished = true;
                gameStarted = false;
                finishedGame();
            }
        }

        private void finishedGame()
        {
            using (var db = new Memory_Game.MemoryContextContainer1())
            {
                string CompiledLstCoords = "";

                foreach (var item in lstPlayed)
                {
                    CompiledLstCoords += ("," + item);
                }

                Jouer jouer = new Jouer()
                {
                    listeCombine = CompiledLstCoords,
                    Partie = new Partie() { dateHeurePartie = gameInfo.DateGameStart.ToString() },
                    Utilisateur = new Utilisateur()
                    {
                        nomUser = gameInfo.PlyerLastName,
                        prenomUser = gameInfo.PlayerFirstName
                    },
                    Etat = new Etat() { nomEtat = gameInfo.Theme }
                };

                db.Jouers.Add(jouer);

                db.SaveChanges();
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

        private void MenuItemNewGame_Click(object sender, RoutedEventArgs e)
        {
            lblTurn.Content = ("It's player1's turn");
            gameStarted = true;
            GameFinished = false;
            TurnAllCards();
            logique.StartNewGame();
        }

        private void MenuItemRestart_Click(object sender, RoutedEventArgs e)
        {
            lblTurn.Content = ("It's player1's turn");
            gameStarted = true;
            GameFinished = false;
            TurnAllCards();
            logique.StartNewGame();
        }

        private void MenuItemShowAll_Click(object sender, RoutedEventArgs e)
        {
            if (gameStarted)
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
        }

        private void MenuItemHideAll_Click(object sender, RoutedEventArgs e)
        {
            if (gameStarted)
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

        private void btnCPU_Click(object sender, RoutedEventArgs e)
        {
            if (logique.GetCurrentPlayer() == logique.Joueurs.joueur2)
            {
                TurnAllCards();

                int FirstRndX;
                int FirstRndY;
                int SecondRndX;
                int SecondRndY;

                finished = false;

                do
                {
                    FirstRndX = _rnd.Next(0, size);
                    FirstRndY = _rnd.Next(0, size);

                    if (!aCartes[FirstRndX, FirstRndY].clicked)
                    {
                        aCartes[FirstRndX, FirstRndY].ShowBackground(this);
                        finished = true;
                    }

                } while (!finished);

                finished = false;

                do
                {
                    SecondRndX = _rnd.Next(0, size);
                    SecondRndY = _rnd.Next(0, size);

                    if (!aCartes[SecondRndX, SecondRndY].clicked)
                    {
                        aCartes[SecondRndX, SecondRndY].ShowBackground(this);
                        finished = true;
                    }

                } while (!finished);

                lstPlayed.Add("[" + FirstRndX + ", " + FirstRndY + "]");
                lstPlayed.Add("[" + SecondRndX + ", " + SecondRndY + "]");

                logique.ChangeCurrentPlayer();

                if (logique.GetImage(FirstRndX, FirstRndY).imageName.Contains("parking") || logique.GetImage(FirstRndX, FirstRndY).imageName.Contains("melange") ||
                    logique.GetImage(SecondRndX, SecondRndY).imageName.Contains("parking") ||
                    logique.GetImage(SecondRndX, SecondRndY).imageName.Contains("melange"))
                {
                    logique.ReshuffleCards();
                }
                else if (logique.GetImage(FirstRndX, FirstRndY).imageName.Contains("maudite1") || logique.GetImage(FirstRndX, FirstRndY).imageName.Contains("maudite2"))
                {
                    logique.ReshuffleCardsCursed(FirstRndX, FirstRndY, logique.GetImage(FirstRndX, FirstRndY).imageName);
                }
                else if (logique.GetImage(SecondRndX, SecondRndY).imageName.Contains("maudite1") || logique.GetImage(SecondRndX, SecondRndY).imageName.Contains("maudite2"))
                {
                    logique.ReshuffleCardsCursed(SecondRndX, SecondRndY, logique.GetImage(SecondRndX, SecondRndY).imageName);
                }
                else if (logique.GetImage(FirstRndX, FirstRndY).imageName.Contains("joker"))
                {
                    aCartes[FirstRndX, FirstRndY].SetBackground(Brushes.Red);
                    logique.rmJoker(FirstRndX, FirstRndY);
                }
                else if (logique.GetImage(SecondRndX, SecondRndY).imageName.Contains("joker"))
                {
                    aCartes[SecondRndX, SecondRndY].SetBackground(Brushes.Red);
                    logique.rmJoker(SecondRndX, SecondRndY);
                }
                else if (logique.GetImage(SecondRndX, SecondRndY).imageName == logique.GetImage(FirstRndX, FirstRndY).imageName)
                {
                    if (logique.GetCurrentPlayer() == logique.Joueurs.joueur1)
                        scoreP1++;
                    else if (logique.GetCurrentPlayer() == logique.Joueurs.joueur2)
                        scoreP2++;

                    logique.rmCarte(SecondRndX, SecondRndY, FirstRndX, FirstRndY);
                    aCartes[SecondRndX, SecondRndY].SetBackground(Brushes.Red);
                    aCartes[FirstRndX, FirstRndY].SetBackground(Brushes.Red);
                }

                UpdateScore();
                IsCPUTurn = false;
            }
        }
    }
}
