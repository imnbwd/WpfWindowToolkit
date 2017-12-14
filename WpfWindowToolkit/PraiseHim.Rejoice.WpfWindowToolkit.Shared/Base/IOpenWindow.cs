using System;

namespace PraiseHim.Rejoice.WpfWindowToolkit.Base
{
    /// <summary>
    /// Provides property and method for opening a new Window.
    /// </summary>
    public interface IOpenWindow
    {
        /// <summary>
        /// Open a window with the specific window info.
        /// </summary>
        /// <param name="openWindowInfo">The info of the window to be opened</param>
        void ShowWindow(OpenWindowInfo openWindowInfo);
    }

    /// <summary>
    /// Provides property and method for opening a new Window and handling the value.
    /// </summary>
    /// <typeparam name="TReturnValue">The data type of the return value</typeparam>
    public interface IOpenWindow2<TReturnValue> : IOpenWindow
    {
        /// <summary>
        /// Open a window with the specific window info, and handle the return value when the target window closed.
        /// </summary>
        /// <param name="openWindowInfo">The info of the window to be opened</param>
        /// <param name="action">The <see cref="Action"/> to handle the return value</param>
        void ShowWindow(OpenWindowInfo openWindowInfo, Action<TReturnValue> action);
    }

    /// <summary>
    /// Provides property and method for opening a new Window and handling the value.
    /// </summary>    
    public interface IOpenWindow2 : IOpenWindow
    {
        /// <summary>
        /// Open a window with the specific window info, and handle the return value when the target window closed.
        /// </summary>
        /// <param name="openWindowInfo">The info of the window to be opened</param>
        /// <param name="action">The <see cref="Action"/> to handle the return value</param>
        void ShowWindow(OpenWindowInfo openWindowInfo, Action<object> action);
    }
}