using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Memory_Game
{
    public class logique
    {
        public enum Joueurs { joueur1, joueur2, CPU }
        public enum Theme { voitures, fruits }

        Theme theme;
        int size;
        Joueurs lastPlayedPlayer = Joueurs.joueur2;
        Joueurs currentPlayer = Joueurs.joueur1;
        Random _rnd = new Random();
        ImageCarte[,] lstImage;
        CoordUsed[,] lstCoordUsed;
        bool Done = false;

        public logique(int PlateauSize, Theme ChoixTheme)
        {
            theme = ChoixTheme;
            size = PlateauSize;

            lstCoordUsed = new CoordUsed[size, size];
            lstImage = new ImageCarte[size, size];

            for (int x = 0; x < size; x++)
                for (int y = 0; y < size; y++)
                    lstCoordUsed[x, y] = new CoordUsed();

            NewGame(size, theme);
        }

        private void NewGame(int size, Theme theme)
        {
            if (theme == Theme.voitures)
            {
                for (int i = 0; i < 6; i++)
                {
                    switch (i)
                    {
                        case 0:
                            ChangerCarteSpecial("joker", size);
                            break;
                        case 1:
                            ChangerCarteSpecial("maudite1", size);
                            break;
                        case 2:
                            ChangerCarteSpecial("maudite2", size);
                            break;
                        case 3:
                            ChangerCarteSpecial("parking", size);
                            break;
                        case 4:
                            ChangerCarteSpecial("lfa", size);
                            break;
                        case 5:
                            ChangerCarteSpecial("saratoga", size);
                            break;
                        default:
                            break;
                    }

                }
                    ChargerCarteNormale(size);
            }

            if (theme == Theme.fruits)
            {
                for (int i = 0; i < 6; i++)
                {
                    switch (i)
                    {
                        case 0:
                            ChangerCarteSpecial("joker", size);
                            break;
                        case 1:
                            ChangerCarteSpecial("maudite1", size);
                            break;
                        case 2:
                            ChangerCarteSpecial("maudite2", size);
                            break;
                        case 3:
                            ChangerCarteSpecial("melange", size);
                            break;
                        case 4:
                            ChangerCarteSpecial("sapote", size);
                            break;
                        case 5:
                            ChangerCarteSpecial("maypop", size);
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        internal void ReshuffleCards()
        {
            NewGame(size, theme);
        }

        internal Joueurs GetLastPlayerPlayed()
        {
            return lastPlayedPlayer;
        }

        internal Joueurs GetCurrentPlayer()
        {
            return currentPlayer;
        }

        internal ImageCarte GetImage(int x, int y)
        {
            return lstImage[x, y];
        }

        internal void ChangeCurrentPlayer()
        {
            lastPlayedPlayer = currentPlayer;
            switch (currentPlayer)
            {
                case Joueurs.joueur1:
                    currentPlayer = Joueurs.joueur2;
                    break;
                case Joueurs.joueur2:
                    currentPlayer = Joueurs.joueur1;
                    break;
                default:
                    break;
            }
        }

        private void ChargerCarteNormale(int size)
        {
            for (int x = 0; x < size; x++)
            {
                for (int y = 0; y < size; y++)
                {
                    if (!lstCoordUsed[x, y].IsUsed)
                    {
                        lstImage[x, y] = new ImageCarte();
                        lstImage[x, y].imageName = "sapote";
                        lstCoordUsed[x, y].IsUsed = true;
                    }
                }
            }
        }

        private void ChangerCarteSpecial(string carte, int size)
        {
            int rndX;
            int rndY;
            do
            {
                rndX = _rnd.Next(0, size);
                rndY = _rnd.Next(0, size);
                if (!lstCoordUsed[rndX, rndY].IsUsed)
                {
                    lstImage[rndX, rndY] = new ImageCarte();
                    lstImage[rndX, rndY].imageName = carte;
                    lstCoordUsed[rndX, rndY].IsUsed = true;
                    Done = true;
                }
            } while (!Done);
            Done = false;
        }
    }
}
