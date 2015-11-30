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
            for (int x = 0; x < size; x++)
                for (int y = 0; y < size; y++)
                    lstCoordUsed[x, y] = new CoordUsed();

            switch (size)
            {
                case 8:
                    NewGame8x8();
                    break;
                default:
                    break;
            }
        }

        void NewGame8x8()
        {
            int rndX = _rnd.Next(0, 8);
            int rndY = _rnd.Next(0, 8);
            if (!lstCoordUsed[rndX, rndY].IsUsed)
            {
                lstImage[rndX, rndY] = new ImageCarte();
                lstImage[rndX, rndY].imageName = "joker";
                lstCoordUsed[rndX, rndY].IsUsed = true;
            }

            lstImage = new ImageCarte[8, 8];
            for (int x = 0; x < 8; x++)
            {
                for (int y = 0; y < 8; y++)
                {
                    lstImage[x, y] = new ImageCarte();
                    lstImage[x, y].imageName = "sapote";
                }
            }
        }

        public ImageCarte GetImage(int x, int y)
        {
            return lstImage[x, y];
        }
    }
}
