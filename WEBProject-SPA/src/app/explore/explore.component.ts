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

  constructor(private route: ActivatedRoute, private apartmentService: ApartmentService,
              private alertify: AlertifyService) { }

  ngOnInit() {
    this.route.data.subscribe(data => {
      const key = 'apartments';
      this.apartments = data[key].result;
      this.pagination = data[key].pagination;
    });
  }

  loadApartments() {
    this.apartmentService.getApartments(this.pagination.currentPage, this.pagination.itemsPerPage)
    .subscribe((res: PaginatedResult<Apartment[]>) => {
      this.apartments = res.result;
      this.pagination = res.pagination;
    }, error => {
      this.alertify.error(error);
    });
  }

  pageChanged(event: any): void {
    this.pagination.currentPage = event.page;
    this.loadApartments();
  }


}
