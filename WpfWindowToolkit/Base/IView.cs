namespace PraiseHim.Rejoice.WpfWindowToolkit.Base
{
    /// <summary>
    /// Provides methods for handling what to do after a view loaded.
    /// </summary>
    public interface IView
    {
        /// <summary>
        /// View loaded.
        /// </summary>
        void ViewLoaded();
    }

    /// <summary>
    /// Provides methods for handling what to do after a view loaded and unloaded.
    /// </summary>
    public interface IView2 : IView
    {
        /// <summary>
        /// View unloaded.
        /// </summary>
        void ViewUnloaded();
    }
}