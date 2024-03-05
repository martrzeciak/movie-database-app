import { Component, OnInit } from '@angular/core';
import { Genre } from 'src/app/_models/genre';
import { Movie } from 'src/app/_models/movie';
import { GenreService } from 'src/app/_services/genre.service';
import { MovieService } from 'src/app/_services/movie.service';

@Component({
  selector: 'app-movie-list',
  templateUrl: './movie-list.component.html',
  styleUrls: ['./movie-list.component.css']
})
export class MovieListComponent implements OnInit {
  movies: Movie[] = [];
  genres: Genre[] = [];

  constructor(private movieService: MovieService, 
    private genreService: GenreService) {}

  ngOnInit(): void {
    this.loadMovies();
    this.loadGenres();
  }

  loadMovies(): void {
    this.movieService.getMovies().subscribe((movies) => {
      this.movies = movies;
    });
  }

  loadGenres(): void {
    this.genreService.getGenres().subscribe((genres) => {
      this.genres = genres;
    });
  }
}