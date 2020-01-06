import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, ChildActivationStart } from '@angular/router';
import { Apartment } from '../_models/apartment';
import { ApartmentService } from '../_services/apartment.service';
import { AlertifyService } from '../_services/alertify.service';
import { PaginatedResult, Pagination } from '../_models/pagination';
import { AuthService } from '../_services/auth.service';
import { ApartmentCardComponent } from '../apartment-card/apartment-card.component';
import { UserService } from '../_services/user.service';
import { User } from '../_models/user';

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
  currentUser: any = {};
  admin = false;

  constructor(private route: ActivatedRoute, private apartmentService: ApartmentService,
              private alertify: AlertifyService, private authService: AuthService,
              private userService: UserService) {}

  ngOnInit() {

    this.route.params.subscribe(params => {
      const key = 'id';
      if (params[key]) {
        this.admin = true;
        this.loadApartmentsForUser(params[key]);
        this.id = params[key];
        this.userService.getUser(params[key]).subscribe((user: User) => {
          this.currentUser = user;
        }, error => {
          this.alertify.error('There is an error retreiving user data');
        });
      } else {
        this.route.data.subscribe(data => {
          const key = 'apartments';
          this.apartments = data[key].result;
          this.pagination = data[key].pagination;
        });
      }
    });
  }

  loadApartmentsForUser(id: number) {
    this.apartmentService.getApartmentsForUser(id, 1, 8).subscribe((res: PaginatedResult<Apartment[]>) => {
      this.apartments = res.result;
      this.pagination = res.pagination;
    }, error => {
      this.alertify.error(error);
    });
  }

  deleteApartment(event) {
    this.apartmentService.deleteApartment(event).subscribe(() => {
      this.alertify.success('Successfully deleted apartment!');
      this.loadApartments();
    }, error => {
      this.alertify.error('Error while deleting apartment');
    });
  }

  loadApartments() {

    if (this.admin)
    {
      this.route.params.subscribe(params => {
        const key = 'id';
        this.apartmentService.getApartmentsForUser(params[key], this.pagination.currentPage,
          this.pagination.itemsPerPage, this.apartmentParams).subscribe((res: PaginatedResult<Apartment[]>) => {
          this.apartments = res.result;
          this.pagination = res.pagination;
        }, error => {
          this.alertify.error(error);
        });
      });
    } else {
        this.apartmentService.getApartmentsForUser(this.authService.decodedToken.nameid, this.pagination.currentPage,
          this.pagination.itemsPerPage, this.apartmentParams)
        .subscribe((res: PaginatedResult<Apartment[]>) => {
          this.apartments = res.result;
          this.pagination = res.pagination;
        }, error => {
          this.alertify.error(error);
        });
    }
  }

  pageChanged(event: any): void {
    this.pagination.currentPage = event.page;
    this.loadApartments();
  }

}
