using EntitiesLayer;
using EntitiesLayer.Entiteti;
using Newtonsoft.Json;
using PresentationLayer.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for UcVoziloCard.xaml
    /// </summary>
    public partial class UcVoziloCard : UserControl
    {
        private RestClient Client = new RestClient();
        public Vozilo Vozilo { get; set; }

        public UcVoziloCard()
        {
            InitializeComponent();
            this.DataContextChanged += UcVoziloCard_DataContextChanged;

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

        public UcVoziloCard(Vozilo vozilo)
        {
            InitializeComponent();
            Vozilo = vozilo;
        }

        private void UcVoziloCard_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (DataContext is Vozilo vozilo)
            {
                Vozilo = vozilo;
            }
        }

        private async void IzbrisiButton_Click(object sender, RoutedEventArgs e)
        {
            if (Vozilo.Prikljucak != null)
            {
                GreskaView greska = new GreskaView("Nije moguće obrisati vozilo jer je na njega spojen priključak!");
                greska.ShowDialog();
                return;
            }

            DeleteView deleteView = new DeleteView();
            this.Opacity = 0.4;
            bool? rezultat = deleteView.ShowDialog();
            this.Opacity = 1;

            if (rezultat == true)
            {
                var uspjesnoBrisanje = await Obrisi(Vozilo.Registracija);
                if (uspjesnoBrisanje == true)
                { 
                    Visibility = Visibility.Collapsed;
                }
            }
        }

        private void UrediButton_Click(object sender, RoutedEventArgs e)
        {
            EditVoziloView editVoziloView = new EditVoziloView(Vozilo);
            editVoziloView.ShowDialog();
            Vozilo = editVoziloView.Vozilo;
            UserControl_Loaded(sender, e);
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            TxtRegistracija.Content = Vozilo.Registracija;  
            TxtMarka.Content = Vozilo.Marka;
            TxtNaziv.Content = Vozilo.Vrsta;
            TxtStatus.Content = Vozilo.Dostupnost;

            if (Vozilo.Dostupnost == "Dostupno")
            {
                BtnZauzmi.Content = "Zauzmi";
                TxtPrikljucak.Content = "Vozilo nema spojeni prikljucak";
            }
            else
            {
                BtnZauzmi.Content = "Vrati";
                if (Vozilo.Prikljucak.Count != 0)
                {
                    TxtPrikljucak.Content = Vozilo.Prikljucak.First().ToString();
                    BtnPrikljucak.Visibility = Visibility.Visible;
                    BtnPrikljucak.Background = Brushes.Red;
                    BtnPrikljucak.Content = "Odspoji";
                }
                else
                {
                    TxtPrikljucak.Content = "Vozilo nema spojeni prikljucak";
                    BtnPrikljucak.Visibility = Visibility.Visible;
                }
            }
        }

        private async void BtnZauzmi_Click(object sender, RoutedEventArgs e)
        {
            if (Vozilo.Dostupnost == "Dostupno")
            {
                var uspjeh = await ZauzmiVozilo();
                if (uspjeh)
                {
                    PorukaView poruka = new PorukaView((Control)sender, "Vozilo zauzeto!");
                    poruka.Show();
                    TxtStatus.Content = "Nedostupno";
                    BtnZauzmi.Content = "Vrati";
                    BtnPrikljucak.Visibility = Visibility.Visible;
                }
            }
            else if (Vozilo.Dostupnost == "Nedostupno" && Vozilo.Prikljucak.Count != 0)
            {
                GreskaView greska = new GreskaView($"Nije moguće vratiti vozilo! \n {"Na vozilo je spojen priključak"}");
                greska.ShowDialog();
                return;
            }
            else
            {
                var uspjeh = await VratiVozilo();
                if (uspjeh)
                {
                    PorukaView poruka = new PorukaView((Control)sender, "Vozilo vraćeno!");
                    poruka.Show();
                    TxtStatus.Content = "Dostupno";
                    BtnZauzmi.Content = "Zauzmi";
                    BtnPrikljucak.Visibility = Visibility.Collapsed;
                }
            }
        }

        private async void BtnPrikljucak_Click(object sender, RoutedEventArgs e)
        {
            if (Vozilo.Prikljucak.Count == 0)
            {
                AppKontekst.vozilo = Vozilo;
                AppKontekst.mainWindow.btnPrikljucci_Click(sender, e);
            }
            else
            {
                await OdspojiPrikljucak();
                AppKontekst.vozilo = null;
                BtnPrikljucak.Content = "Prikljucak";
                TxtPrikljucak.Content = "Vozilo nema spojeni prikljucak";
                BtnPrikljucak.Background = (Brush)FindResource("LimeGreenBrush");
                Vozilo.Prikljucak.Clear();
            }
        }

        private async Task<bool> Obrisi(string registracija)
        {
            try
            {
                var odgovor = await Client.DeleteAsync<bool>("/api/Vozilo/" + registracija);
                return odgovor;
            }
            catch (Exception ex)
            {
                GreskaView greska = new GreskaView($"Greška pri brisanju podataka: {ex.Message}");
                greska.ShowDialog();
                return false;
            }
        }

        private void ImageVozilo_MouseDown(object sender, MouseButtonEventArgs e)
        {
            NapomenaView opisVozila = new NapomenaView(Vozilo.Opis);
            opisVozila.ShowDialog();
        }

        private async Task<bool> ZauzmiVozilo()
        {
            try
            {
                if (Vozilo.Dostupnost == "Nedostupno")
                {
                    GreskaView greska = new GreskaView("Vozilo je vec zauzeto");
                    greska.ShowDialog();
                    return false;
                }

                var vozilo = new Vozilo
                {
                    Registracija = Vozilo.Registracija,
                    Vrsta = Vozilo.Vrsta,
                    Marka = Vozilo.Marka,
                    Jacina_motora_u_KS = Vozilo.Jacina_motora_u_KS,
                    Opis = Vozilo.Opis,
                    Dostupnost = "Nedostupno",
                    Poduzece_id = AppKontekst.prijavljeniKorisnik.Poduzece_id
                };

                string podaci = JsonConvert.SerializeObject(vozilo);
                var odgovor = await Client.PutAsync<bool>("/api/Vozilo", podaci);

                if (odgovor == true)
                {
                    Vozilo = vozilo;
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

        private async Task<bool> VratiVozilo()
        {
            try
            {
                if (Vozilo.Dostupnost == "Dostupno")
                {
                    GreskaView greska = new GreskaView("Vozilo nije dostupno");
                    greska.ShowDialog();
                    return false;
                }

                var vozilo = new Vozilo
                {
                    Registracija = Vozilo.Registracija,
                    Vrsta = Vozilo.Vrsta,
                    Marka = Vozilo.Marka,
                    Jacina_motora_u_KS = Vozilo.Jacina_motora_u_KS,
                    Opis = Vozilo.Opis,
                    Dostupnost = "Dostupno",
                    Poduzece_id = AppKontekst.prijavljeniKorisnik.Poduzece_id
                };

                string podaci = JsonConvert.SerializeObject(vozilo);
                var odgovor = await Client.PutAsync<bool>("/api/Vozilo", podaci);

                if (odgovor == true)
                {
                    Vozilo = vozilo;
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

        private async Task<bool> OdspojiPrikljucak()
        {
            try
            {
                var prikljucak = new Prikljucak
                {
                    Prikljucak_id = Vozilo.Prikljucak.First().Prikljucak_id,
                    Registracija = Vozilo.Prikljucak.First().Registracija,
                    Vrsta_prikljucka_id = Vozilo.Prikljucak.First().Vrsta_prikljucka_id,
                    Marka = Vozilo.Prikljucak.First().Marka,
                    Dostupnost = "Dostupno",
                    Poduzece_id = AppKontekst.prijavljeniKorisnik.Poduzece_id,
                    Registracija_vozila = null
                };

                string podaci = JsonConvert.SerializeObject(prikljucak);
                var odgovor = await Client.PutAsync<bool>("/api/Prikljucak", podaci);

                if (odgovor == true)
                {
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
