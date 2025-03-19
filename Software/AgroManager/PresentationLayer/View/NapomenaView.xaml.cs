using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PresentationLayer.View
{
    /// <summary>
    /// Interaction logic for NapomenaView.xaml
    /// </summary>
    public partial class NapomenaView : Window
    {
        public NapomenaView()
        {
            InitializeComponent();
            
        }

        public NapomenaView(string tekstualniOpis)
        {
            InitializeComponent();
            TxtNapomene.Text = tekstualniOpis;
        }

        private void BtnIzadi_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
