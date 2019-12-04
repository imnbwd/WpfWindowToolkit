using PraiseHim.Rejoice.WpfWindowToolkit.Utilities;
using System;
using System.Linq;
using System.Windows;

namespace PraiseHim.Rejoice.WpfWindowToolkit.Base
{
    /// <summary>
    /// Provides the way to open a window.
    /// </summary>
    public class ViewModelBaseEx : BindableBase, IOpenWindow, IOpenWindow2
    {
        /// <summary>
        /// Open a window with the specific window info.
        /// </summary>
        /// <param name="openWindowInfo">The info of the window to be opened.</param>
        public virtual void ShowWindow(OpenWindowInfo openWindowInfo)
        {
            if (openWindowInfo == null || openWindowInfo.WindowType == null)
            {
                throw new ArgumentNullException(nameof(openWindowInfo), "WindowType cannot be null");
            }

            Window window = null;
            object windowObj = null;

            try
            {
                windowObj = Activator.CreateInstance(openWindowInfo.WindowType);
                window = windowObj as Window;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Cannot create a window with the given type", ex);
            }

            if (openWindowInfo.Parameter != null && window.DataContext != null
                    && window.DataContext is ViewModelRootBase)
            {
                (window.DataContext as ViewModelRootBase).Data = openWindowInfo.Parameter;
            }
            
            if (openWindowInfo.IsModal)
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

        /// <summary>
        /// Open a window with the specific window info, and handle the return value when the target window closed.
        /// </summary>
        /// <param name="openWindowInfo">The info of the window to be opened.</param>
        /// <param name="action">The <see cref="Action"/> to handle the return value.</param>
        public virtual void ShowWindow(OpenWindowInfo openWindowInfo, Action<object> action)
        {
            if (openWindowInfo == null || openWindowInfo.WindowType == null)
            {
                throw new ArgumentNullException(nameof(openWindowInfo), "WindowType cannot be null");
            }

            Window window = null;
            object windowObj = null;

            try
            {
                windowObj = Activator.CreateInstance(openWindowInfo.WindowType);
                window = windowObj as Window;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Cannot create a window with the given type", ex);
            }

            if (openWindowInfo.Parameter != null && window.DataContext != null
                && window.DataContext is ViewModelRootBase)
            {
                (window.DataContext as ViewModelRootBase).Data = openWindowInfo.Parameter;
            }

            EventHandler closeEventHanlder = (s, e) =>
            {
                var vmType = window.DataContext.GetType();
                var geType = typeof(IWindowReturnValue<>);

                if (vmType is IWindowReturnValue)
                {
                    var value = (window.DataContext as IWindowReturnValue).ReturnValue;
                    action?.Invoke(value);
                }
                else
                {
                    // generic interface
                    var geTypeInterface = vmType.GetInterfaces()
                        .Where(t => t.IsGenericType)
                        .FirstOrDefault(t => t.GetGenericTypeDefinition() == typeof(IWindowReturnValue<>));

                    if (geTypeInterface != null)
                    {
                        var value = geTypeInterface.GetProperty(nameof(IWindowReturnValue.ReturnValue)).GetValue(window.DataContext, null);
                        action?.Invoke(value);
                    }
                }
            };

            window.Closed -= closeEventHanlder;
            window.Closed += closeEventHanlder;

            if (openWindowInfo.IsModal)
            {
                window.ShowDialog();
            }
            else
            {
                window.Show();
            }
        }
    }

    /// <summary>
    /// Provides the way to open a window and return a value.
    /// </summary>
    /// <typeparam name="TReturnValue">The data type of the value to be returned.</typeparam>
    public class ViewModelBaseEx<TReturnValue> : ViewModelBaseEx, IOpenWindow, IOpenWindow2<TReturnValue>
    {
        /// <summary>
        /// Open a window with the specific window info, and handle the return value when the target window closed.
        /// </summary>
        /// <param name="openWindowInfo">The info of the window to be opened</param>
        /// <param name="action">The <see cref="Action"/> to handle the return value</param>
        public virtual void ShowWindow(OpenWindowInfo openWindowInfo, Action<TReturnValue> action)
        {
            if (openWindowInfo == null || openWindowInfo.WindowType == null)
            {
                throw new ArgumentNullException(nameof(openWindowInfo), "WindowType cannot be null");
            }

            Window window = null;
            object windowObj = null;

            try
            {
                windowObj = Activator.CreateInstance(openWindowInfo.WindowType);
                window = windowObj as Window;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Cannot create a window with the given type", ex);
            }

            if (openWindowInfo.Parameter != null && window.DataContext != null
                && window.DataContext is ViewModelRootBase)
            {
                (window.DataContext as ViewModelRootBase).Data = openWindowInfo.Parameter;
            }

            EventHandler closeEventHanlder = (s, e) =>
            {
                var vmType = window.DataContext.GetType();
                var geType = typeof(IWindowReturnValue<>);

                if (vmType is IWindowReturnValue)
                {
                    var value = (window.DataContext as IWindowReturnValue).ReturnValue;
                    if (value != null)
                    {
                        action?.Invoke((TReturnValue)value);
                    }
                    else
                    {
                        action?.Invoke(default(TReturnValue));
                    }
                }
                else
                {
                    // generic interface
                    var geTypeInterface = vmType.GetInterfaces()
                        .Where(t => t.IsGenericType)
                        .FirstOrDefault(t => t.GetGenericTypeDefinition() == typeof(IWindowReturnValue<>));

                    if (geTypeInterface != null)
                    {
                        var value = geTypeInterface.GetProperty(nameof(IWindowReturnValue.ReturnValue)).GetValue(window.DataContext, null);
                        if (value != null)
                        {
                            action?.Invoke((TReturnValue)value);
                        }
                        else
                        {
                            action?.Invoke(default(TReturnValue));
                        }
                    }
                }
            };

            window.Closed -= closeEventHanlder;
            window.Closed += closeEventHanlder;

            if (openWindowInfo.IsModal)
            {
                window.ShowDialog();
            }
            else
            {
                window.Show();
            }
        }
    }
}