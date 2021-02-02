using Microsoft.AspNetCore.Authorization;

namespace blog_api.KeycloakAuthorization
{
    public class KeycloakRequirement : IAuthorizationRequirement
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="KeycloakRequirement"/> class.
        /// </summary>
        /// <param name="policyName">Name of the policy.</param>
        public KeycloakRequirement(string policyName)
        {
            PolicyName = policyName;
        }

        /// <summary>
        /// Gets the name of the policy.
        /// </summary>
        /// <value>
        /// The name of the policy.
        /// </value>
        public string PolicyName { get; }
    }
}
