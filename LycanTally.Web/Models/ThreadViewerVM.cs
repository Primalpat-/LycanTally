using LycanTally.Core.Entities;
using System.Collections.Generic;

namespace LycanTally.Web.Models
{
    public class ThreadViewerVM
    {
        public int ThreadID { get; set; }
        public Thread Thread { get; set; }
        public int totalDays { get; set; }
        public List<UserItemVM> userItems { get; set; }
    }
}