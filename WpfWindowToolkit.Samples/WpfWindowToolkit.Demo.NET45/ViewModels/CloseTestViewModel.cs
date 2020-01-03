using PraiseHim.Rejoice.WpfWindowToolkit.Base;
using System;

namespace WpfWindowToolkit.Demo.ViewModels
{
    public class CloseTestViewModel : BindableBase, IClosable
    {
        public CloseTestViewModel()
        {
            TestCloseCommand = new RelayCommand(TestClose);
        }

        public Action CloseWindow { get; set; }

        public RelayCommand TestCloseCommand { get; set; }

        private void TestClose()
        {
            // Your logic here
            // do something            

            // Finally, close the window
            CloseWindow?.Invoke();
        }
    }
}