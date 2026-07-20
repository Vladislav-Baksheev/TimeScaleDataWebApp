import { Component, signal } from '@angular/core';
import { RouterOutlet, RouterLink } from '@angular/router';
import { UploadComponent } from './upload/upload';
import {ValuesComponent} from './values/values'
import { ResultsComponent } from "./results/results";

@Component({
  selector: 'app-root',
  imports: [
    RouterOutlet,
    UploadComponent,
    ValuesComponent,
    ResultsComponent,
    RouterLink
  ],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App {
  protected readonly title = signal('angular-app');

}
