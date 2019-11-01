import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Apartment } from '../_models/apartment';

@Injectable({
  providedIn: 'root'
})
export class ApartmentService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  getApartments(): Observable<Apartment[]> {
    return this.http.get<Apartment[]>(this.baseUrl + 'apartments/');
  }

  getApartment(id: number): Observable<Apartment> {
    return this.http.get<Apartment>(this.baseUrl + 'apartments/' + id);
  }

  createApartment(id: number, apartment: Apartment) {
    return this.http.post(this.baseUrl + 'apartments/' + id, apartment);
  }

}
