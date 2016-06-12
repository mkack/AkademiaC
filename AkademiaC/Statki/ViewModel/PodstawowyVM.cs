using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Statki.Model;
using System.Windows.Data;
using System.ComponentModel;
using System.Windows.Input;

namespace Statki.ViewModel
{
    abstract class PodstawowyVM
    {
        protected GraczPrawdziwy _graczPrawdziwy;
        protected GraczKomp _graczKomp;

        public PodstawowyVM(GraczPrawdziwy graczPrawdziwy, GraczKomp graczKomp)
        {
            _graczPrawdziwy = graczPrawdziwy;
            _graczKomp = graczKomp;
        }

        public abstract bool Clicked(Morze content, bool automated=false);
        public abstract List<List<Morze>> MyGrid { get; }
    }
}
