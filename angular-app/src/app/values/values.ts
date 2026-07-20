import { Component, signal } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { FilesService } from '../core/services/files.service';


@Component({
  selector: 'app-values',
  standalone: true,
  imports: [
    FormsModule
  ],
  templateUrl: './values.html',
  styleUrl: './values.css'
})
export class ValuesComponent {

  fileName = "";

  values = signal<any[]>([]);


  constructor(
    private filesService: FilesService
  ) {}


  search(){

    this.filesService
      .getSortedValues(this.fileName)
      .subscribe({
        next: data => {

          console.log(data);

          this.values.set(data);

        },
        error: error => {

          console.error(error);

        }
      });

  }

}
