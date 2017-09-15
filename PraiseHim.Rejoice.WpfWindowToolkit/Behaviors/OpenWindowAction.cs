using PraiseHim.Rejoice.WpfWindowToolkit.Base;
using System;
using System.Diagnostics;
using System.Reflection;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interactivity;

namespace PraiseHim.Rejoice.WpfWindowToolkit.Behaviors
{
    /// <summary>    
    /// Open a specified window, parameter can also be taken to pass to the new Window.
    /// </summary>
    /// <remarks>
    /// Known issue:
    /// 1) When using this Action in MenuItem, it cannot invoke the CommandAfterClose command and MethodAfterClose    
    /// </remarks>
    public class OpenWindowAction : TriggerAction<DependencyObject>
    {
        public static readonly DependencyProperty CommandAfterCloseProperty =
            DependencyProperty.Register("CommandAfterClose", typeof(ICommand), typeof(OpenWindowAction), new PropertyMetadata(null));

        public static readonly DependencyProperty IsModalProperty =
                    DependencyProperty.Register("IsModal", typeof(bool), typeof(OpenWindowAction), new PropertyMetadata(true));

        public static readonly DependencyProperty MethodAfterCloseProperty =
            DependencyProperty.Register("MethodAfterClose", typeof(string), typeof(OpenWindowAction), new PropertyMetadata(null));

        public static readonly DependencyProperty MethodOfTargetObjectProperty =
            DependencyProperty.Register("MethodOfTargetObject", typeof(object), typeof(OpenWindowAction), new PropertyMetadata(null));

        public static readonly DependencyProperty ParameterProperty =
                                    DependencyProperty.Register("Parameter", typeof(object), typeof(OpenWindowAction), new PropertyMetadata(null));

        public static readonly DependencyProperty WindowTypeProperty =
                    DependencyProperty.Register("WindowType", typeof(Type), typeof(OpenWindowAction), new PropertyMetadata(null));

        /// <summary>
        /// Get or set the command to execute when the target window is closed
        /// </summary>
        public ICommand CommandAfterClose
        {
            get { return (ICommand)GetValue(CommandAfterCloseProperty); }
            set { SetValue(CommandAfterCloseProperty, value); }
        }

        /// <summary>
        /// Specify whether the windows is modal or not
        /// </summary>
        public bool IsModal
        {
            get { return (bool)GetValue(IsModalProperty); }
            set { SetValue(IsModalProperty, value); }
        }

        /// <summary>
        /// The name of the method that will be executed after the window closed
        /// </summary>
        public string MethodAfterClose
        {
            get { return (string)GetValue(MethodAfterCloseProperty); }
            set { SetValue(MethodAfterCloseProperty, value); }
        }

        /// <summary>
        /// The object that possesses the method MethodAfterClose, commonly it is a ViewModel        
        /// </summary>
        public object MethodOfTargetObject
        {
            get { return (object)GetValue(MethodOfTargetObjectProperty); }
            set { SetValue(MethodOfTargetObjectProperty, value); }
        }

        /// <summary>
        /// The parameter need to pass to the new window
        /// </summary>
        public object Parameter
        {
            get { return (object)GetValue(ParameterProperty); }
            set { SetValue(ParameterProperty, value); }
        }

        /// <summary>
        /// The type of the window to open
        /// </summary>
        public Type WindowType
        {
            get { return (Type)GetValue(WindowTypeProperty); }
            set { SetValue(WindowTypeProperty, value); }
        }

        protected override void Invoke(object parameter)
        {
            try
            {
                var windowObj = Activator.CreateInstance(WindowType);

                var window = windowObj as Window;

                window.Closed += (s, e) =>
                {
                    // first execute CommandAfterClose command, then invoke MethodAfterClose method (if they are set)
                    CommandAfterClose?.Execute(null);

                    if (!string.IsNullOrWhiteSpace(MethodAfterClose))
                    {
                        MethodInfo method = null;

                        // if MethodOfTargetObject is not set, then the DataContext of AssociatedObject will be used as such a purpose
                        if (MethodOfTargetObject == null && MethodOfTargetObject is FrameworkElement)
                        {
                            var dataContext = (MethodOfTargetObject as FrameworkElement).DataContext;
                            if (dataContext != null)
                            {
                                method = dataContext.GetType().GetMethod(MethodAfterClose, BindingFlags.Public | BindingFlags.Instance);
                                method?.Invoke(dataContext, null);
                            }
                        }
                        else
                        {
                            method = MethodOfTargetObject?.GetType().GetMethod(MethodAfterClose, BindingFlags.Public | BindingFlags.Instance);
                            method?.Invoke(MethodOfTargetObject, null);
                        }
                    }
                };

                if (window.DataContext != null && window.DataContext is ViewModelRootBase)
                {
                    // set the data to viewmodel
                    (window.DataContext as ViewModelRootBase).Data = Parameter;
                }
                else
                {
                    window.Tag = Parameter;
                }

                if (IsModal)
                {
                    window.ShowDialog();
                }
                else
                {
                    window.Show();
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex);
            }
        }
    }
}