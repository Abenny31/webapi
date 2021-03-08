using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using webapi1.Models;

namespace webapi1.Data
{
    public class BlogPostsContext : DbContext
    {
        public BlogPostsContext (DbContextOptions<BlogPostsContext> options)
            : base(options)
        {
        }

        public DbSet<BlogPost> BlogPosts { get; set; }
    }
}
