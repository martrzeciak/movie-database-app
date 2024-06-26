import { Component, OnInit } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { Router } from '@angular/router';
import { take } from 'rxjs';
import { Genre } from 'src/app/_models/genre';
import { Movie } from 'src/app/_models/movie';
import { MovieParams } from 'src/app/_models/movieParams';
import { Pagination } from 'src/app/_models/pagination';
import { User } from 'src/app/_models/user';
import { AccountService } from 'src/app/_services/account.service';
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
  movieNameList: any[] = [];
  searchValue = '';
  searchForm = this.formBuilder.nonNullable.group({
    searchValue: '',
  });
  user: User | null = null;

  constructor(
    private movieService: MovieService, 
    private genreService: GenreService, 
    private formBuilder: FormBuilder,
    private accountService: AccountService,
    private router: Router) {
      this.movieParams = this.movieService.getMovieParams();
      this.accountService.currentUser$.pipe(take(1)).subscribe({
        next: user => {
          this.user = user;
        }
      })
    }

  ngOnInit(): void {
    this.movieParams = this.movieService.resetMovieParams();
    this.loadMovies();
    this.loadGenres();
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
      const emptyGenre: Genre = { id: '', name: '' };
      this.genres = [emptyGenre, ...genres];
    });
  }

  pageChanged(event: any) {
    if (this.movieParams && this.movieParams?.pageNumber !== event.page) {
      this.scrollToTheTop();
      this.movieParams.pageNumber = event.page;
      this.movieService.setMovieParams(this.movieParams);
      this.loadMovies();
    }
  }

  resetFiliters() {
    this.movieParams = this.movieService.resetMovieParams();
    this.loadMovies();
  }

  getMovieNameList(query: string) {
    this.movieService.getSearchSuggestions(query).subscribe({
      next: movieNameList => {
        this.movieNameList = movieNameList;
      }
    });
  }

  onSearchInput() {
    const searchValue = this.searchForm.get('searchValue')?.value;
    if (searchValue && searchValue.length >= 3) {
      this.getMovieNameList(searchValue);
    } else {
      this.movieNameList = [];
    }
  }

  searchMovies() {
    const searchValue = this.searchForm.get('searchValue')?.value;
    if (searchValue && searchValue.length >= 3) {
      this.router.navigate(['movies/search-results'], { queryParams: { query: searchValue } });
    }
  }

  scrollToTheTop() {
    window.scroll({ 
      top: 0, 
      left: 0, 
      behavior: 'smooth' 
    });
  }
}