using System;
using System.Collections.Generic;
using System.Linq;
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
            Navigation.PushModalAsync(new MainPage());
        }

        private void SavedListsBTN_Clicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new MainPage());
        }

        private void NewOrderBTN_Clicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new UserPage());
        }
    }
}