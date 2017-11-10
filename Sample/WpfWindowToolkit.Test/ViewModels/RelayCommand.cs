using System;

namespace WpfWindowToolkit.Test.ViewModels
{
    using System.Windows.Input;

    public class RelayCommand : ICommand
    {
        private Action _action;
        private Func<bool> _func;

        public RelayCommand()
        {
        }

        public RelayCommand(Action action) : this()
        {
            _action = action;
        }

        public RelayCommand(Action action, Func<bool> func) : this(action)
        {
            _func = func;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            if (_func != null)
            {
                return _func();
            }
            else
            {
                return true;
            }
        }

        public void Execute(object parameter)
        {
            if (_action != null)
            {
                _action();
            }
        }

        public void RaiseCanExecuteChanged()
        {
            if (CanExecuteChanged != null)
            {
                CanExecuteChanged(this, new EventArgs());
            }
        }
    }
}