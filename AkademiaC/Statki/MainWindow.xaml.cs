using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using Statki.ViewModel;
using Statki.Model;

namespace Statki
{
    public partial class MainWindow : Window
    {
        GraczPrawdziwy _graczPrawdziwy;
        GraczKomp _graczKomp;

        VMGracza _gracz;
        VMKomputera _komputer;
        
        public MainWindow()
        {
            _graczPrawdziwy = new GraczPrawdziwy();
            _graczKomp = new GraczKomp();
            _gracz = new VMGracza(_graczPrawdziwy, _graczKomp);
            _komputer = new VMKomputera(_graczPrawdziwy, _graczKomp);

            InitializeComponent();
            siatkaGracza.DataContext = _gracz;
            siatkaKomputera.DataContext = _komputer;
        }

        private void ExecutedNowaGra(object sender, ExecutedRoutedEventArgs e)
        {
            _graczPrawdziwy.Reset();
            _graczKomp.Reset();            
        }

        private void ExecutedGraAutomatyczna(object sender, ExecutedRoutedEventArgs e)
        {
            ExecutedNowaGra(sender, e);
            while (!_komputer.Clicked(null, true))
            { }
        }

        private void ExecutedWyjscie(object sender, ExecutedRoutedEventArgs e)
        {
            this.Close();
        }
    }
}
