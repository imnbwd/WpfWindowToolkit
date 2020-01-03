using System;

namespace WpfWindowToolkit.Demo.ViewModels
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
            _action?.Invoke();
        }

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, new EventArgs());
        }
    }

    public class RelayCommand<T> : ICommand
    {
        private readonly Func<T, bool> _canExecute;
        private readonly Action<T> _execute;

        public RelayCommand(Action<T> execute)
            : this(execute, null)
        {
        }

        public RelayCommand(Action<T> execute, Func<T, bool> canExecute)
        {
            if (execute == null)
            {
                throw new ArgumentNullException("execute");
            }

            _execute = new Action<T>(execute);

            if (canExecute != null)
            {
                _canExecute = new Func<T, bool>(canExecute);
            }
        }

        public event EventHandler CanExecuteChanged
        {
            add
            {
                if (_canExecute != null)
                {
                    CommandManager.RequerySuggested += value;
                }
            }

            remove
            {
                if (_canExecute != null)
                {
                    CommandManager.RequerySuggested -= value;
                }
            }
        }

        public bool CanExecute(object parameter)
        {
            if (_canExecute == null)
            {
                return true;
            }

            if (parameter == null || parameter is T)
            {
                return _canExecute((T)parameter);
            }
            else
            {
                return _canExecute(default(T));
            }
        }

        public virtual void Execute(object parameter)
        {
            var val = parameter;

            if (parameter != null && parameter is T)
            {
                _execute((T)parameter);
            }
            else
            {
                _execute(default(T));
            }

            if (parameter != null
                && parameter.GetType() != typeof(T))
            {
                if (parameter is IConvertible)
                {
                    val = Convert.ChangeType(parameter, typeof(T), null);
                }
            }
        }

        public void RaiseCanExecuteChanged()
        {
            CommandManager.InvalidateRequerySuggested();
        }
    }
}