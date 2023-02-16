using System;
using System.ComponentModel.DataAnnotations;

namespace Assessment.Web.Models
{
    public class BlogViewModel
    {

        public int Id { get; set; }
        public string Title { get; set; }

        public string Body { get; set; }

        public int AuthorId { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
