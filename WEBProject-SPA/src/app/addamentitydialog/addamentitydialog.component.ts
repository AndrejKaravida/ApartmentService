import { Component, OnInit, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';

@Component({
  selector: 'app-addamentitydialog',
  templateUrl: './addamentitydialog.component.html',
  styleUrls: ['./addamentitydialog.component.css']
})
export class AddamentitydialogComponent implements OnInit {
  amentities = {
    essential: false,
    privateparking: false,
    telephone: false,
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
  
  constructor(
    public dialogRef: MatDialogRef<AddamentitydialogComponent>,
    @Inject(MAT_DIALOG_DATA) public apartment: any = {}
  ) {}

  ngOnInit() {
    this.loadAmenities();
  }

  loadAmenities() {
    // tslint:disable-next-line: prefer-for-of
    for (let i = 0; i < this.apartment.apartment.apartmentAmentities.length; i++) {
      const name = this.apartment.apartment.apartmentAmentities[i].amentity.name.toLowerCase();
      this.amentities[name] = true;
    }
  }

  returnAmenities() {

    let amentities = '';

    if (this.amentities.privateparking) {
      amentities += 'privateparking,';
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
    if (this.amentities.telephone) {
      amentities += 'telephone,';
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
    
    this.dialogRef.close({data: amentities});

  }

  onNoClick(): void {
    this.dialogRef.close();
  }

}
