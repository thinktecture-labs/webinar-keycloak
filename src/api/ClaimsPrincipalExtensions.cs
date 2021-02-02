using System.Security.Claims;

namespace blog_api
{
    /// <summary>
    /// Extensions for claims principal
    /// </summary>
    public static class ClaimsPrincipalExtensions
    {
        /// <summary>
        /// Determines whether the user has the editor- or publisher-role.
        /// </summary>
        /// <param name="principal">The principal.</param>
        /// <returns>
        ///   <c>true</c> if the user has the editor- or publisher-role; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsEditorOrPublisher(this ClaimsPrincipal principal)
        {
            return principal.IsInRole(Roles.Editor) || principal.IsInRole(Roles.Publisher);
        }
    }
}
