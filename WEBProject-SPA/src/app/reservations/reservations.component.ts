import { Component, OnInit } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { Reservation } from '../_models/reservation';
import { ApartmentService } from '../_services/apartment.service';
import { AuthService } from '../_services/auth.service';

@Component({
  selector: 'app-reservations',
  templateUrl: './reservations.component.html',
  styleUrls: ['./reservations.component.css']
})
export class ReservationsComponent implements OnInit {
  displayedColumns = [
    'position',
    'image',
    'location',
    'startdate',
    'enddate',
    'timetoarrive',
    'timetoleave',
    'numofnights',
    'totalprice',
    'status'
  ];

  dataSource = new MatTableDataSource<Reservation>();

  constructor(private apartmentService: ApartmentService, private authService: AuthService) {}

  ngOnInit() {
   this.loadReservations();
  }

  loadReservations() {
    this.apartmentService.getReservations(this.authService.decodedToken.nameid).subscribe((reservations) => {
      this.dataSource = new MatTableDataSource<Reservation>(reservations);
    });
  }

  doFilter(filterValue: string) {
    this.dataSource.filter = filterValue.trim().toLowerCase();
  }

}
