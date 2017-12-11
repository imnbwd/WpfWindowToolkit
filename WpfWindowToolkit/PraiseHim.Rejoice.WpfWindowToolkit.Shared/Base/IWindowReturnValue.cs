namespace PraiseHim.Rejoice.WpfWindowToolkit.Base
{
    /// <summary>
    /// Supports returning a value.
    /// </summary>
    public interface IWindowReturnValue
    {
        /// <summary>
        /// Get or set the value to be returned.
        /// </summary>
        object Value { get; set; }
    }
}