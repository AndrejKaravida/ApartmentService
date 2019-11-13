import { Component, OnInit, ViewChild } from '@angular/core';
import { Apartment } from '../_models/apartment';
import { ApartmentService } from '../_services/apartment.service';
import { AlertifyService } from '../_services/alertify.service';
import { ActivatedRoute } from '@angular/router';
import { Moment, relativeTimeThreshold } from 'moment';
import { DaterangepickerComponent } from 'ngx-daterangepicker-material';
import { moment } from 'ngx-bootstrap/chronos/test/chain';
import { NgxGalleryOptions, NgxGalleryImage, NgxGalleryAnimation } from 'ngx-gallery';
import { MatDialog } from '@angular/material/dialog';
import { AddamentitydialogComponent } from '../addamentitydialog/addamentitydialog.component';

@Component({
  selector: 'app-apartment-detail',
  templateUrl: './apartment-detail.component.html',
  styleUrls: ['./apartment-detail.component.css']
})
export class ApartmentDetailComponent implements OnInit {
  apartment: any;
  numOfAmentities = 0;
  grade = 0;
  oldPrice = 0;
  modify = false;
  priceChange = false;
  numOfGrades = 0;
  selected: {startDate: Moment, endDate: Moment};
  galleryOptions: NgxGalleryOptions[];
  galleryImages: NgxGalleryImage[];

  constructor( private route: ActivatedRoute, private alertify: AlertifyService,
               private apartmentService: ApartmentService, public dialog: MatDialog) { }

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
      this.oldPrice = this.apartment.pricePerNight;
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

  changePrice() {
    this.priceChange = !this.priceChange;
  }

  applyPriceChange() {

    if (this.apartment.pricePerNight >= 0 &&
       this.apartment.pricePerNight <= 99 &&
       this.apartment.pricePerNight !== this.oldPrice) {
      this.apartmentService.changePrice(this.apartment.id, this.apartment.pricePerNight).subscribe(() => {
        this.alertify.success('Price successfully changed!');
      }, error => {
        this.alertify.error('Error while saving new price');
      });
      this.changePrice();
    } else if (this.apartment.pricePerNight === this.oldPrice) {
      this.alertify.error('You cannot enter the same price');
    } else {
      this.alertify.error('Please specify different price between 0 and 99');
    }

  }

  addAmentity() {
    const dialogRef = this.dialog.open(AddamentitydialogComponent, {
      width: '500px',
      data: { apartment: this.apartment }
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        const amenities = result.data;
        this.apartmentService.addAmenities(this.apartment.id, amenities).subscribe(() => {
          this.alertify.success('Amenities updated!');
          this.apartmentService.getApartment(this.apartment.id).subscribe(result => {
            this.apartment = result;
          });
        }, error => {
          this.alertify.error('Failed to add new amenities');
        });
      }
    });
  }

  removeAmentity(name: string) {
    this.apartmentService.removeAmentity(this.apartment.id, name).subscribe(() => {
      this.alertify.success('Amenity removed!');
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
