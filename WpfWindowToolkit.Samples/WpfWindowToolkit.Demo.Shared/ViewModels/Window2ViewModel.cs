using System;
using System.Collections.Generic;
using System.Text;
using WpfWindowToolkit.Demo.Models;

namespace WpfWindowToolkit.Demo.ViewModels
{
    public class Window2ViewModel : PraiseHim.Rejoice.WpfWindowToolkit.Base.ViewModelBaseData<Friend>
    {
        protected override Friend InternalData { get; set; }

        private Friend _currentFriend;
        /// <summary>
        /// Get or set CurrentFriend value
        /// </summary>
        public Friend CurrentFriend
        {
            get { return _currentFriend; }
#if NET45
            set { Set(ref _currentFriend, value); }
#elif NET40
            set { _currentFriend = value; this.OnPropertyChanged("CurrentFriend"); }
#endif
        }
        public void ViewLoaded()
        {
            this.CurrentFriend = InternalData;
        }
    }
}
