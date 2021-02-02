import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { CreatePostModel } from '../models/create-post-model';
import { Post } from '../models/post';
import { PostListItem } from '../models/post-list-item';

@Injectable({
  providedIn: 'root',
})
export class BlogService {
  constructor(private http: HttpClient) {}

  public get(): Promise<PostListItem[]> {
    return this.http
      .get<PostListItem[]>(`${environment.apiBaseUrl}/api/posts`)
      .toPromise<PostListItem[]>();
  }

  public detail(id: number): Promise<Post> {
    return this.http
      .get<Post>(`${environment.apiBaseUrl}/api/posts/${id}`)
      .toPromise<Post>();
  }

  public create(post: CreatePostModel): Promise<CreatePostModel> {
    return this.http
      .post<CreatePostModel>(`${environment.apiBaseUrl}/api/posts`, post)
      .toPromise<CreatePostModel>();
  }

  public publish(id: number): Promise<Post> {
    return this.http
      .post<Post>(`${environment.apiBaseUrl}/api/posts/${id}/publish`, null)
      .toPromise<Post>();
  }

  public unpublish(id: number): Promise<Post> {
    return this.http
      .post<Post>(`${environment.apiBaseUrl}/api/posts/${id}/unpublish`, null)
      .toPromise<Post>();
  }

  public delete(id: number): Promise<any> {
    return this.http
      .delete(`${environment.apiBaseUrl}/api/posts/${id}`)
      .toPromise();
  }
}
