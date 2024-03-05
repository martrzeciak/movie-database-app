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

const routes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'register', component: RegisterComponent },
  { path: 'movies', component: MovieListComponent },
  { path: 'movies/:title', component: MovieDetailComponent },
  { path: 'actors', component: ActorListComponent },
  { path: 'user/edit', component: EditUserComponent, canDeactivate: [] },
  { path: 'not-found', component: NotFoundComponent },
  { path: 'server-error', component: ServerErrorComponent },
  { path: '**', component: NotFoundComponent, pathMatch: 'full' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
