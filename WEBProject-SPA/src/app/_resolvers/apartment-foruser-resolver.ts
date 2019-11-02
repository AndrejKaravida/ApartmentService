import { ApartmentService } from '../_services/apartment.service';
import { Resolve, Router, ActivatedRouteSnapshot } from '@angular/router';
import { Apartment } from '../_models/apartment';
import { AlertifyService } from '../_services/alertify.service';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { Injectable } from '@angular/core';
import { AuthService } from '../_services/auth.service';

@Injectable()
export class ApartmentForUserResolver implements Resolve<Apartment[]> {

    constructor(private apartmentService: ApartmentService, private authService: AuthService,
                private router: Router, private alertify: AlertifyService) {}

     resolve(route: ActivatedRouteSnapshot): Observable<Apartment[]> {
        return this.apartmentService.getApartmentsForUser(this.authService.decodedToken.nameid).pipe(
                        catchError(() => {
                            this.alertify.error('Problem retrieving data!');
                            this.router.navigate(['/explore']);
                            return of(null);
                        })
                    );
         }

}
