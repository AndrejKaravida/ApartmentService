import { Component, OnInit, Input, EventEmitter, Output } from '@angular/core';
import { environment } from 'src/environments/environment';
import { ApartmentService } from '../_services/apartment.service';
import { AuthService } from '../_services/auth.service';
import { AlertifyService } from '../_services/alertify.service';
import { FileUploader } from 'ng2-file-upload';
import { Photo } from '../_models/photo';

@Component({
  selector: 'app-photo-editor',
  templateUrl: './photo-editor.component.html',
  styleUrls: ['./photo-editor.component.css']
})
export class PhotoEditorComponent implements OnInit {
  @Input() photos: Photo [];
  @Input() apartment: any;
  @Output() changed = new EventEmitter();
  @Output() changed2 = new EventEmitter();
  uploader: FileUploader;
  hasBaseDropZoneOver = false;
  baseUrl = environment.apiUrl;
  currentMain: Photo;

  constructor(private authService: AuthService, private apartmentService: ApartmentService,
              private alertify: AlertifyService) { }

  ngOnInit() {
    this.initializeUploader();
  }

  fileOverBase(e: any): void {
    this.hasBaseDropZoneOver = e;
  }

  initializeUploader() {
    this.uploader = new FileUploader({
      url: this.baseUrl + 'upload/' + this.apartment.apartmentId,
      authToken: 'Bearer ' + localStorage.getItem('token'),
      isHTML5: true,
      allowedFileType: ['image'],
      removeAfterUpload: true,
      autoUpload: false,
      maxFileSize: 5 * 1024 * 1024 // 5mb
    });

    this.uploader.onAfterAddingFile = (file) => {file.withCredentials = false; };

    this.uploader.onSuccessItem = (item, response, status, headers) => {
      if (response) {
        const res: Photo = JSON.parse(response);
        const photo = {
          id: res.id,
          url: res.url,
          isMain: res.isMain
        };
        this.photos.push(photo);
      }
    };
  }

  setMainPhoto(photo: Photo) {
    this.apartmentService.setMainPhoto(this.apartment.apartmentId, photo.id).subscribe(() => {
      this.currentMain = this.photos.filter(p => p.isMain === true)[0];
      this.currentMain.isMain = false;
      photo.isMain = true;
      this.changed.emit(photo.url);
    }, error => {
      this.alertify.error(error);
    });
  }

  deletePhoto(id: number) {
    this.alertify.confirm('Are you sure you want to delete this photo?', () => {
      this.apartmentService.deletePhoto(id).subscribe(() => {
        this.photos.splice(this.photos.findIndex(p => p.id === id), 1);
        this.alertify.success('Photo has been deleted');
        this.changed2.emit(true);
      }, error => {
        this.alertify.error('Failed to delete the photo');
      });
    });
  }

}
