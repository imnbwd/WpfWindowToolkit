using PraiseHim.Rejoice.WpfWindowToolkit.Base;

namespace WpfWindowToolkit.Demo.ViewModels
{
    public class Window1ViewModel : ViewModelBaseData<string>
    {
        private string _parameter;

        /// <summary>
        /// Get or set Parameter value
        /// </summary>
        public string Parameter
        {
            get { return _parameter; }
            set { Set(ref _parameter, value); }
        }

        protected override string InternalData { get; set; }

        public void ViewLoaded()
        {
            Parameter = InternalData;
        }
    }
}