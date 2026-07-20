import { Component, signal } from '@angular/core';
import { FilesService } from '../core/services/files.service';

@Component({
  selector: 'app-upload',
  standalone: true,
  imports: [],
  templateUrl: './upload.html',
  styleUrl: './upload.css',
})


export class UploadComponent {

  constructor(private filesService: FilesService) {
  }

  selectedFile?: File;
  uploadSuccess = signal(false);

  onFileSelected(event: Event) {

    const input = event.target as HTMLInputElement;

    if (input.files?.length) {
        this.selectedFile = input.files[0];

        console.log(this.selectedFile.name);
    }
  }

  upload() {

    if (!this.selectedFile)
        return;

    this.filesService
        .upload(this.selectedFile)
        .subscribe({
            next: (response) => {
              console.log("SUCCESS", response);
              this.uploadSuccess.set(true);
            },
            error: error => console.error(error)
        });

  }
}

