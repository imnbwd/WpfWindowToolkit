using PraiseHim.Rejoice.WpfWindowToolkit.Base;
using System.Windows;
using WpfWindowToolkit.Demo.Models;
using WpfWindowToolkit.Demo.Views;

namespace WpfWindowToolkit.Demo.ViewModels
{
    public class ReturnValueMainWindowViewModel : ViewModelBaseEx<Friend>
    {
        public ReturnValueMainWindowViewModel()
        {
            ProcessReturnValueCommand = new RelayCommand<Friend>(ProcessReturnValue);
        }

        public RelayCommand<Friend> ProcessReturnValueCommand { get; set; }

        public void ShowFriendSelectionWindow()
        {
            this.ShowWindow(new OpenWindowInfo { WindowType = typeof(ReturnValueTestWindow) }, friend => ShowResult(friend));
        }

        private void ProcessReturnValue(Friend parameter)
        {
            ShowResult(parameter);
        }

        public void ShowResult(Friend friend)
        {
            if (friend != null)
            {
                MessageBox.Show($"You have selected this friend: {friend.Name}");
            }
            else
            {
                MessageBox.Show("No friend has been selected");
            }
        }
    }
}