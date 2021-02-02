import { Injectable } from '@angular/core';
import { KeycloakService } from 'keycloak-angular';
import { KeycloakAuthorizationService } from './keycloakAuthorization.service';

@Injectable({
  providedIn: 'root',
})
export class SecurityService {
  private readonly resource = "blog-api";

  constructor(
    private keycloak: KeycloakService,
    private keycloakAuthorization: KeycloakAuthorizationService) {
  }

  public isEditor() {
    return this.keycloak.isUserInRole('editor', this.resource);
  }

  public isPublisher() {
    return this.keycloak.isUserInRole('publisher', this.resource);
  }

  public async canUnpublish(): Promise<boolean> {
    if (!(await this.keycloak.isLoggedIn())) {
      return false;
    }

    try {
      return await this.keycloakAuthorization.hasEntitlement(this.resource, "Posts", "Unpublish");
    } catch (error) {
      return false;
    }
  }
}
