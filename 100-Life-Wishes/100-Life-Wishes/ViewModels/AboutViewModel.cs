using _100_Life_Wishes.Views;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace _100_Life_Wishes.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        public AboutViewModel()
        {
            Title = "О приложении";
            OpenWebCommand = new Command(async () => await Browser.OpenAsync("https://aka.ms/xamarin-quickstart"));
            GoTo = new Command(async () => await Shell.Current.GoToAsync($"//{nameof(ItemsPage)}"));
        }

        public ICommand OpenWebCommand { get; }
        public ICommand GoTo { get; }
    }
}