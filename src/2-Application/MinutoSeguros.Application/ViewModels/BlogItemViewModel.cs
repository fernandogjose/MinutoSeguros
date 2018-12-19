using System;
using System.Collections.Generic;

namespace MinutoSeguros.Application.ViewModels
{
    public class BlogItemViewModel
    {
        public string Title { get; set; }

        public string Link { get; set; }

        public string Comments { get; set; }

        public DateTime PubDate { get; set; }

        public List<string> Categories { get; set; }

        public string Description { get; set; }

        public string Image { get; set; }

        public List<BlogBestWordViewModel> BestWords{ get; set; }
    }
}