using _100_Life_Wishes.Models;
using _100_Life_Wishes.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace _100_Life_Wishes.Views
{
    public partial class NewItemPage : ContentPage
    {
        public TaskItem Item { get; set; }

        public NewItemPage()
        {
            InitializeComponent();
            BindingContext = new NewItemViewModel();
        }
    }
}