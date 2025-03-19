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

    public partial class UcAddButton : UserControl
    {
        public event RoutedEventHandler ButtonClick;
        public event RoutedEventHandler ButtonMouseOver;
        public event RoutedEventHandler ButtonMouseLeave;


        public UcAddButton()
        {
            InitializeComponent();
            DodajButton.Click += OnDodajButtonClick; // Povezivanje lokalnog klika s metodom
            DodajButton.MouseEnter += OnDodajButtonMouseEnter;
            DodajButton.MouseLeave += OnDodajButtonMouseLeave;
        }

        private void OnDodajButtonClick(object sender, RoutedEventArgs e)
        {
            // Prosljeđivanje događaja vanjskom kontroleru
            ButtonClick?.Invoke(this, e);
        }

        private void OnDodajButtonMouseEnter(object sender, RoutedEventArgs e)
        {
            // Prosljeđivanje događaja vanjskom kontroleru
            ButtonMouseOver?.Invoke(this, e);
            Opacity = 0.85;
        }
        private void OnDodajButtonMouseLeave(object sender, RoutedEventArgs e)
        {
            // Prosljeđivanje događaja vanjskom kontroleru
            ButtonMouseLeave?.Invoke(this, e);
            Opacity = 1;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (AppKontekst.prijavljeniKorisnik != null)
            {
                if (AppKontekst.prijavljeniKorisnik.Tip_korisnika.Razina_prava == 1 || AppKontekst.vozilo != null)
                {
                    DodajButton.Visibility = Visibility.Collapsed;
                }
            }
        }
    }
}
