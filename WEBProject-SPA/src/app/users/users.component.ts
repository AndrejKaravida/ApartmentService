import { Component, OnInit, ViewChild } from '@angular/core';
import { User } from '../_models/user';
import { MatTableDataSource } from '@angular/material/table';
import { UserService } from '../_services/user.service';
import { AlertifyService } from '../_services/alertify.service';
import { Router } from '@angular/router';
import { MatPaginator } from '@angular/material/paginator';

@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.css']
})
export class UsersComponent implements OnInit {
  @ViewChild(MatPaginator, {static: true}) paginator: MatPaginator;
  displayedColumns = [
    'position',
    'username',
    'gender',
    'role',
    'details',
    'edit',
    'block',
    'unblock',
    'host',
    'delete'
  ];

  dataSource = new MatTableDataSource<User>();

  constructor(private userService: UserService, private alertify: AlertifyService,
              private router: Router) { }

  ngOnInit() {
   this.loadUsers();
  }

  loadUsers() {
    this.userService.getUsers().subscribe((users) => {
      this.dataSource = new MatTableDataSource<User>(users);
      this.dataSource.paginator = this.paginator;
    });
  }

  doFilter(filterValue: string) {
    this.dataSource.filter = filterValue.trim().toLowerCase();
  }

  blockUser(id: number) {
    this.alertify.confirm('Are you sure you want to block this user?', () => {
      this.userService.blockUser(id).subscribe(() => {
        this.alertify.success('User blocked successfully!');
        this.loadUsers();
      }, error => {
        this.alertify.error('Error when trying to block user!');
      });
    });
  }

 

  unblockUser(id: number) {
    this.alertify.confirm('Are you sure you want to unblock this user?', () => {
      this.userService.unblockUser(id).subscribe(() => {
        this.alertify.success('User unblocked successfully!');
        this.loadUsers();
      }, error => {
        this.alertify.error('Error when trying to unblock user!');
      });
    });
  }

  makeHost(id: number) {
    this.userService.makehost(id).subscribe(() => {
      this.alertify.success('User made host successfully!');
      this.loadUsers();
    }, error => {
      this.alertify.error('Error when trying to make user host!');
    });
  }

  deleteUser(id: number) {
    this.alertify.confirm('Are you sure you want to delete this user?', () => {
      this.userService.deleteUser(id).subscribe(() => {
        this.alertify.success('User deleted successfully!');
        this.loadUsers();
      }, error => {
        this.alertify.error('Error when trying to delete user!');
      });
    });
  }

  onProfileEdit(id: number) {
    const url = '/profile/' + id;
    this.router.navigateByUrl(url);
  }

   onUserDetails(id: number) {
     const url = '/myapps/' + id;
     this.router.navigateByUrl(url);
   }

}
