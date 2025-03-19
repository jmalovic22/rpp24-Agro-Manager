using EntitiesLayer;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PresentationLayer.UserControls
{
    /// <summary>
    /// Interaction logic for UcCardTemplate.xaml
    /// </summary>
    public partial class UcCardTemplate : UserControl
    {
        public UcCardTemplate()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (AppKontekst.prijavljeniKorisnik != null)
            {
                if (AppKontekst.prijavljeniKorisnik.Tip_korisnika.Razina_prava == 1 || AppKontekst.vozilo != null)
                {
                    UcBtnIzbrisi.Visibility = Visibility.Collapsed;
                    UcBtnUredi.Visibility = Visibility.Collapsed;
                }
            }
        }
    }
}
