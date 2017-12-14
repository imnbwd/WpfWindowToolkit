using PraiseHim.Rejoice.WpfWindowToolkit.Base;
using System;
using System.Windows;
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
#if NET45
            set { Set(ref _preCheck, value); }
#elif NET40
            set { _preCheck = value; this.OnPropertyChanged("PreCheck"); }
#endif
        }

        public RelayCommand WhenWindowClosedCommand { get; set; }

        public void WhenWindowClosed()
        {
            MessageBox.Show("Window1 closed");
        }
    }
}