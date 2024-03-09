import { Component, OnInit } from '@angular/core';
import { Genre } from 'src/app/_models/genre';
import { Movie } from 'src/app/_models/movie';
import { MovieParams } from 'src/app/_models/movieParams';
import { Pagination } from 'src/app/_models/pagination';
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
  pagination: Pagination | undefined;
  movieParams: MovieParams | undefined;
  // movie: Movie | undefined;

  constructor(private movieService: MovieService, 
    private genreService: GenreService) {
      this.movieParams = this.movieService.getMovieParams();
      console.log(this.movieParams)
    }

  ngOnInit(): void {
    this.loadMovies();
    this.loadGenres();
    this.movieService.resetMovieParams();
  }

  loadMovies(): void {
    if (this.movieParams) {
      this.movieService.setMovieParams(this.movieParams);
      this.movieService.getMovies(this.movieParams).subscribe({
        next: response => {
          if (response.result && response.pagination) {
            this.movies = response.result;
            this.pagination = response.pagination;
          }
        }
      })
    }
  }

  loadGenres(): void {
    this.genreService.getGenres().subscribe((genres) => {
      this.genres = genres;
    });
  }

  pageChanged(event: any) {
    if (this.movieParams && this.movieParams?.pageNumber !== event.page) {
      this.movieParams.pageNumber = event.page;
      this.movieService.setMovieParams(this.movieParams);
      this.loadMovies();
    }
  }
}