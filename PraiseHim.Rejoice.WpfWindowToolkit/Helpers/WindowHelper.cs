using PraiseHim.Rejoice.WpfWindowToolkit.Base;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;

namespace PraiseHim.Rejoice.WpfWindowToolkit.Helpers
{
    public static class WindowHelper
    {
        public static readonly DependencyProperty CommandAfterCloseProperty =
            DependencyProperty.RegisterAttached("CommandAfterClose", typeof(ICommand), typeof(WindowHelper), new PropertyMetadata(null));

        public static readonly DependencyProperty IsModalProperty =
            DependencyProperty.RegisterAttached("IsModal", typeof(bool), typeof(WindowHelper), new PropertyMetadata(true));

        public static readonly DependencyProperty OpenWindowTypeProperty =
                            DependencyProperty.RegisterAttached("OpenWindowType", typeof(Type), typeof(WindowHelper), new PropertyMetadata(null, OnOpenWindowTypeChanged));

        public static readonly DependencyProperty ParameterProperty =
            DependencyProperty.RegisterAttached("Parameter", typeof(object), typeof(WindowHelper), new PropertyMetadata(null));

        public static ICommand GetCommandAfterClose(DependencyObject obj)
        {
            return (ICommand)obj.GetValue(CommandAfterCloseProperty);
        }

        public static bool GetIsModal(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsModalProperty);
        }

        public static Type GetOpenWindowType(DependencyObject obj)
        {
            return (Type)obj.GetValue(OpenWindowTypeProperty);
        }

        public static object GetParameter(DependencyObject obj)
        {
            return (object)obj.GetValue(ParameterProperty);
        }

        public static void SetCommandAfterClose(DependencyObject obj, ICommand value)
        {
            obj.SetValue(CommandAfterCloseProperty, value);
        }

        public static void SetIsModal(DependencyObject obj, bool value)
        {
            obj.SetValue(IsModalProperty, value);
        }

        public static void SetOpenWindowType(DependencyObject obj, Type value)
        {
            obj.SetValue(OpenWindowTypeProperty, value);
        }

        public static void SetParameter(DependencyObject obj, object value)
        {
            obj.SetValue(ParameterProperty, value);
        }

        private static void OnOpenWindowTypeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            // make it available for Button, Hyperlink, MenutItem
            dynamic control = null;
            switch (d.GetType().Name)
            {
                case nameof(Button):
                    control = d as Button;
                    break;

                case nameof(Hyperlink):
                    control = d as Hyperlink;
                    break;

                case nameof(MenuItem):
                    control = d as MenuItem;
                    break;

                default:
                    return;
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
                    //else
                    //{
                    //    window.Tag = GetParameter(d);
                    //}
                }

                var isModel = GetIsModal(d);

                window.Closed += (win, closeArgs) =>
                {
                    var command = GetCommandAfterClose(d);
                    if (command != null)
                    {
                        command.Execute(null);
                    }

                    // set the object to null after it is closed
                    window = null;
                };

                if (isModel)
                {
                    window.ShowDialog();
                }
                else
                {
                    window.Show();
                }
            });

            control.Click += clickEventHandler;
        }
    }
}