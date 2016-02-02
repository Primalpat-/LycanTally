using System;

namespace LycanTally.Core.Entities
{
    public partial class Article
    {
        public int ID { get; set; }
        public int ThreadID { get; set; }
        public int UserID { get; set; }
        public string Link { get; set; }
        public DateTime PostDate { get; set; }
        public DateTime EditDate { get; set; }
        public int NumEdits { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public virtual Thread Thread { get; set; }
        public virtual User User { get; set; }
    }
}
