using PraiseHim.Rejoice.WpfWindowToolkit.Base;
using System;
using System.Windows;

namespace WpfWindowToolkit.Demo.ViewModels
{
    public class MainViewModel : BindableBase
    {
        private Func<bool> _preCheck;

        public MainViewModel()
        {
            WhenWindowClosedCommand = new RelayCommand(WhenWindowClosed);
            PreCheck = new Func<bool>(() =>
            {
                return MessageBox.Show("really open a new window?", "Confirm", MessageBoxButton.OKCancel) == MessageBoxResult.OK;
            });

            CheckBeforeCloseWindow = new Func<bool>(() =>
            {
                return MessageBox.Show("really want to close the window?", "Confirm", MessageBoxButton.OKCancel) == MessageBoxResult.OK;
            });
        }

        public Func<bool> CheckBeforeCloseWindow { get; set; }

        public Func<bool> PreCheck

        {
            get { return _preCheck; }
#if NET45
            set { Set(ref _preCheck, value); }
#elif NET40
            set { _preCheck = value; this.OnPropertyChanged("PreCheck"); }
#endif
        }

        public RelayCommand<object> TestAACommand { get; set; }
        public RelayCommand WhenWindowClosedCommand { get; set; }

        public void WhenWindowClosed()
        {
            MessageBox.Show("Window1 closed");
        }
    }
}