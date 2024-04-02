import { Component, OnInit } from '@angular/core';
import { Movie } from 'src/app/_models/movie';
import { MovieService } from 'src/app/_services/movie.service';

@Component({
  selector: 'app-user-want-to-watch-list',
  templateUrl: './user-want-to-watch-list.component.html',
  styleUrls: ['./user-want-to-watch-list.component.css']
})
export class UserWantToWatchListComponent implements OnInit {
  movies: Movie[] = [];

  constructor(private movieService: MovieService) {}

  ngOnInit(): void {
    this.loadMovies();
  }

  loadMovies() {
    this.movieService.getUserWantToWatchList().subscribe({
      next: movies => {
        this.movies = movies;
      }
    })
  }
}
