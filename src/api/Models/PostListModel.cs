using System;

namespace blog_api.Models
{
    public record PostListModel(int Id, string Excerpt, DateTime? PublishDate, string Title);
}
