# Demo for Keycloak Integration in ASP.NET Core and Angular.

## Start

Use 

```
docker-compose up
```

to start Keycloak, the API and Angular Frontend.

## Urls

| System   | Url                   |
| -------- | --------------------- |
| Keycloak | http://localhost:8080 |
| API      | http://localhost:5000 |
| Frontend | http://localhost:4200 |

## Logins

| Username  | Password  | Description                                     |
| --------- | --------- | ----------------------------------------------- |
| editor    | editor    | Can create posts and delete not published posts |
| publisher | publisher | Can publish and unpublish posts                 |
| legal     | lega      | Can unpublish posts                             |


## Point of interest

### API
| File                                                                                                                   | Description                                                   |
| ---------------------------------------------------------------------------------------------------------------------- | ------------------------------------------------------------- |
| [startup.cs](src/api/Startup.cs#L67-L76)                                                                               | Configures Jwt Bearer authentication to use Keycloak          |
| [KeycloakRolesClaimsTransformation.cs](src/api/KeycloakRolesClaimsTransformation.cs)                                   | Transforms keycloak role claims to be compatible with ASP.NET |
| [startup.cs](src/api/Startup.cs#L80-L87)                                                                               | Configures authorization with Keycloak                        |
| [KeycloakAuthorization\KeycloakAuthorizationHandler.cs](src/api/KeycloakAuthorization/KeycloakAuthorizationHandler.cs) | Authorization handler that makes decision call to Keycloak    |

### Frontend

| File                                                                                               | Description                                                 |
| -------------------------------------------------------------------------------------------------- | ----------------------------------------------------------- |
| [keycloakAuthorization.service.ts](src/frontend/src/app/services/keycloakAuthorization.service.ts) | Authorization Service that makes decision call to Keycloak. |
| [security.service.ts](src/frontend/src/app/services/security.service.ts)                           | Security service that checks the roles of the user.         |
| [auth.guard.ts](src/frontend/src/app/services/auth.guard.ts)                                       | Auth guard to protect routes.                               |