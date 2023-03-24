using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using TabloidCLI.Models;

namespace TabloidCLI
{
  public class Note
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreateDateTime { get; set; }
        public int PostId { get; set; }
        public Post post { get; set; }
        public List<Note> notes { get; set; } = new List<Note>();

        public override string ToString()
        {
            return $"{Title}";
        }

    }  
}
