import { Component, Input, OnInit } from '@angular/core';
import { PostListItem } from '../../models/post-list-item';
import { BlogService } from '../../services/blog.service';
import { SecurityService } from '../../services/security.service';

@Component({
  selector: 'app-post-item',
  templateUrl: './post-item.component.html',
  styleUrls: ['./post-item.component.css'],
})
export class PostItemComponent implements OnInit {
  @Input()
  public post: PostListItem | null = null;

  public canUnpublish = false;

  constructor(private blog: BlogService, public security: SecurityService) {}

  async ngOnInit(): Promise<void> {
    this.canUnpublish = await this.security.canUnpublish();
  }

  public async publish() {
    if (this.post != null) {
      this.post = await this.blog.publish(this.post.id);
    }
  }

  public async unpublish() {
    if (this.post != null) {
      this.post = await this.blog.unpublish(this.post.id);
    }
  }

  public async delete() {
    if (this.post != null) {
      this.post = await this.blog.delete(this.post.id);
      this.post = null;
    }
  }
}
