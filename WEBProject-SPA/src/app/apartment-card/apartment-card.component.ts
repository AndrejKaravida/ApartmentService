import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { DeleteapartmentdialogComponent } from '../deleteapartmentdialog/deleteapartmentdialog.component';
import { AuthService } from '../_services/auth.service';


@Component({
  selector: 'app-apartment-card',
  templateUrl: './apartment-card.component.html',
  styleUrls: ['./apartment-card.component.css']
})
export class ApartmentCardComponent implements OnInit {
  @Input() apartment: any;
  @Output() changed = new EventEmitter();
  role = '';
  username = '';

  constructor(public dialog: MatDialog) { }

  ngOnInit() {
    this.role = localStorage.getItem('role');
    this.role = this.role.substr(1);
    this.role = this.role.substr(0, this.role.length - 1);
    this.username = localStorage.getItem('username');
    this.username = this.username.substr(1);
    this.username = this.username.substr(0, this.username.length - 1);
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
