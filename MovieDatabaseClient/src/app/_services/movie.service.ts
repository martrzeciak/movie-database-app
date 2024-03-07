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

  getMovie(movieId: string) {
    const movie = this.movies.find(x => x.id === movieId);
    if (movie) return of(movie);
    return this.http.get<Movie>(this.baseUrl + 'movies/' + movieId);
  }

  getActorMovies(actorId: string) {
    return this.http.get<Movie[]>(this.baseUrl + 'movies/actor-movies/' + actorId);
  }
}
