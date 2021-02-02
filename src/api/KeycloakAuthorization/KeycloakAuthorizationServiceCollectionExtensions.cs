using blog_api.KeycloakAuthorization;
using Microsoft.AspNetCore.Authorization;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class KeycloakAuthorizationServiceCollectionExtensions
    {
        /// <summary>
        /// Add keycloak authorization components.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <param name="configure">The configure.</param>
        /// <returns></returns>
        public static IServiceCollection AddKeycloakAuthorization(this IServiceCollection services, Action<KeycloakAuthorizationOptions> configure)
        {
            services.Configure(configure);
            services.AddHttpContextAccessor();
            services.AddSingleton<IAuthorizationHandler, KeycloakAuthorizationHandler>();

            return services;
        }
    }
}
