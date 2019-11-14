import { Component, OnInit } from '@angular/core';
import { Apartment } from '../_models/apartment';
import { ActivatedRoute } from '@angular/router';
import { ApartmentService } from '../_services/apartment.service';
import { PaginatedResult, Pagination } from '../_models/pagination';
import { AlertifyService } from '../_services/alertify.service';

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
  
  constructor(private route: ActivatedRoute, private apartmentService: ApartmentService,
              private alertify: AlertifyService) { }

  ngOnInit() {
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

    if(this.role === 'Admin') { 
      this.loadApartmentsForAdmin();
    }
  }

  loadApartments() {
    this.apartmentService.getApartments(this.pagination.currentPage, this.pagination.itemsPerPage, this.apartmentParams)
    .subscribe((res: PaginatedResult<Apartment[]>) => {
      this.apartments = res.result;
      this.pagination = res.pagination;
    }, error => {
      this.alertify.error(error);
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

    if(this.role === 'Admin') { 
      this.loadApartmentsForAdmin();
    }
    else { 
      this.loadApartments();
    }
  }

  pageChanged(event: any): void {
    this.pagination.currentPage = event.page;

    if(this.role === 'Admin') { 
      this.loadApartmentsForAdmin();
    }
    else { 
      this.loadApartments();
    }
  }


}
