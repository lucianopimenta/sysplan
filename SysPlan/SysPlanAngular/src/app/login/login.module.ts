import { NgModule, CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { LoginComponent } from './login.component';

@NgModule({
    imports: [
      FormsModule, 
      ReactiveFormsModule, 
      RouterModule.forChild([
        { path: '', component: LoginComponent },
      ]), 
      CommonModule      
    ],
    declarations: [
      LoginComponent
    ],
    schemas: [CUSTOM_ELEMENTS_SCHEMA ],
    providers: []
  })
  export class LoginModule { }