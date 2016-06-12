using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Statki.Model
{
    class GraczKomp: Gracz
    {
        public void RuchGracza(Gracz otherPlayer)
        {
            RuchAutomatyczny(otherPlayer);
        }
    }
}
