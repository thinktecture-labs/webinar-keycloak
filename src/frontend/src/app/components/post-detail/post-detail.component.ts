import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Post } from '../../models/post';
import { BlogService } from '../../services/blog.service';

@Component({
  selector: 'app-post-detail',
  templateUrl: './post-detail.component.html',
  styleUrls: ['./post-detail.component.css']
})
export class PostDetailComponent implements OnInit {

  public post: Post | null = null;

  constructor(private route: ActivatedRoute, private blog: BlogService) { }

  async ngOnInit(): Promise<void> {
    this.post = await this.blog.detail(this.route.snapshot.params.id);
  }

}
