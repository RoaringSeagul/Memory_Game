using System;
using System.Collections.Generic;
using System.IO;
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

        int size;
        bool cursed1;
        bool cursed2;
        bool JokerRemoved;
        bool bNewGame;
        bool Done = false;

        Theme theme;
        Joueurs lastPlayedPlayer = Joueurs.joueur2;
        Joueurs currentPlayer = Joueurs.joueur1;

        Random _rnd = new Random();
        ImagePossible[] imagesPossibles;
        ImageCarte[,] lstImage;
        CoordUsed[,] lstCoordUsed;

        public logique(int PlateauSize, Theme ChoixTheme)
        {
            JokerRemoved = false;
            cursed1 = false;
            cursed2 = false;
            bNewGame = true;

            theme = ChoixTheme;
            size = PlateauSize;

            lstCoordUsed = new CoordUsed[size, size];
            lstImage = new ImageCarte[size, size];

            NewGame(PlateauSize, theme);
        }

        private void NewGame(int size, Theme theme)
        {
            if (bNewGame)
            {
                var path = Directory.GetCurrentDirectory() + @"\Image\fruits\";
                var images = Directory.GetFiles(path, "*.png");

                imagesPossibles = new ImagePossible[images.Count() * 2];
                int counter1 = 0;
                int counter2 = 0;

                foreach (var item in images)
                {
                    if (counter2 < (size * size))
                    {
                        imagesPossibles[counter2] = new ImagePossible();
                        imagesPossibles[counter2].imageName = images[counter1];
                        ++counter2;
                        imagesPossibles[counter2] = new ImagePossible();
                        imagesPossibles[counter2].imageName = images[counter1];
                        ++counter2;
                        ++counter1;
                    }
                }

                for (int i = 0; i < imagesPossibles.Count(); i++)
                {
                    imagesPossibles[i].IsRemoved = false;
                    imagesPossibles[i].IsUsed = false;
                }

                for (int x = 0; x < size; x++)
                    for (int y = 0; y < size; y++)
                        lstCoordUsed[x, y] = new CoordUsed();

                for (int Ux = 0; Ux < size; Ux++)
                    for (int Uy = 0; Uy < size; Uy++)
                        lstCoordUsed[Ux, Uy].IsRemoved = false;
                bNewGame = false;
            }

            if (theme == Theme.voitures)
            {
                for (int i = 0; i < 6; i++)
                {
                    switch (i)
                    {
                        case 0:
                            if (!JokerRemoved)
                                ChangerCarteSpecial("joker", size);
                            break;
                        case 1:
                            if (!cursed1)
                                ChangerCarteSpecial("maudite1", size);
                            break;
                        case 2:
                            if (!cursed2)
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
                            if (!JokerRemoved)
                                ChangerCarteSpecial("joker", size);
                            break;
                        case 1:
                            if (!cursed1)
                                ChangerCarteSpecial("maudite1", size);
                            break;
                        case 2:
                            if (!cursed2)
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
                ChargerCarteNormale(size);
            }
        }

        internal bool CheckIfFinished()
        {
            for (int x = 0; x < size; x++)
            {
                for (int y = 0; y < size; y++)
                {
                    if (!lstCoordUsed[x, y].IsSpecial && !lstCoordUsed[x, y].IsRemoved && !lstImage[x, y].imageName.Contains("maypop") &&
                        !lstImage[x, y].imageName.Contains("sapote") && !lstImage[x, y].imageName.Contains("saratoga") && 
                        !lstImage[x, y].imageName.Contains("lfa") && !lstImage[x, y].imageName.Contains("melange") &&
                        !lstImage[x, y].imageName.Contains("parking"))
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        internal void rmCarte(int firstCardX, int firstCardY, int x, int y)
        {
            for (int i = 0; i < imagesPossibles.Count(); i++)
            {
                if (imagesPossibles[i].imageName == lstImage[firstCardX, firstCardY].imageName || imagesPossibles[i].imageName == lstImage[x, y].imageName)
                {
                    imagesPossibles[i].IsRemoved = true;
                }
            }
            lstCoordUsed[firstCardX, firstCardY].IsRemoved = true;
            lstCoordUsed[x, y].IsRemoved = true;
        }

        internal void rmJoker(int x, int y)
        {
            JokerRemoved = true;
            lstCoordUsed[x, y].IsRemoved = true;
        }

        internal void ReshuffleCards()
        {
            for (int x = 0; x < size; x++)
            {
                for (int y = 0; y < size; y++)
                {
                    if (!lstCoordUsed[x, y].IsRemoved)
                    {
                        lstCoordUsed[x, y].IsUsed = false;
                    }
                }
            }

            for (int i = 0; i < imagesPossibles.Count(); i++)
            {
                imagesPossibles[i].IsUsed = false;
            }

            NewGame(size, theme);
        }

        internal void StartNewGame()
        {
            bNewGame = true;
            NewGame(size, Theme.fruits);
        }

        internal void ReshuffleCardsCursed(int Ux, int Uy, string maudite)
        {
            if (maudite.Contains("maudite1"))
                cursed1 = true;
            if (maudite.Contains("maudite2"))
                cursed2 = true;
            
            for (int x = 0; x < size; x++)
            {
                for (int y = 0; y < size; y++)
                {
                    if (GetImage(x, y).imageName != maudite && !lstCoordUsed[x, y].IsRemoved)
                    {
                        lstCoordUsed[x, y].IsUsed = false;
                    }
                }
            }

            for (int i = 0; i < imagesPossibles.Count(); i++)
            {
                if (imagesPossibles[i].imageName != maudite && !lstCoordUsed[Ux, Uy].IsRemoved)
                {
                    imagesPossibles[i].IsUsed = false;
                }
            }

            NewGame(size, theme);

            cursed1 = false;
            cursed2 = false;
        }

        internal Joueurs GetLastPlayerPlayed()
        {
            return lastPlayedPlayer;
        }

        internal Joueurs GetCurrentPlayer()
        {
            return currentPlayer;
        }

        internal bool GetIsRemoved(int x, int y)
        {
            return lstCoordUsed[x, y].IsRemoved;
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
            Done = false;

            for (int i = 0; i < imagesPossibles.Count(); i++)
            {
                if (!imagesPossibles[i].IsUsed && !imagesPossibles[i].IsRemoved)
                {
                    do
                    {
                        int rndX = _rnd.Next(0, size);
                        int rndY = _rnd.Next(0, size);

                        if (!lstCoordUsed[rndX, rndY].IsUsed && !lstCoordUsed[rndX, rndY].IsRemoved)
                        {
                            lstImage[rndX, rndY] = new ImageCarte();
                            lstImage[rndX, rndY].imageName = imagesPossibles[i].imageName;
                            lstCoordUsed[rndX, rndY].IsUsed = true;
                            imagesPossibles[i].IsUsed = true;
                            Done = true;
                        }
                    } while (!Done);
                    Done = false;
                }
            }
        }

        private void ChangerCarteSpecial(string carte, int size)
        {
            Done = false;

            do
            {
                int rndX = _rnd.Next(0, size);
                int rndY = _rnd.Next(0, size);

                if (!lstCoordUsed[rndX, rndY].IsUsed && !lstCoordUsed[rndX, rndY].IsRemoved)
                {
                    lstImage[rndX, rndY] = new ImageCarte();
                    lstImage[rndX, rndY].imageName = Directory.GetCurrentDirectory() + @"\Image\" + carte + ".png";
                    lstCoordUsed[rndX, rndY].IsUsed = true;
                    lstCoordUsed[rndX, rndY].IsSpecial = true;
                    Done = true;
                }
            } while (!Done);
            Done = false;
        }
    }
}
