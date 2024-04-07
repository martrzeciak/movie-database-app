import { Component } from '@angular/core';
import { Movie } from 'src/app/_models/movie';

@Component({
  selector: 'app-admin-add-movie',
  templateUrl: './admin-add-movie.component.html',
  styleUrls: ['./admin-add-movie.component.css']
})
export class AdminAddMovieComponent {
  movie: Movie | undefined;
  isMovieAdded = false;

  handleMovieCreation(createdMovie: Movie) {
   this.movie = createdMovie;
   this.isMovieAdded = true;
  }
}
