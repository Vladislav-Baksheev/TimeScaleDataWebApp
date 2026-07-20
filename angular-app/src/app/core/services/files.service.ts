import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Values } from '../../models/values';
import { HttpParams } from '@angular/common/http';
import { Results } from '../../models/results';

@Injectable({
  providedIn: 'root'
})
export class FilesService {

  private apiUrl = "http://localhost:5062/TimeScaleData";

  constructor(
    private http: HttpClient
  ) {}


  upload(file: File) {

    const formData = new FormData();

    formData.append("file", file);

    return this.http.post(
      `${this.apiUrl}/upload`,
      formData
    );
  }


  getSortedValues(fileName: string) {

    return this.http.get<Values[]>(
      `${this.apiUrl}/getSortedValues`,
      {
        params: {
          fileName: fileName
        }
      }
    );
  }

  getResults(filter: any) {

    let params = new HttpParams();

    Object.keys(filter).forEach(key => {

      if (
        filter[key] !== null &&
        filter[key] !== undefined &&
        filter[key] !== ""
      ) {
        params = params.set(
          key,
          filter[key]
        );
      }

    });


    return this.http.get<Results[]>(
      `${this.apiUrl}/getResults`,
      {
        params
      }
    );

  }

}
