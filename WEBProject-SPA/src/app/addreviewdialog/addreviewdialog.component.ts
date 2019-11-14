import { Component, OnInit, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-addreviewdialog',
  templateUrl: './addreviewdialog.component.html',
  styleUrls: ['./addreviewdialog.component.css']
})
export class AddreviewdialogComponent implements OnInit {

  constructor( 
    public dialogRef: MatDialogRef<AddreviewdialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any = {}
  ) {}

  ngOnInit() {
  }

  onNoClick(): void {
    this.dialogRef.close();
  }

  onAdd(form: NgForm) { 
    const text = form.value.content;
    const grade = form.value.grade;

    this.dialogRef.close({text, grade});
  }

}
