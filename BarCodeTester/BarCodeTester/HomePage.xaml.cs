using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BarCodeTester
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            InitializeComponent();
            NewOrder.Clicked += NewOrderBTN_Clicked;
            SavedLists.Clicked += SavedListsBTN_Clicked;
            PreviousOrders.Clicked += PreviousOrdersBTN_Clicked;
        }

        private void PreviousOrdersBTN_Clicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new ProductList());
        }

        private void SavedListsBTN_Clicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new ProductList());
        }

        private void NewOrderBTN_Clicked(object sender, EventArgs e)
        {

            Navigation.PushModalAsync(new MainOrderPage());
        }
    }
}