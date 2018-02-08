using PraiseHim.Rejoice.WpfWindowToolkit.Base;
using System.Linq;
using System.Windows;

namespace PraiseHim.Rejoice.WpfWindowToolkit.Utilities
{
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
        internal static object TryGetReturnValue(this Window window)
        {
            object returnValue = null;

            if (window.DataContext != null)
            {
                if (window.DataContext is IWindowReturnValue)
                {
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
                        returnValue = geTypeInterface.GetProperty(nameof(IWindowReturnValue.ReturnValue))?.GetValue(window.DataContext, null);
                    }
                }
            }

            return returnValue;
        }
    }
}