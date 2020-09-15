import { Component } from "@angular/core";
import { ActivatedRoute, Router } from "@angular/router";
import { AuthService } from "../core/service/auth.service";

@Component({
    selector: 'app-dashboard',
    templateUrl: './dashboard.component.html',
    providers: []
  })
  export class DashboardComponent {
    
    constructor(public _route: ActivatedRoute, 
      public _router: Router,
      public _auth: AuthService) {
     }

     logout(){
        this._auth.logout();
     }
}