import { Component, Input, OnInit } from '@angular/core';
import { Movie } from 'src/app/_models/movie';
import { MovieService } from 'src/app/_services/movie.service';

@Component({
  selector: 'app-suggested-movies',
  templateUrl: './suggested-movies.component.html',
  styleUrls: ['./suggested-movies.component.css']
})
export class SuggestedMoviesComponent implements OnInit {
  @Input() movieId: string | undefined;
  suggestedMovies: Movie[] = [];

  constructor(private movieService: MovieService) {}

  ngOnInit(): void {
    this.loadSuggestedMovies();
  }

  loadSuggestedMovies() {
    if (!this.movieId) return;
    this.movieService.getSuggestedMovies(this.movieId).subscribe({
      next: suggestedMovies => {
        this.suggestedMovies = suggestedMovies;
      }
    })
  }
}
