using _100_Life_Wishes.Models;
using System.Windows.Input;
using Xamarin.Forms;

namespace _100_Life_Wishes.ViewModels
{
    public class Subtask : BaseViewModel
    {
        private string name;
        private string color = "#FFFFFF";
        public bool IsCompleted { get; set; }
        public Command DeleteSubtaskCommand { get; private set; }
        public Command HighlightSubtaskCommand { get; private set; }
        public string Name
        {
            get => name;
            set => SetProperty(ref name, value);
        }
        public string SubtaskColor
        {
            get => color;
            set => SetProperty(ref color, value);
        }

        public Subtask()
        {
            DeleteSubtaskCommand = new Command<Subtask>(RemoveSubtask);
            HighlightSubtaskCommand = new Command<Subtask>(HighlightSubtask);
        }
        private void RemoveSubtask(Subtask subtask)
        {
            MessagingCenter.Send(this, "DeleteSubtask", subtask);
        }
        private void HighlightSubtask(Subtask subtask)
        {
            if (!IsCompleted)
            {
                color = "#90EE90";
                IsCompleted = true;
            }
            else
            {
                IsCompleted = false;
                color = "#FFFFFF";
            }
        }
    }


}