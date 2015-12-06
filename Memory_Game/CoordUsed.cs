using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Memory_Game
{
    public class CoordUsed
    {
        public bool IsUsed { get; set; }
        public bool IsRemoved { get; set; }
        public bool IsSpecial { get; set; }

        public CoordUsed()
        {
            IsUsed = false;
        }


    }
}
