using EntitiesLayer;
using EntitiesLayer.Entiteti;
using EntitiesLayer.Models;
using EntitiesLayer.Utilities;
using Newtonsoft.Json;
using QRCoder;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
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
using System.Drawing;
using System.Drawing.Imaging;


namespace PresentationLayer.View
{
    /// <summary>
    /// Interaction logic for LoginView.xaml
    /// </summary>
    public partial class LoginView : Window
    {
        private RestClient client = new RestClient();

        public LoginView()
        {
            InitializeComponent();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private async void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            btnLogin.IsEnabled = false;
            if (btnLogin.Content.ToString() == "LOGIN")
            {
                await Prijavikorisnika();
            } else
            {
                string otp = TOTP.GetTOTP(TOTP.DecodeBase32(AppKontekst.prijavljeniKorisnik.Secret).ToArray(), 6, 30);
                if (otp == txtOTPkod.Text)
                {
                    AppKontekst.mainWindow = new MainWindow();
                    AppKontekst.mainWindow.Show();
                    this.Close();
                }
                else
                {
                    btnLogin.IsEnabled = true;
                    GreskaView greska = new GreskaView($"Pogrešan OTP kod!");
                    greska.ShowDialog();
                    txtOTPkod.Text = "";
                }
            }
        }

        private async Task Prijavikorisnika()
        {
            //var txtKorisnickoIme = (TextBox)FindName("txtKorisnickoIme");
            //var txtLozinka = (PasswordBox)FindName("txtLozinka");

            KorisnikPrijava prijava = new KorisnikPrijava
            {
                Korisnicko_ime = txtKorisnickoIme.Text,
                Lozinka = txtLozinka.Password
            };

            try
            {
                string data = JsonConvert.SerializeObject(prijava);
                var korisnikApp = await client.PostAsync<Korisnik>("/api/Korisnik/prijava", data);
                if (korisnikApp != null)
                {
                    AppKontekst.prijavljeniKorisnik = korisnikApp;
                    //lblPoruka.Text = "Uspješno ste se prijavili " + AppContext.Korisnik.Ime + " " + AppContext.Korisnik.Prezime + "  Prijavljeni ste u:" + AppContext.Korisnik.Poduzece.Naziv;
                    AppKontekst.korisnici = await client.GetAsync<List<Korisnik>>("/api/Korisnik/Poduzece/" + AppKontekst.prijavljeniKorisnik.Poduzece_id);
                    AppKontekst.uzgojnaKulture = await client.GetAsync<List<Uzgojna_kultura>>("/api/UzgojnaKultura");
                    AppKontekst.vrstePrikljucaka = await client.GetAsync<List<Vrsta_prikljucka>>("/api/VrstaPrikljucka");
                    AppKontekst.vrsteStoke = await client.GetAsync<List<Vrsta_stoke>>("/api/VrstaStoke");

                    //this.scBase32 = TOTP.EncodeBase32(TOTP.GenerateKey(32));
                   
                    string totpURI = TOTP.GetTOTPUri("AgroManager", AppKontekst.prijavljeniKorisnik.Korisnicko_ime, AppKontekst.prijavljeniKorisnik.Secret, 6, 30);

                    // Generate QR code image.
                    using (QRCodeGenerator qrGenerator = new QRCodeGenerator())
                    {
                        using (QRCodeData qrCodeData = qrGenerator.CreateQrCode(totpURI, QRCodeGenerator.ECCLevel.Q))
                        {
                            using (QRCode qrCode = new QRCode(qrCodeData))
                            {
                                using (Bitmap qrCodeImage = qrCode.GetGraphic(20))
                                {
                                    // Convert QR code image to byte array.
                                    using (MemoryStream stream = new MemoryStream())
                                    {
                                        qrCodeImage.Save(stream, ImageFormat.Png);
                                        stream.Position = 0;

                                        BitmapImage bitmapImage = new BitmapImage();
                                        bitmapImage.BeginInit();
                                        bitmapImage.StreamSource = stream;
                                        bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                                        bitmapImage.EndInit();
                                        bitmapImage.Freeze();

                                        // Display QR code.
                                        imgLogoQR.Source = bitmapImage;
                                    }
                                }
                            }
                        }
                    }

                    lblOTPkod.Visibility = Visibility.Visible;
                    txtOTPkod.Visibility = Visibility.Visible;

                    btnLogin.Content = "OTP provjera";
                    btnLogin.Width += 20; 
                    btnLogin.IsEnabled = true;
                }
                else
                {
                    btnLogin.IsEnabled = true;
                    GreskaView greska = new GreskaView($"Neispravno korisničko ime ili lozinka!");
                    greska.ShowDialog();
                    txtKorisnickoIme.Text = "";
                    txtLozinka.Password = "";
                }
            }
            catch (Exception ex)
            {
                btnLogin.IsEnabled = true;
                GreskaView greska = new GreskaView($"Greška: {ex.Message}");
                greska.ShowDialog();
                txtKorisnickoIme.Text = "";
                txtLozinka.Password = "";
            }
        }
    }
}
