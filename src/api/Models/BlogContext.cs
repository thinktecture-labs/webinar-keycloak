using Microsoft.EntityFrameworkCore;

namespace blog_api.Models
{
    /// <summary>
    /// Database context that holds the blog data
    /// </summary>
    /// <seealso cref="Microsoft.EntityFrameworkCore.DbContext" />
    public class BlogContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BlogContext"/> class.
        /// </summary>
        /// <param name="options">The options.</param>
        public BlogContext(DbContextOptions<BlogContext> options) : base(options)
        {
        }

        /// <summary>
        /// Collection to hold posts.
        /// </summary>
        /// <value>
        /// The posts.
        /// </value>
        public DbSet<Post> Posts => Set<Post>();
    }
}
