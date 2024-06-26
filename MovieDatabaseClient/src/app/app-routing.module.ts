import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { RegisterComponent } from './register/register.component';
import { MovieListComponent } from './movies/movie-list/movie-list.component';
import { ActorListComponent } from './actors/actor-list/actor-list.component';
import { NotFoundComponent } from './errors/not-found/not-found.component';
import { ServerErrorComponent } from './errors/server-error/server-error.component';
import { EditUserComponent } from './users/edit-user/edit-user.component';
import { MovieDetailComponent } from './movies/movie-detail/movie-detail.component';
import { ActorDetailComponent } from './actors/actor-detail/actor-detail.component';
import { authGuard } from './_guard/authGuard';
import { AdminPanelComponent } from './admin/admin-panel/admin-panel.component';
import { adminGuard } from './_guard/admin.guard';
import { AdminMovieListComponent } from './admin/admin-movie-list/admin-movie-list.component';
import { AdminActorListComponent } from './admin/admin-actor-list/admin-actor-list.component';
import { AdminAddMovieComponent } from './admin/admin-add-movie/admin-add-movie.component';
import { AdminEditMovieComponent } from './admin/admin-edit-movie/admin-edit-movie.component';
import { AdminAddActorComponent } from './admin/admin-add-actor/admin-add-actor.component';
import { AdminEditActorComponent } from './admin/admin-edit-actor/admin-edit-actor.component';
import { UserManagementComponent } from './admin/user-management/user-management.component';
import { UserRatedMoviesComponent } from './users/user-rated-movies/user-rated-movies.component';
import { MovieFullActorListComponent } from './movies/movie-full-actor-list/movie-full-actor-list.component';
import { UserWantToWatchListComponent } from './users/user-want-to-watch-list/user-want-to-watch-list.component';
import { UserRatedActorsComponent } from './users/user-rated-actors/user-rated-actors.component';
import { AdminMovieCommentListComponent } from './admin/admin-movie-comment-list/admin-movie-comment-list.component';
import { MovieSearchResultsComponent } from './movies/movie-search-results/movie-search-results.component';
import { ActorSearchResultsComponent } from './actors/actor-search-results/actor-search-results.component';
import { ReviewAddComponent } from './reviews/review-add/review-add.component';
import { MovieReviewFullListComponent } from './movies/movie-review-full-list/movie-review-full-list.component';
import { ReviewDetailComponent } from './reviews/review-detail/review-detail.component';
import { ReviewListComponent } from './users/review-list/review-list.component';
import { EditReviewComponent } from './users/edit-review/edit-review.component';
import { AdminMovieReviewsComponent } from './admin/admin-movie-reviews/admin-movie-reviews.component';

const routes: Routes = [
  { path: '', component: HomeComponent },
  { path: '',
    runGuardsAndResolvers: 'always',
    canActivate: [authGuard],
    children: [
      { path: 'edit/user', component: EditUserComponent },
      { path: 'user/want-to-watch', component: UserWantToWatchListComponent },     
      { path: 'user/user-rated-movies', component: UserRatedMoviesComponent },
      { path: 'user/user-rated-actors', component: UserRatedActorsComponent },
      { path: 'review/add-review', component:ReviewAddComponent },
      { path: 'review/user-list', component:ReviewListComponent },
      { path: 'review/edit/:id', component:EditReviewComponent },
      {
        path: 'admin',
        component: AdminPanelComponent,
        canActivate: [adminGuard],
        children: [
          { path: 'movie-list', component: AdminMovieListComponent },
          { path: 'actor-list', component: AdminActorListComponent },
          { path: 'movie-list/add-movie', component: AdminAddMovieComponent },
          { path: 'movie-list/edit-movie/:id', component: AdminEditMovieComponent },
          { path: 'movie-list/movie-reviews/:id', component: AdminMovieReviewsComponent },
          { path: 'movie-list/movie-comments/:id/:title', component: AdminMovieCommentListComponent },
          { path: 'actor-list/add-actor', component: AdminAddActorComponent },
          { path: 'actor-list/edit-actor/:id', component: AdminEditActorComponent },
          { path: 'user-management', component: UserManagementComponent },
        ],
      },
      ]
  },
  { path: 'register', component: RegisterComponent },
  
  { path: 'movies', component: MovieListComponent },
  { path: 'movies/actor-list/:id', component: MovieFullActorListComponent },
  { path: 'movies/reviews/:id', component: MovieReviewFullListComponent },
  { path: 'movies/search-results', component: MovieSearchResultsComponent },
  { path: 'movies/:id', component: MovieDetailComponent },

  { path: 'review/:id', component: ReviewDetailComponent },

  { path: 'actors', component: ActorListComponent },
  { path: 'actors/search-results', component: ActorSearchResultsComponent },
  { path: 'actors/:id', component: ActorDetailComponent },

  { path: 'not-found', component: NotFoundComponent },
  { path: 'server-error', component: ServerErrorComponent },
  { path: '**', component: NotFoundComponent, pathMatch: 'full' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes, { scrollPositionRestoration: "enabled" })],
  exports: [RouterModule]
})
export class AppRoutingModule { }
