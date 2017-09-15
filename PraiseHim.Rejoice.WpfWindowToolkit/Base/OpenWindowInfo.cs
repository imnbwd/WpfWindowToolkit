using System;

namespace PraiseHim.Rejoice.WpfWindowToolkit.Base
{
    /// <summary>
    /// The info described the window need to open
    /// </summary>
    public class OpenWindowInfo
    {
        public OpenWindowInfo()
        {
            IsModal = true;
        }

        public bool IsModal { get; set; }
        public object Parameter { get; set; }
        public Type WindowType { get; set; }
    }
}