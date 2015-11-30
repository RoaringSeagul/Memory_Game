using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Memory_Game
{
    public class logique
    {
        ImageCarte[,] lstImage;
        public logique(int size)
        {
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
            lstImage = new ImageCarte[8, 8];
            for (int x = 0; x < 8; x++)
            {
                for (int y = 0; y < 8; y++)
                {
                    lstImage[x, y].imageName = new ImageCarte();
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
