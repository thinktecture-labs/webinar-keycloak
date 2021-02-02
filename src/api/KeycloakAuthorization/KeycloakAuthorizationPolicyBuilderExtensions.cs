using blog_api.KeycloakAuthorization;
using Microsoft.AspNetCore.Authorization;

namespace Microsoft.Extensions.DependencyInjection
{

    public static class KeycloakAuthorizationPolicyBuilderExtensions
    {
        /// <summary>
        /// Adds a Keycloak requirement to the Requirements.
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <param name="resource">The resource.</param>
        /// <param name="scope">The scope.</param>
        /// <returns></returns>
        public static AuthorizationPolicyBuilder RequiresKeycloakEntitlement(this AuthorizationPolicyBuilder builder, string resource, string scope)
        {
            builder.AddRequirements(new KeycloakRequirement($"{resource}#{scope}"));
            return builder;
        }
    }
}
