using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace PraiseHim.Rejoice.WpfWindowToolkit.Utilities
{
    public class ApplicationHelper
    {

        public static Window GetCurrentActivatedWindow()
        {
            return Application.Current.Windows.OfType<Window>().SingleOrDefault(m => m.IsActive);
        }

        public static T GetWindow<T>() where T : Window
        {
            return Application.Current.Windows.OfType<T>().FirstOrDefault();
        }

        public static IEnumerable<T> GetWindows<T>() where T : Window
        {
            return Application.Current.Windows.OfType<T>();
        }

        public static bool IsWindowOpen<T>(string name = "") where T : Window
        {
            return string.IsNullOrEmpty(name)
               ? Application.Current.Windows.OfType<T>().Any()
               : Application.Current.Windows.OfType<T>().Any(w => w.Name.Equals(name));
        }

        public static bool IsWindowOpen(Type windowType)
        {
            return Application.Current.Windows.OfType<Window>().Any(m => m.GetType() == windowType);
        }

        public static void ShowOrActivateWindow(Type windowType)
        {
            if (IsWindowOpen(windowType))
            {
                GetWindow(windowType).Activate();
            }
            else
            {
                Window window = Activator.CreateInstance(windowType) as Window;
                if (window == null)
                {
                    throw new InvalidOperationException("cannot create window with target window type");
                }

                window.ShowInTaskbar = true;
                window.Show();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="windowType"></param>
        /// <returns>如果不存在，返回null，否则返回打开的窗口实例</returns>
        public static Window GetWindow(Type windowType)
        {
            return Application.Current.Windows.OfType<Window>().FirstOrDefault(m => m.GetType() == windowType);
        }
    }
}
