using EntitiesLayer;
using EntitiesLayer.Entiteti;
using EntitiesLayer.Enumeracije;
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
    /// Interaction logic for AddRadniNalogView2.xaml
    /// </summary>
    public partial class AddRadniNalogView2 : Window
    {
        public RestClient Client = new RestClient();
        public Radni_nalog Radni_Nalog { get; set; }

        public AddRadniNalogView2()
        {
            InitializeComponent();
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            RbAktivan.IsChecked = true;
            RbZavrsen.IsChecked = false;
            RbZavrsen.IsEnabled = false;

            RbZavrsen.Foreground = Brushes.Gray;
            RbAktivan.Foreground = Brushes.Black;

            AppKontekst.privremenaListaZaKorisnikeRadnogNaloga = new List<Korisnik>();
            AppKontekst.privremenaListaZaStavkeRadnogNaloga = new List<Stavka_radnog_naloga>();

            await DohvatiPolja();
            DPdatumKreiranja.SelectedDate = DateTime.Now;
        }

        private void BtnRadniNalogDodajZaposlenika_Click(object sender, RoutedEventArgs e)
        {
            var dodajZaposlenikaView = new RadniNalogDodajZaposlenikaView();

            if (dodajZaposlenikaView.ShowDialog() == true)
            {
                PosloziExpanderZaZaposlenike();
            }
        }

        private void BtnRadniNalogDodajStavku_Click(object sender, RoutedEventArgs e)
        {
            var dodajStavkuView = new RadniNalogDodajStavkuView();
            if (dodajStavkuView.ShowDialog() == true)
            {
                PosloziExpanderZaStavke();
            }

        }

        private async Task DohvatiPolja()
        {
            try
            {
                var polja = await Client.GetAsync<List<Polje>>($"/api/Polje/Poduzece/{AppKontekst.prijavljeniKorisnik.Poduzece_id}");

                if (polja != null)
                {
                    CmbPolje.ItemsSource = polja;
                    CmbPolje.DisplayMemberPath = "Ime_polja";
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

                Button btnIzbrisi = new Button
                {
                    Content = "Izbriši",
                    Background = Brushes.Red,
                    Foreground = Brushes.White,
                    Margin = new Thickness(0, 5, 0, 0)
                };

                btnIzbrisi.Click += (s, e) =>
                {
                    AppKontekst.privremenaListaZaKorisnikeRadnogNaloga.Remove(zaposlenik);
                    PosloziExpanderZaZaposlenike();
                };

                stackPanel.Children.Add(new TextBlock { Text = $"OIB: {zaposlenik.Korisnik_OIB}" });
                stackPanel.Children.Add(new TextBlock { Text = $"Email: {zaposlenik.Email}" });
                stackPanel.Children.Add(new TextBlock { Text = $"Položene kategorije: {zaposlenik.Polozene_kategorije}" });
                stackPanel.Children.Add(new TextBlock { Text = $"Pozicija: {zaposlenik.Tip_korisnika.Pozicija}" });
                stackPanel.Children.Add(btnIzbrisi);

                expander.Content = stackPanel;
                StackPanelZaposlenici.Children.Add(expander);
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

                Button btnIzbrisi = new Button
                {
                    Content = "Izbriši",
                    Background = Brushes.Red,
                    Foreground = Brushes.White,
                    Margin = new Thickness(0, 5, 0, 0)
                };

                CheckBox chkZavrseno = new CheckBox
                {
                    Content = "Završeno",
                    IsEnabled = false,
                    Foreground = Brushes.Gray,
                    Margin = new Thickness(0, 5, 0, 0)
                };

                btnIzbrisi.Click += (s, e) =>
                {
                    AppKontekst.privremenaListaZaStavkeRadnogNaloga.Remove(stavka);
                    PosloziExpanderZaStavke();
                };

                stackPanel.Children.Add(new TextBlock { Text = $"Status: {stavka.Status}" });
                stackPanel.Children.Add(new TextBlock { Text = $"Razina prioriteta: {stavka.Razina_prioriteta}" });
                stackPanel.Children.Add(new TextBlock { Text = $"Napomena: {stavka.Napomena}" });
                stackPanel.Children.Add(chkZavrseno);
                stackPanel.Children.Add(btnIzbrisi);

                expander.Content = stackPanel;
                StackPanelStavkeRadnogNaloga.Children.Add(expander);
            }
        }

        private async void BtnSpremi_Click(object sender, RoutedEventArgs e)
        {
            StartState();
            var uspjesnoDodavanjeRadnogNaloga = await DodajRadniNalog();
            if (uspjesnoDodavanjeRadnogNaloga)
            {
                Close();
                EndState();
            }
            EndState();
        }

        private void BtnOdustani_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private async Task<bool> DodajRadniNalog()
        {
            var polje = CmbPolje.SelectedItem as Polje;

            if (string.IsNullOrEmpty(DPkrajnjiRok.Text)
                || string.IsNullOrEmpty(DPdatumKreiranja.Text)
                || string.IsNullOrEmpty(CmbPolje.Text)
                || polje == null
                || AppKontekst.privremenaListaZaStavkeRadnogNaloga == null || !AppKontekst.privremenaListaZaStavkeRadnogNaloga.Any()
                || AppKontekst.privremenaListaZaKorisnikeRadnogNaloga == null || !AppKontekst.privremenaListaZaKorisnikeRadnogNaloga.Any())
            {
                GreskaView greska = new GreskaView("Molimo ispunite sve potrebne podatke i dodajte barem jednu stavku i korisnika.");
                greska.ShowDialog();
                return false;
            }


            if (!DateTime.TryParse(DPdatumKreiranja.Text, out DateTime datumKreiranja)
                || !DateTime.TryParse(DPkrajnjiRok.Text, out DateTime krajnjiRok))
            {
                GreskaView greska = new GreskaView("Neispravan format datuma.");
                greska.ShowDialog();
                return false;
            }

            try
            {
                var radniNalog = new Radni_nalog
                {
                    Datum_kreiranja = datumKreiranja,
                    Zavrsni_rok = krajnjiRok,
                    Datum_zavrsetka = null,
                    Status = RbAktivan.IsChecked == true ? "Aktivan" : "Završen",
                    Polje_id = polje.Polje_id,
                    Korisnik = AppKontekst.privremenaListaZaKorisnikeRadnogNaloga,
                    Stavka_radnog_naloga = new List<Stavka_radnog_naloga>()
                };

                string podaci = JsonConvert.SerializeObject(radniNalog);
                var odgovor = await Client.PostAsync<bool>("/api/RadniNalog", podaci);

                if (odgovor == true)
                {
                    var radniNalogOdgovor = await Client.GetAsync<List<Radni_nalog>>($"/api/RadniNalog/Polje/{polje.Polje_id}");

                    if (radniNalogOdgovor != null)
                    {
                        var kreiraniRadniNalog = radniNalogOdgovor.Last();
                        int radniNalogId = kreiraniRadniNalog.Radni_nalog_id;
                        try
                        {
                            foreach (var stavka in AppKontekst.privremenaListaZaStavkeRadnogNaloga)
                            {
                                stavka.Radni_nalog_id = radniNalogId;
                                stavka.Radni_nalog = kreiraniRadniNalog;

                                string podaciStavke = JsonConvert.SerializeObject(stavka);
                                var odgovorStavke = await Client.PostAsync<bool>("/api/StavkaRadnogNaloga", podaciStavke);

                                if (odgovorStavke == false)
                                {
                                    GreskaView greska = new GreskaView("Greška prilikom spremanja stavki radnog naloga.");
                                    greska.ShowDialog();
                                    return false;
                                }
                            }

                            AppKontekst.privremenaListaZaStavkeRadnogNaloga?.Clear();
                            AppKontekst.privremenaListaZaKorisnikeRadnogNaloga?.Clear();
                            return true;
                        }
                        catch (Exception ex)
                        {
                            GreskaView greska = new GreskaView($"Greška pri spremanju stavki: {ex.Message}");
                            greska.ShowDialog();
                            return false;
                        }
                    }
                    else
                    {
                        GreskaView greska = new GreskaView("Greška prilikom dohvaćanja radnog naloga.");
                        greska.ShowDialog();
                        return false;
                    }
                }

                return false;
            }
            catch (Exception ex)
            {
                GreskaView greska = new GreskaView($"Greška pri spremanju: {ex.Message}");
                greska.ShowDialog();
                return false;
            }
        }

        private void StartState()
        {
            BtnOdustani.IsEnabled = false;
            BtnOdustani.Opacity = 0.5;
            BtnSpremi.IsEnabled = false;
            BtnSpremi.Opacity = 0.5;

        }
        private void EndState()
        {
            BtnOdustani.IsEnabled = true;
            BtnOdustani.Opacity = 1;
            BtnSpremi.IsEnabled = true;
            BtnSpremi.Opacity = 1;
        }
    }
}
