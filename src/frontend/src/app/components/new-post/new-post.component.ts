import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { BlogService } from '../../services/blog.service';

@Component({
  selector: 'app-new-post',
  templateUrl: './new-post.component.html',
  styleUrls: ['./new-post.component.css']
})
export class NewPostComponent implements OnInit {

  postForm = new FormGroup({
    title: new FormControl(''),
    excerpt: new FormControl(''),
    body: new FormControl('')
  });

  constructor(private blog: BlogService, private router: Router) { }

  ngOnInit(): void {
  }

  async onSubmit() {
    await this.blog.create(this.postForm.value);
    this.router.navigate(["/"]);
  }

}
