using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace Statki.Model
{
    enum RodzajPola { Nieznane, Woda, Nietrafiony, Trafiony, Zatopiony }

    class Morze : DependencyObject
    {
        public int Row { get; private set;  }
        public int Col { get; private set; }
        public int ShipIndex { get; set; }

        public RodzajPola Type
        {
            get { return (RodzajPola)GetValue(TypeProperty); }
            set { SetValue(TypeProperty, value); }
        }
        public static readonly DependencyProperty TypeProperty =
        DependencyProperty.Register("Type", typeof(RodzajPola), typeof(Morze), null);

        public Morze(int row, int col)
        {
            Row = row;
            Col = col;
        }

        public void Reset(RodzajPola type)
        {
            Type = type;
            ShipIndex = -1;
        }
    }
}
