using PraiseHim.Rejoice.WpfWindowToolkit.Extensions;
using PraiseHim.Rejoice.WpfWindowToolkit.Utilities;
using System;
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

        /// <summary>
        /// CanMaximize attached property
        /// </summary>
        public static readonly DependencyProperty CanMaximize =
            DependencyProperty.RegisterAttached("CanMaximize", typeof(bool), typeof(WindowHelper),
                new PropertyMetadata(true, new PropertyChangedCallback(OnCanMaximizeChanged)));

        /// <summary>
        /// Get the value indicated whether the attached Window can be maximized
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        public static bool GetCanMaximize(DependencyObject d)
        {
            return (bool)d.GetValue(CanMaximize);
        }

        /// <summary>
        /// Set the value indicated whether the attached Window can be maximized
        /// </summary>
        /// <param name="d"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static void SetCanMaximize(DependencyObject d, bool value)
        {
            d.SetValue(CanMaximize, value);
        }

        private static void OnCanMaximizeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Window window = d as Window;

            if (window == null)
            {
                window = d.GetParent<Window>();
            }

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
                    window.Loaded -= loadedHandler;
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

        /// <summary>
        /// CanMinimize attached property
        /// </summary>
        public static readonly DependencyProperty CanMinimize =
            DependencyProperty.RegisterAttached("CanMinimize", typeof(bool), typeof(WindowHelper),
                new PropertyMetadata(true, new PropertyChangedCallback(OnCanMinimizeChanged)));

        /// <summary>
        /// Get the value indicated whether the attached Window can be minimized
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        public static bool GetCanMinimize(DependencyObject d)
        {
            return (bool)d.GetValue(CanMinimize);
        }

        /// <summary>
        /// Set the value indicated whether the attached Window can be minimized
        /// </summary>
        /// <param name="d"></param>
        /// <param name="value"></param>
        public static void SetCanMinimize(DependencyObject d, bool value)
        {
            d.SetValue(CanMinimize, value);
        }

        private static void OnCanMinimizeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Window window = d as Window;

            if (window == null)
            {
                window = d.GetParent<Window>();
            }

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
                    window.Loaded -= loadedHandler;
                    window.Loaded += loadedHandler;
                }
                else
                {
                    loadedHandler(null, null);
                }
            }
        }

        #endregion CanMinimize

        #region CanClose

        // Using a DependencyProperty as the backing store for CanClose.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CanCloseProperty =
            DependencyProperty.RegisterAttached("CanClose", typeof(bool), typeof(WindowHelper), new PropertyMetadata(true, OnCanCloseChanged));

        public static bool GetCanClose(DependencyObject obj)
        {
            return (bool)obj.GetValue(CanCloseProperty);
        }

        public static void SetCanClose(DependencyObject obj, bool value)
        {
            obj.SetValue(CanCloseProperty, value);
        }

        private static void OnCanCloseChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Window window = d as Window;

            if (window == null)
            {
                window = d.GetParent<Window>();
            }

            if (window != null)
            {
                RoutedEventHandler loadedHandler = null;
                loadedHandler = delegate
                {
                    if ((bool)e.NewValue)
                    {
                        WindowController.EnableClose(window);
                    }
                    else
                    {
                        WindowController.DisableClose(window);
                    }
                    window.Loaded -= loadedHandler;
                };

                if (!window.IsLoaded)
                {
                    window.Loaded -= loadedHandler;
                    window.Loaded += loadedHandler;
                }
                else
                {
                    loadedHandler(null, null);
                }
            }
        }

        #endregion CanClose

        #region ControlAction

        public static readonly DependencyProperty ControlActionProperty =
            DependencyProperty.RegisterAttached("ControlAction", typeof(WindowControlAction), typeof(WindowHelper), new PropertyMetadata(WindowControlAction.None, OnControlActionChanged));

        public static WindowControlAction GetControlAction(DependencyObject obj)
        {
            return (WindowControlAction)obj.GetValue(ControlActionProperty);
        }

        public static void SetControlAction(DependencyObject obj, WindowControlAction value)
        {
            obj.SetValue(ControlActionProperty, value);
        }

        private static void OnControlActionChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var eventInfo = d.GetType().GetEvent("Click");
            if (eventInfo == null)
            {
                throw new InvalidOperationException("Could find click event from the attached control");
            }

            Window currentWindow = d.GetParent<Window>();
            if (currentWindow == null)
            {
                throw new InvalidOperationException("Could not find the target window");
            }

            var newValue = (WindowControlAction)e.NewValue;

            RoutedEventHandler clickEventHandler = null;
            switch (newValue)
            {
                case WindowControlAction.Close:
                    clickEventHandler = new RoutedEventHandler((s, arg) =>
                    {
                        currentWindow.Close();
                    });

                    eventInfo.RemoveEventHandler(d, clickEventHandler);
                    eventInfo.AddEventHandler(d, clickEventHandler);
                    break;

                case WindowControlAction.Minimize:
                    clickEventHandler = new RoutedEventHandler((s, arg) =>
                    {
                        currentWindow.WindowState = WindowState.Minimized;
                    });

                    eventInfo.RemoveEventHandler(d, clickEventHandler);
                    eventInfo.AddEventHandler(d, clickEventHandler);
                    break;

                case WindowControlAction.NormalOrMaximize:
                    clickEventHandler = new RoutedEventHandler((s, arg) =>
                    {
                        currentWindow.WindowState = currentWindow.WindowState == WindowState.Normal ? WindowState.Maximized : WindowState.Normal;
                    });

                    eventInfo.RemoveEventHandler(d, clickEventHandler);
                    eventInfo.AddEventHandler(d, clickEventHandler);
                    break;

                case WindowControlAction.None:
                    break;
            }
        }

        #endregion ControlAction
    }
}