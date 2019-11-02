import { Component, OnInit, Input } from '@angular/core';
import { Apartment } from '../_models/apartment';

@Component({
  selector: 'app-apartment-card',
  templateUrl: './apartment-card.component.html',
  styleUrls: ['./apartment-card.component.css']
})
export class ApartmentCardComponent implements OnInit {
  @Input() apartment: any;

  constructor() { }

  ngOnInit() {
  }

}
