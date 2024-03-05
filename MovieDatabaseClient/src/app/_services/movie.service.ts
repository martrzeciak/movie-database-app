import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Movie } from '../_models/movie';
import { environment } from 'src/environments/environment';
import { map, of } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class MovieService {
  baseUrl = environment.apiUrl;
  movies: Movie[] = [];

  constructor(private http: HttpClient) { }

  getMovies() {
    if (this.movies.length > 0) return of(this.movies);
    return this.http.get<Movie[]>(this.baseUrl + 'movies').pipe(
      map(movies => {
        this.movies = movies;
        return movies;
      })
    )
  }
}
