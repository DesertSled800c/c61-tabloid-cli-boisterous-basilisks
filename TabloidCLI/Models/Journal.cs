using System;

namespace TabloidCLI.Models
{
	public class Journal
	{
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreateDateTime { get; set; }

        public string JournalSummary
        {
            get
            {
                return $"{Title}: {Content}. Created On: {CreateDateTime}.";
            }
        }
        public override string ToString()
        {
            return JournalSummary;
        }

    }
}

