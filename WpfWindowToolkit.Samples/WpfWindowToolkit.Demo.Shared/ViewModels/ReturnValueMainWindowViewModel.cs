using PraiseHim.Rejoice.WpfWindowToolkit.Base;
using System.Windows;
using WpfWindowToolkit.Demo.Models;
using WpfWindowToolkit.Demo.Views;

namespace WpfWindowToolkit.Demo.ViewModels
{   
    public class ReturnValueMainWindowViewModel : ViewModelBaseEx<Friend>
    {
        public void ShowFriendSelectionWindow()
        {
            this.ShowWindow(new OpenWindowInfo { WindowType = typeof(ReturnValueTestWindow) }, friend =>
            {
                if (friend != null)
                {
                    MessageBox.Show($"You have selected this friend: {friend.Name}");
                }
                else
                {
                    MessageBox.Show("No friend has been selected");
                }
            });
        }
    }
}