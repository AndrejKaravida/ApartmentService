import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';
import { JwtHelperService } from '@auth0/angular-jwt';
import { Router } from '@angular/router';
import { environment } from 'src/environments/environment';
import { User } from '../_models/user';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  baseUrl = environment.apiUrl + 'auth/';
  jwtHelper = new JwtHelperService();
  decodedToken: any;
  currentUser: User;
  currentRole: string;
  
constructor(private http: HttpClient, private router: Router) {}

  login(model: any) {
  return this.http.post(this.baseUrl + 'login', model)
  .pipe(
    map((response: any) => {
      const user = response;
      if (user) {
        localStorage.setItem('token', user.token);
        localStorage.setItem('user', JSON.stringify(user.user));
        localStorage.setItem('role', JSON.stringify(user.user.role));
        this.decodedToken = this.jwtHelper.decodeToken(user.token);
        this.currentUser = user.user;
        this.currentRole = user.user.role;
        this.router.navigate(['explore']);
      }
    })
  );
}

  register(model: any) {
   return this.http.post(this.baseUrl + 'register', model);
}

loggedIn() {
  const token = localStorage.getItem('token');
  return !this.jwtHelper.isTokenExpired(token);
}

isAdmin() {
  const user = JSON.parse(localStorage.getItem('user'));
  return user.role === 'Admin';
}

isHost(){
  const user = JSON.parse(localStorage.getItem('user'));
  return user.role === 'Host';
}


}
