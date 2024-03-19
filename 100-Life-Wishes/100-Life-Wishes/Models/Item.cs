using _100_Life_Wishes.ViewModels;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace _100_Life_Wishes.Models
{
    public class Subtask
    {
        public string Name { get; set; }
    }

    public class Item
    {
        public string Id { get; set; }
        public string Text { get; set; }
        public string Description { get; set; }
        public ObservableCollection<SubtaskViewModel> Subtasks { get; set; }
        public Color Importance { get; set; }
    }
}