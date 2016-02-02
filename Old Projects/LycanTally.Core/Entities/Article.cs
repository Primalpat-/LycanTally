namespace LycanTally.Core.Entities
{
    public partial class Article
    {
        public int ID { get; set; }
        public int ThreadID { get; set; }
        public string UserName { get; set; }
        public string Link { get; set; }
        public System.DateTime PostDate { get; set; }
        public System.DateTime EditDate { get; set; }
        public int NumEdits { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public virtual Thread Thread { get; set; }
    }
}
