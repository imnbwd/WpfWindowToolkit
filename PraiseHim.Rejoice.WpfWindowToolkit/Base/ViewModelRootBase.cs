namespace PraiseHim.Rejoice.WpfWindowToolkit.Base
{
    public abstract class ViewModelRootBase : BindableBase
    {
        public abstract object Data { get; set; }
    }

    public abstract class ViewModelBaseData<T> : ViewModelRootBase
    {
        public override object Data
        {
            get { return this.InternalData; }
            set { this.InternalData = (T)value; }
        }

        protected abstract T InternalData { get; set; }
    }
}