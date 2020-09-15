import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NgxSpinnerModule } from 'ngx-spinner';

import { AuthService } from './core/service/auth.service';
import { AuthGuard } from './core/helper/auth.guard';
import { AlertService } from './core/service/alert.service';
import { HttpClientModule } from '@angular/common/http';
import { InterceptorModule } from './core/service/interceptors.service';
import { ToasterModule, ToasterService } from 'angular2-toaster';
import { FilmeModule } from './filme/filme.module';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { PageService, SortService, FilterService, GroupService, AggregateService  } from '@syncfusion/ej2-angular-grids';

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    HttpClientModule,
    InterceptorModule,
    NgxSpinnerModule,
    ToasterModule.forRoot(),
    FilmeModule,    
  ],
  providers: [AuthService, AuthGuard, AlertService, ToasterService],
  bootstrap: [AppComponent]
})
export class AppModule { }
