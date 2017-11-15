using System;
using System.Diagnostics;
using System.Windows;

namespace PraiseHim.Rejoice.WpfWindowToolkit.Base
{
    public class ViewModelBaseSubEx : BindableBase, IOpenWindow
    {
        public OpenWindowInfo OpenWindow { get; set; }

        public virtual void ShowWindow()
        {
            if (OpenWindow == null || OpenWindow.WindowType == null)
            {
                throw new ArgumentNullException(nameof(OpenWindow.WindowType), "WindowType cannot be null");
            }

            try
            {
                var windowObj = Activator.CreateInstance(OpenWindow.WindowType);
                var window = windowObj as Window;

                if (OpenWindow.IsModal)
                {
                    window.ShowDialog();
                }
                else
                {
                    window.Show();
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.ToString());
            }
        }
    }
}