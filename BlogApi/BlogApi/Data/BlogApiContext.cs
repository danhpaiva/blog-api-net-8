using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BlogApi.Models;

namespace BlogApi.Data
{
    public class BlogApiContext : DbContext
    {
        public BlogApiContext (DbContextOptions<BlogApiContext> options)
            : base(options)
        {
        }

        public DbSet<BlogApi.Models.Blog> Blog { get; set; } = default!;
        public DbSet<BlogApi.Models.Post> Post { get; set; } = default!;
    }
}
