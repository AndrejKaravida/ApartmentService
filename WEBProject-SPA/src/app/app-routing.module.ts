import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { AuthGuard } from './_guards/auth.guard';
import { ExploreComponent } from './explore/explore.component';
import { MyAppartmentsComponent } from './my-appartments/my-appartments.component';
import { ProfileComponent } from './profile/profile.component';
import { UsersComponent } from './users/users.component';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { ApartmentDetailComponent } from './apartment-detail/apartment-detail.component';
import { ApartmentDetailResolver } from './_resolvers/apartment-detail-resolver';
import { ApartmentListResolver } from './_resolvers/apartment-list-resolver';
import { UserProfileResolver } from './_resolvers/user-profile-resolver';
import { PreventUnsavedChanges } from './_guards/prevent-unsaved-changes.guard';
import { AddApartmentComponent } from './add-apartment/add-apartment.component';
import { ApartmentForUserResolver } from './_resolvers/apartment-foruser-resolver';
import { ReservationsComponent } from './reservations/reservations.component';

const routes: Routes = [
  { path: '', component: HomeComponent},
  { path: 'explore', component: ExploreComponent, canActivate: [AuthGuard],
                        resolve: {apartments: ApartmentListResolver}},
  { path: 'explore/:id', component: ApartmentDetailComponent, canActivate: [AuthGuard],
                        resolve: {apartment: ApartmentDetailResolver}},
  { path: 'myapps', component: MyAppartmentsComponent , canActivate: [AuthGuard],
                        resolve: {apartments: ApartmentForUserResolver}},
  { path: 'addnew', component: AddApartmentComponent , canActivate: [AuthGuard]},
  { path: 'reservations', component: ReservationsComponent , canActivate: [AuthGuard]},
  { path: 'profile', component: ProfileComponent , canActivate: [AuthGuard],
      resolve: {user: UserProfileResolver}, canDeactivate: [PreventUnsavedChanges]},
  { path: 'users', component: UsersComponent , canActivate: [AuthGuard]},
  { path: 'login', component: LoginComponent},
  { path: 'register', component: RegisterComponent},
  { path: '**', redirectTo: 'login', pathMatch: 'full'}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
