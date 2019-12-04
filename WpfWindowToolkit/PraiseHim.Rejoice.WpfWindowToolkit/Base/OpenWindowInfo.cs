using System;

namespace PraiseHim.Rejoice.WpfWindowToolkit.Base
{
    /// <summary>
    /// Identifies the info for a window to be opened.
    /// </summary>
    public class OpenWindowInfo
    {
        /// <summary>
        /// Get or set whether the windows is modal or not. The defalut value is true.
        /// </summary>
        public bool IsModal { get; set; } = true;

        /// <summary>
        /// Get or set the parameter to pass to the new window.
        /// </summary>
        public object Parameter { get; set; }

        /// <summary>
        /// Get or set the type of the window to be opened.
        /// </summary>
        public Type WindowType { get; set; }
    }
}