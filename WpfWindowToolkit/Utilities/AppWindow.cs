using PraiseHim.Rejoice.WpfWindowToolkit.Base;
using System;
using System.Linq;
using System.Windows;

namespace PraiseHim.Rejoice.WpfWindowToolkit.Utilities
{
    /// <summary>
    /// Indicates whether a window has value returned
    /// </summary>
    internal class ReturnValueInfo
    {
        public bool HasValue { get; set; }
        public object Value { get; set; }
    }

    internal static class AppWindow
    {
        /// <summary>
        /// Get the activated Window of the application.
        /// </summary>
        /// <returns></returns>
        internal static Window GetCurrentActivatedWindow()
        {
            return Application.Current.Windows.OfType<Window>().SingleOrDefault(w => w.IsActive);
        }

        /// <summary>
        /// Get the value of the ReturnValue property from the specified window's data context, see <see cref="IWindowReturnValue"/> for more info.
        /// </summary>
        /// <param name="window"></param>
        /// <returns></returns>
        internal static ReturnValueInfo TryGetReturnValue(this Window window)
        {
            bool hasReturnValue = false;
            object returnValue = null;

            if (window.DataContext != null)
            {
                if (window.DataContext is IWindowReturnValue)
                {
                    hasReturnValue = true;
                    returnValue = (window.DataContext as IWindowReturnValue).ReturnValue;
                }
                else
                {
                    // generic interface
                    var geTypeInterface = window.DataContext.GetType().GetInterfaces()
                        .Where(t => t.IsGenericType)
                        .FirstOrDefault(t => t.GetGenericTypeDefinition() == typeof(IWindowReturnValue<>));

                    if (geTypeInterface != null)
                    {
                        hasReturnValue = true;
                        returnValue = geTypeInterface.GetProperty(nameof(IWindowReturnValue.ReturnValue))?.GetValue(window.DataContext, null);
                    }
                }
            }

            return new ReturnValueInfo { HasValue = hasReturnValue, Value = returnValue };
        }
    }
}