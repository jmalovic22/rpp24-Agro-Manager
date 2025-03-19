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
    /// Interaction logic for UcFarmaCard.xaml
    /// </summary>
    public partial class UcFarmaCard : UserControl
    {
        private RestClient Client = new RestClient();
        public Farma Farma { get; set; }
        public UcFarmaCard()
        {
            InitializeComponent();

            this.DataContextChanged += UcFarmaCard_DataContextChanged;

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

        public UcFarmaCard(Farma farma)
        {
            InitializeComponent();
            Farma = farma;
        }
        private void UcFarmaCard_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (DataContext is Farma farma)
            {
                Farma = farma;
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
                MessageBox.Show("Brisanje: " + Farma.Lokacija);
                var uspjesnoBrisanje = await Obrisi(Farma.Farma_id);
                if (uspjesnoBrisanje == true)
                {
                    Visibility = Visibility.Collapsed;
                }
            }
        }

        private async Task<bool> Obrisi(int farma_id)
        {
            try
            {
                var odgovor = await Client.DeleteAsync<bool>("/api/Farma/" + farma_id);
                return odgovor;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Greška prilikom učitavanja podataka: {ex.Message}");
                return false;
            }
        }

        private void UrediButton_Click(object sender, RoutedEventArgs e)
        {
            EditFarmaView editFarmaView = new EditFarmaView(Farma);
            editFarmaView.ShowDialog();
            Farma = editFarmaView.Farma;
            UserControl_Loaded(sender, e);
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            TxtLokacija.Content = Farma.Lokacija;
            TxtPovrsina.Content = Farma.Povrsina;
            TxtBrojZaposlenih.Content = Farma.Broj_zaposlenih;
            TxtStatusFarme.Content = Farma.Status;
            if ((string)TxtStatusFarme.Content == "Aktivna")
            {
                TxtStatusFarme.Foreground = Brushes.Green;
            }
            else if ((string)TxtStatusFarme.Content == "Neaktivna")
            {
                TxtStatusFarme.Foreground = Brushes.Red;
            }
        }

        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            FarmaDokumentView farmaDokumentView = new FarmaDokumentView(Farma);
            farmaDokumentView.ShowDialog();
        }
    }
}
