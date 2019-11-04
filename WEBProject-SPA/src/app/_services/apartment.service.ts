import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Apartment } from '../_models/apartment';
import { PaginatedResult } from '../_models/pagination';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class ApartmentService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  getApartments(page?, itemsPerPage?, apartmentParams?): Observable<PaginatedResult<Apartment[]>> {

    const paginatedResult: PaginatedResult<Apartment[]> = new PaginatedResult<Apartment[]>();

    let params = new HttpParams();

    if (page !== null && itemsPerPage !== null) {
      params = params.append('pageNumber', page);
      params = params.append('pageSize', itemsPerPage);
    }

    if (apartmentParams !== null && apartmentParams !== undefined) {
      params = params.append('minPrice', apartmentParams.minPrice);
      params = params.append('maxPrice', apartmentParams.maxPrice);
      params = params.append('minRooms', apartmentParams.minRooms);
      params = params.append('maxRooms', apartmentParams.maxRooms);
      params = params.append('guests', apartmentParams.guests);
      params = params.append('city', apartmentParams.city);
      params = params.append('country', apartmentParams.country);
    }

    return this.http.get<Apartment[]>(this.baseUrl + 'apartments', {observe: 'response', params}).pipe(
      map(response => {
        paginatedResult.result = response.body;
        if (response.headers.get('Pagination') !== null) {
          paginatedResult.pagination = JSON.parse(response.headers.get('Pagination'));
        }
        return paginatedResult;
      })
    );
  }

  getApartmentsForUser(id: number, page?, itemsPerPage?): Observable<PaginatedResult<Apartment[]>> {

    const paginatedResult: PaginatedResult<Apartment[]> = new PaginatedResult<Apartment[]>();

    let params = new HttpParams();

    if (page !== null && itemsPerPage !== null) {
      params = params.append('pageNumber', page);
      params = params.append('pageSize', itemsPerPage);
    }

    return this.http.get<Apartment[]>(this.baseUrl + 'apartments/users/' + id, {observe: 'response', params}).pipe(
      map(response => {
        paginatedResult.result = response.body;
        if (response.headers.get('Pagination') !== null) {
          paginatedResult.pagination = JSON.parse(response.headers.get('Pagination'));
        }
        return paginatedResult;
      })
    );
  }

  getApartment(id: number): Observable<Apartment> {
    return this.http.get<Apartment>(this.baseUrl + 'apartments/' + id);
  }

  createApartment(id: number, apartment: Apartment) {
    return this.http.post(this.baseUrl + 'apartments/' + id, apartment);
  }

}
