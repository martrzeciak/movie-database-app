import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Movie } from '../_models/movie';
import { environment } from 'src/environments/environment';
import { map, of } from 'rxjs';
import { MovieParams } from '../_models/movieParams';
import { getPaginatedResult, getPaginationHeaders } from './paginationHelper';
import { ActorMovieParams } from '../_models/actorMovieParams';

@Injectable({
  providedIn: 'root'
})
export class MovieService {
  baseUrl = environment.apiUrl;
  movies: Movie[] = [];
  movieCache = new Map();
  movieParams: MovieParams | undefined;
  actorMovieParams: ActorMovieParams | undefined;

  constructor(private http: HttpClient) { 
    this.movieParams = new MovieParams();
    this.actorMovieParams = new ActorMovieParams();
  }

  getMovies(movieParams: MovieParams) {
    const response = this.movieCache.get(Object.values(movieParams).join('-'));

    if (response) return of(response);

    let params = getPaginationHeaders(movieParams.pageNumber, movieParams.pageSize);

    params = params.append('genre', movieParams.genre);
    console.log(movieParams.genre)

    if (movieParams.releaseDate) {
      params = params.append('releaseDate', movieParams.releaseDate);
    }
    else {
      params = params.append('releaseDate', -1);
    }
      
    return getPaginatedResult<Movie[]>(this.baseUrl + 'movies', params, this.http).pipe(
      map(response => {
        this.movieCache.set(Object.values(movieParams).join('-'), response);
        return response;
      })
    )
  }

  getMovie(movieId: string) {
    const movie = [...this.movieCache.values()]
      .reduce((arr, elem) => arr.concat(elem.result), [])
      .find((movie: Movie) => movie.id === movieId);

    if (movie) return of(movie);

    return this.http.get<Movie>(this.baseUrl + 'movies/' + movieId);
  }

  getActorMovies(actorId: string, actorMovieParamys: ActorMovieParams) {
    let params = getPaginationHeaders(actorMovieParamys.pageNumber, actorMovieParamys.pageSize);

    return getPaginatedResult<Movie[]>(
      this.baseUrl + 'movies/actor-movies/' + actorId, params, this.http
    );
  }

  getMovieParams() {
    return this.movieParams;
  }

  getActorMovieParams() {
    return this.actorMovieParams;
  }

  setActorMovieParams(actorMovieParams: ActorMovieParams) {
    this.actorMovieParams = actorMovieParams;
  }

  setMovieParams(movieParams: MovieParams) {
    this.movieParams = movieParams;
  }

  resetMovieParams() {
    this.movieParams = new MovieParams();
    return this.movieParams;
  }

  resetActorMovieParams() {
    this.actorMovieParams = new ActorMovieParams();
    return this.actorMovieParams;
  }

  getSearchSuggestions(query: string) {
    return this.http.get<string[]>(`${this.baseUrl}movies/search-suggestions?query=${query}`);
  }
}
