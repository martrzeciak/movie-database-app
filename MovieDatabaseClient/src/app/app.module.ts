import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NavComponent } from './nav/nav.component';
import { HomeComponent } from './home/home.component';
import { TextInputComponent } from './_forms/text-input/text-input.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RegisterComponent } from './register/register.component';
import { DatePickerComponent } from './_forms/date-picker/date-picker.component';
import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';
import { MovieCardComponent } from './movies/movie-card/movie-card.component';
import { MovieListComponent } from './movies/movie-list/movie-list.component';
import { ActorListComponent } from './actors/actor-list/actor-list.component';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { ErrorInterceptor } from './_interceptors/error.interceptor';
import { JwtInterceptor } from './_interceptors/jwt.interceptor';
import { ToastrModule } from 'ngx-toastr';
import { NotFoundComponent } from './errors/not-found/not-found.component';
import { ServerErrorComponent } from './errors/server-error/server-error.component';
import { EditUserComponent } from './users/edit-user/edit-user.component';
import { PaginationModule } from 'ngx-bootstrap/pagination';
import { MovieDetailComponent } from './movies/movie-detail/movie-detail.component';
import { ActorCardComponent } from './actors/actor-card/actor-card.component';
import { ActorDetailComponent } from './actors/actor-detail/actor-detail.component';
import { ActorMovieListComponent } from './actors/actor-movie-list/actor-movie-list.component';
import { ActorMovieListCardComponent } from './actors/actor-movie-list-card/actor-movie-list-card.component';
import { MovieActorListComponent } from './movies/movie-actor-list/movie-actor-list.component';
import { MovieActorListCardComponent } from './movies/movie-actor-list-card/movie-actor-list-card.component';
import { ConfirmDialogComponent } from './modals/confirm-dialog/confirm-dialog.component';
import { ModalModule } from 'ngx-bootstrap/modal';
import { FileUploadModule } from 'ng2-file-upload';
import { UserPhotoUploaderComponent } from './users/user-photo-uploader/user-photo-uploader.component';
import { HasRoleDirective } from './_directives/has-role.directive';
import { AdminPanelComponent } from './admin/admin-panel/admin-panel.component';
import { UserManagmentComponent } from './admin/user-managment/user-managment.component';
import { RatingModule } from 'ngx-bootstrap/rating';
import { ButtonsModule } from 'ngx-bootstrap/buttons';

@NgModule({
  declarations: [
    AppComponent,
    NavComponent,
    HomeComponent,
    TextInputComponent,
    RegisterComponent,
    DatePickerComponent,
    MovieCardComponent,
    MovieListComponent,
    ActorListComponent,
    NotFoundComponent,
    ServerErrorComponent,
    EditUserComponent,
    MovieDetailComponent,
    ActorCardComponent,
    ActorDetailComponent,
    ActorMovieListComponent,
    ActorMovieListCardComponent,
    MovieActorListComponent,
    MovieActorListCardComponent,
    ConfirmDialogComponent,
    UserPhotoUploaderComponent,
    HasRoleDirective,
    AdminPanelComponent,
    UserManagmentComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    ReactiveFormsModule,
    FormsModule,
    HttpClientModule,
    BsDropdownModule.forRoot(),
    BsDatepickerModule.forRoot(),
    ToastrModule.forRoot({
      positionClass: 'toast-bottom-right'
    }),
    PaginationModule.forRoot(),
    ModalModule.forRoot(),
    FileUploadModule,
    RatingModule.forRoot(),
    ButtonsModule.forRoot()
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
