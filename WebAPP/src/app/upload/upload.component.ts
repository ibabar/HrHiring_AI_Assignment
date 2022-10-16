import { HttpClient, HttpEventType, HttpErrorResponse } from '@angular/common/http';
import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import configurl from '../../assets/config/config.json'

@Component({
  selector: 'app-upload',
  templateUrl: './upload.component.html',
  styleUrls: ['./upload.component.css']
})
export class UploadComponent implements OnInit {

  progress: number = 0;
  message: string = "";
  @Output() public onUploadFinished = new EventEmitter();

  constructor(private http: HttpClient, private toastr: ToastrService) { }

  ngOnInit(): void {
  }


  uploadFile = (files: any) => {
    if (files.length === 0) {
      return;
    }

    const maxAllowedSize = 5 * 1024 * 1024;


    let fileToUpload = <File>files[0];
    if (fileToUpload.size > maxAllowedSize) {
      // Here you can ask your users to load correct file
      this.toastr.error("File is too large", 'Large File');
      return;
    }
    const formData = new FormData();
    formData.append('file', fileToUpload, fileToUpload.name);

    this.http.post(`${configurl.apiServer.url}/api/upload`, formData, { reportProgress: true, observe: 'events' })
      .subscribe({
        next: (event: any) => {
          if (event.type === HttpEventType.UploadProgress)
            this.progress = Math.round(100 * event.loaded / event.total);
          else if (event.type === HttpEventType.Response) {
            this.message = 'Upload success.';
            this.onUploadFinished.emit(event.body);
          }
        },
        error: (err: HttpErrorResponse) => console.log(err)
      });
  }


}
