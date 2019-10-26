import { HttpErrorResponse, HttpResponse } from '@angular/common/http';
import { throwError } from 'rxjs';


export abstract class HttpHelper {

    constructor() { }

	onSucess<T>(res: HttpResponse<T>, functionName: string) {
        return res;
    }
    onError(error: HttpErrorResponse, functionName: string) {
        console.log(error, functionName);
        return throwError(error);
    }
    onComplete(functionName: string) {
    }
}
