import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Apartment } from '../_models/apartment';
import { PaginatedResult } from '../_models/pagination';
import { map } from 'rxjs/operators';
import { Reservation } from '../_models/reservation';

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
      params = params.append('orderby', apartmentParams.orderby);
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


  getApartmentsForAdmin(page?, itemsPerPage?, apartmentParams?): Observable<PaginatedResult<Apartment[]>> {

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
      params = params.append('orderby', apartmentParams.orderby);
    }

    return this.http.get<Apartment[]>(this.baseUrl + 'apartments/admin', {observe: 'response', params}).pipe(
      map(response => {
        paginatedResult.result = response.body;
        if (response.headers.get('Pagination') !== null) {
          paginatedResult.pagination = JSON.parse(response.headers.get('Pagination'));
        }
        return paginatedResult;
      })
    );
  }

  getApartmentsForUser(id: number, page?, itemsPerPage?, apartmentParams?): Observable<PaginatedResult<Apartment[]>> {

    const paginatedResult: PaginatedResult<Apartment[]> = new PaginatedResult<Apartment[]>();

    let params = new HttpParams();

    if (page !== null && itemsPerPage !== null) {
      params = params.append('pageNumber', page);
      params = params.append('pageSize', itemsPerPage);
    }

    if (apartmentParams !== null && apartmentParams !== undefined) {
      params = params.append('orderby', apartmentParams.orderby);
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

  addAmenities(apid: number, amenities: string) {
    return this.http.post(this.baseUrl + 'apartments/addamentities/' + apid, {amenities});
  }

  removeAmentity(apid: number, amname: string): Observable<Apartment> {
    return this.http.get<Apartment>(this.baseUrl + 'apartments/removeamentity/' + apid + '/' + amname);
  }

  changePrice(apid: number, new_price: number): Observable<Apartment> {
    return this.http.get<Apartment>(this.baseUrl + 'apartments/changeprice/' + apid + '/' + new_price);
  }

  changeGuests(apid: number, new_number: number): Observable<Apartment> {
    return this.http.get<Apartment>(this.baseUrl + 'apartments/changeguests/' + apid + '/' + new_number);
  }

  changeRooms(apid: number, new_number: number): Observable<Apartment> {
    return this.http.get<Apartment>(this.baseUrl + 'apartments/changerooms/' + apid + '/' + new_number);
  }

  changeArrival(apid: number, new_time: string): Observable<Apartment> {
    return this.http.get<Apartment>(this.baseUrl + 'apartments/changearrival/' + apid + '/' + new_time);
  }

  changeDeparture(apid: number, new_time: string): Observable<Apartment> {
    return this.http.get<Apartment>(this.baseUrl + 'apartments/changedeparture/' + apid + '/' + new_time);
  }

  deleteApartment(apid: number): Observable<Apartment> {
    return this.http.get<Apartment>(this.baseUrl + 'apartments/delete/' + apid);
  }

  commentApartment(apid: number, userid: number, content: string, grade: number): Observable<Apartment> {
    return this.http.post<Apartment>(this.baseUrl + 'comments/' + apid, {content, grade, userid});
  }

  approveComment(id: number): Observable<Apartment> {
    return this.http.get<Apartment>(this.baseUrl + 'comments/approve/' + id);
  }

  deleteComment(id: number): Observable<Apartment> {
    return this.http.get<Apartment>(this.baseUrl + 'comments/delete/' + id);
  }

  setMainPhoto(appId: number, photoid: number) {
    return this.http.get(this.baseUrl + 'upload/setmain/' + appId + '/' + photoid);
  }

  deletePhoto(photoid: number) {
    return this.http.get(this.baseUrl + 'upload/delete/' + photoid);
  }

  makeActive(apId: number) {
    return this.http.get(this.baseUrl + 'apartments/makeactive/' + apId);
  }

  makeReservation(apartmentid: number, userid: number, startdate: string, enddate: string) {
    return this.http.post(this.baseUrl + 'reservations', {apartmentid, userid, startdate, enddate});
  }

  getReservations(id: number): Observable<Reservation[]> {
    return this.http.get<Reservation[]>(this.baseUrl + 'reservations/' + id);
  }

  getReservationsForApartment(id: number): Observable<Reservation[]> {
    return this.http.get<Reservation[]>(this.baseUrl + 'reservations/apt/' + id);
  }

  acceptReservation(id: number) {
    return this.http.get(this.baseUrl + 'reservations/accept/' + id);
  }

  denyReservation(id: number) {
    return this.http.get(this.baseUrl + 'reservations/deny/' + id);
  }

  finishReservation(id: number) {
    return this.http.get(this.baseUrl + 'reservations/finish/' + id);
  }

  quitReservation(id: number) {
    return this.http.get(this.baseUrl + 'reservations/quit/' + id);
  }

  getPermission(usid: number, appid: number) {
    return this.http.get(this.baseUrl + 'comments/getpermission/' + usid + '/' + appid);
  }

}
