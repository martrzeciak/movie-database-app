import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { NgSelectModule } from '@ng-select/ng-select';

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
import { RatingModule } from 'ngx-bootstrap/rating';
import { ButtonsModule } from 'ngx-bootstrap/buttons';
import { CommentComponent } from './comments/comment/comment.component';
import { CommentListComponent } from './comments/comment-list/comment-list.component';
import { CommentFormComponent } from './comments/comment-form/comment-form.component';
import { TimeagoModule } from "ngx-timeago";
import { FooterComponent } from './footer/footer.component';
import { AdminMovieListComponent } from './admin/admin-movie-list/admin-movie-list.component';
import { AdminActorListComponent } from './admin/admin-actor-list/admin-actor-list.component';
import { AdminAddMovieComponent } from './admin/admin-add-movie/admin-add-movie.component';
import { AdminEditMovieComponent } from './admin/admin-edit-movie/admin-edit-movie.component';
import { AdminAddActorComponent } from './admin/admin-add-actor/admin-add-actor.component';
import { AdminEditActorComponent } from './admin/admin-edit-actor/admin-edit-actor.component';
import { AdminMoviePosterUploaderComponent } from './admin/admin-movie-poster-uploader/admin-movie-poster-uploader.component';
import { AdminActorImageUploaderComponent } from './admin/admin-actor-image-uploader/admin-actor-image-uploader.component';
import { UserManagementComponent } from './admin/user-management/user-management.component';
import { RolesModalComponent } from './modals/roles-modal/roles-modal.component';
import { UserRatedMoviesComponent } from './users/user-rated-movies/user-rated-movies.component';
import { SuggestedMoviesComponent } from './movies/suggested-movies/suggested-movies.component';
import { MovieFullActorListComponent } from './movies/movie-full-actor-list/movie-full-actor-list.component';
import { AddMovieFormComponent } from './admin/add-movie-form/add-movie-form.component';
import { AddActorFormComponent } from './admin/add-actor-form/add-actor-form.component';
import { EditCommentModalComponent } from './modals/edit-comment-modal/edit-comment-modal.component';
import { UserWantToWatchListComponent } from './users/user-want-to-watch-list/user-want-to-watch-list.component';
import { UserRatedActorsComponent } from './users/user-rated-actors/user-rated-actors.component';
import { AdminMovieCommentListComponent } from './admin/admin-movie-comment-list/admin-movie-comment-list.component';
import { MovieSearchResultsComponent } from './movies/movie-search-results/movie-search-results.component';
import { ActorSearchResultsComponent } from './actors/actor-search-results/actor-search-results.component';
import { ReviewAddComponent } from './reviews/review-add/review-add.component';
import { MovieReviewListComponent } from './movies/movie-review-list/movie-review-list.component';
import { ReviewCardComponent } from './reviews/review-card/review-card.component';
import { MovieReviewFullListComponent } from './movies/movie-review-full-list/movie-review-full-list.component';
import { ReviewDetailComponent } from './reviews/review-detail/review-detail.component';
import { ReviewListComponent } from './users/review-list/review-list.component';
import { EditReviewComponent } from './users/edit-review/edit-review.component';
import { AdminMovieReviewsComponent } from './admin/admin-movie-reviews/admin-movie-reviews.component';




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
    CommentComponent,
    CommentListComponent,
    CommentFormComponent,
    FooterComponent,
    AdminMovieListComponent,
    AdminActorListComponent,
    AdminAddMovieComponent,
    AdminEditMovieComponent,
    AdminAddActorComponent,
    AdminEditActorComponent,
    AdminMoviePosterUploaderComponent,
    AdminActorImageUploaderComponent,
    UserManagementComponent,
    RolesModalComponent,
    UserRatedMoviesComponent,
    SuggestedMoviesComponent,
    MovieFullActorListComponent,
    AddMovieFormComponent,
    AddActorFormComponent,
    EditCommentModalComponent,
    UserWantToWatchListComponent,
    UserRatedActorsComponent,
    AdminMovieCommentListComponent,
    MovieSearchResultsComponent,
    ActorSearchResultsComponent,
    ReviewAddComponent,
    MovieReviewListComponent,
    ReviewCardComponent,
    MovieReviewFullListComponent,
    ReviewDetailComponent,
    ReviewListComponent,
    EditReviewComponent,
    AdminMovieReviewsComponent
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
    ButtonsModule.forRoot(),
    TimeagoModule.forRoot(),
    NgSelectModule,
    FormsModule,
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
