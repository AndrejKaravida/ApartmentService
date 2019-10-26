import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { AuthGuard } from './_guards/auth.guard';
import { ExploreComponent } from './explore/explore.component';
import { MyAppartmentsComponent } from './my-appartments/my-appartments.component';

const routes: Routes = [
  { path: '', component: HomeComponent},
  { path: 'explore', component: ExploreComponent, canActivate: [AuthGuard]},
  { path: 'myapps', component: MyAppartmentsComponent , canActivate: [AuthGuard]},
  { path: '**', redirectTo: '', pathMatch: 'full'}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }