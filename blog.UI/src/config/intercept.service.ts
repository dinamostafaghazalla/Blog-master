import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest, HttpResponse, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { finalize, tap } from 'rxjs/operators';
import { Router } from '@angular/router';
import { environment } from 'src/environments/environment';

@Injectable()
export class InterceptService implements HttpInterceptor {
	constructor(
		private _router: Router
	) {}

	// intercept request and add token
	intercept(
		request: HttpRequest<any>,
		next: HttpHandler
	): Observable<HttpEvent<any>> {
		// driver realtime
			request = request.clone({
				setHeaders: {
					Content_Type: `application/json`,
					//Authorization: `${localStorage.getItem('accessToken')}`
				}
			});
		return next.handle(request).pipe(
			tap(

				event => {
					if (event instanceof HttpResponse) {
						// console.log('all looks good');
						// http response status code
						// console.log(event.status);
					}
				},
				error => {
					if (error instanceof HttpErrorResponse) {
						// if (error.status === 401 || error.status === 403) {
						// 	localStorage.clear();
						// 	this._router.navigate(['login']);
						// }
					}
				}
			),
			finalize(() => {
			})
		);
	}
}
