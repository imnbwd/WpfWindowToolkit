using Microsoft.Xaml.Behaviors;
using System.Windows;
using System.Windows.Input;

namespace PraiseHim.Rejoice.WpfWindowToolkit.Behaviors
{
    /// <summary>
    /// An action to invoke a <see cref="ICommand"/>.
    /// </summary>
    public class InvokeCommandAction : TriggerAction<FrameworkElement>
    {
        public static readonly DependencyProperty CommandParameterProperty =
            DependencyProperty.Register("CommandParameter", typeof(object), typeof(InvokeCommandAction), new PropertyMetadata(null));

        public static readonly DependencyProperty CommandProperty =
                    DependencyProperty.Register("Command", typeof(ICommand), typeof(InvokeCommandAction), new PropertyMetadata(null));

        /// <summary>
        /// Get or set the ICommand
        /// </summary>
        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        /// <summary>
        /// Get or set the parameter for the command
        /// </summary>
        public object CommandParameter
        {
            get { return (object)GetValue(CommandParameterProperty); }
            set { SetValue(CommandParameterProperty, value); }
        }

        /// <summary>
        /// Override Invoke method
        /// </summary>
        /// <param name="parameter"></param>
        protected override void Invoke(object parameter)
        {
            if (CommandParameter != null)
            {
                Command?.Execute(CommandParameter);
            }
            else
            {
                Command?.Execute(parameter);
            }
        }
    }
}