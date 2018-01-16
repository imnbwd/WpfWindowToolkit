using System.Linq;
using System.Windows;

namespace PraiseHim.Rejoice.WpfWindowToolkit.Utilities
{
    internal class AppWindow
    {
        /// <summary>
        /// Get the activated Window of the application
        /// </summary>
        /// <returns></returns>
        internal static Window GetCurrentActivatedWindow()
        {
            return Application.Current.Windows.OfType<Window>().SingleOrDefault(w => w.IsActive);
        }
    }
}