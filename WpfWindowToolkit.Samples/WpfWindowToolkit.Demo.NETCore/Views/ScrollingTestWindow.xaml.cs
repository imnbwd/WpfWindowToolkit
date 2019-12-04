using System.Windows;

namespace WpfWindowToolkit.Demo.Views
{
    /// <summary>
    /// Interaction logic for ScrollingTestWindow.xaml
    /// </summary>
    public partial class ScrollingTestWindow : Window
    {
        public ScrollingTestWindow()
        {
            InitializeComponent();
        }

        public void ScrolledToBorder8()
        {
            MessageBox.Show("Reached Border 8");
        }
    }
}