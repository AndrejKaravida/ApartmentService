import { Component, OnInit, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';

@Component({
  selector: 'app-deleteapartmentdialog',
  templateUrl: './deleteapartmentdialog.component.html',
  styleUrls: ['./deleteapartmentdialog.component.css']
})
export class DeleteapartmentdialogComponent implements OnInit {
 
  constructor( 
    public dialogRef: MatDialogRef<DeleteapartmentdialogComponent>,
    @Inject(MAT_DIALOG_DATA) public apartment: any = {}
  ) {}

  ngOnInit() {
  }

  onNoClick(): void {
    this.dialogRef.close();
  }

  delete() { 
    this.dialogRef.close({data: true});
  }

}
