import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject } from 'rxjs';
import { User } from '../model/user';
import { AlertService } from './alert.service';
import { NgxSpinnerService } from 'ngx-spinner';

@Injectable()
export class AuthService {
  private loggedIn: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(false);
  private user: any;

  get isLoggedIn() {
    if (localStorage.getItem('userSysPlan') !== '' && localStorage.getItem('userSysPlan') !== null)
        this.loggedIn.next(true);
    else
        this.loggedIn.next(false);

    return this.loggedIn.asObservable();
  }

  get userLogged() {
    if (localStorage.getItem('userSysPlan') !== '' && localStorage.getItem('userSysPlan') !== null)
        this.user = JSON.parse(localStorage.getItem('userSysPlan')) as User;
    else
        this.user = null;

    return this.user;
  }

  constructor(
    private router: Router, 
    private _http: HttpClient, 
    private alertService: AlertService,
    public spinnerService: NgxSpinnerService) {
  }

  async login(user: User){ 
    localStorage.clear();
    this.spinnerService.show();

    this._http.post("usuario/login",{
      "login": user.Login,
      "password": encodeURIComponent(user.Senha)
    })
    .subscribe(res => {
        if (res["success"] == true) {
            var userAPI = res["data"] as User;
            localStorage.setItem("userSysPlan", JSON.stringify(userAPI));
            
            this.spinnerService.hide();
            this.loggedIn.next(true);
            this.router.navigate(['dashboard']);
        }
        else {
          this.spinnerService.hide();
          this.alertService.error(res["errorMessage"]);
         }
      },
      err => {
        this.spinnerService.hide();
        this.alertService.error(JSON.parse(err.error).errorMessage);        
      }
    );
  }

  logout(){
    localStorage.clear();
    this.loggedIn.next(false);
    this.router.navigate(['login']);
  }  
}