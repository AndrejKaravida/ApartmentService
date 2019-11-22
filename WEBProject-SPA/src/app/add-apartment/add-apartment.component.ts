import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { FormGroup, Validators, FormBuilder } from '@angular/forms';
import { Apartment } from '../_models/apartment';
import { ApartmentService } from '../_services/apartment.service';
import { AlertifyService } from '../_services/alertify.service';
import { AuthService } from '../_services/auth.service';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';


@Component({
  selector: 'app-add-apartment',
  templateUrl: './add-apartment.component.html',
  styleUrls: ['./add-apartment.component.css']
})
export class AddApartmentComponent implements OnInit {
  lat = 51.678418;
  lng = 7.809007;
  firstFormGroup: FormGroup;
  secondFormGroup: FormGroup;
  thirdFormGroup: FormGroup;
  amentities = {
    essential: false,
    airconditioning: false,
    heat: false,
    hairdryer: false,
    closet: false,
    iron: false,
    tv: false,
    privateentrance: false,
    shampoo: false,
    wifi: false,
    desk: false,
    breakfast: false,
    fireextinguisher: false,
    carbon: false,
    smoke: false,
    firstaidkit: false
  };

  newApartment: Apartment = {
    id: null,
    type: null,
    numberOfGuests: 0,
    numberOfRooms: 0,
    pricePerNight: 0,
    timeToArrive: null,
    timeToLeave: null,
    street: '',
    city: '',
    country: '',
    zip: null,
    apt: null,
    status: 'Inactive',
    amentities: ''
  };

  country = '';

  selectedFile = null;

  constructor(private formBuilder: FormBuilder, private apartmentService: ApartmentService,
              private alertify: AlertifyService, private authService: AuthService,
              private http: HttpClient, private router: Router) { }

  ngOnInit() {
    this.firstFormGroup = this.formBuilder.group({
      type: ['', Validators.required]
    });
    this.secondFormGroup = this.formBuilder.group({
      numberOfGuests: ['', [Validators.min(1), Validators.required]],
      numberOfRooms: ['', Validators.required],
      timeToArrive: ['', Validators.required],
      timeToLeave: ['', Validators.required]
    });
    this.thirdFormGroup = this.formBuilder.group({
      country: ['', [Validators.required, Validators.minLength(5)]],
      street: ['', [Validators.required, Validators.minLength(5)]],
      apt: [''],
      city: ['', [Validators.required, Validators.minLength(5)]],
      zip: ['', [Validators.required, Validators.min(1), Validators.max(100000)]]
    });
  }

  onFileSelected(event){
    this.selectedFile = event.target.files[0] as File;
  }

  addApartment() {

    let amentities = '';

    if (this.amentities.airconditioning) {
      amentities += 'airconditioning,';
    }
    if (this.amentities.breakfast) {
      amentities += 'breakfast,';
    }
    if (this.amentities.carbon) {
      amentities += 'carbon,';
    }
    if (this.amentities.closet) {
      amentities += 'closet,';
    }
    if (this.amentities.desk) {
      amentities += 'desk,';
    }
    if (this.amentities.essential) {
      amentities += 'essential,';
    }
    if (this.amentities.fireextinguisher) {
      amentities += 'fireextinguisher,';
    }
    if (this.amentities.firstaidkit) {
      amentities += 'firstaidkit,';
    }
    if (this.amentities.hairdryer) {
      amentities += 'hairdryer,';
    }
    if (this.amentities.heat) {
      amentities += 'heat,';
    }
    if (this.amentities.iron) {
      amentities += 'iron,';
    }
    if (this.amentities.privateentrance) {
      amentities += 'privateentrance,';
    }
    if (this.amentities.shampoo) {
      amentities += 'shampoo,';
    }
    if (this.amentities.smoke) {
      amentities += 'smoke,';
    }

    if (this.amentities.tv) {
      amentities += 'tv,';
    }

    if (this.amentities.wifi) {
      amentities += 'wifi';
    }

    this.newApartment.numberOfGuests = this.secondFormGroup.get('numberOfGuests').value;
    this.newApartment.numberOfRooms = +this.secondFormGroup.get('numberOfRooms').value;
    this.newApartment.timeToArrive = this.secondFormGroup.get('timeToArrive').value;
    this.newApartment.timeToLeave = this.secondFormGroup.get('timeToLeave').value;
    this.newApartment.country = this.thirdFormGroup.get('country').value;
    this.newApartment.street = this.thirdFormGroup.get('street').value;
    this.newApartment.apt = this.thirdFormGroup.get('apt').value;
    this.newApartment.zip = this.thirdFormGroup.get('zip').value;
    this.newApartment.city = this.thirdFormGroup.get('city').value;
    this.newApartment.amentities = amentities;

    this.apartmentService.createApartment(this.authService.decodedToken.nameid, this.newApartment).subscribe((data: any) => {
      const fd = new FormData();
  
      fd.append('file', this.selectedFile, this.selectedFile.name);
      return this.http.post('http://localhost:5000/api/upload/' + data.apartmentId, fd)
      .subscribe(res => {
        this.alertify.success('Successfully added apartment!');
        this.router.navigate(['myapps']);
        
      });

     }, error => {
       this.alertify.error('There was a problem saving new apartment, please try again');
     });

  }

  increasePrice() {
    this.newApartment.pricePerNight++;
  }

  decreasePrice() {
    this.newApartment.pricePerNight--;
  }

}
