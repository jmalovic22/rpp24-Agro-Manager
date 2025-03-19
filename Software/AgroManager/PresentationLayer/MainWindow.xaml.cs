using EntitiesLayer;
using PresentationLayer.UserControls;
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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PresentationLayer
{
    public partial class MainWindow : Window
    {
        private List<Button> buttonList;
        private Dictionary<Button, Image> buttonNavSelectedBackgroundMap;

        public MainWindow()
        {
            InitializeComponent();
            buttonList = new List<Button> 
            { 
                btnHome, 
                btnFarme, 
                btnSilosi,
                btnPolja,
                btnVozila,
                btnPrikljucci,
                btnRadniNalozi,
                BtnEvidencija,
                BtnZaposlenici,
                BtnPoslovi
            };

            buttonNavSelectedBackgroundMap = new Dictionary<Button, Image>
            {
                { btnHome, imgNavSelectedHome },
                { btnFarme, imgNavSelectedFarme },
                { btnSilosi, imgNavSelectedSilosi },
                { btnPolja, imgNavSelectedPolja },
                { btnVozila, imgNavSelectedVozila },
                { btnPrikljucci, imgNavSelectedPrikljucci },
                { btnRadniNalozi, imgNavSelectedRadniNalozi },
                { BtnEvidencija, imgNavSelectedEvidencija },
                { BtnZaposlenici, imgNavSelectedZaposlenici },
                { BtnPoslovi, imgNavSelectedPoslovi }
            };

            LblKorisnickoIme.Content = AppKontekst.prijavljeniKorisnik.Korisnicko_ime;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            mainControl.Content = new UserControls.UcHome();
            if (AppKontekst.prijavljeniKorisnik.Tip_korisnika.Razina_prava == 1)
            {
                BtnZaposlenici.Visibility = Visibility.Collapsed;
                BtnPoslovi.Visibility = Visibility.Collapsed;
                ImgPoslovi.Visibility = Visibility.Collapsed;
                ImgZaposlenici.Visibility = Visibility.Collapsed;
                ImgWhiteLineAdmin.Visibility = Visibility.Collapsed;
                LblAdmin.Visibility = Visibility.Collapsed;
            }
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            navImage.Height = this.ActualHeight;
            double marginValue = this.ActualHeight * (-0.165);
            navImage.Margin = new Thickness(marginValue, 0, 0, 0);
        }

        private void ActiveButton(Button button)
        {
            button.FontWeight = FontWeights.Bold;
            buttonNavSelectedBackgroundMap[button].Visibility = Visibility.Visible;

            var storyboard = (Storyboard)FindResource("btnNavBackgroundAnimation");
            storyboard.Begin(buttonNavSelectedBackgroundMap[button]);
        }

        private void InactiveButton(Button button)
        {
            button.FontWeight = FontWeights.Normal;
            buttonNavSelectedBackgroundMap[button].Visibility = Visibility.Hidden;
        }

        private void SetButtonStates(Button activeButton)
        {
            foreach (var button in buttonList)
            {
                if (button == activeButton)
                {
                    ActiveButton(button);
                }
                else
                {
                    InactiveButton(button);
                }
            }
        }

        private void btnHome_Click(object sender, RoutedEventArgs e)
        {
            SetButtonStates(btnHome);
            mainControl.Content = new UcHome();
        }

        private void btnFarme_Click(object sender, RoutedEventArgs e)
        {
            SetButtonStates(btnFarme);
            mainControl.Content = new UcFarma();
        }

        private void btnSilosi_Click(object sender, RoutedEventArgs e)
        {
            SetButtonStates(btnSilosi);
            mainControl.Content = new UcSilos();
        }

        private void btnPolja_Click(object sender, RoutedEventArgs e)
        {
            SetButtonStates(btnPolja);
        }

        public void btnVozila_Click(object sender, RoutedEventArgs e)
        {
            SetButtonStates(btnVozila);
            mainControl.Content = new UcVozilo();
        }

        public void btnPrikljucci_Click(object sender, RoutedEventArgs e)
        {
            SetButtonStates(btnPrikljucci);
            mainControl.Content = new UcPrikljucak();
        }

        private void btnRadniNalozi_Click(object sender, RoutedEventArgs e)
        {
            SetButtonStates(btnRadniNalozi);
            mainControl.Content = new UcRadniNalog();
        }

        private void btnOdjava_Click(object sender, RoutedEventArgs e)
        {
            LoginView loginView = new LoginView();
            loginView.Show();
            this.Close();
        }

        private void BtnEvidencija_Click(object sender, RoutedEventArgs e)
        {
            SetButtonStates(BtnEvidencija);
            mainControl.Content = new UcEvidencijeStokeFarma();
        }

        private void BtnZaposlenici_Click(object sender, RoutedEventArgs e)
        {
            SetButtonStates(BtnZaposlenici);
            mainControl.Content = new UcZaposlenik();
        }

        private void BtnPoslovi_Click(object sender, RoutedEventArgs e)
        {
            SetButtonStates(BtnPoslovi);
            mainControl.Content = new UcPosao();
        }
    }
}
