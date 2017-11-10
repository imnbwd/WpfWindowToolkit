namespace WpfWindowToolkit.Test.ViewModels
{
    public class ViewModelLocator
    {
        public ViewModelLocator()
        {
        }

        public MainViewModel MainViewModel { get => new MainViewModel(); }

        public Window1ViewModel Window1ViewModel { get => new Window1ViewModel(); }
    }
}