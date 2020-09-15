import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AppComponent } from './app.component';
import { AuthGuard } from './core/helper/auth.guard';
import { DashboardComponent } from './dashboard/dashboard.component';
import { FilmeComponent } from './filme/filme.component';
import { LoginComponent } from './login/login.component';

const routes: Routes = [
  {
    path: '',
    component: DashboardComponent,
    children: [
      {
        path: '',
        canActivate: [AuthGuard],
        redirectTo: '/dashboard',
        pathMatch: 'full'
      },
      {
        path: 'dashboard',
        canActivate: [AuthGuard],
        loadChildren: () => import('./dashboard/dashboard.module').then(module => module.DashboardModule)
      }
    ]
  },
  {
    path: '',
    component: FilmeComponent,
    children: [
      {
        path: 'filme',
        canActivate: [AuthGuard],
        loadChildren: () => import('./filme/filme.module').then(module => module.FilmeModule)
      },
    ]
  },
  {
    path: '',
    component: LoginComponent,
    children: [
      {
        path: 'login',
        loadChildren: () => import('./login/login.module').then(module => module.LoginModule)
      }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
