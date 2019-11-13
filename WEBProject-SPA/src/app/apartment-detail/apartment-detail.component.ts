import { Component, OnInit, ViewChild } from '@angular/core';
import { Apartment } from '../_models/apartment';
import { ApartmentService } from '../_services/apartment.service';
import { AlertifyService } from '../_services/alertify.service';
import { ActivatedRoute } from '@angular/router';
import { Moment, relativeTimeThreshold } from 'moment';
import { DaterangepickerComponent } from 'ngx-daterangepicker-material';
import { moment } from 'ngx-bootstrap/chronos/test/chain';
import { NgxGalleryOptions, NgxGalleryImage, NgxGalleryAnimation } from 'ngx-gallery';

@Component({
  selector: 'app-apartment-detail',
  templateUrl: './apartment-detail.component.html',
  styleUrls: ['./apartment-detail.component.css']
})
export class ApartmentDetailComponent implements OnInit {
  apartment: any;
  numOfAmentities = 0;
  grade = 0;
  modify = false;
  numOfGrades = 0;
  selected: {startDate: Moment, endDate: Moment};
  galleryOptions: NgxGalleryOptions[];
  galleryImages: NgxGalleryImage[];

  constructor( private route: ActivatedRoute, private alertify: AlertifyService,
               private apartmentService: ApartmentService) { }

  ngOnInit() {

    this.loadApartment();

    this.galleryOptions = [
    {
      width: '500px',
      height: '500px',
      imagePercent: 100,
      thumbnailsColumns: 4,
      imageAnimation: NgxGalleryAnimation.Slide,
      preview: false
    }
  ];
    this.galleryImages = this.getImages();
  }

  loadApartment() {
    this.route.data.subscribe(data => {
      const key = 'apartment';
      this.apartment = data[key];
      this.numOfAmentities = this.apartment.amentities.length;
      let totalGrade = 0;
      this.numOfGrades = this.apartment.comments.length;
      // tslint:disable-next-line: prefer-for-of
      for (let i = 0; i < this.apartment.comments.length; i++) {
       totalGrade += this.apartment.comments[i].grade;
      }
      this.grade = totalGrade / this.numOfGrades;
    });
  }

  getImages() {
    const imageUrls = [];
    // tslint:disable-next-line: prefer-for-of
    for (let i = 0; i < this.apartment.photos.length; i++) {
      imageUrls.push({
        small: this.apartment.photos[i].url,
        medium: this.apartment.photos[i].url,
        big: this.apartment.photos[i].url,
        description: this.apartment.photos[i].description
      });
    }
    return imageUrls;
  }

  choosedDate(event: any) {

  }

  modifyToogle() {
    this.modify = !this.modify;
  }

  removeAmentity(name: string) {
    this.apartmentService.removeAmentity(this.apartment.id, name).subscribe(() => {
      this.alertify.success('Amentity removed!');
      for (let i = 0; i < this.apartment.amentities.length; i++) {
        if (this.apartment.amentities[i].name === name) {
          this.apartment.amentities.splice(i, 1);
          break;
        }
      }
    }, error => {
      this.alertify.error('Failed to remove amentity.');
    });
  }
}
