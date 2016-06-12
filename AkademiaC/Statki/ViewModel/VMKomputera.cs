using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Statki.Model;
using System.Windows.Data;
using System.ComponentModel;
using System.Windows.Input;
using System.Windows;

namespace Statki.ViewModel
{
    class VMKomputera : PodstawowyVM
    {
        public VMKomputera(GraczPrawdziwy graczPrawdziwy, GraczKomp graczKomp)
            : base(graczPrawdziwy, graczKomp)
        {
        }

        public override List<List<Morze>> MyGrid
        {
            get
            {
                return _graczPrawdziwy.PolePrzeciwnika;
            }
        }

        //returns true if game is over
        public override bool Clicked(Morze pole, bool automated)
        {
            if (automated)
                _graczPrawdziwy.RuchAutomatyczny(_graczKomp);
            else
            {
                if (pole.Type != RodzajPola.Nieznane)
                {
                    MessageBox.Show("Wybierz nowe pole!");
                    return false;
                }

                _graczPrawdziwy.RuchGracza(pole.Row, pole.Col, _graczKomp);
            }

            if (_graczKomp.BrakStatkow())
            {
                MessageBox.Show("Wygrales!");
                return true;
            }
            else
            {
                _graczKomp.RuchGracza(_graczPrawdziwy);
                if (_graczPrawdziwy.BrakStatkow())
                {
                    MessageBox.Show("Przegrales :(");
                    return true;
                }
            }

            return false;
        }
    }
}
