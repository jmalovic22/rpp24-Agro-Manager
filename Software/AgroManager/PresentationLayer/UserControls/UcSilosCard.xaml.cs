using EntitiesLayer.Entiteti;
using EntitiesLayer.Models;
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
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PresentationLayer.UserControls
{
    /// <summary>
    /// Interaction logic for UcSilosCard.xaml
    /// </summary>
    public partial class UcSilosCard : UserControl
    {
        private RestClient Client = new RestClient();
        public Silos Silos { get; set; }


        public UcSilosCard()
        {
            InitializeComponent();
            this.DataContextChanged += UcSilosCard_DataContextChanged;

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

        public UcSilosCard(Silos silos)
        {
            InitializeComponent();
            Silos = silos;
        }

        private void UcSilosCard_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (DataContext is Silos silos)
            {
                Silos = silos;
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
                var uspjesnoBrisanje = await Obrisi(Silos.Silos_id);
                if (uspjesnoBrisanje == true)
                {
                    Visibility = Visibility.Collapsed;
                }
            }
        }

        private void UrediButton_Click(object sender, RoutedEventArgs e)
        {
            EditSilosView editSilosView = new EditSilosView(Silos);
            editSilosView.ShowDialog();
            Silos.Silos_id = editSilosView.Silos.Silos_id;
            Silos.Kapacitet = editSilosView.Silos.Kapacitet;
            Silos.Dostupnost = editSilosView.Silos.Dostupnost;
            Silos.Popunjenost = editSilosView.Silos.Popunjenost;
            Silos.Uzgojna_kultura_id = editSilosView.Silos.Uzgojna_kultura_id;
            Silos.Farma_id = editSilosView.Silos.Farma_id;
            UserControl_Loaded(sender, e);
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            double popunjenost = 0;
            if (Silos.Kapacitet > 0)
            {
                popunjenost = ((double)Silos.Popunjenost / Silos.Kapacitet) * 100;
            }

            KapacitetBorder.Height = popunjenost;
            TxtPopunjenost.Content = popunjenost.ToString("F2") + "%";
            TxtUzgojnaKultura.Content = Silos.Uzgojna_kultura.Naziv;
            TxtFarma.Content = Silos.Farma.Lokacija;

            if(Silos.Dostupnost == 1)
            {
                TxtDostupnost.Foreground = Brushes.Green;
                TxtDostupnost.Content = "Dostupan";
            }
            else
            {
                TxtDostupnost.Foreground = Brushes.Red;
                TxtDostupnost.Content = "Nedostupan";
            }
        }

        private async Task<bool> Obrisi(int id)
        {
            try
            {
                var odgovor = await Client.DeleteAsync<bool>("/api/Silos/" + id);
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