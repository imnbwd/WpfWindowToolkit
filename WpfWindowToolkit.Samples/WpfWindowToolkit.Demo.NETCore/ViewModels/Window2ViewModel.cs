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
            set { Set(ref _currentFriend, value); }
        }
        public void ViewLoaded()
        {
            this.CurrentFriend = InternalData;
        }
    }
}
