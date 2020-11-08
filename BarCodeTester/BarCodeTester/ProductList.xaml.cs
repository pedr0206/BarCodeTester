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
    public partial class ProductList : ContentPage
    {
        public ProductList()
        {
            InitializeComponent();
            RefreshPage();
            
        }

        private void RefreshPage()
        {
            MainStack.Children.Clear();
            List<string> products = ProductRepository.Instance.GetAllProducts();

            foreach (string product in products)
            {
                StackLayout layout = new StackLayout();
                layout.Orientation = StackOrientation.Horizontal;

                Label lbl = new Label() { Text = product };
                Button btn = new Button();
                btn.Text = "X";
                btn.Clicked += (sender, e) =>
                {
                    ProductRepository.Instance.RemoveProduct(product);
                    RefreshPage();
                    //Dar refresh a lista
                    //Navigation.
                };

                layout.Children.Add(lbl);
                layout.Children.Add(btn);

                MainStack.Children.Add(layout);
            }
        }
    }
}