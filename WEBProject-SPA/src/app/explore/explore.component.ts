import { Component, OnInit } from '@angular/core';
import { Apartment } from '../_models/apartment';
import { ActivatedRoute } from '@angular/router';
import { ApartmentService } from '../_services/apartment.service';
import { PaginatedResult, Pagination } from '../_models/pagination';
import { AlertifyService } from '../_services/alertify.service';
import { Moment } from 'moment';
import * as moment from 'moment';
import { MatDialog } from '@angular/material/dialog';
import { AddamentitydialogComponent } from '../addamentitydialog/addamentitydialog.component';
import { SelectamenitiesdialogComponent } from '../selectamenitiesdialog/selectamenitiesdialog.component';



@Component({
  selector: 'app-explore',
  templateUrl: './explore.component.html',
  styleUrls: ['./explore.component.css']
})
export class ExploreComponent implements OnInit {
  apartments: Apartment[];
  pagination: Pagination;
  apartmentParams: any = {};
  options = ['Ascending', 'Descending'];
  role = '';
  selected: {startDate: Moment, endDate: Moment};
  maxDate: moment.Moment;
  minDate: moment.Moment;
  status = '';
  type = '';

  constructor(private route: ActivatedRoute, private apartmentService: ApartmentService,
              private alertify: AlertifyService, public dialog: MatDialog) { }

  ngOnInit() {

    this.maxDate = moment().add(1,  'years');
    this.minDate = moment();
    this.route.data.subscribe(data => {
      const key = 'apartments';
      this.apartments = data[key].result;
      this.pagination = data[key].pagination;
      this.role = localStorage.getItem('role');
      this.role = this.role.substr(1);
      this.role = this.role.substr(0, this.role.length - 1);

    });

    this.apartmentParams.minPrice = 0;
    this.apartmentParams.maxPrice = 99;
    this.apartmentParams.city = '';
    this.apartmentParams.country = '';
    this.apartmentParams.guests = 1;
    this.apartmentParams.minRooms = 1;
    this.apartmentParams.maxRooms = 10;
    this.apartmentParams.filtertype = '';
    this.apartmentParams.filterstatus = '';
    this.apartmentParams.filteramenities = '';

    if (this.role === 'Admin') {
      this.loadApartmentsForAdmin();
    }
  }

  resetFilters2() {
    this.apartmentParams.filtertype = '';
    this.apartmentParams.filterstatus = '';
    this.apartmentParams.filteramenities = '';

    this.loadApartments();

  }

  selectAmentities() {
    const dialogRef = this.dialog.open(SelectamenitiesdialogComponent, {
      width: '500px',
      data: {}
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.apartmentParams.filteramenities = result.data;
      }
    });
  }

  loadApartments() {
    if (this.selected != null) {

      if (this.selected.startDate != null) {

        const starting: Date = this.selected.startDate.toDate();

        const curr_date_st = starting.getDate();
        const curr_month_st = starting.getMonth() + 1;
        const curr_year_st = starting.getFullYear();

        const startDate = curr_date_st + '-' + curr_month_st + '-' + curr_year_st;
        this.apartmentParams.startDate = startDate;

      }
      if (this.selected.endDate != null) {

        const ending: Date = this.selected.endDate.toDate();

        const curr_date_en = ending.getDate();
        const curr_month_en = ending.getMonth() + 1;
        const curr_year_en = ending.getFullYear();

        const endDate = curr_date_en + '-' + curr_month_en + '-' + curr_year_en;
        this.apartmentParams.endDate = endDate;
      }
    }

    if (this.role === 'Admin') {
      this.loadApartmentsForAdmin();
    } else {
      this.apartmentService.getApartments(this.pagination.currentPage, this.pagination.itemsPerPage, this.apartmentParams)
      .subscribe((res: PaginatedResult<Apartment[]>) => {
        this.apartments = res.result;
        this.pagination = res.pagination;
      }, error => {
        this.alertify.error(error);
      });
    }

  }

  deleteApartment(event) {
    this.apartmentService.deleteApartment(event).subscribe(() => {
      this.alertify.success('Successfully deleted apartment!');
      this.loadApartments();
    }, error => {
      this.alertify.error('Error while deleting apartment');
    });
  }

  loadApartmentsForAdmin() {
    this.apartmentService.getApartmentsForAdmin(this.pagination.currentPage, this.pagination.itemsPerPage, this.apartmentParams)
    .subscribe((res: PaginatedResult<Apartment[]>) => {
      this.apartments = res.result;
      this.pagination = res.pagination;
    }, error => {
      this.alertify.error(error);
    });
  }

  resetFilters() {
    this.apartmentParams.minPrice = 0;
    this.apartmentParams.maxPrice = 99;
    this.apartmentParams.city = '';
    this.apartmentParams.country = '';
    this.apartmentParams.guests = 1;
    this.apartmentParams.minRooms = 1;
    this.apartmentParams.maxRooms = 10;
    this.apartmentParams.startDate = null;
    this.apartmentParams.endDate = null;
    this.selected = null;

    this.loadApartments();

  }

  pageChanged(event: any): void {
    this.pagination.currentPage = event.page;

    if (this.role === 'Admin') {
      this.loadApartmentsForAdmin();
    } else {
      this.loadApartments();
    }
  }


}
