using _100_Life_Wishes.Models;
using _100_Life_Wishes.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace _100_Life_Wishes.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public Item Item { get; set; }
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}