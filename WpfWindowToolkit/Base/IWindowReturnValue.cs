namespace PraiseHim.Rejoice.WpfWindowToolkit.Base
{
    /// <summary>
    /// Supports returning a value after a window closed.
    /// </summary>
    public interface IWindowReturnValue
    {
        /// <summary>
        /// Get or set the value to be returned.
        /// </summary>
        object ReturnValue { get; set; }
    }


    /// <summary>
    /// Supports returning a value after a window closed.
    /// </summary>
    /// <typeparam name="T">The data type of return value</typeparam>
    public interface IWindowReturnValue<T>
    {
        /// <summary>
        /// Get or set the value to be returned.
        /// </summary>
        T ReturnValue { get; set; }
    }
}