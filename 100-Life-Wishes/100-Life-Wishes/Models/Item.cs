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
        public string Importance { get; set; }
        public DateTime? Deadline { get; set; }

        public string FormattedDeadline => Deadline.HasValue ? $"Deadline: {Deadline:dd/MM/yyyy}" : string.Empty;
        public bool IsDeadlineVisible => Deadline.HasValue;
        public Color DeadlineColor => (Deadline.HasValue && Deadline < DateTime.Now) ? Color.Red : Color.Blue;


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