using SynicTools;
using System;
using System.Collections.Generic;
using System.Text;

namespace Assessment.Services.Filters
{
    public class BlogFilters : EntityFilters
    {

        public string? Title { get; set; }
        public int? AuthorID { get; set; }
        public string? Body { get; set; }
    }
}
