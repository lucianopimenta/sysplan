import { Injectable, NgModule} from '@angular/core';
import { Observable, throwError} from 'rxjs';
import { HttpEvent, HttpInterceptor, HttpHandler, HttpRequest, HttpErrorResponse} from '@angular/common/http';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import 'rxjs/add/operator/do';
import { map, catchError } from 'rxjs/operators';
import { AuthService } from '../service/auth.service';
import { AlertService } from '../service/alert.service';
import { NgxSpinnerService } from 'ngx-spinner';

@Injectable()
export class HttpsRequestInterceptor implements HttpInterceptor {

  constructor(public alert: AlertService,public auth: AuthService, public spinnerService: NgxSpinnerService) { }

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {

    var url = 'http://localhost:8081/api/' + req.url;
    var dupReq: HttpRequest<any>;

      dupReq = req.clone({ 
        url: url
      });

    return next.handle(dupReq).pipe(
      map((event: HttpEvent<any>) => {
        return event;
      }),
      catchError((error: HttpErrorResponse) => {
        
        //token
        if (error.status == 401){
          this.alert.error('Sessão expirou!\nRefaça o login.');
          this.auth.logout();          
        }
        //erros da API
        else if (error.status == 400 || error.status == 500){
          this.alert.error(error.error.errorMessage);
        }
        else        
          return throwError(error);

          this.spinnerService.hide();
    }));
  }
};
@NgModule({
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: HttpsRequestInterceptor, multi: true }
  ]
})
export class InterceptorModule { }
