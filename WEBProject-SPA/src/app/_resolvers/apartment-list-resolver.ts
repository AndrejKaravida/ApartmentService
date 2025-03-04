import { ApartmentService } from '../_services/apartment.service';
import { Resolve, Router, ActivatedRouteSnapshot } from '@angular/router';
import { Apartment } from '../_models/apartment';
import { AlertifyService } from '../_services/alertify.service';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { Injectable } from '@angular/core';

@Injectable()
export class ApartmentListResolver implements Resolve<Apartment[]> {
    pageNumber = 1;
    pageSize = 8;

    constructor(private apartmentService: ApartmentService,
                private router: Router, private alertify: AlertifyService) {}

     resolve(route: ActivatedRouteSnapshot): Observable<Apartment[]> {
        return this.apartmentService.getApartments(this.pageNumber, this.pageSize).pipe(
                        catchError(() => {
                            this.alertify.error('Problem retrieving data!');
                            this.router.navigate(['/login']);
                            return of(null);
                        })
                    );
         }

}
