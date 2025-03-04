import { Component, OnInit, Input, ViewChild } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { Reservation } from '../_models/reservation';
import { ApartmentService } from '../_services/apartment.service';
import { AlertifyService } from '../_services/alertify.service';
import { MatSort } from '@angular/material/sort';
import { MatPaginator } from '@angular/material/paginator';

@Component({
  selector: 'app-apartmentreservations',
  templateUrl: './apartmentreservations.component.html',
  styleUrls: ['./apartmentreservations.component.css']
})
export class ApartmentreservationsComponent implements OnInit {
  @Input() apartment: number;
  @ViewChild(MatSort, {static: true}) sort: MatSort;
  @ViewChild(MatPaginator, {static: true}) paginator: MatPaginator;

  displayedColumns = [
    'position',
    'user',
    'date',
    'numofnights',
    'totalPrice',
    'status',
    'action'
  ];

  dataSource = new MatTableDataSource<Reservation>();

  constructor(private apartmentService: ApartmentService, private alertify: AlertifyService) {}

  ngOnInit() {
    this.loadReservations();
  }

  loadReservations() {
    this.apartmentService.getReservationsForApartment(this.apartment).subscribe((reservations) => {
      this.dataSource = new MatTableDataSource<Reservation>(reservations);
      this.dataSource.sort = this.sort;
      this.dataSource.paginator = this.paginator;
    });
  }

  doFilter(filterValue: string) {
    this.dataSource.filter = filterValue.trim().toLowerCase();
  }

  acceptReservation(id: number) {
    this.apartmentService.acceptReservation(id).subscribe(() => {
      this.alertify.success('Reservation accepted!');
      this.loadReservations();
    }, error => {
      this.alertify.error('Problem accepting reservation.');
    });
  }

  denyReservation(id: number) {
    this.apartmentService.denyReservation(id).subscribe(() => {
      this.alertify.warning('Reservation denied!');
      this.loadReservations();
    }, error => {
      this.alertify.error('Problem deniding reservation.');
    });
  }

  finishReservation(id: number) {
    this.apartmentService.finishReservation(id).subscribe(() => {
      this.alertify.success('Reservation finished!');
      this.loadReservations();
    }, error => {
      this.alertify.error('You cannot set reservation to finished before its end date!');
    });
  }


}
