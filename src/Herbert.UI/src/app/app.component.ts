import { Component } from '@angular/core';
import { MD_TOOLBAR_DIRECTIVES } from '@angular2-material/toolbar';
import { MD_SIDENAV_DIRECTIVES } from '@angular2-material/sidenav';
import { MD_BUTTON_DIRECTIVES } from '@angular2-material/button';

@Component({
  moduleId: module.id,
  selector: 'app-root',
  templateUrl: 'app.component.html',
  styleUrls: ['app.component.css'],
  directives: [
    MD_TOOLBAR_DIRECTIVES,
    MD_SIDENAV_DIRECTIVES,
    MD_BUTTON_DIRECTIVES
  ]
})

export class AppComponent {
  title = 'Herbert';
}
