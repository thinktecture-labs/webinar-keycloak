using System;

namespace blog_api.Models
{
    /// <summary>
    /// Represents a blog post
    /// </summary>
    public class Post
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the body.
        /// </summary>
        /// <value>
        /// The body.
        /// </value>
        public string Body { get; set; }

        /// <summary>
        /// Gets or sets the excerpt.
        /// </summary>
        /// <value>
        /// The excerpt.
        /// </value>
        public string Excerpt { get; set; }

        /// <summary>
        /// Gets or sets the publish date.
        /// </summary>
        /// <value>
        /// The publish date.
        /// </value>
        public DateTime? PublishDate { get; set; }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>
        /// The title.
        /// </value>
        public string Title { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Post"/> class.
        /// </summary>
        /// <param name="body">The body.</param>
        /// <param name="excerpt">The excerpt.</param>
        /// <param name="title">The title.</param>
        public Post(string body, string excerpt, string title)
        {
            Body = body;
            Excerpt = excerpt;
            Title = title;
        }
    }
}
