import { Component } from "@angular/core";
import { ActivatedRoute, Router } from "@angular/router";
import { User } from "../core/model/user";
import { AuthService } from "../core/service/auth.service";

@Component({
    selector: 'app-login',
    templateUrl: './login.component.html',
    styleUrls:['./login.component.scss'],
    providers: []
  })
  export class LoginComponent {
    
    public login: string = '';
    public senha: string = '';

    constructor(public _route: ActivatedRoute, 
      public _router: Router,
      public _auth: AuthService) {
     }

     onLogin(){
        var user = new User();
        user.Login = this.login;
        user.Senha = this.senha;
        this._auth.login(user);
     }
}