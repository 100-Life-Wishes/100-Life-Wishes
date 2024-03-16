using _100_Life_Wishes.Models;
using _100_Life_Wishes.ViewModels;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Xamarin.Forms;

namespace _100_Life_Wishes.Views
{
    public class Subtask
    {
        public string Name { get; set; }
        public string Detail { get; set; }
    }

    public partial class ItemDetailPage : ContentPage
    {
        public Item Item { get; set; }
        public ObservableCollection<Subtask> Subtasks { get; set; }

        public ItemDetailPage()
        {
            InitializeComponent();

            Subtasks = new ObservableCollection<Subtask>
        {
            new Subtask { Name = "Подзадача 1", Detail = "Детали подзадачи 1" },
            new Subtask { Name = "Подзадача 2", Detail = "Детали подзадачи 2" },
            // Добавьте здесь больше подзадач...
        };

            SubtasksListView.ItemsSource = Subtasks;
            BindingContext = new ItemDetailViewModel();
        }
    }
}