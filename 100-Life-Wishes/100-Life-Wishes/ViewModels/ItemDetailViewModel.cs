using _100_Life_Wishes.Models;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace _100_Life_Wishes.ViewModels
{
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    public class ItemDetailViewModel : BaseViewModel
    {
        private string itemId;
        private string text;
        private string description;
        private ObservableCollection<Subtask> subtasks;
        private Color importance;

        public string Id { get; set; }

        public ItemDetailViewModel()
        {
            Subtasks = new ObservableCollection<Subtask>();
            DeleteCommand = new Command(OnDelete);
            UpdateCommand = new Command(OnUpdate);
            AddCommand = new Command(OnAdd);
            SetHighImportance = new Command(OnHighImportance);
            SetStandardImportance = new Command(OnStandardImportance);
        }
        public string Text
        {
            get => text;
            set => SetProperty(ref text, value);
        }

        public string Description
        {
            get => description;
            set => SetProperty(ref description, value);
        }

        public ObservableCollection<Subtask> Subtasks
        {
            get => subtasks;
            set => SetProperty(ref subtasks, value);
        }

        public Color Importance
        {
            get => importance;
            set => SetProperty(ref importance, value);
        }

        public Command DeleteCommand { get; }
        public Command UpdateCommand { get; }
        public Command AddCommand { get; }
        public Command SetHighImportance { get; }
        public Command SetStandardImportance { get; }
        private async void OnDelete()
        {
            // This will pop the current page off the navigation stack
            await DataStore.DeleteItemAsync(itemId);
            await Shell.Current.GoToAsync("..");
        }
        private async void OnUpdate()
        {
            Item updatedItem = new Item()
            {
                Id = this.Id, // Use the existing Id
                Text = this.Text, // Use the Text property
                Description = this.Description, // Use the Description property
                Subtasks = this.Subtasks, // Use the Subtasks property
                Importance = this.Importance
            };

            try
            {
                await DataStore.UpdateItemAsync(updatedItem);
                Debug.WriteLine("Item Updated Successfully");
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Update Item");
            }

            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }

        private async void OnAdd()
        {
            // Create a new subtask
            Subtask newSubtask = new Subtask()
            {
                Name = "New Subtask",
            };

            // Add the new subtask to the collection
            Subtasks.Add(newSubtask);

            // Update the item with the new subtask list
            Item updatedItem = new Item()
            {
                Id = this.Id, // Use the existing Id
                Text = this.Text, // Use the Text property
                Description = this.Description, // Use the Description property
                Subtasks = this.Subtasks, // Use the updated Subtasks property
                Importance = this.Importance
            };

            try
            {
                // Save the updated item to the data store
                await DataStore.UpdateItemAsync(updatedItem);
                Debug.WriteLine("Subtask Added Successfully");
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Add Subtask");
            }
        }

        private async void OnHighImportance()
        {
            Importance = Color.Red;
            Item updatedItem = new Item()
            {
                Id = this.Id, // Use the existing Id
                Text = this.Text, // Use the Text property
                Description = this.Description, // Use the Description property
                Subtasks = this.Subtasks, // Use the updated Subtasks property
                Importance = this.Importance
            };
            try
            {
                // Save the updated item to the data store
                await DataStore.UpdateItemAsync(updatedItem);
                Debug.WriteLine("Subtask Added Successfully");
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Add Subtask");
            }
        }
        private async void OnStandardImportance()
        {
            Importance = Color.White;
            Item updatedItem = new Item()
            {
                Id = this.Id, // Use the existing Id
                Text = this.Text, // Use the Text property
                Description = this.Description, // Use the Description property
                Subtasks = this.Subtasks, // Use the updated Subtasks property
                Importance = this.Importance
            };
            try
            {
                // Save the updated item to the data store
                await DataStore.UpdateItemAsync(updatedItem);
                Debug.WriteLine("Subtask Added Successfully");
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Add Subtask");
            }
        }
        public string ItemId
        {
            get
            {
                return itemId;
            }
            set
            {
                itemId = value;
                LoadItemId(value);
            }
        }

        public async void LoadItemId(string itemId)
        {
            try
            {
                var item = await DataStore.GetItemAsync(itemId);
                Id = item.Id;
                Text = item.Text;
                Description = item.Description;
                Subtasks = item.Subtasks ?? new ObservableCollection<Subtask>();
                Importance = item.Importance;
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
            }
        }
    }
}
