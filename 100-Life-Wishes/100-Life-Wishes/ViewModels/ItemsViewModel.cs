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
        private TaskItem _selectedItem;

        public ObservableCollection<TaskItem> Items { get; }
        public Command LoadItemsCommand { get; }
        public Command AddItemCommand { get; }
        public Command SortItemsCommand { get; }
        public Command<TaskItem> ItemTapped { get; }

        public ItemsViewModel()
        {
            Title = "Задачи";
            Items = new ObservableCollection<TaskItem>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
            ItemTapped = new Command<TaskItem>(OnItemSelected);
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

        public TaskItem SelectedItem
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
            string action = await Application.Current.MainPage.DisplayActionSheet("Сортировать по:", "Отмена", null, "Прогресс", "Название", "Приоритет");
            var newItems = new ObservableCollection<TaskItem>();
            switch (action)
            {
                case "Прогресс":
                    newItems = new ObservableCollection<TaskItem>(Items.OrderBy(x => x.Progress).Reverse());
                    break;
                case "Название":
                    newItems = new ObservableCollection<TaskItem>(Items.OrderBy(x => x.Text));
                    break;
                case "Приоритет":
                    newItems = new ObservableCollection<TaskItem>(Items.OrderBy(x => x.Importance).Reverse());
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

        async void OnItemSelected(TaskItem item)
        {
            if (item == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            await Shell.Current.GoToAsync($"{nameof(ItemDetailPage)}?{nameof(ItemDetailViewModel.ItemId)}={item.Id}");
        }
    }
}