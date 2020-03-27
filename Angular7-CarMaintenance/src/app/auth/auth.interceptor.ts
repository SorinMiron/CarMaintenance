import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent } from '@angular/common/http';
import { Injectable, ɵConsole } from '@angular/core';
import { Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
@Injectable()
export class AuthInterceptor implements HttpInterceptor{

    /**
     *
     */
    constructor(private router: Router, private toastr:ToastrService) {
        
    }
    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>>{
        if(localStorage.getItem('token') != null){
            const clonedRequest = req.clone({
                headers : req.headers.set('Authorization', 'Bearer ' + localStorage.getItem('token'))
            });
            return next.handle(clonedRequest).pipe(tap(
                succ =>{},
                err =>{
                    if(err.status == 401){
                        localStorage.removeItem("token");
                        this.router.navigateByUrl('/user/login');
                        this.toastr.error("401 ERROR");
                        console.log(err);
                    }
                }
            ));
        } else{
            return next.handle(req.clone());
        }
    }
}