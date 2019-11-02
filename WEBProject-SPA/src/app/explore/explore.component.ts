import { Component, OnInit } from '@angular/core';
import { Apartment } from '../_models/apartment';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-explore',
  templateUrl: './explore.component.html',
  styleUrls: ['./explore.component.css']
})
export class ExploreComponent implements OnInit {
  apartments: Apartment[];

  constructor(private route: ActivatedRoute) { }

  ngOnInit() {
    this.route.data.subscribe(data => {
      const key = 'apartments';
      this.apartments = data[key];
    });
  }

}
