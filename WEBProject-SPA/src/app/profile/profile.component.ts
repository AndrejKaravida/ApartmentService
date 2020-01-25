import { Component, OnInit, ViewChild, HostListener } from '@angular/core';
import { User } from '../_models/user';
import { ActivatedRoute } from '@angular/router';
import { AlertifyService } from '../_services/alertify.service';
import { NgForm } from '@angular/forms';
import { UserService } from '../_services/user.service';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent implements OnInit {
  selectedValue = '';
  currentUser: any = {};
  admin = false;
  @ViewChild('editForm', {static: false}) editForm: NgForm;
  @HostListener('window:beforeunload', ['$event'])
   unloadNotification($event: any) {
     if (this.editForm.dirty) {
       $event.returnValue = true;
     }
  }
  constructor(private route: ActivatedRoute, private alertify: AlertifyService,
              private userService: UserService) { }

  ngOnInit() {

    // tslint:disable-next-line: deprecation
    this.route.params.subscribe(params => {
      const key = 'id';
      if (params[key]) {
        this.admin = true;
        this.userService.getUser(params[key]).subscribe((user: User) => {
          this.currentUser = user;
        }, error => {
          this.alertify.error('There is an error retreiving user data');
        });
      } else{
        this.route.data.subscribe(data => {
          const key = 'user';
          this.currentUser = data[key];
        });
      }
    });
  }

  updateUser() {
    this.userService.updateUser(this.currentUser.id, this.currentUser).subscribe(next => {
      this.alertify.success('Updated successfully!');
      this.editForm.reset(this.currentUser);
    }, error => {
      this.alertify.error(error);
    });
  } 
}
