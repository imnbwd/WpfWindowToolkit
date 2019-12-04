using PraiseHim.Rejoice.WpfWindowToolkit.Base;
using System;
using System.Collections.Generic;
using WpfWindowToolkit.Demo.Models;

namespace WpfWindowToolkit.Demo.ViewModels
{
    public class ReturnValueTestWindowViewModel : BindableBase, IWindowReturnValue<Friend>
    {
        private List<Friend> _allFriends;
        private Friend _selectedFriend;

        /// <summary>
        /// Get or set AllFriends value
        /// </summary>
        public List<Friend> AllFriends
        {
            get { return _allFriends; }
            set { Set(ref _allFriends, value); }
        }

        public Friend ReturnValue { get; set; }

        /// <summary>
        /// Get or set SelectedFriend value
        /// </summary>
        public Friend SelectedFriend
        {
            get { return _selectedFriend; }
            set { Set(ref _selectedFriend, value); }
        }

        /// <summary>
        /// Set the ReturnValue property of <see cref="IWindowReturnValue"/>
        /// </summary>
        public void SetReturnValue() => ReturnValue = SelectedFriend;

        public void ViewLoaded()
        {
            var friends = new List<Friend>();
            friends.Add(new Friend { Id = "1", Name = "Jim", Email = "jim@xxx.com", BirthDate = new DateTime(1990, 4, 15) });
            friends.Add(new Friend { Id = "2", Name = "Kailey", Email = "kailey@xxx.com", BirthDate = new DateTime(1990, 7, 6) });
            friends.Add(new Friend { Id = "3", Name = "Walter", Email = "walter@xxx.com", BirthDate = new DateTime(1990, 9, 29) });
            friends.Add(new Friend { Id = "4", Name = "Gale", Email = "gale@xxx.com", BirthDate = new DateTime(1990, 10, 31) });
            AllFriends = friends;
        }
    }
}