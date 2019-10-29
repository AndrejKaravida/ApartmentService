import { Component, OnInit } from '@angular/core';
import { Apartment } from '../_models/apartment';
import { ApartmentService } from '../_services/apartment.service';
import { AlertifyService } from '../_services/alertify.service';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-apartment-detail',
  templateUrl: './apartment-detail.component.html',
  styleUrls: ['./apartment-detail.component.css']
})
export class ApartmentDetailComponent implements OnInit {
  apartment: Apartment;

  constructor(private apartmentService: ApartmentService, private alertify: AlertifyService,
              private route: ActivatedRoute) { }

  ngOnInit() {
   this.route.data.subscribe(data => {
     const key = 'apartment';
     this.apartment = data[key];
   });
  }
}
