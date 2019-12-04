using System;

namespace PraiseHim.Rejoice.WpfWindowToolkit.Base
{
    /// <summary>
    /// Provides action to close a Window form its view model. In order to do so, that view modle should implement this interface.
    /// </summary>
    public interface IClosable
    {
        /// <summary>
        /// Get or set CloseWindow action, this action should be a method that could close the corresponding Window.
        /// </summary>
        Action CloseWindow { get; set; }
    }
}