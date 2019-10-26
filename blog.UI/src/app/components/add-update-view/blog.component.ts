import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { Http } from '@angular/http';
import { FormBuilder, FormGroup, FormControl, Validators } from '@angular/forms';
import { BlogPostService } from '../../services/blog-post.service';
import {config} from '../../../config/cosmo.config';
import {blogModel} from '../../models/blog-post.model';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-blog',
  templateUrl: './blog.component.html',
  styleUrls: ['./blog.component.css']
})
export class BlogComponent implements OnInit {
  data; 
  allBlogs;
  allPosts;
  blogForm: FormGroup;
  message;
  menu : boolean = false;
  isEdit = false;
  isDetails = false;

  constructor(private router: Router,
     private _http: Http,
     private fb: FormBuilder,
     private blogPostService: BlogPostService,
     private route: ActivatedRoute,
     private toastr: ToastrService,
     private _router: Router
  ) {
    this.blogForm = this.fb.group({
      'id': new FormControl(''),
      'title': ['', [Validators.required]],
      'author': ['', [Validators.required]],
      'subtitle': [''],
      'body': [''],
      'imageUrl': ['']
    });
      if (this._router.url.includes('details')) {
        this.isDetails = true;
        this.blogForm.disable();
        this.loadData();
	  }
	  if (this._router.url.includes('edit')) {
		this.isEdit = true;
		this.loadData();
	}
  }

  toggleMenu()
  {
    this.menu = !this.menu;
  }

  loadData() {
		this.route.paramMap.subscribe(params => {
			const id = params.get('id');
			if (id) {
				this.loadDataById(id);
			}
		});
	}

	loadDataById(id: any) {
		this.blogPostService.GetService(id).subscribe((data: any) => {
			if (data.success) {
					this.blogForm.patchValue(data.data);
					this.blogForm.markAsTouched();
					this.blogForm.markAsDirty();

				}
			},
			(error) => {
				if (error.error.errors) {
					console.log(error);
					this.openSnackBar(error.error.errors[0].message);
				} else {
					this.openSnackBar('Some error occure while processing your request, please try again.');
				}
			},
			() => {
			});
	}

  submitForm() {
	const data = this.blogForm.value;
		if (this.isEdit) {
			if (this.blogForm.valid) {
				this.blogPostService.Update(data).subscribe(
					(result: any) => {
             			this.toastr.success(result.succcessMessage)
						setTimeout(() => {
							this._router.navigate(['/']);
						},
							2500);
					},
					(error: any) => {
						console.error('Error while logistic company insert ', error);
						if (error.error && error.error.errors) {
							this.openSnackBar(error.error.errors[0].message);
						}
					},
					() => { }
				);
			}
		} else {
			if (this.blogForm.valid) {
       		 delete data['id'];
				this.blogPostService.addBlog(data).subscribe(
					(result: any) => {
						if (result.statuscode === 200) {
              			this.toastr.success(result.succcessMessage)
							setTimeout(() => {
								this._router.navigate(['/']);
							},
								2500);
							this.blogForm.reset();
						}
					},
					(error: any) => {
						console.error('Error while customer insert ', error);
						if (error.error && error.error.errors) {
							this.openSnackBar(error.error.errors[0].message);
						}
					},
					() => { }
				);
			} else {
			}
		}
  }
  
	openSnackBar(message: string) {
		// this.snackBar.open(message,
		// 	'',
		// 	{
		// 		duration: 5000,
		// 	});
	}

  ngOnInit() {

  }

}
