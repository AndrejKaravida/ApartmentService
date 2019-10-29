import { Component, OnInit } from '@angular/core';
import { User } from '../_models/user';
import { MatTableDataSource } from '@angular/material/table';
import { UserService } from '../_services/user.service';

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
    'delete'
  ];

  dataSource = new MatTableDataSource<User>();

  constructor(private userService: UserService) { }

  ngOnInit() {
    this.userService.getUsers().subscribe((users) => {
      this.dataSource = new MatTableDataSource<User>(users);
    });
  }

  doFilter(filterValue: string) {
    this.dataSource.filter = filterValue.trim().toLowerCase();
  }

}
