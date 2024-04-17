using _100_Life_Wishes.Models;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using Xamarin.Forms;

namespace _100_Life_Wishes.ViewModels
{
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    public class ItemDetailViewModel : BaseViewModel
    {
        private string itemId;
        private string text;
        private string description;
        private ObservableCollection<SubtaskViewModel> subtasks;
        private string importance;
        private DateTime? deadline;

        public ObservableCollection<SubtaskViewModel> Subtasks
        {
            get => subtasks;
            set => SetProperty(ref subtasks, value);
        }

        public string Id { get; private set; }

        public bool IsDeadlinePickerVisible => Deadline.HasValue;
        public string DeadlineButtonLabel => Deadline.HasValue ? "Remove Deadline" : "Add Deadline";


        public ItemDetailViewModel()
        {
            Subtasks = new ObservableCollection<SubtaskViewModel>();
            DeleteCommand = new Command(OnDelete);
            UpdateCommand = new Command(OnUpdate);
            AddCommand = new Command(OnAdd);
            SetHighImportance = new Command(OnHighImportance);
            SetStandardImportance = new Command(OnStandardImportance);
            ToggleDeadlineCommand = new Command(ExecuteToggleDeadline);
            MessagingCenter.Subscribe<SubtaskViewModel, SubtaskViewModel>(this, "DeleteSubtask", (sender, arg) =>
            {
                Subtasks.Remove(arg);
            });
        }

        public string ItemId
        {
            get => itemId;
            set
            {
                itemId = value;
                LoadItemId(value);
            }
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


        public string Importance
        {
            get => importance;
            set => SetProperty(ref importance, value);
        }

        public DateTime? Deadline
        {
            get => deadline;
            set
            {
                SetProperty(ref deadline, value);
                OnPropertyChanged(nameof(DeadlineButtonLabel));
            }
        }


        // Commands
        public Command DeleteCommand { get; }
        public Command UpdateCommand { get; }
        public Command AddCommand { get; }
        public Command SetHighImportance { get; }
        public Command SetStandardImportance { get; }
        public Command ToggleDeadlineCommand { get; }

        private Item UpdateItem()
        {
            return new Item
            {
                Id = Id,
                Text = Text,
                Description = Description,
                Subtasks = Subtasks,
                Importance = Importance,
                Deadline = Deadline
            };
        }

        private void ExecuteToggleDeadline()
        {
            if (Deadline.HasValue)
            {
                Deadline = null;
            }
            else
            {
                Deadline = DateTime.Now.AddDays(7);
            }
            OnPropertyChanged(nameof(IsDeadlinePickerVisible));
        }

        private async void OnDelete()
        {
            // This will pop the current page off the navigation stack
            await DataStore.DeleteItemAsync(itemId);
            await Shell.Current.GoToAsync("..");
        }
        private async void OnUpdate()
        {
            for (var i = Subtasks.Count - 1; i >= 0; i--)
            {
                if (string.IsNullOrEmpty(Subtasks[i].Name))
                {
                    Subtasks.RemoveAt(i);
                }
            }
            var updatedItem = UpdateItem();
            await DataStore.UpdateItemAsync(updatedItem);
            await Shell.Current.GoToAsync("..");
        }

        private void OnAdd()
        {
            var newSubtaskViewModel = new SubtaskViewModel()
            {
                Name = "New Subtask",
                SubtaskColor = "#FFFFFF"
            };
            Subtasks.Add(newSubtaskViewModel);
        }

        private void OnHighImportance()
        {
            Importance = "#F08080";
        }
        private void OnStandardImportance()
        {
            Importance = "#FFFFFF";
        }

        public async void LoadItemId(string itemId)
        {
            try
            {
                var item = await DataStore.GetItemAsync(itemId);
                Id = item.Id;
                Text = item.Text;
                Description = item.Description;
                Importance = item.Importance;
                Subtasks = item.Subtasks ?? new ObservableCollection<SubtaskViewModel>();

                Deadline = item.Deadline;
                OnPropertyChanged(nameof(IsDeadlinePickerVisible));
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
            }
        }
    }
}
