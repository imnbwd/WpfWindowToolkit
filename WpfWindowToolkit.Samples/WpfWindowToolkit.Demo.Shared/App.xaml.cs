using System.Windows;
using WpfWindowToolkit.Demo.ViewModels;

namespace WpfWindowToolkit.Demo
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            //App.Current.Resources.Add("Locator", new ViewModelLocator());
            //Views.MainWindow main = new Views.MainWindow();
            //App.Current.MainWindow = main;
            //main.Show();
        }
    }
}