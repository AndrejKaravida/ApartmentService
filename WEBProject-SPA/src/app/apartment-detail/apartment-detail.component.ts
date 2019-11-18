import { Component, OnInit } from '@angular/core';
import { ApartmentService } from '../_services/apartment.service';
import { AlertifyService } from '../_services/alertify.service';
import { ActivatedRoute } from '@angular/router';
import { Moment } from 'moment';
import { NgxGalleryOptions, NgxGalleryImage, NgxGalleryAnimation } from 'ngx-gallery';
import { MatDialog } from '@angular/material/dialog';
import { AddamentitydialogComponent } from '../addamentitydialog/addamentitydialog.component';
import { AuthService } from '../_services/auth.service';
import { AddreviewdialogComponent } from '../addreviewdialog/addreviewdialog.component';
import { Photo } from '../_models/photo';
import * as moment from 'moment';

@Component({
  selector: 'app-apartment-detail',
  templateUrl: './apartment-detail.component.html',
  styleUrls: ['./apartment-detail.component.css']
})
export class ApartmentDetailComponent implements OnInit {
  maxDate: moment.Moment;
  minDate: moment.Moment;
  postpermission = false;
  role = '';
  username = '';
  apartment: any;
  photoUrl = '';
  photos: Photo[] = [];
  numOfAmentities = 0;
  grade = 0;
  oldPrice = 0;
  oldRooms = 0;
  oldGuests = 0;
  oldArrival = '';
  oldDeparture = '';
  modify = false;
  priceChange = false;
  guestsChange = false;
  roomsChange = false;
  arrivalChange = false;
  departureChange = false;
  numOfGrades = 0;
  selected: {startDate: Moment, endDate: Moment};
  galleryOptions: NgxGalleryOptions[];
  galleryImages: NgxGalleryImage[];
  invalidDates: moment.Moment[] = [];

  constructor( private route: ActivatedRoute, private alertify: AlertifyService,
               private apartmentService: ApartmentService, public dialog: MatDialog,
               private authService: AuthService) { }

  ngOnInit() {

    this.loadApartment();

    this.maxDate = moment().add(1,  'years');
    this.minDate = moment();



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
    this.role = localStorage.getItem('role');
    this.role = this.role.substr(1);
    this.role = this.role.substr(0, this.role.length - 1);
    this.username = localStorage.getItem('username');
    this.username = this.username.substr(1);
    this.username = this.username.substr(0, this.username.length - 1);
  }

