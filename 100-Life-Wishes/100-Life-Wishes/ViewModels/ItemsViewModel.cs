using _100_Life_Wishes.Models;
using _100_Life_Wishes.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace _100_Life_Wishes.ViewModels
{
    public class ItemsViewModel : BaseViewModel
    {
        private Item _selectedItem;

        public ObservableCollection<Item> Items { get; }
        public Command LoadItemsCommand { get; }
        public Command AddItemCommand { get; }
        public Command SortItemsCommand { get; }
        public Command<Item> ItemTapped { get; }

        public ItemsViewModel()
        {
            Title = "Tasks";
            Items = new ObservableCollection<Item>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
            ItemTapped = new Command<Item>(OnItemSelected);
            AddItemCommand = new Command(OnAddItem);
            SortItemsCommand = new Command(OnSortItems);
        }

        async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;
            try
            {
                Items.Clear();
                var items = await DataStore.GetItemsAsync(true);
                foreach (var item in items)
                {
                    Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        public void OnAppearing()
        {
            IsBusy = true;
            SelectedItem = null;
        }

        public Item SelectedItem
        {
            get => _selectedItem;
            set
            {
                SetProperty(ref _selectedItem, value);
                OnItemSelected(value);
            }
        }

        private async void OnSortItems()
        {
            string action = await Application.Current.MainPage.DisplayActionSheet("Сортировать по:", "Отмена", null, "Progress", "Text", "Importance");
            var newItems = new ObservableCollection<Item>();
            switch (action)
            {
                case "Progress":
                    newItems = new ObservableCollection<Item>(Items.OrderBy(x => x.Progress).Reverse());
                    break;
                case "Text":
                    newItems = new ObservableCollection<Item>(Items.OrderBy(x => x.Text));
                    break;
                case "Importance":
                    newItems = new ObservableCollection<Item>(Items.OrderBy(x => x.Importance).Reverse());
                    break;
                default:
                    newItems = Items;
                    break;
            }
            foreach (var item in newItems)
            {
                await DataStore.UpdateItemAsync(item);
            }
            await ExecuteLoadItemsCommand();
        }

        private async void OnAddItem(object obj)
        {
            await Shell.Current.GoToAsync(nameof(NewItemPage));
        }

        async void OnItemSelected(Item item)
        {
            if (item == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            await Shell.Current.GoToAsync($"{nameof(ItemDetailPage)}?{nameof(ItemDetailViewModel.ItemId)}={item.Id}");
        }
    }
}