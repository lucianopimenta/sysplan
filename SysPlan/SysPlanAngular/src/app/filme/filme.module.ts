import { NgModule, CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { AuthGuard } from '../core/helper/auth.guard';
import { FilmeComponent } from './filme.component';
import { FilmeService } from './filme.service';
import { GridModule, PageService, SortService, FilterService, GroupService, AggregateService  } from '@syncfusion/ej2-angular-grids';

@NgModule({
    imports: [
      FormsModule, 
      ReactiveFormsModule, 
      RouterModule.forChild([
        { path: '', component: FilmeComponent, canActivate: [AuthGuard] },
      ]), 
      CommonModule,
      GridModule
    ],
    declarations: [
      FilmeComponent
    ],
    schemas: [CUSTOM_ELEMENTS_SCHEMA ],
    providers: [FilmeService, PageService, SortService, FilterService, GroupService, AggregateService]
  })
  export class FilmeModule { }