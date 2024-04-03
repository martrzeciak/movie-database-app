import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Movie } from 'src/app/_models/movie';
import { MovieService } from 'src/app/_services/movie.service';

@Component({
  selector: 'app-movie-search-results',
  templateUrl: './movie-search-results.component.html',
  styleUrls: ['./movie-search-results.component.css']
})
export class MovieSearchResultsComponent implements OnInit {
  searchQuery: string = '';
  searchResults: Movie[] = [];

  constructor(private route: ActivatedRoute, private movieService: MovieService) { }

  ngOnInit(): void {
    this.route.queryParams.subscribe(params => {
      this.searchQuery = params['query'];
      if (this.searchQuery) {
        this.searchMovies();
      }
    });
  }

  searchMovies() {
    this.movieService.searchMovies(this.searchQuery).subscribe({
      next: movies => {
        this.searchResults = movies;
        console.log(this.searchResults)
      }
    });
  }
}
