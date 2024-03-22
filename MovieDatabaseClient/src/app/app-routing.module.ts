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
import { preventUnsavedChangesGuard } from './_guard/prevent-unsaved-changes.guard';
import { AdminPanelComponent } from './admin/admin-panel/admin-panel.component';
import { adminGuard } from './_guard/admin.guard';
import { AdminMovieListComponent } from './admin/admin-movie-list/admin-movie-list.component';
import { AdminActorListComponent } from './admin/admin-actor-list/admin-actor-list.component';
import { AdminAddMovieComponent } from './admin/admin-add-movie/admin-add-movie.component';
import { preventUnsavedAddMovieChangesGuard } from './_guard/prevent-unsaved-add-movie-changes.guard';
import { AdminEditMovieComponent } from './admin/admin-edit-movie/admin-edit-movie.component';
import { AdminAddActorComponent } from './admin/admin-add-actor/admin-add-actor.component';
import { AdminEditActorComponent } from './admin/admin-edit-actor/admin-edit-actor.component';
import { UserManagementComponent } from './admin/user-management/user-management.component';
import { UserRatedMoviesComponent } from './users/user-rated-movies/user-rated-movies.component';

const routes: Routes = [
  { path: '', component: HomeComponent },
  { path: '',
    runGuardsAndResolvers: 'always',
    canActivate: [authGuard],
    children: [
      { path: 'edit/user', component: EditUserComponent, canDeactivate: [preventUnsavedChangesGuard] },
      { path: 'admin', component: AdminPanelComponent, canActivate: [adminGuard] },
      { path: 'admin/movie-list', component: AdminMovieListComponent, canActivate: [adminGuard]},
      { path: 'admin/actor-list', component: AdminActorListComponent, canActivate: [adminGuard]},
      { path: 'admin/movie-list/add-movie', component: AdminAddMovieComponent, 
        canActivate: [adminGuard], 
        canDeactivate: [preventUnsavedAddMovieChangesGuard]},
      { path: 'admin/movie-list/edit-movie/:id', component: AdminEditMovieComponent, 
        canActivate: [adminGuard]},
      { path: 'admin/actor-list/add-actor', component: AdminAddActorComponent, canActivate: [adminGuard]},
      { path: 'admin/actor-list/edit-actor/:id', component: AdminEditActorComponent, canActivate: [adminGuard]},
      { path: 'admin/user-management', component: UserManagementComponent, canActivate: [adminGuard]},
    ]
  },
  { path: 'register', component: RegisterComponent },
  { path: 'movies', component: MovieListComponent },
  { path: 'movies/user-rated-movies', component: UserRatedMoviesComponent },
  { path: 'movies/:id', component: MovieDetailComponent },

  { path: 'actors', component: ActorListComponent },
  { path: 'actors/:id', component: ActorDetailComponent },
  { path: 'not-found', component: NotFoundComponent },
  { path: 'server-error', component: ServerErrorComponent },
  { path: '**', component: NotFoundComponent, pathMatch: 'full' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
