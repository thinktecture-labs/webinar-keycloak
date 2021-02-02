using blog_api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace blog_api.Controllers
{
    /// <summary>
    /// Controller to handle posts
    /// </summary>
    /// <seealso cref="Controller" />
    [ApiController]
    [Route("api/[controller]")]
    public class Posts : Controller
    {
        private readonly BlogContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="Posts"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public Posts(BlogContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Endpoint to retrieve a list of all posts.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<PostListModel[]> Index()
        {
            var query = _context.Posts.AsQueryable();

            if (!User.IsEditorOrPublisher())
            {
                query = query.Where(p => p.PublishDate != null);
            }

            query = query.OrderBy(p => p.PublishDate);

            return await query
                .Select(p => new PostListModel(p.Id, p.Excerpt, p.PublishDate, p.Title))
                .ToArrayAsync();
        }

        /// <summary>
        /// Endpoint to retrieve a single post.
        /// </summary>
        /// <param name="id">The identifier of the post.</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Post))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Post>> Index(int id)
        {
            var query = _context.Posts.AsQueryable();
            Post? post = null;

            if (!User.IsEditorOrPublisher())
            {
                query = query.Where(p => p.PublishDate != null);
            }

            post = await query.FirstOrDefaultAsync(p => p.Id == id);
            return (post != null) ? post : NotFound();
        }

        /// <summary>
        /// Endpoint to create a new post.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        [Authorize(Roles = Roles.Editor)]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Post))]
        public async Task<Post> Post(PostInputModel model)
        {
            var post = new Post(model.Body, model.Excerpt, model.Title);

            await _context.Posts.AddAsync(post);
            await _context.SaveChangesAsync();

            return post;
        }

        /// <summary>
        /// Endpoint to publish a post.
        /// </summary>
        /// <param name="id">The identifier of the post.</param>
        /// <returns></returns>
        [Authorize(Roles = Roles.Publisher)]
        [HttpPost("{id}/publish")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Post))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Post>> Publish(int id)
        {
            var post = await _context.Posts.FindAsync(id);
            if (post == null)
            {
                return NotFound();
            }

            post.PublishDate = DateTime.Now;

            await _context.SaveChangesAsync();

            return post;
        }

        /// <summary>
        /// Endpoint to unpublish a post.
        /// </summary>
        /// <param name="id">The identifier of the post.</param>
        /// <returns></returns>
        [Authorize(Policies.CanUnpublish)]
        [HttpPost("{id}/unpublish")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Post))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Post>> Unpublish(int id)
        {
            var post = await _context.Posts.FindAsync(id);
            if (post == null)
            {
                return NotFound();
            }

            post.PublishDate = null;

            await _context.SaveChangesAsync();

            return post;
        }

        /// <summary>
        /// Endpoint to delete a post.
        /// </summary>
        /// <param name="id">The identifier of the post.</param>
        /// <returns></returns>
        [Authorize(Roles = Roles.Editor)]
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Delete(int id)
        {
            var post = await _context.Posts.FindAsync(id);
            if (post == null)
            {
                return NotFound();
            }

            if (post.PublishDate != null)
            {
                return Forbid();
            }

            _context.Posts.Remove(post);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
