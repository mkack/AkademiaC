using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Statki.Model
{
    class GraczPrawdziwy: Gracz
    {
        public void RuchGracza(int row, int col, Gracz otherPlayer)
        {
            Strzal(row, col, otherPlayer);
        }
    }
}
