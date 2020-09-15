import { take } from 'rxjs/operators'
import { map } from 'rxjs/operators'
import 'rxjs/add/operator/map';
import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { AuthService } from '../service/auth.service';

@Injectable()
export class AuthGuard implements CanActivate {
  constructor(private authService: AuthService, private router: Router) {}

  canActivate(
    next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ): Observable<boolean> {
    return this.authService.isLoggedIn.pipe(take(1)).pipe(map((isLoggedIn: boolean) => {
        if (!isLoggedIn){
          this.router.navigate(['login']);
          return false;
        }
        return true;
      }));
  }
}