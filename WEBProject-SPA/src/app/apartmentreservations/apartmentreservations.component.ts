import { Component, OnInit, Input } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { Reservation } from '../_models/reservation';
import { ApartmentService } from '../_services/apartment.service';

@Component({
  selector: 'app-apartmentreservations',
  templateUrl: './apartmentreservations.component.html',
  styleUrls: ['./apartmentreservations.component.css']
})
export class ApartmentreservationsComponent implements OnInit {
  @Input() apartment: number;
  displayedColumns = [
    'position',
    'user',
    'date',
    'numofnights',
    'totalprice',
    'status'
  ];

  dataSource = new MatTableDataSource<Reservation>();

  constructor(private apartmentService: ApartmentService) {}

  ngOnInit() {
    this.loadReservations();
  }

  loadReservations() {
    this.apartmentService.getReservationsForApartment(this.apartment).subscribe((reservations) => {
      this.dataSource = new MatTableDataSource<Reservation>(reservations);
    });
  }

  doFilter(filterValue: string) {
    this.dataSource.filter = filterValue.trim().toLowerCase();
  }


}
