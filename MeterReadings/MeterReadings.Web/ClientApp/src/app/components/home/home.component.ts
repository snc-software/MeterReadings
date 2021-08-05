import { Component, OnInit } from '@angular/core';
import {MeterReadingUploadService} from "../../shared/services/meter-reading-upload.service";
import {HttpErrorResponse, HttpEventType} from "@angular/common/http";

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {

  public file: File | null | undefined;
  public pending: boolean;
  public progress: number;
  public uploadComplete : boolean;

  constructor(private uploadService: MeterReadingUploadService) { }

  ngOnInit() {
  }

  onFileInput(files: FileList | null): void {
    if (files) {
      this.file = files.item(0);
      this.pending = true;
      this.progress = null;
      this.uploadComplete = false;
    }
  }

  onSubmit() {
    if (this.file) {
      this.pending = false;
      this.progress = 0;

      this.uploadService.upload(this.file).subscribe(event => {
        if (event.type === HttpEventType.UploadProgress) {
          this.progress = Math.round(100 * event.loaded / event.total);
        }
        else if (event.type === HttpEventType.Response) {
          this.progress = 100;
          this.uploadComplete = true;
        }
      }, (error: HttpErrorResponse) => {
        console.log(JSON.stringify(error.error))
      });

    }
  }

}
