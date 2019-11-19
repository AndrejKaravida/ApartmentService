import { Component, OnInit, ViewChild } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { Reservation } from '../_models/reservation';
import { ApartmentService } from '../_services/apartment.service';
import { AuthService } from '../_services/auth.service';
import { AlertifyService } from '../_services/alertify.service';
import { MatSort } from '@angular/material/sort';
import { MatPaginator } from '@angular/material/paginator';

@Component({
  selector: 'app-reservations',
  templateUrl: './reservations.component.html',
  styleUrls: ['./reservations.component.css']
})
export class ReservationsComponent implements OnInit {
  @ViewChild(MatSort, {static: true}) sort: MatSort;
  @ViewChild(MatPaginator, {static: true}) paginator: MatPaginator;
  displayedColumns = [
    'position',
    'image',
    'location',
    'startdate',
    'enddate',
    'timetoarrive',
    'timetoleave',
    'numofnights',
    'totalPrice',
    'status',
    'quit'
  ];

  dataSource = new MatTableDataSource<Reservation>();

  constructor(private apartmentService: ApartmentService, private authService: AuthService,
              private alertify: AlertifyService) {}

  ngOnInit() {
   this.loadReservations();
  }
  
  loadReservations() {
    this.apartmentService.getReservations(this.authService.decodedToken.nameid).subscribe((reservations: Reservation[]) => {
      this.dataSource = new MatTableDataSource<Reservation>(reservations);
      this.dataSource.sort = this.sort;
      this.dataSource.paginator = this.paginator;
    });
  }

  quitReservation(id: number) {
    this.apartmentService.quitReservation(id).subscribe(() => {
      this.alertify.success('Reservation quit!');
      this.loadReservations();
    }, error => { 
      this.alertify.error('Problem quitting reservation.');
    });
  }

  doFilter(filterValue: string) {
    this.dataSource.filter = filterValue.trim().toLowerCase();
  }

}
