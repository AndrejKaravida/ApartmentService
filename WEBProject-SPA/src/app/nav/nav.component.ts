import { Component, OnInit } from '@angular/core';
import { AuthService } from '../_services/auth.service';
import { AlertifyService } from '../_services/alertify.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {

  model: any = {};

  constructor(public authService: AuthService, private alertify: AlertifyService,
              private router: Router) { }

  ngOnInit() {
  }

  loggedIn() {
    return this.authService.loggedIn();
  }

  isHost(){
    return this.authService.isHost();
  }

  isAdmin(){
    return this.authService.isAdmin();
  }

  logout() {
    this.authService.decodedToken = null;
    this.authService.currentUser = null;
    this.authService.currentRole = null;
    localStorage.removeItem('token');
    localStorage.removeItem('user');
    localStorage.removeItem('role');
    localStorage.removeItem('username');
    this.alertify.message('Logged out!');
    this.router.navigate(['login']);
  }

}
