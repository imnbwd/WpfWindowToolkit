using PraiseHim.Rejoice.WpfWindowToolkit.Base;
using System;
using System.Text;
using System.Windows;
using System.Windows.Input;
using WpfWindowToolkit.Demo.Models;
using WpfWindowToolkit.Demo.Views;

namespace WpfWindowToolkit.Demo.ViewModels
{
    public class MainViewModel : ViewModelBaseEx
    {
        private Func<bool> _preCheck;

        public MainViewModel()
        {
            CurrentFriend = new Friend { Id = "1", Name = "Jim", Email = "jim@xx.com", BirthDate = new DateTime(1990, 2, 4), IsDeveloper = true };

            WhenWindowClosedCommand = new RelayCommand(WhenWindowClosed);
            PreCheck = new Func<bool>(() =>
            {
                return MessageBox.Show("really open a new window?", "Confirm", MessageBoxButton.OKCancel, MessageBoxImage.Question) == MessageBoxResult.OK;
            });

            CheckBeforeCloseWindow = new Func<bool>(() =>
            {
                return MessageBox.Show("really want to close the window?", "Confirm", MessageBoxButton.OKCancel, MessageBoxImage.Question) == MessageBoxResult.OK;
            });
        }

        public Friend CurrentFriend { get; set; }

        public Func<bool> CheckBeforeCloseWindow { get; set; }

        public RelayCommand ComplexLogicForOpeningAWindowCommand
        {
            get
            {
                return new RelayCommand(() =>
                {
                    /*
                     * Firstly, the current view mdoel should inherit from ViewModelBaseEx
                     */

                    // your logic here (before)
                    this.ShowWindow(new OpenWindowInfo { IsModal = true, Parameter = CurrentFriend, WindowType = typeof(Window2) });
                    // your logic here (after)
                });
            }
        }
        public Func<bool> PreCheck
        {
            get { return _preCheck; }
            set { Set(ref _preCheck, value); }
        }

        public RelayCommand WhenWindowClosedCommand { get; set; }

        public void WhenWindowClosed()
        {
            MessageBox.Show("Command/method executed after Window1 closed");
        }

        #region Show event argument

        public ICommand ShowEventArgumentCommand => new RelayCommand<RoutedEventArgs>(ShowEventArgument);

        public void ShowEventArgument(RoutedEventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Event argument:");
            sb.AppendLine($"\tType: \t{e.GetType().Name}");
            sb.AppendLine($"\tSource: \t{e.Source}");
            sb.AppendLine($"\tOriginalSource: \t{e.OriginalSource}");
            MessageBox.Show(sb.ToString());
        }

        #endregion Show event argument
    }
}