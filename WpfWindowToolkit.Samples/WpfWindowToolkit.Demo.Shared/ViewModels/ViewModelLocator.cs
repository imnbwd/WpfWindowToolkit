namespace WpfWindowToolkit.Demo.ViewModels
{
    public class ViewModelLocator
    {
        public CloseTestViewModel CloseTestViewModel { get { return new CloseTestViewModel(); } }

        public MainViewModel MainViewModel { get { return new MainViewModel(); } }

        public ReturnValueMainWindowViewModel ReturnValueMainWindowViewModel { get { return new ReturnValueMainWindowViewModel(); } }
        public ReturnValueTestWindowViewModel ReturnValueTestWindowViewModel { get { return new ReturnValueTestWindowViewModel(); } }
        public Window1ViewModel Window1ViewModel { get { return new Window1ViewModel(); } }

        public Window2ViewModel Window2ViewModel { get { return new Window2ViewModel(); } }
    }
}