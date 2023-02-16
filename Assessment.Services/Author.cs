using SynicTools;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Assessment.Data
{
    public class Author : BaseEntity
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public ICollection<Blog> Blogs { get; set; }

        public Author()
        {
            this.Blogs = new Collection<Blog>();
        }
    }
}
