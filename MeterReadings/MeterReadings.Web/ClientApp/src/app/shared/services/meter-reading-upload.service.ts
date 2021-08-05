import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Observable} from "rxjs";

@Injectable({
  providedIn: 'root'
})
export class MeterReadingUploadService {

  constructor(private http: HttpClient) { }

  upload(file: File): Observable<any> {
    const data = new FormData();
    data.append('file', file);

    return this.http.post<string>('meter-reading-uploads', data, {reportProgress: true, observe: 'events'})
  }
}
