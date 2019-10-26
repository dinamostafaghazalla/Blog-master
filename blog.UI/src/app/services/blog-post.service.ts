import { Injectable } from '@angular/core';
import { Http } from '@angular/http';
import { Router } from '@angular/router';
import {config} from '../../config/cosmo.config'
import {blogModel} from '../models/blog-post.model';
import { environment } from 'src/environments/environment';
import { catchError, map, finalize, filter } from 'rxjs/operators';
import { HttpClient, HttpResponse, HttpErrorResponse, HttpParams } from '@angular/common/http';
import { HttpHelper } from 'src/config/helper';

@Injectable({
	providedIn: 'root'
})

export class BlogPostService extends HttpHelper {
    data;
    message;
    public API_ARTICLE: string = environment.Version1_0 + 'Articles';
    URL = config.URL;
    bucket_slug = config.bucket_slug;
    constructor(private _http: HttpClient, private router: Router)
    {
      super();
    }


      /**  add blog to backend */
      addBlog(blogModel: blogModel) {
        return this._http.post(this.API_ARTICLE, blogModel)
        .pipe(
          // tslint:disable-next-line:no-shadowed-variable
          map((blogPost: HttpResponse<any>) => this.onSucess(blogPost, 'saveBlogPost')),
          catchError((error: HttpErrorResponse) => this.onError(error, 'saveBlogPost')),
          finalize(() => { this.onComplete('saveBlogPost'); })
        );
      }

    //showing all blogs
    showAllBlogs () {
      return this._http.get(this.API_ARTICLE).pipe(
        // tslint:disable-next-line:no-shadowed-variable
        map((data: HttpResponse<any>) => this.onSucess(data, 'saveBlogPost')),
        catchError((error: HttpErrorResponse) => this.onError(error, 'saveBlogPost')),
        finalize(() => { this.onComplete('saveBlogPost'); })
      );
    }

    GetService(id: any) {
      return this._http.get((this.API_ARTICLE + `/${id}`)).pipe(
        // tslint:disable-next-line:no-shadowed-variable
        map((data: HttpResponse<any>) => this.onSucess(data, 'commissionView')),
        catchError((error: HttpErrorResponse) => this.onError(error, 'commission get')),
        finalize(() => { this.onComplete('commission get'); })
      );
    }
    Update(data: any) {
      return this._http.put(this.API_ARTICLE + '/', JSON.stringify(data), environment.header)
        .pipe(
          // tslint:disable-next-line:no-shadowed-variable
          map((data: HttpResponse<any>) => this.onSucess(data, 'commissionView')),
          catchError((error: HttpErrorResponse) => this.onError(error, 'commissionView')),
          finalize(() => { this.onComplete('commissionView'); })
        );
    }
    Delete(id: any) {
      return this._http.delete(this.API_ARTICLE + `/?id=${id}`, environment.header)
        .pipe(
          // tslint:disable-next-line:no-shadowed-variable
          map((data: HttpResponse<any>) => this.onSucess(data, 'commissionView')),
          catchError((error: HttpErrorResponse) => this.onError(error, 'delete logistic company')),
          finalize(() => { this.onComplete('commissionView'); })
        );
    }  
  }