import { Component, OnInit } from '@angular/core';
import { promise } from 'protractor';
import { PostListItem } from '../../models/post-list-item';
import { BlogService } from '../../services/blog.service';

@Component({
  selector: 'app-post-list',
  templateUrl: './post-list.component.html',
  styleUrls: ['./post-list.component.css']
})
export class PostListComponent implements OnInit {

  public posts: PostListItem[] = [];

  constructor(private blog: BlogService) { }

  async ngOnInit(): Promise<void> {
    this.posts = await this.blog.get();
  }
}
