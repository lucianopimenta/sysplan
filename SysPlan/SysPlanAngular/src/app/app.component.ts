import { Component } from '@angular/core';
import { ToasterConfig } from 'angular2-toaster';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'SysPlan';

  public config: ToasterConfig = 
  new ToasterConfig(
  {
      animation: 'fade',
      positionClass: 'toast-top-right',
      timeout: 5000
  });
}
