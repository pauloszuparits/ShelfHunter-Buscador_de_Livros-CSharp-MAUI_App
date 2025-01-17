using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiAppAlexandria
{
    public class Book
    {
        public string Title { get; set; }
        public string Authors { get; set; }
        public string Description { get; set; }
        public string Publisher { get; set; }
        public string Thumbnail { get; set; }

        public int Pages { get; set; }

        public string PublishedYear { get; set; }
    }
}
