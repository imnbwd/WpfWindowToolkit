using PraiseHim.Rejoice.WpfWindowToolkit.Base;
using System;

namespace WpfWindowToolkit.Demo.Models
{
    public class Friend : BindableBase
    {
        /// <summary>
        /// Get or set BirthDate value
        /// </summary>
        public DateTime BirthDate { get; set; }

        /// <summary>
        /// Get or set Email value
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Get or set Id value
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Get or set IsDeveloper value
        /// </summary>
        public bool IsDeveloper { get; set; }

        /// <summary>
        /// Get or set Name value
        /// </summary>
        public string Name { get; set; }

        public override string ToString()
        {
            return $"Friend: {Name},{Email},{BirthDate.ToShortDateString()}";
        }
    }
}