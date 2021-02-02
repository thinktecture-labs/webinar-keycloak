import { APP_INITIALIZER, NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './components/app/app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { PostListComponent } from './components/post-list/post-list.component';
import { BlogService } from './services/blog.service';
import { HeaderComponent } from './components/header/header.component';
import { PostDetailComponent } from './components/post-detail/post-detail.component';
import { KeycloakAngularModule, KeycloakService } from 'keycloak-angular';
import { NewPostComponent } from './components/new-post/new-post.component';
import { ReactiveFormsModule } from '@angular/forms';
import { SecurityService } from './services/security.service';
import { AuthGuard } from './services/auth.guard';
import { PostItemComponent } from './components/post-item/post-item.component';
import { environment } from '../environments/environment';
import { KeycloakAuthorizationService } from './services/keycloakAuthorization.service';

function initializeKeycloak(keycloak: KeycloakService): () => Promise<void> {
  return async () => {
    await keycloak.init(environment.keycloak);
  };
}

@NgModule({
  declarations: [
    AppComponent,
    PostListComponent,
    HeaderComponent,
    PostDetailComponent,
    NewPostComponent,
    PostItemComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    BrowserAnimationsModule,
    KeycloakAngularModule,
    ReactiveFormsModule
  ],
  providers: [
    AuthGuard,
    BlogService,
    SecurityService,
    {
      provide: APP_INITIALIZER,
      useFactory: initializeKeycloak,
      multi: true,
      deps: [KeycloakService],
    },
    KeycloakAuthorizationService
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}
