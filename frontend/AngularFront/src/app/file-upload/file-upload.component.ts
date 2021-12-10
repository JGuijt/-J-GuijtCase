import { Component, OnInit } from '@angular/core';
import { FileUploadService } from '../file-upload.service';

@Component({
  selector: 'app-file-upload',
  templateUrl: './file-upload.component.html',
  styleUrls: ['./file-upload.component.css']
})
export class FileUploadComponent implements OnInit {

  constructor(private fileUploadService: FileUploadService) { }

  ngOnInit(): void {
  }

file: any = null;
reqResponse: any = null;

onChange(event: any) {
  this.file = event.target.files[0];
}

onUpload() {
  this.fileUploadService.upload(this.file).subscribe((response) => this.reqResponse = response);
    
}

}
