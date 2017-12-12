namespace WpfWindowToolkit.Demo.ViewModels
{
    public class ViewModelLocator
    {
        public MainViewModel MainViewModel { get => new MainViewModel(); }

        public ReturnValueTestWindowViewModel ReturnValueTestWindowViewModel { get => new ReturnValueTestWindowViewModel(); }

        public ReturnValueMainWindowViewModel ReturnValueMainWindowViewModel { get => new ReturnValueMainWindowViewModel(); }

        public Window1ViewModel Window1ViewModel { get => new Window1ViewModel(); }

        public Window2ViewModel Window2ViewModel { get => new Window2ViewModel(); }
    }
}