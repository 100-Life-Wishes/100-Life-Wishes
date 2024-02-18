using _100_Life_Wishes.Services;
using _100_Life_Wishes.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace _100_Life_Wishes
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            DependencyService.Register<MockDataStore>();
            MainPage = new AppShell();
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
