using System;
using System.Data.Entity;

namespace MvcBlog.Models
{
    public class Blog
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Category { get; set; }
        public string Slug { get; set; }
        public DateTime Date { get; set; }
        public string Body { get; set; }
    }

    public class BlogDBContext : DbContext
    {
        public DbSet<Blog> Blog { get; set; }
    }
}