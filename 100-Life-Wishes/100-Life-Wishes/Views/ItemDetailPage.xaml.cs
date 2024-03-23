using _100_Life_Wishes.Models;
using _100_Life_Wishes.ViewModels;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Xamarin.Forms;

namespace _100_Life_Wishes.Views
{
 
    public partial class ItemDetailPage : ContentPage
    {
        public Item Item { get; set; }
        public ObservableCollection<Subtask> Subtasks { get; set; }

        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
        private void OnButtonClicked(object sender, EventArgs e)
        {
            if (sender is Button button)
            {
                var stackLayout = button.Parent as StackLayout;
                if (stackLayout != null)
                    if (stackLayout.BackgroundColor != Color.LightGreen)
                        stackLayout.BackgroundColor = Color.LightGreen;
                    else
                        stackLayout.BackgroundColor = Color.White;
            }
        }
    }
}