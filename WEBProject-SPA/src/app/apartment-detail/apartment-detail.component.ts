import { Component, OnInit, ViewChild } from '@angular/core';
import { Apartment } from '../_models/apartment';
import { ApartmentService } from '../_services/apartment.service';
import { AlertifyService } from '../_services/alertify.service';
import { ActivatedRoute } from '@angular/router';
import { Moment } from 'moment';
import { DaterangepickerComponent } from 'ngx-daterangepicker-material';
import { moment } from 'ngx-bootstrap/chronos/test/chain';

@Component({
  selector: 'app-apartment-detail',
  templateUrl: './apartment-detail.component.html',
  styleUrls: ['./apartment-detail.component.css']
})
export class ApartmentDetailComponent implements OnInit {
  apartment: any;
  numOfAmentities = 0;
  grade = 0;
  numOfGrades = 0;
  selected: {startDate: Moment, endDate: Moment};

  constructor( private route: ActivatedRoute) { }

  ngOnInit() {
  
    this.route.data.subscribe(data => {
     const key = 'apartment';
     this.apartment = data[key];
     console.log(data[key]);
     this.numOfAmentities = this.apartment.amentities.length;
     let totalGrade = 0;
     this.numOfGrades = this.apartment.comments.length;
     // tslint:disable-next-line: prefer-for-of
     for (let i = 0; i < this.apartment.comments.length; i++) {
      totalGrade += this.apartment.comments[i].grade;
     }
     this.grade = totalGrade / this.numOfGrades;
   });
  }

  choosedDate(event: any) { 
    
  }
}
