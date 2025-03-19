using EntitiesLayer.Entiteti;
using PresentationLayer.View;
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
    /// Interaction logic for UcZaposlenikCard.xaml
    /// </summary>
    public partial class UcZaposlenikCard : UserControl
    {
        private RestClient Client = new RestClient();
        public Korisnik Korisnik { get; set; }


        public UcZaposlenikCard()
        {
            InitializeComponent();
            this.DataContextChanged += UcZaposlenikCard_DataContextChanged;

            var cardTemplate = (UcCardTemplate)FindName("UcCardPredlozak");
            if (cardTemplate != null)
            {
                var urediBtn = (UcEditButton)cardTemplate.FindName("UcBtnUredi");
                if (urediBtn != null)
                {
                    urediBtn.ButtonClick += UrediButton_Click;
                }
                var izbrisiBtn = (UcDeleteButton)cardTemplate.FindName("UcBtnIzbrisi");
                if (izbrisiBtn != null)
                {
                    izbrisiBtn.ButtonClick += IzbrisiButton_Click;
                }
            }
        }

        public UcZaposlenikCard(Korisnik korisnik)
        {
            InitializeComponent();
            Korisnik = korisnik;
        }

        private void UcZaposlenikCard_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (DataContext is Korisnik korisnik)
            {
                Korisnik = korisnik;
            }
        }

        private async void IzbrisiButton_Click(object sender, RoutedEventArgs e)
        {
            DeleteView deleteView = new DeleteView();
            this.Opacity = 0.4;
            bool? rezultat = deleteView.ShowDialog();
            this.Opacity = 1;

            if (rezultat == true)
            {
                var uspjesnoBrisanje = await Obrisi(Korisnik.Korisnik_OIB);
                if (uspjesnoBrisanje == true)
                {
                    Visibility = Visibility.Collapsed;
                }
            }
        }

        private void UrediButton_Click(object sender, RoutedEventArgs e)
        {
            EditZaposlenikView editZaposlenikView = new EditZaposlenikView(Korisnik);
            editZaposlenikView.ShowDialog();
            Korisnik.Ime = editZaposlenikView.Korisnik.Ime;
            Korisnik.Prezime = editZaposlenikView.Korisnik.Prezime;
            Korisnik.Email = editZaposlenikView.Korisnik.Email; 
            Korisnik.Tip_korisnika_id = editZaposlenikView.Korisnik.Tip_korisnika_id;
            UserControl_Loaded(sender, e);
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            TxtImePrezime.Content = Korisnik.Ime + " " + Korisnik.Prezime;
            TxtOIB.Content = Korisnik.Korisnik_OIB;
        }

        private async Task<bool> Obrisi(string oib)
        {
            try
            {
                var odgovor = await Client.DeleteAsync<bool>("/api/Korisnik/" + oib);
                return odgovor;
            }
            catch (Exception ex)
            {
                GreskaView greska = new GreskaView($"Greška prilikom brisanja podataka: {ex.Message}");
                greska.ShowDialog();
                return false;
            }
        }
    }
}
