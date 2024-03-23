using _100_Life_Wishes.Models;
using System.Windows.Input;
using Xamarin.Forms;

namespace _100_Life_Wishes.ViewModels
{
    public class SubtaskViewModel : BaseViewModel
    {
        private string name;
        private Color сolor = Color.White;
        public bool IsCompleted;
        public Command DeleteSubtaskCommand { get; private set; }
        public Command HighlightSubtaskCommand { get; private set; }
        public string Name
        {
            get => name;
            set => SetProperty(ref name, value);
        }
        public Color SubtaskColor
        {
            get => сolor;
            set => SetProperty(ref сolor, value);
        }
        public SubtaskViewModel()
        {
            DeleteSubtaskCommand = new Command<SubtaskViewModel>(RemoveSubtask);
            HighlightSubtaskCommand = new Command<SubtaskViewModel>(HighlightSubtask);
        }
        private void RemoveSubtask(SubtaskViewModel subtask)
        {
            MessagingCenter.Send(this, "DeleteSubtask", subtask);
        }
        private void HighlightSubtask(SubtaskViewModel subtask)
        {
            if (!IsCompleted)
            {
                сolor = Color.LightGreen;
                IsCompleted = true;
            }
            else
            {
                IsCompleted = false;
                сolor = Color.White;
            }
            MessagingCenter.Send(this, "HighlightSubtask");
        }
    }

}