using PraiseHim.Rejoice.WpfWindowToolkit.Utilities;
using System.Windows;

namespace PraiseHim.Rejoice.WpfWindowToolkit.Helpers
{
    /// <summary>
    /// WindowHelper, provides attached properties to do some operations on a window including open, maximize, minimize, etc.
    /// </summary>
    /// <remarks>Part of the code from here: https://coderelief.net/2010/04/19/wpf-window-disable-minimize-and-maximize-buttons-through-attached-properties-from-xaml/ </remarks>
    public partial class WindowHelper
    {
        #region CanMaximize

        public static readonly DependencyProperty CanMaximize =
            DependencyProperty.RegisterAttached("CanMaximize", typeof(bool), typeof(Window),
                new PropertyMetadata(true, new PropertyChangedCallback(OnCanMaximizeChanged)));

        public static bool GetCanMaximize(DependencyObject d)
        {
            return (bool)d.GetValue(CanMaximize);
        }

        public static void SetCanMaximize(DependencyObject d, bool value)
        {
            d.SetValue(CanMaximize, value);
        }

        private static void OnCanMaximizeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Window window = d as Window;
            if (window != null)
            {
                RoutedEventHandler loadedHandler = null;
                loadedHandler = delegate
                {
                    if ((bool)e.NewValue)
                    {
                        WindowController.EnableMaximize(window);
                    }
                    else
                    {
                        WindowController.DisableMaximize(window);
                    }
                    window.Loaded -= loadedHandler;
                };

                if (!window.IsLoaded)
                {
                    window.Loaded += loadedHandler;
                }
                else
                {
                    loadedHandler(null, null);
                }
            }
        }

        #endregion CanMaximize

        #region CanMinimize

        public static readonly DependencyProperty CanMinimize =
            DependencyProperty.RegisterAttached("CanMinimize", typeof(bool), typeof(Window),
                new PropertyMetadata(true, new PropertyChangedCallback(OnCanMinimizeChanged)));

        public static bool GetCanMinimize(DependencyObject d)
        {
            return (bool)d.GetValue(CanMinimize);
        }

        public static void SetCanMinimize(DependencyObject d, bool value)
        {
            d.SetValue(CanMinimize, value);
        }

        private static void OnCanMinimizeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Window window = d as Window;
            if (window != null)
            {
                RoutedEventHandler loadedHandler = null;
                loadedHandler = delegate
                {
                    if ((bool)e.NewValue)
                    {
                        WindowController.EnableMinimize(window);
                    }
                    else
                    {
                        WindowController.DisableMinimize(window);
                    }
                    window.Loaded -= loadedHandler;
                };

                if (!window.IsLoaded)
                {
                    window.Loaded += loadedHandler;
                }
                else
                {
                    loadedHandler(null, null);
                }
            }
        }

        #endregion CanMinimize
    }
}