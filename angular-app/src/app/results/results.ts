import { Component, signal } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { FilesService } from '../core/services/files.service';
import { Results } from '../models/results';


@Component({
  selector: 'app-results',
  standalone: true,
  imports: [
    FormsModule
  ],
  templateUrl: './results.html',
  styleUrl: './results.css'
})
export class ResultsComponent {

  fileName = "";

  dateFrom = "";
  dateTo = "";

  avgValueFrom?: number;
  avgValueTo?: number;

  avgExecutionTimeFrom?: number;
  avgExecutionTimeTo?: number;


  results = signal<Results[]>([]);


  constructor(
    private filesService: FilesService
  ) {}


  search(){

    const filter = {

      fileName: this.fileName,

      dateFrom: this.dateFrom || null,
      dateTo: this.dateTo || null,

      avgValueFrom: this.avgValueFrom,
      avgValueTo: this.avgValueTo,

      avgExecutionTimeFrom: this.avgExecutionTimeFrom,
      avgExecutionTimeTo: this.avgExecutionTimeTo

    };

    this.filesService
      .getResults(filter)
      .subscribe({

        next: data => {
          console.log(data);
          this.results.set(data);
        },

        error: error => {
          console.error(error);
        }

      });
  }

}
