namespace PraiseHim.Rejoice.WpfWindowToolkit.Base
{
    /// <summary>
    /// A view model base class, could receive data with the given data type.
    /// </summary>
    /// <typeparam name="T">The parameter data type.</typeparam>
    public abstract class ViewModelBaseData<T> : ViewModelRootBase
    {
        /// <summary>
        /// Get or set the data that passed to this view model
        /// </summary>
        public override object Data
        {
            get { return this.InternalData; }
            set { this.InternalData = (T)value; }
        }

        /// <summary>
        /// Get or set the data with a specifc type
        /// </summary>
        protected abstract T InternalData { get; set; }
    }

    /// <summary>
    /// ViewModelRootBase, inherits from <see cref="BindableBase"/> which implements <see cref="System.ComponentModel.INotifyPropertyChanged"/>, can be the base class of view model.
    /// </summary>
    public abstract class ViewModelRootBase : BindableBase
    {
        /// <summary>
        /// Get or set the data that passed to this view model
        /// </summary>
        public abstract object Data { get; set; }
    }
}