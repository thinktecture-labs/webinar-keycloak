using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Net.Http;

namespace blog_api.KeycloakAuthorization
{
    /// <summary>
    /// Defines options for Keycloak authorization
    /// </summary>
    public class KeycloakAuthorizationOptions
    {
        /// <summary>
        /// Gets or sets the required aithentication scheme that holds the token.
        /// </summary>
        /// <value>
        /// The required scheme.
        /// </value>
        public string RequiredScheme { get; set; } = JwtBearerDefaults.AuthenticationScheme;

        /// <summary>
        /// Gets or sets the token endpoint.
        /// </summary>
        /// <value>
        /// The token endpoint.
        /// </value>
        public string TokenEndpoint { get; set; }

        /// <summary>
        /// Gets or sets the backchannel handler.
        /// </summary>
        /// <value>
        /// The backchannel handler.
        /// </value>
        public HttpMessageHandler BackchannelHandler { get; set; } = new HttpClientHandler();

        /// <summary>
        /// Gets or sets the audience.
        /// </summary>
        /// <value>
        /// The audience.
        /// </value>
        public string Audience { get; set; }
    }
}
