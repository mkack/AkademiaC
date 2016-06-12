using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Statki.ViewModel;
using Statki.Model;

namespace Statki.View
{

    public partial class SiatkaMorze : UserControl
    {
        public SiatkaMorze()
        {
            InitializeComponent();
        }

        private void Item_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            PodstawowyVM vm = this.DataContext as PodstawowyVM;
            ListBoxItem item = sender as ListBoxItem;
            Morze content = item.Content as Morze;

            if (content == null)
                return;

            vm.Clicked(content);            
        }
    }
}
