using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BarCodeTester
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DeliveryAddressListPage : ContentPage
    {
        public event DeliveryAddressListPageDelegate OnAddressSelected;
        public delegate void DeliveryAddressListPageDelegate(DbAddress result);

        public ICommand SelectedAddressCommand { private set; get; }
        public ICommand DeleteAddressCommand { private set; get; }

        public DeliveryAddressListPage()
        {
            InitializeComponent();

            SelectedAddressCommand = new Command<DbAddress>(async (DbAddress address) =>
            {
                OnAddressSelected?.Invoke(address);
                await Navigation.PopModalAsync();
            });

            DeleteAddressCommand = new Command<DbAddress>(async (DbAddress address) =>
            {
                await App.Database.DeleteAddressAsync(address);
                await RefreshPageAsync();
            });
        }

        public static async Task<DeliveryAddressListPage> CreateDeliveryAddressListPageAsync()
        {
            DeliveryAddressListPage page = new DeliveryAddressListPage();

            await page.RefreshPageAsync();

            return page;

        }

        private async Task RefreshPageAsync()
        {
            MainStack.Children.Clear();
            List<DbAddress> addresses = await App.Database.GetAddressesAsync();

            foreach (DbAddress address in addresses)
            {
                StackLayout layout = new StackLayout();
                layout.Orientation = StackOrientation.Horizontal;

                string fullAddress = address.Address + " - " + address.ZipCode;

                Button btnAddress = new Button();
                btnAddress.Text = fullAddress;
                btnAddress.Command = SelectedAddressCommand;
                btnAddress.CommandParameter = address;

                Button btnDelete = new Button();
                btnDelete.Text = "X";
                btnDelete.Command = DeleteAddressCommand;
                btnDelete.CommandParameter = address;

                layout.Children.Add(btnAddress);
                layout.Children.Add(btnDelete);

                MainStack.Children.Add(layout);
            }
        }
    }
}