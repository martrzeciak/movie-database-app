import { Component, OnInit } from '@angular/core';
import { Movie } from 'src/app/_models/movie';
import { MovieService } from 'src/app/_services/movie.service';

@Component({
  selector: 'app-user-rated-movies',
  templateUrl: './user-rated-movies.component.html',
  styleUrls: ['./user-rated-movies.component.css']
})
export class UserRatedMoviesComponent implements OnInit {
  movies: Movie[] = [];

  constructor(private movieService: MovieService) {}

  ngOnInit(): void {
    this.loadMovies();
  }

  loadMovies() {
    this.movieService.getRatedMoviesForUser().subscribe({
      next: movies => {
        this.movies = movies;
      }
    })
  }
}
