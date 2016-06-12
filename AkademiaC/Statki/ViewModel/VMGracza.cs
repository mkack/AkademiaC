using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Statki.Model;
using System.Windows.Data;
using System.ComponentModel;

namespace Statki.ViewModel
{
    class VMGracza: PodstawowyVM
    {
        public VMGracza(GraczPrawdziwy graczPrawdziwy, GraczKomp graczKomp)
            : base(graczPrawdziwy, graczKomp)
        {
        }

        //for design mode only
        public VMGracza()
            : base(null, null)
        {
            _graczPrawdziwy = new GraczPrawdziwy();
        }

        public override List<List<Morze>> MyGrid
        {
            get
            {
                return _graczPrawdziwy.MojePole;
            }
        }

        public override bool Clicked(Morze content, bool automated)
        {
            return false;
        }
    }
}
