import { Component, OnInit } from '@angular/core';
import { User } from '../_models/user';
import { MatTableDataSource } from '@angular/material/table';
import { UserService } from '../_services/user.service';
import { AlertifyService } from '../_services/alertify.service';

@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.css']
})
export class UsersComponent implements OnInit {
  displayedColumns = [
    'position',
    'username',
    'gender',
    'role',
    'details',
    'edit',
    'block',
    'unblock',
    'delete'
  ];

  dataSource = new MatTableDataSource<User>();

  constructor(private userService: UserService, private alertify: AlertifyService) { }

  ngOnInit() {
   this.loadUsers();
  }

  loadUsers() {
    this.userService.getUsers().subscribe((users) => {
      this.dataSource = new MatTableDataSource<User>(users);
    });
  }

  doFilter(filterValue: string) {
    this.dataSource.filter = filterValue.trim().toLowerCase();
  }

  blockUser(id: number){
    this.userService.blockUser(id).subscribe(() => {
      this.alertify.success('User blocked successfully!');
      this.loadUsers();
    }, error => { 
      this.alertify.error('Error when trying to block user!');
    });
  }

  unblockUser(id: number){
    this.userService.unblockUser(id).subscribe(() => {
      this.alertify.success('User unblocked successfully!');
      this.loadUsers();
    }, error => { 
      this.alertify.error('Error when trying to unblock user!');
    });
  }

  deleteUser(id: number){
    this.userService.deleteUser(id).subscribe(() => {
      this.alertify.success('User deleted successfully!');
      this.loadUsers();
    }, error => { 
      this.alertify.error('Error when trying to delete user!');
    });
  }

}
