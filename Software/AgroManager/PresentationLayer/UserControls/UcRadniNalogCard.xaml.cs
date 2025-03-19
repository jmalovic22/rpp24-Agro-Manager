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
    /// Interaction logic for UcRadniNalogCard.xaml
    /// </summary>
    public partial class UcRadniNalogCard : UserControl
    {
        private Radni_nalog RadniNalog;
        private RestClient Client = new RestClient();

        public UcRadniNalogCard()
        {
            InitializeComponent();

            var cardTemplate = (UcCardTemplate)FindName("UcCardPredlozak");
            if (cardTemplate != null)
            {
                var urediBtn = (UcEditButton)cardTemplate.FindName("UcBtnUredi");
                urediBtn.Visibility = Visibility.Hidden;
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

        private void DocumentImage_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var dokumentRadnogNaloga = new RadniNalogDokument(RadniNalog);
            dokumentRadnogNaloga.ShowDialog();
        }

        private async void IzbrisiButton_Click(object sender, RoutedEventArgs e)
        {
            DeleteView deleteView = new DeleteView();
            this.Opacity = 0.4;
            bool? rezultat = deleteView.ShowDialog();
            this.Opacity = 1;

            if (rezultat == true)
            {
                var uspjesnoBrisanje = await Obrisi(RadniNalog.Radni_nalog_id);
                if (uspjesnoBrisanje == true)
                {
                    this.Visibility = Visibility.Collapsed;
                }
            }
        }

        private void UrediButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            this.RadniNalog = DataContext as Radni_nalog;
            txtDatumKreiranja.Content = $"Datum kreiranja: {RadniNalog.Datum_kreiranja.ToString("dd.MM.yyyy")}";
            txtKrajnjiRok.Content = $"Završni rok: {RadniNalog.Zavrsni_rok.ToString("dd.MM.yyyy")}";
            txtPolje.Content = $"Polje: {RadniNalog.Polje.Ime_polja}";
            txtStatus.Content = RadniNalog.Status;
        }

        private async Task<bool> Obrisi(int id)
        {
            try
            {
                var stavke = await Client.GetAsync<List<Stavka_radnog_naloga>>($"/api/StavkaRadnogNaloga/RadniNalog/{id}");

                bool sveUspjesno = true;
                if (stavke != null)
                {
                    foreach (var stavka in stavke)
                    {
                        var odgovor = await Client.DeleteAsync<bool>($"/api/StavkaRadnogNaloga/{stavka.Stavka_radnog_naloga_id}");
                        if (!odgovor)
                        {
                            sveUspjesno = false;
                        }
                    }
                }

                if(sveUspjesno)
                {
                    var azuriraniRadniNalog = new Radni_nalog
                    {
                        Radni_nalog_id = RadniNalog.Radni_nalog_id,
                        Datum_kreiranja = RadniNalog.Datum_kreiranja,
                        Zavrsni_rok = RadniNalog.Zavrsni_rok,
                        Status = RadniNalog.Status,
                        Polje_id = RadniNalog.Polje_id,
                        Polje = RadniNalog.Polje,
                        Korisnik = new List<Korisnik>()
                    };

                    string podaci = JsonConvert.SerializeObject(azuriraniRadniNalog);
                    var azuriranjeOdgovor = await Client.PutAsync<bool>($"/api/RadniNalog", podaci);

                    if (!azuriranjeOdgovor)
                    {
                        return false;
                    }

                    var odgovor = await Client.DeleteAsync<bool>($"/api/RadniNalog/{id}");
                    return odgovor;
                }

                return false;
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
