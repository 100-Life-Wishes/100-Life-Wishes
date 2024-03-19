namespace _100_Life_Wishes.ViewModels
{
    public class SubtaskViewModel : BaseViewModel
    {
        private string name;

        public string Name
        {
            get => name;
            set => SetProperty(ref name, value);
        }
    }

}