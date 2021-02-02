namespace blog_api
{
    /// <summary>
    /// Policies defined for the application.
    /// </summary>
    public static class Policies
    {
        /// <summary>
        /// Name of the policy to check if a user can unpublish posts.
        /// </summary>
        public const string CanUnpublish = "CanUnpublish";
    }
}
