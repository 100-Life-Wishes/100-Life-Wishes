using _100_Life_Wishes.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace _100_Life_Wishes.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}