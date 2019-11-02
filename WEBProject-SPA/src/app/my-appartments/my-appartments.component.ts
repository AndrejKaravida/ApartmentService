import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Apartment } from '../_models/apartment';

@Component({
  selector: 'app-my-appartments',
  templateUrl: './my-appartments.component.html',
  styleUrls: ['./my-appartments.component.css']
})
export class MyAppartmentsComponent implements OnInit {
  apartments: Apartment[];

  constructor(private route: ActivatedRoute) { }

  ngOnInit() {
    this.route.data.subscribe(data => {
      const key = 'apartments';
      this.apartments = data[key];
    });
  }

}
