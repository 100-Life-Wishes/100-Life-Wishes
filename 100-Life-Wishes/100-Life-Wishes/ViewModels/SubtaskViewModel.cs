using Newtonsoft.Json;
using Xamarin.Forms;

namespace _100_Life_Wishes.ViewModels
{
    public class SubtaskViewModel : BaseViewModel
    {
        private string name;
        private Color color = Color.White;
        public bool IsCompleted { get; set; }
        public Command DeleteSubtaskCommand { get; private set; }
        public Command HighlightSubtaskCommand { get; private set; }
        public string Name
        {
            get => name;
            set => SetProperty(ref name, value);
        }
        public Color SubtaskColor
        {
            get => color;
            set => SetProperty(ref color, value);
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
                color = Color.LightGreen;
                IsCompleted = true;
            }
            else
            {
                IsCompleted = false;
                color = Color.White;
            }
        }
    }


}