  isInvalidDate = (m: moment.Moment) =>  {

    return this.invalidDates.some(d => d.isSame(m, 'day'));
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
      this.oldGuests = this.apartment.numberOfGuests;
      this.oldRooms = this.apartment.numberOfRooms;
      this.oldArrival = this.apartment.timeToArrive;
      this.oldDeparture = this.apartment.timeToLeave;

      for (let i = 0; i < data[key].photos.length; i++) {
        if (data[key].photos[i].isMain) {
          this.photoUrl = data[key].photos[i].url;
          break;
        }
      }

      for (let i = 0; i < data[key].photos.length; i++) {
        if (!data[key].photos[i].isDeleted) {
          this.photos.push(data[key].photos[i]);
        }
      }

      this.invalidDates = [];

      for (let i = 0; i < data[key].reservedDaysFromToday.length; i++) {
        const invaliddate = data[key].reservedDaysFromToday[i].dayFromToday;
        this.invalidDates.push(moment().add(invaliddate, 'days'));
      }

      this.apartmentService.getPermission(this.authService.decodedToken.nameid,
        this.apartment.id).subscribe((result: boolean) => {
          this.postpermission = result;
        });
    });
  }

  getImages() {
    const imageUrls = [];
    // tslint:disable-next-line: prefer-for-of
    for (let i = 0; i < this.apartment.photos.length; i++) {

      if (!this.apartment.photos[i].isDeleted) {
        imageUrls.push({
          small: this.apartment.photos[i].url,
          medium: this.apartment.photos[i].url,
          big: this.apartment.photos[i].url,
          description: this.apartment.photos[i].description
        });
      }
    }
    return imageUrls;
  }

  makeReservation(event) {
    const starting: Date = event.startDate.toDate();

    const curr_date_st = starting.getDate();
    const curr_month_st = starting.getMonth() + 1;
    const curr_year_st = starting.getFullYear();

    const startDate = curr_date_st + '-' + curr_month_st + '-' + curr_year_st;

    const ending: Date = event.endDate.toDate();

    const curr_date_en = ending.getDate();
    const curr_month_en = ending.getMonth() + 1;
    const curr_year_en = ending.getFullYear();

    const endDate = curr_date_en + '-' + curr_month_en + '-' + curr_year_en;

    const apid = this.apartment.id;
    const usid = this.authService.decodedToken.nameid;

    this.apartmentService.makeReservation(apid, usid, startDate, endDate).subscribe(() => {
      this.alertify.success('Reservation has been made!');
      this.loadApartment();
    }, error => {
      this.alertify.error('Problem while making reservation!');
    });

  }

  modifyToogle() {
    this.modify = !this.modify;
  }

  changePrice() {
    this.priceChange = !this.priceChange;
  }

  changeGuests() {
    this.guestsChange = !this.guestsChange;
  }

  changeRooms() {
    this.roomsChange = !this.roomsChange;
  }

  changeArrival() {
    this.arrivalChange = !this.arrivalChange;
  }

  changeDeparture() {
    this.departureChange = !this.departureChange;
  }

  applyPriceChange() {

    if (this.apartment.pricePerNight >= 0 &&
       this.apartment.pricePerNight <= 99 &&
       this.apartment.pricePerNight !== this.oldPrice) {
      this.apartmentService.changePrice(this.apartment.id, this.apartment.pricePerNight).subscribe(() => {
        this.alertify.success('Price successfully changed!');
      }, () => {
        this.alertify.error('Error while saving new price');
      });
      this.changePrice();
      this.oldPrice = this.apartment.pricePerNight;
    } else if (this.apartment.pricePerNight === this.oldPrice) {
      this.alertify.error('You cannot enter the same price');
    } else {
      this.alertify.error('Please specify different price between 0 and 99');
    }
  }

  loadAgain() {
      this.apartmentService.getApartment(this.apartment.id).subscribe(result => {
        this.apartment = result;
        this.galleryImages = this.getImages();
      });
  }

  applyArrivalChange() {

    if (this.apartment.timeToArrive != this.oldArrival) {
      this.apartmentService.changeArrival(this.apartment.id, this.apartment.timeToArrive).subscribe(() => {
        this.alertify.success('Time to arrive successfully changed!');
      }, () => {
        this.alertify.error('Error while saving new time');
      });
      this.changeArrival();
      this.oldArrival = this.apartment.timeToArrive;
    } else {
      this.alertify.error('You cannot enter the same time');
    }
  }

  applyDepartureChange() {

    if (this.apartment.timeToLeave != this.oldDeparture) {
      this.apartmentService.changeDeparture(this.apartment.id, this.apartment.timeToLeave).subscribe(() => {
        this.alertify.success('Time to depart successfully changed!');
      }, () => {
        this.alertify.error('Error while saving new time');
      });
      this.changeDeparture();
      this.oldDeparture = this.apartment.timeToLeave;
    } else {
      this.alertify.error('You cannot enter the same time');
    }
  }

  applyGuestsChange() {

    if (this.apartment.numberOfGuests > 0 &&
       this.apartment.numberOfGuests <= 10 &&
       this.apartment.numberOfGuests !== this.oldGuests) {
      this.apartmentService.changeGuests(this.apartment.id, this.apartment.numberOfGuests).subscribe(() => {
        this.alertify.success('Number of guests successfully changed!');
      }, () => {
        this.alertify.error('Error while saving new guests number');
      });
      this.changeGuests();
      this.oldGuests = this.apartment.numberOfGuests;
    } else if (this.apartment.numberOfGuests === this.oldGuests) {
      this.alertify.error('You cannot enter the same number of guests');
    } else {
      this.alertify.error('Please specify guests number between 0 and 10');
    }
  }

  applyRoomsChange() {

    if (this.apartment.numberOfRooms > 0 &&
       this.apartment.numberOfRooms <= 10 &&
       this.apartment.numberOfRooms !== this.oldRooms) {
      this.apartmentService.changeRooms(this.apartment.id, this.apartment.numberOfRooms).subscribe(() => {
        this.alertify.success('Number of rooms successfully changed!');
      }, () => {
        this.alertify.error('Error while saving new rooms number');
      });
      this.changeRooms();
      this.oldRooms = this.apartment.numberOfRooms;
    } else if (this.apartment.numberOfRooms === this.oldRooms) {
      this.alertify.error('You cannot enter the same number of rooms twice');
    } else {
      this.alertify.error('Please specify rooms number between 0 and 10');
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
        }, () => {
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
    }, () => {
      this.alertify.error('Failed to remove amentity.');
    });
  }

  approveComment(id: number) {

    this.alertify.confirm('Are you sure you want to approve this comment?', () => {
      this.apartmentService.approveComment(id).subscribe(() => {
        this.alertify.success('Comment approved!');
        this.apartmentService.getApartment(this.apartment.id).subscribe(result => {
          this.apartment = result;
        });
      }, () => {
        this.alertify.error('Failed to approve comment!');
      });
    });
  }

  changeMain(event) {
    this.photoUrl = event;
  }

  deleteComment(id: number) {

    this.alertify.confirm('Are you sure you want do delete this comment?', () => {
      this.apartmentService.deleteComment(id).subscribe(() => {
        this.alertify.success('Comment deleted!');
        this.apartmentService.getApartment(this.apartment.id).subscribe(result => {
          this.apartment = result;
        });
      }, () => {
        this.alertify.error('Failed to delete comment!');
      });
    });

  }

  OnSaveReview() {

    const dialogRef = this.dialog.open(AddreviewdialogComponent, {
      width: '600px',
      data: {}
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result.text.length > 5 && result.grade) {

        const grade = result.grade;
        const text = result.text;

        if (text.length >= 5 && grade) {
          this.apartmentService.commentApartment(this.apartment.id,
            this.authService.decodedToken.nameid, text, grade).subscribe(() => {
            this.alertify.success('Successfull!');
            this.alertify.confirm('Your comment has been sent for approval', () => {});
            this.apartmentService.getApartment(this.apartment.id).subscribe(result => {
              this.apartment = result;
            });
          }, () => {
            this.alertify.error('Error saving comment');
          });
        } else {
          this.alertify.error('Please enter valid review');
        }
      }
    });
  }
}
