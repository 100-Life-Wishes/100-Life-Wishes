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
                var grid = button.Parent as Grid;
                if (grid != null)
                    if (grid.BackgroundColor != Color.LightGreen)
                        grid.BackgroundColor = Color.LightGreen;
                    else
                        grid.BackgroundColor = Color.White;
            }
        }
    }
}