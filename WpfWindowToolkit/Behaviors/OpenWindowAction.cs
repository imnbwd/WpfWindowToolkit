using Microsoft.Xaml.Behaviors;
using PraiseHim.Rejoice.WpfWindowToolkit.Base;
using PraiseHim.Rejoice.WpfWindowToolkit.Utilities;
using System;
using System.Reflection;
using System.Windows;
using System.Windows.Input;

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
        /// <summary>
        /// CommandAfterCloseProperty
        /// </summary>
        public static readonly DependencyProperty CommandAfterCloseProperty =
            DependencyProperty.Register("CommandAfterClose", typeof(ICommand), typeof(OpenWindowAction), new PropertyMetadata(null));

        /// <summary>
        /// IsModalProperty
        /// </summary>
        public static readonly DependencyProperty IsModalProperty =
                    DependencyProperty.Register("IsModal", typeof(bool), typeof(OpenWindowAction), new PropertyMetadata(true));

        /// <summary>
        /// MethodAfterCloseProperty
        /// </summary>
        public static readonly DependencyProperty MethodAfterCloseProperty =
            DependencyProperty.Register("MethodAfterClose", typeof(string), typeof(OpenWindowAction), new PropertyMetadata(null));

        /// <summary>
        /// MethodOfTargetObjectProperty
        /// </summary>
        public static readonly DependencyProperty MethodOfTargetObjectProperty =
            DependencyProperty.Register("MethodOfTargetObject", typeof(object), typeof(OpenWindowAction), new PropertyMetadata(null));

        /// <summary>
        /// ParameterProperty
        /// </summary>
        public static readonly DependencyProperty ParameterProperty =
                                    DependencyProperty.Register("Parameter", typeof(object), typeof(OpenWindowAction), new PropertyMetadata(null));

        /// <summary>
        /// ChenckingFuncBeforeOpenning
        /// </summary>
        public static readonly DependencyProperty ChenckingFuncBeforeOpenningProperty =
            DependencyProperty.Register("ChenckingFuncBeforeOpenning", typeof(Func<bool>), typeof(OpenWindowAction), new PropertyMetadata(null));

        /// <summary>
        /// WindowTypeProperty
        /// </summary>
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
        /// Get or set the command to execute before the target window opens
        /// </summary>
        public ICommand CommandBeforeOpen
        {
            get { return (ICommand)GetValue(CommandBeforeOpenProperty); }
            set { SetValue(CommandBeforeOpenProperty, value); }
        }

        /// <summary>
        /// CommandBeforeOpenProperty
        /// </summary>
        public static readonly DependencyProperty CommandBeforeOpenProperty =
            DependencyProperty.Register("CommandBeforeOpen", typeof(ICommand), typeof(OpenWindowAction), new PropertyMetadata(null));

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
        /// Get or set a function that return a boolean value indicating whether the target window can be openned or not
        /// </summary>
        public Func<bool> ChenckingFuncBeforeOpenning
        {
            get { return (Func<bool>)GetValue(ChenckingFuncBeforeOpenningProperty); }
            set { SetValue(ChenckingFuncBeforeOpenningProperty, value); }
        }

        /// <summary>
        /// The type of the window to open
        /// </summary>
        public Type WindowType
        {
            get { return (Type)GetValue(WindowTypeProperty); }
            set { SetValue(WindowTypeProperty, value); }
        }

        /// <summary>
        /// Overrides Invoke method
        /// </summary>
        /// <param name="parameter"></param>
        protected override void Invoke(object parameter)
        {
            if (ChenckingFuncBeforeOpenning != null && !ChenckingFuncBeforeOpenning())
            {
                return;
            }

            if (CommandBeforeOpen != null)
            {
                CommandBeforeOpen.Execute(null);
            }

            Window window = null;
            object windowObj = null;

            try
            {
                windowObj = Activator.CreateInstance(WindowType);
                window = windowObj as Window;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Cannot create a window with the given type", ex);
            }

            var closeEventHanlder = new EventHandler((s, e) =>
                {
                    // check if need to process the return value
                    var returnValueInfo = window.TryGetReturnValue();

                    // first execute CommandAfterClose command, then invoke MethodAfterClose method (if they are set)
                    CommandAfterClose?.Execute(returnValueInfo.HasValue ? returnValueInfo.Value : null);

                    if (!string.IsNullOrWhiteSpace(MethodAfterClose))
                    {
                        MethodInfo method = null;

                        // if MethodOfTargetObject is not set, then the DataContext of AssociatedObject will be used as such a purpose
                        if (MethodOfTargetObject == null)
                        {
                            var dataContext = (AssociatedObject as FrameworkElement).DataContext;
                            if (dataContext != null)
                            {
                                method = dataContext.GetType().GetMethod(MethodAfterClose, BindingFlags.Public | BindingFlags.Instance);
                                var methodPara = returnValueInfo.HasValue ? new object[] { returnValueInfo.Value } : null;
                                method?.Invoke(dataContext, methodPara);
                            }
                        }
                        else
                        {
                            method = MethodOfTargetObject?.GetType().GetMethod(MethodAfterClose, BindingFlags.Public | BindingFlags.Instance);
                            var methodPara = returnValueInfo.HasValue ? new object[] { returnValueInfo.Value } : null;
                            method?.Invoke(MethodOfTargetObject, methodPara);
                        }
                    }
                });

            window.Closed -= closeEventHanlder;
            window.Closed += closeEventHanlder;

            if (window.DataContext != null && window.DataContext is ViewModelRootBase)
            {
                // set the data to viewmodel
                (window.DataContext as ViewModelRootBase).Data = Parameter;
            }

            if (IsModal)
            {
                // set the owner
                window.Owner = AppWindow.GetCurrentActivatedWindow();

                window.ShowDialog();
            }
            else
            {
                window.Show();
            }
        }
    }
}