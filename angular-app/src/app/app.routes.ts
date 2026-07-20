import { Routes } from '@angular/router';
import { UploadComponent } from './upload/upload';
import { ValuesComponent } from './values/values';
import { ResultsComponent } from './results/results';

export const routes: Routes = [
  {
    path: 'upload',
    component: UploadComponent
  },

  {
    path: 'filter',
    component: ResultsComponent
  },

  {
    path: 'values',
    component: ValuesComponent
  }
];
