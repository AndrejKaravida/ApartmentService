import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BsDropdownModule, PaginationModule, ButtonsModule, TabsModule } from 'ngx-bootstrap';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { AuthService } from './_services/auth.service';
import { NavComponent } from './nav/nav.component';
import { RegisterComponent } from './register/register.component';
import { ErrorInterceptorProvider } from './_services/error.interceptor';
import { AlertifyService } from './_services/alertify.service';
import { AuthGuard } from './_guards/auth.guard';
import { ExploreComponent } from './explore/explore.component';
import { MyAppartmentsComponent } from './my-appartments/my-appartments.component';
import { ProfileComponent } from './profile/profile.component';
import { UsersComponent } from './users/users.component';
import { LoginComponent } from './login/login.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule} from '@angular/material/form-field';
import { MatDividerModule} from '@angular/material/divider';
import { MatInputModule} from '@angular/material/input';
import { MatButtonModule} from '@angular/material/button';
import { UserService } from './_services/user.service';
import { ApartmentService } from './_services/apartment.service';
import { ApartmentCardComponent } from './apartment-card/apartment-card.component';
import { JwtModule } from '@auth0/angular-jwt';
import { ApartmentDetailResolver } from './_resolvers/apartment-detail-resolver';
import { ApartmentDetailComponent } from './apartment-detail/apartment-detail.component';
import { ApartmentListResolver } from './_resolvers/apartment-list-resolver';
import { MatTableModule } from '@angular/material/table';
import { MatIconModule } from '@angular/material/icon';
import { MatSelectModule } from '@angular/material/select';
import { UserProfileResolver } from './_resolvers/user-profile-resolver';
import { PreventUnsavedChanges } from './_guards/prevent-unsaved-changes.guard';
import {MatCheckboxModule} from '@angular/material/checkbox';
import { AddApartmentComponent } from './add-apartment/add-apartment.component';
import {MatStepperModule} from '@angular/material/stepper';
import {MatSliderModule} from '@angular/material/slider';
import { ApartmentForUserResolver } from './_resolvers/apartment-foruser-resolver';
import { ReservationsComponent } from './reservations/reservations.component';
import { AdminGuard } from './_guards/admin.guard';
import { HostGuard } from './_guards/host.guard';
import { NgxGalleryModule } from 'ngx-gallery';
import { NgxDaterangepickerMd } from 'ngx-daterangepicker-material';
import {MatListModule} from '@angular/material/list';
import {MatDialogModule} from '@angular/material/dialog';
import { AddamentitydialogComponent } from './addamentitydialog/addamentitydialog.component';
import { BlockuserdialogComponent } from './blockuserdialog/blockuserdialog.component';
import { DeleteapartmentdialogComponent } from './deleteapartmentdialog/deleteapartmentdialog.component';
import { AddreviewdialogComponent } from './addreviewdialog/addreviewdialog.component';
import { ApprovedPipe } from './approved.pipe';
import { PhotoEditorComponent } from './photo-editor/photo-editor.component';
import { FileUploadModule } from 'ng2-file-upload';
import { AngularOpenlayersModule } from 'ngx-openlayers';
import { ApartmentreservationsComponent } from './apartmentreservations/apartmentreservations.component';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatSortModule } from '@angular/material/sort';
import {MatRadioModule} from '@angular/material/radio';
import { SelectamenitiesdialogComponent } from './selectamenitiesdialog/selectamenitiesdialog.component';


export function tokenGetter() {
   return localStorage.getItem('token');
}


@NgModule({
   declarations: [
      AppComponent,
      NavComponent,
      RegisterComponent,
      ExploreComponent,
      MyAppartmentsComponent,
      ProfileComponent,
      UsersComponent,
      LoginComponent,
      ApartmentCardComponent,
      ApartmentDetailComponent,
      ReservationsComponent,
      AddApartmentComponent,
      AddamentitydialogComponent,
      BlockuserdialogComponent,
      PhotoEditorComponent,
      DeleteapartmentdialogComponent,
      AddreviewdialogComponent,
      ApprovedPipe,
      ApartmentreservationsComponent,
      SelectamenitiesdialogComponent
   ],
   imports: [
      BrowserModule,
      ReactiveFormsModule,
      AngularOpenlayersModule,
      FormsModule,
      MatListModule,
      FileUploadModule,
      MatDialogModule,
      MatRadioModule,
      NgxDaterangepickerMd.forRoot(),
      MatSliderModule,
      TabsModule.forRoot(),
      HttpClientModule,
      MatButtonModule,
      NgxGalleryModule,
      AppRoutingModule,
      BsDropdownModule.forRoot(),
      BrowserAnimationsModule,
      PaginationModule.forRoot(),
      MatTableModule, 
      ButtonsModule.forRoot(),
      MatCheckboxModule,
      MatCardModule,
      MatFormFieldModule,
      MatPaginatorModule,
      MatSortModule,
      MatDividerModule,
      MatInputModule,
      MatButtonModule,
      MatTableModule,
      MatStepperModule,
      MatIconModule,
      MatSelectModule,
      JwtModule.forRoot({
         config: {
            tokenGetter,
            whitelistedDomains: ['localhost:5000'],
            blacklistedRoutes: ['localhost:5000/api/auth']
         }
      })
   ],

   providers: [
      AuthService,
      ApartmentService,
      UserService,
      ErrorInterceptorProvider,
      AlertifyService,
      AuthGuard,
      AdminGuard,
      HostGuard,
      ApartmentDetailResolver,
      ApartmentListResolver,
      UserProfileResolver,
      ApartmentForUserResolver,
      PreventUnsavedChanges
   ],
   bootstrap: [
      AppComponent
   ],
   entryComponents: [
      AddamentitydialogComponent,
      BlockuserdialogComponent,
      DeleteapartmentdialogComponent,
      SelectamenitiesdialogComponent,
      AddreviewdialogComponent]
})
export class AppModule { }
