using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Statki.Model
{
    enum RodzajStatku {Transportowiec, Okret, Niszczyciel, Podwodna, Patrolowiec};

    class Statek
    {
        private int _zycie;

        private readonly RodzajStatku rodzaj;
        
        private static readonly Dictionary<RodzajStatku, int> dlugoscStatku =
            new Dictionary<RodzajStatku, int>()
        {
            {RodzajStatku.Transportowiec, 5},
            {RodzajStatku.Okret, 4},
            {RodzajStatku.Niszczyciel, 3},
            {RodzajStatku.Podwodna, 3},
            {RodzajStatku.Patrolowiec, 2}
        };

        public Statek(RodzajStatku type)
        {
            rodzaj = type;
            Reincarnate();
        }

        public void Reincarnate()
        {
            _zycie = dlugoscStatku[rodzaj];
        }

        public int Dlugosc
        {
            get
            {
                return dlugoscStatku[rodzaj];
            }
        }
        
        public bool Zatopiony
        {
            get
            {
                return _zycie == 0 ? true : false;
            }
        }

        public bool Trafiony()
        {
            _zycie--;
            return Zatopiony;
        }
    }
}
