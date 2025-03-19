using EntitiesLayer;
using EntitiesLayer.Entiteti;
using Newtonsoft.Json;
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
    /// Interaction logic for UcPrikljucakCard.xaml
    /// </summary>
    public partial class UcPrikljucakCard : UserControl
    {
        private RestClient Client = new RestClient();
        public Prikljucak Prikljucak { get; set; }
        public UcPrikljucakCard()
        {
            InitializeComponent();
            this.DataContextChanged += UcPrikljucakCard_DataContextChanged;

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

        public UcPrikljucakCard(Prikljucak prikljucak)
        {
            InitializeComponent();
            Prikljucak = prikljucak;
        }

        private void UcPrikljucakCard_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (DataContext is Prikljucak prikljucak)
            {
                Prikljucak = prikljucak;
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
                var uspjesnoBrisanje = await Obrisi(Prikljucak.Prikljucak_id);
                if (uspjesnoBrisanje == true)
                {
                    Visibility = Visibility.Collapsed;
                }
            }
        }

        private void UrediButton_Click(object sender, RoutedEventArgs e)
        {
            EditPrikljucakView editPrikljucakView = new EditPrikljucakView(Prikljucak);
            editPrikljucakView.ShowDialog();
            Prikljucak = editPrikljucakView.Prikljucak;
            UserControl_Loaded(sender, e);
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            TxtNaziv.Content = Prikljucak.Vrsta_prikljucka.Naziv;
            TxtMarka.Content = Prikljucak.Marka;
            TxtStatus.Content = Prikljucak.Dostupnost;

            if (AppKontekst.vozilo != null)
            {
                BtnSpoji.Visibility = Visibility.Visible;
            }
            else
            {
                BtnSpoji.Visibility = Visibility.Collapsed;
            }
        }

        private async void BtnSpoji_Click(object sender, RoutedEventArgs e)
        {
            var uspjeh = await SpojiPrikljucak();
            if (uspjeh)
            {
                TxtStatus.Content = "Nedostupno";
                AppKontekst.vozilo = null;
                AppKontekst.mainWindow.btnVozila_Click(sender, e);
            }
        }
        private async Task<bool> Obrisi(int id)
        {
            try
            {
                var odgovor = await Client.DeleteAsync<bool>("/api/Prikljucak/" + id);
                return odgovor;
            }
            catch (Exception ex)
            {
                GreskaView greska = new GreskaView($"Greška pri brisanju podataka: {ex.Message}");
                greska.ShowDialog();
                return false;
            }
        }

        private async Task<bool> SpojiPrikljucak()
        {
            try
            {
                var prikljucak = new Prikljucak
                {
                    Prikljucak_id = Prikljucak.Prikljucak_id,
                    Registracija = Prikljucak.Registracija,
                    Vrsta_prikljucka_id = Prikljucak.Vrsta_prikljucka_id,
                    Marka = Prikljucak.Marka,
                    Dostupnost = "Nedostupno",
                    Poduzece_id = AppKontekst.prijavljeniKorisnik.Poduzece_id,
                    Registracija_vozila = AppKontekst.vozilo.Registracija
                };

                string podaci = JsonConvert.SerializeObject(prikljucak);
                var odgovor = await Client.PutAsync<bool>("/api/Prikljucak", podaci);

                if (odgovor == true)
                {
                    Prikljucak = prikljucak;
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                GreskaView greska = new GreskaView($"Greška pri ažuriranju: {ex.Message}");
                greska.ShowDialog();
                return false;
            }
        }
    }
}
