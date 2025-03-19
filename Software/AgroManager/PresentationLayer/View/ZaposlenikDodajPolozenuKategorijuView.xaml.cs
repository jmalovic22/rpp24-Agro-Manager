using EntitiesLayer.Entiteti;
using EntitiesLayer.Enumeracije;
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
    /// Interaction logic for ZaposlenikDodajPolozenuKategorijuView.xaml
    /// </summary>
    public partial class ZaposlenikDodajPolozenuKategorijuView : Window
    {
        public EnumPolozeneKategorije? OdabranaKategorija { get; private set; }

        public ZaposlenikDodajPolozenuKategorijuView()
        {
            InitializeComponent();
        }

        private void BtnOdustani_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void BtnSpremi_Click(object sender, RoutedEventArgs e)
        {
            if (CmbKategorija.SelectedItem is EnumPolozeneKategorije odabrana)
            {
                OdabranaKategorija = odabrana;
                DialogResult = true;
                Close();
            }
            else
            {
                GreskaView greska = new GreskaView("Molimo odaberite kategoriju.");
                greska.ShowDialog();
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            PopuniComboBoxove();
        }

        private void PopuniComboBoxove()
        {
            try
            {
                CmbKategorija.ItemsSource = Enum.GetValues(typeof(EnumPolozeneKategorije)).Cast<EnumPolozeneKategorije>().ToList();
            }
            catch (Exception ex)
            {
                GreskaView greska = new GreskaView($"Greška prilikom učitavanja podataka: {ex.Message}");
                greska.ShowDialog();
            }
        }
    }
}
