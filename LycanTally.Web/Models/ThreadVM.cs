using LycanTally.Core.Entities;
using System.Collections.Generic;

namespace LycanTally.Web.Models
{
    public class ThreadVM
    {
        public ThreadVM()
        {
            this.userItems = new List<UserItemVM>();
        }
        public Thread Thread { get; set; }
        public int totalDays { get; set; }
        public List<UserItemVM> userItems { get; set; }
    }
}