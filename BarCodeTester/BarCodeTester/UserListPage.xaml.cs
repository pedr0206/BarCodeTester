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
    public partial class UserListPage : ContentPage
    {
        public event UserListPageDelegate OnUserSelected;
        public delegate void UserListPageDelegate(DbUser result);

        public UserListPage()
        {
            InitializeComponent();
            //AddUserBtn.Clicked += AddUserBtn_Clicked;
        }

        //private async void AddUserBtn_Clicked(object sender, EventArgs e)
        //{
        //    //Abrir nova pagina de inserção
        //    await RefreshPageAsync();
        //}

        public static async Task<UserListPage> CreateUserListPageAsync()
        {
            UserListPage page = new UserListPage();

            await page.RefreshPageAsync();

            return page;

        }

        private async Task RefreshPageAsync()
        {
            MainStack.Children.Clear();
            List<DbUser> users = await App.Database.GetUsersAsync();

            foreach (DbUser user in users)
            {
                StackLayout layout = new StackLayout();
                layout.Orientation = StackOrientation.Horizontal;

                Button btnUser = new Button() { Text = user.Name + " - " + user.PhoneNumber };
                btnUser.Clicked += async (sender, e) =>
                {
                    OnUserSelected?.Invoke(user);
                    await Navigation.PopModalAsync();
                };
                
                Button btnDelete = new Button();
                btnDelete.Text = "X";
                btnDelete.Clicked += async (sender, e) =>
                {
                    await App.Database.DeleteUserAsync(user);
                    await RefreshPageAsync();
                    //Dar refresh a lista
                    //Navigation.
                };

                layout.Children.Add(btnUser);
                layout.Children.Add(btnDelete);

                MainStack.Children.Add(layout);
            }
        }
    }
}