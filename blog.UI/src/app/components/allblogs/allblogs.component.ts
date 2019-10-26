import { Component, OnInit } from '@angular/core';
import { Http } from '@angular/http';
import { Router } from '@angular/router';
import { BlogPostService } from '../../services/blog-post.service'
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-allblogs',
  templateUrl: './allblogs.component.html',
  styleUrls: ['./allblogs.component.css']
})
export class AllblogsComponent implements OnInit {
  data;
  allBlogs;
  allPosts;
  author;
  jdata;
  constructor(private _http: Http, private route: Router,
     private blogPostService: BlogPostService,
     private toastr: ToastrService) 
  {
    this.viewAllBlogs();
  }
  ngOnInit() {
  }
  viewAllBlogs() {
    // this.count = false;
    // this.show = true;
    this.blogPostService.showAllBlogs().subscribe(
      (result: any) => {
        //var jsondata = JSON.parse(result.result);
        this.allBlogs = result.result;
      });
  }

  delete(id : any){
    if(window.confirm('Are sure you want to delete this item ?')){
			this.blogPostService.Delete(id).subscribe(
				(result: any) => {
					this.toastr.success((result.succcessMessage);
					this.viewAllBlogs();
				},
				(error: any) => {
					this.toastr.success(error.error.errors[0].message);
				},
				() => { }
			);
   }
}
}
