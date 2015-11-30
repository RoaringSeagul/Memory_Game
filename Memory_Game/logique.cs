using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Memory_Game
{
    public class logique
    {
        Random _rnd = new Random();
        ImageCarte[,] lstImage;
        CoordUsed[,] lstCoordUsed;

        public logique(int size)
        {
            lstCoordUsed = new CoordUsed[size, size];
            lstImage = new ImageCarte[size, size];

            for (int x = 0; x < size; x++)
                for (int y = 0; y < size; y++)
                    lstCoordUsed[x, y] = new CoordUsed();

            NewGame(size);
        }

        void NewGame(int size)
        {
            int rndX = _rnd.Next(0, size);
            int rndY = _rnd.Next(0, size);
            if (!lstCoordUsed[rndX, rndY].IsUsed)
            {
                lstImage[rndX, rndY] = new ImageCarte();
                lstImage[rndX, rndY].imageName = "joker";
                lstCoordUsed[rndX, rndY].IsUsed = true;
            }

            for (int x = 0; x < size; x++)
            {
                for (int y = 0; y < size; y++)
                {
                    if (!lstCoordUsed[x, y].IsUsed)
                    {
                        lstImage[x, y] = new ImageCarte();
                        lstImage[x, y].imageName = "sapote";
                    }
                }
            }
        }

        public ImageCarte GetImage(int x, int y)
        {
            return lstImage[x, y];
        }
    }
}
