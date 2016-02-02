using System.Collections.Generic;

namespace LycanTally.Core.Entities
{
    public partial class Thread
    {
        public Thread()
        {
            this.Articles = new List<Article>();
        }

        public int ID { get; set; }
        public int NumArticles { get; set; }
        public string Link { get; set; }
        public string TermsOfUse { get; set; }
        public string Subject { get; set; }
        public virtual ICollection<Article> Articles { get; set; }
    }
}
