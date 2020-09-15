import { NgModule, CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { AuthGuard } from '../core/helper/auth.guard';
import { DashboardComponent } from './dashboard.component';

@NgModule({
    imports: [
      FormsModule, 
      ReactiveFormsModule, 
      RouterModule.forChild([
        { path: '', component: DashboardComponent, canActivate: [AuthGuard] },
      ]), 
      CommonModule      
    ],
    declarations: [
      DashboardComponent
    ],
    schemas: [CUSTOM_ELEMENTS_SCHEMA ],
    providers: []
  })
  export class DashboardModule { }