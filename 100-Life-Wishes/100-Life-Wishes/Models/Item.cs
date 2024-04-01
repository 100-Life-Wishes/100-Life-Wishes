using _100_Life_Wishes.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace _100_Life_Wishes.Models
{

    public class Item
    {
        public string Id { get; set; }
        public string Text { get; set; }
        public string Description { get; set; }
        public ObservableCollection<SubtaskViewModel> Subtasks { get; set; }
        public Color Importance { get; set; }
        public double Progress
        {
            get
            {
                if (Subtasks == null || Subtasks.Count == 0) return 0;
                return Subtasks.Count(st => st.IsCompleted) / (double)Subtasks.Count;
            }
        }
    }
}