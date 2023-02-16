using SynicTools;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Assessment.Data
{
    public class Blog : BaseEntity
    {
        public string Title { get; set; }

        public string Body { get; set; }

        public int AuthorID { get; set; }

        [Column("AuthorID")]
        public Author Author { get; set; }

        public DateTime Created { get; set; }

        public Blog()
        {
            this.Created = DateTime.UtcNow;
        }
    }
}
