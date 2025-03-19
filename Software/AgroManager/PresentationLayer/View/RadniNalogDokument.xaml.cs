using EntitiesLayer;
using EntitiesLayer.Entiteti;
using Newtonsoft.Json;
using PresentationLayer.UserControls;
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
    /// Interaction logic for RadniNalogDokument.xaml
    /// </summary>
    public partial class RadniNalogDokument : Window
    {
        private RestClient Client = new RestClient();
        public Radni_nalog RadniNalog { get; set; }

        public RadniNalogDokument(Radni_nalog radniNalog)
        {
            InitializeComponent();
            RadniNalog = radniNalog;
        }

        private void BtnIzadji_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            AppKontekst.privremenaListaZaKorisnikeRadnogNaloga = new List<Korisnik>();
            AppKontekst.privremenaListaZaStavkeRadnogNaloga = new List<Stavka_radnog_naloga>();
            await DohvatiStavke();
            DohvatiZaposlenike();
            if(RadniNalog.Status == "Završen")
            {
                TxtDatumZavrsetka.Content = RadniNalog.Datum_zavrsetka.ToString();
            }
        }

        private void DohvatiZaposlenike()
        {
            if (RadniNalog.Korisnik.ToList() != null)
            {
                AppKontekst.privremenaListaZaKorisnikeRadnogNaloga = RadniNalog.Korisnik.ToList();
                PosloziExpanderZaZaposlenike();
            }
            else
            {
                GreskaView greska = new GreskaView($"Greška prilikom učitavanja podataka.");
                greska.ShowDialog();
            }
        }

        private async Task DohvatiStavke()
        {
            try
            {
                var stavke = await Client.GetAsync<List<Stavka_radnog_naloga>>($"/api/StavkaRadnogNaloga/RadniNalog/{RadniNalog.Radni_nalog_id}");

                if (stavke != null)
                {
                    AppKontekst.privremenaListaZaStavkeRadnogNaloga = stavke;
                    PosloziExpanderZaStavke();
                }
                else
                {
                    GreskaView greska = new GreskaView($"Greška prilikom učitavanja podataka.");
                    greska.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                GreskaView greska = new GreskaView($"Greška prilikom učitavanja podataka: {ex.Message}");
                greska.ShowDialog();
            }
        }

        private void PosloziExpanderZaStavke()
        {
            StackPanelStavkeRadnogNaloga.Children.Clear();

            foreach (var stavka in AppKontekst.privremenaListaZaStavkeRadnogNaloga.ToList())
            {
                Expander expander = new Expander
                {
                    Width = 200,
                    Margin = new Thickness(0, 10, 0, 0),
                    Header = $"{stavka.Posao.Naziv}",
                    Style = FindResource("CustomExpanderStyle") as Style
                };

                StackPanel stackPanel = new StackPanel
                {
                    Margin = new Thickness(5)
                };

                CheckBox chkZavrseno = new CheckBox
                {
                    Content = "Završeno",
                    IsChecked = stavka.Status == "Završen" ? true : false,
                    IsEnabled = RadniNalog.Status != "Završen",
                    Foreground = Brushes.Black,
                    Margin = new Thickness(0, 5, 0, 0)
                };

                chkZavrseno.Checked += async (s, e) =>
                {
                    stavka.Status = "Završen";
                    await AzurirajStavku(stavka);
                    provjeriZavrseneStavke();
                    PosloziExpanderZaStavke();
                };

                chkZavrseno.Unchecked += async (s, e) =>
                {
                    stavka.Status = "Aktivan";
                    await AzurirajStavku(stavka);
                    PosloziExpanderZaStavke();
                };

                stackPanel.Children.Add(new TextBlock { Text = $"Status: {stavka.Status}" });
                stackPanel.Children.Add(new TextBlock { Text = $"Razina prioriteta: {stavka.Razina_prioriteta}" });
                stackPanel.Children.Add(new TextBlock { Text = $"Napomena: {stavka.Napomena}" });
                stackPanel.Children.Add(chkZavrseno);

                expander.Content = stackPanel;
                StackPanelStavkeRadnogNaloga.Children.Add(expander);
            }
        }

        private void PosloziExpanderZaZaposlenike()
        {
            StackPanelZaposlenici.Children.Clear();

            foreach (var zaposlenik in AppKontekst.privremenaListaZaKorisnikeRadnogNaloga.ToList())
            {
                Expander expander = new Expander
                {
                    Width = 200,
                    Margin = new Thickness(0, 10, 0, 0),
                    Header = $"{zaposlenik.Ime} {zaposlenik.Prezime}",
                    Style = FindResource("CustomExpanderStyle") as Style
                };

                StackPanel stackPanel = new StackPanel
                {
                    Margin = new Thickness(5)
                };

                stackPanel.Children.Add(new TextBlock { Text = $"OIB: {zaposlenik.Korisnik_OIB}" });
                stackPanel.Children.Add(new TextBlock { Text = $"Email: {zaposlenik.Email}" });
                stackPanel.Children.Add(new TextBlock { Text = $"Položene kategorije: {zaposlenik.Polozene_kategorije}" });
                stackPanel.Children.Add(new TextBlock { Text = $"Pozicija: {zaposlenik.Tip_korisnika.Pozicija}" });

                expander.Content = stackPanel;
                StackPanelZaposlenici.Children.Add(expander);
            }
        }

        private async void provjeriZavrseneStavke()
        {
            if (AppKontekst.privremenaListaZaStavkeRadnogNaloga.Any(k => k.Status == "Aktivan"))
            {
                RadniNalog.Status = "Aktivan";
            }
            else
            {
                RadniNalog.Datum_zavrsetka = DateTime.Now;
                RadniNalog.Status = "Završen";
                await AzurirajRadniNalog();
                TxtDatumZavrsetka.Content = RadniNalog.Datum_zavrsetka.ToString();
            }
        }

        private async Task AzurirajStavku(Stavka_radnog_naloga stavka)
        {
            try
            {
                var novaSavka = new Stavka_radnog_naloga
                {
                    Stavka_radnog_naloga_id = stavka.Stavka_radnog_naloga_id,
                    Status = stavka.Status,
                    Razina_prioriteta = stavka.Razina_prioriteta,
                    Napomena = stavka.Napomena,
                    Radni_nalog_id = stavka.Radni_nalog_id,
                    Posao_id = stavka.Posao_id
                };
                string podaci = JsonConvert.SerializeObject(novaSavka);
                var odgovor = await Client.PutAsync<bool>($"/api/StavkaRadnogNaloga", podaci);
                if (!odgovor)
                {
                    GreskaView greska = new GreskaView("Greška prilikom ažuriranja podataka.");
                    greska.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                GreskaView greska = new GreskaView($"Greška prilikom ažuriranja podataka: {ex.Message}");
                greska.ShowDialog();
            }
        }

        private async Task AzurirajRadniNalog()
        {
            try
            {
                var noviRadniNalog = new Radni_nalog
                {
                    Radni_nalog_id = RadniNalog.Radni_nalog_id,
                    Datum_zavrsetka = RadniNalog.Datum_zavrsetka,
                    Datum_kreiranja = RadniNalog.Datum_kreiranja,
                    Zavrsni_rok = RadniNalog.Zavrsni_rok,
                    Status = RadniNalog.Status,
                    Polje_id = RadniNalog.Polje_id,
                    Korisnik = RadniNalog.Korisnik,
                    Stavka_radnog_naloga = RadniNalog.Stavka_radnog_naloga
                };
                string podaci = JsonConvert.SerializeObject(noviRadniNalog);
                var odgovor = await Client.PutAsync<bool>($"/api/RadniNalog", podaci);
                if (!odgovor)
                {
                    GreskaView greska = new GreskaView("Greška prilikom ažuriranja podataka.");
                    greska.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                GreskaView greska = new GreskaView($"Greška prilikom ažuriranja podataka: {ex.Message}");
                greska.ShowDialog();
            }
        }
    }
}
