using PraiseHim.Rejoice.WpfWindowToolkit.Base;
using PraiseHim.Rejoice.WpfWindowToolkit.Utilities;
using System;
using System.Windows;
using System.Windows.Input;

namespace PraiseHim.Rejoice.WpfWindowToolkit.Helpers
{
    /// <summary>
    /// WindowHelper, provides attached properties to do some operations on a window including open, maximize, minimize, etc.
    /// </summary>
    public static partial class WindowHelper
    {
        /// <summary>
        /// CommandAfterClose attached property
        /// </summary>
        public static readonly DependencyProperty CommandAfterCloseProperty =
            DependencyProperty.RegisterAttached("CommandAfterClose", typeof(ICommand), typeof(WindowHelper), new PropertyMetadata(null));

        /// <summary>
        /// IsModal attached property
        /// </summary>
        public static readonly DependencyProperty IsModalProperty =
            DependencyProperty.RegisterAttached("IsModal", typeof(bool), typeof(WindowHelper), new PropertyMetadata(true));

        /// <summary>
        /// OpenWindowType attached property
        /// </summary>
        public static readonly DependencyProperty OpenWindowTypeProperty =
                            DependencyProperty.RegisterAttached("OpenWindowType", typeof(Type), typeof(WindowHelper), new PropertyMetadata(null, OnOpenWindowTypeChanged));

        /// <summary>
        /// Parameter attached property
        /// </summary>
        public static readonly DependencyProperty ParameterProperty =
            DependencyProperty.RegisterAttached("Parameter", typeof(object), typeof(WindowHelper), new PropertyMetadata(null));

        /// <summary>
        /// Get the <see cref="ICommand"/> to be executed after the window closed
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static ICommand GetCommandAfterClose(DependencyObject obj)
        {
            return (ICommand)obj.GetValue(CommandAfterCloseProperty);
        }

        /// <summary>
        /// Get the value that indicates whether the window is modal or not
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool GetIsModal(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsModalProperty);
        }

        /// <summary>
        /// Get the type of the window to open
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static Type GetOpenWindowType(DependencyObject obj)
        {
            return (Type)obj.GetValue(OpenWindowTypeProperty);
        }

        /// <summary>
        /// Set the parameter to be passed to the ViewModel of the new window. Notice, that view model need inherits <see cref="Base.ViewModelBaseData{T}"/>
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static object GetParameter(DependencyObject obj)
        {
            return (object)obj.GetValue(ParameterProperty);
        }

        /// <summary>
        /// Set the <see cref="ICommand"/> to be executed after the window closed
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="value"></param>
        public static void SetCommandAfterClose(DependencyObject obj, ICommand value)
        {
            obj.SetValue(CommandAfterCloseProperty, value);
        }

        /// <summary>
        /// Set the value that indicates whether the window is modal or not
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="value"></param>
        public static void SetIsModal(DependencyObject obj, bool value)
        {
            obj.SetValue(IsModalProperty, value);
        }

        /// <summary>
        /// Set the type of the window to open
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="value"></param>
        public static void SetOpenWindowType(DependencyObject obj, Type value)
        {
            obj.SetValue(OpenWindowTypeProperty, value);
        }

        /// <summary>
        /// Get the parameter to be passed to the ViewModel of the new window. Notice, that view model need inherits <see cref="Base.ViewModelBaseData{T}"/>
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="value"></param>
        public static void SetParameter(DependencyObject obj, object value)
        {
            obj.SetValue(ParameterProperty, value);
        }

        private static void OnOpenWindowTypeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            //// make it available for Button, Hyperlink, MenutItem
            //dynamic control = null;
            //switch (d.GetType().Name)
            //{
            //    case nameof(Button):
            //        control = d as Button;
            //        break;

            //    case nameof(Hyperlink):
            //        control = d as Hyperlink;
            //        break;

            //    case nameof(MenuItem):
            //        control = d as MenuItem;
            //        break;

            //    default:
            //        return;
            //}

            // make it avaible for those controls which has Click event
            var eventInfo = d.GetType().GetEvent("Click");
            if (eventInfo == null)
            {
                throw new InvalidOperationException("The control attached does not have a event named Click");
            }

            var type = GetOpenWindowType(d);
            if (type == null && type != typeof(Window))
            {
                return;
            }

            Window window = null;

            var clickEventHandler = new RoutedEventHandler((s, arg) =>
            {
                if (window == null)
                {
                    window = Activator.CreateInstance(type) as Window;

                    if (window == null)
                    {
                        throw new ArgumentException("cannot create a window by the target type");
                    }
                }

                if (GetParameter(d) != null)
                {
                    if (window.DataContext != null && window.DataContext is ViewModelRootBase)
                    {
                        (window.DataContext as ViewModelRootBase).Data = GetParameter(d);
                    }
                }

                var isModel = GetIsModal(d);

                window.Closed += (win, args) =>
                {
                    // try to get the return value from the window's view model
                    var returnValueInfo = window.TryGetReturnValue();
                    var command = GetCommandAfterClose(d);
                    if (command != null)
                    {
                        command.Execute(returnValueInfo.HasValue ? returnValueInfo.Value : null);
                    }

                    // set the object to null after it is closed
                    window = null;
                };

                if (isModel)
                {
                    // set the owner
                    window.Owner = AppWindow.GetCurrentActivatedWindow();

                    window.ShowDialog();
                }
                else
                {
                    window.Show();
                }
            });

            //control.Click -= clickEventHandler;
            //control.Click += clickEventHandler;
            eventInfo.RemoveEventHandler(d, clickEventHandler);
            eventInfo.AddEventHandler(d, clickEventHandler);
        }
    }
}