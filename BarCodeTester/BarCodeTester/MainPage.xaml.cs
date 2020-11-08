using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using ZXing;
using ZXing.Net.Mobile.Forms;

namespace BarCodeTester
{
    public partial class MainPage : ContentPage
    {
        ZXingScannerPage scanPage;
        public MainPage()
        {
            InitializeComponent();
            ButtonScanDefault.Clicked += ButtonScanDefault_Clicked;
            ButtonListOfProducts.Clicked += ButtonListOfProducts_Clicked;
        }

        private void ButtonListOfProducts_Clicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new ProductList());
        }

        private async void ButtonScanDefault_Clicked(object sender, EventArgs e)
        {
            scanPage = new ZXingScannerPage();
            scanPage.OnScanResult += ScanResultHandler;

            await Navigation.PushModalAsync(scanPage);
        }

        private void ScanResultHandler(Result result)
        {
            scanPage.IsScanning = false;
            //Doing something with the result
            Device.BeginInvokeOnMainThread(async () =>
            {
                await Navigation.PopModalAsync();
                string clearRst = ClearResultText(result.Text);

                ProductRepository.Instance.AddProduct(clearRst);
                await DisplayAlert("Scanned barcode and added to product list - ", clearRst, "OK");
            });
        }

        private string ClearResultText(string rstText)
        {
            //ir pela string caracter a caracter, e eliminar tudo o que não seja digitos.
            //char.IsDigit('c');
            //char[] caracteres = rstText.ToCharArray();
            return rstText;
        }
    }
}
