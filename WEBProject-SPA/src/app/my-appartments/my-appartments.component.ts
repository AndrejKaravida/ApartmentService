import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, ChildActivationStart } from '@angular/router';
import { Apartment } from '../_models/apartment';
import { ApartmentService } from '../_services/apartment.service';
import { AlertifyService } from '../_services/alertify.service';
import { PaginatedResult, Pagination } from '../_models/pagination';
import { AuthService } from '../_services/auth.service';
import { ApartmentCardComponent } from '../apartment-card/apartment-card.component';

@Component({
  selector: 'app-my-appartments',
  templateUrl: './my-appartments.component.html',
  styleUrls: ['./my-appartments.component.css']
})
export class MyAppartmentsComponent implements OnInit {
  apartments: Apartment[];
  apartmentParams: any = {};
  pagination: Pagination;
  id;

  constructor(private route: ActivatedRoute, private apartmentService: ApartmentService,
              private alertify: AlertifyService, private authService: AuthService,
             ) {
  }

  ngOnInit() {
    this.route.data.subscribe(data => {
      const key = 'apartments';
      this.apartments = data[key].result;
      this.pagination = data[key].pagination;
    });
  }

  deleteApartment(event) { 
    this.apartmentService.deleteApartment(event).subscribe(()=> { 
      this.alertify.success('Successfully deleted apartment!');
      this.loadApartments();  
    }, error => { 
      this.alertify.error('Error while deleting apartment');
    });
  }

  loadApartments() {
    this.apartmentService.getApartmentsForUser(this.authService.decodedToken.nameid, this.pagination.currentPage,
      this.pagination.itemsPerPage, this.apartmentParams)
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
