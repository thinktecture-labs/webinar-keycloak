export const environment = {
  production: true,
  apiBaseUrl: "http://localhost:5000",
  keycloak: {
    config: {
      url: 'http://localhost:8080/auth',
      realm: 'webinar',
      clientId: 'blog-frontend',
    },
    loadUserProfileAtStartUp: false
  }
};