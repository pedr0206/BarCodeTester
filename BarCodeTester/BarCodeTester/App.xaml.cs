using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BarCodeTester
{
    public partial class App : Application
    {
        static HymatikDatabase database;
        public static HymatikDatabase Database
        {
            get
            {
                if (database == null)
                {
                    database = new HymatikDatabase();
                }
                return database;
            }
        }

        public App()
        {
            InitializeComponent();

            MainPage = new HomePage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
