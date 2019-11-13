import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { AlertifyService } from '../_services/alertify.service';
import { ApartmentService } from '../_services/apartment.service';
import { MatDialog } from '@angular/material/dialog';
import { DeleteapartmentdialogComponent } from '../deleteapartmentdialog/deleteapartmentdialog.component';


@Component({
  selector: 'app-apartment-card',
  templateUrl: './apartment-card.component.html',
  styleUrls: ['./apartment-card.component.css']
})
export class ApartmentCardComponent implements OnInit {
  @Input() apartment: any;
  @Output() changed = new EventEmitter();

  constructor(public dialog: MatDialog) { }

  ngOnInit() {
  }

  deleteApartment() {

    const dialogRef = this.dialog.open(DeleteapartmentdialogComponent, {
      width: '400px',
      data: { apartment: this.apartment }
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.changed.emit(this.apartment.id);
      }
    });
  }
}
