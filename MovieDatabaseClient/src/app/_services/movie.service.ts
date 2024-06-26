import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Movie } from '../_models/movie';
import { environment } from 'src/environments/environment';
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
    let params = getPaginationHeaders(movieParams.pageNumber, movieParams.pageSize);

    params = params.append('genre', movieParams.genre);
    params = params.append('orderBy', movieParams.orderBy);
    
    if (movieParams.releaseDate)
      params = params.append('releaseDate', movieParams.releaseDate);
      
    return getPaginatedResult<Movie[]>(this.baseUrl + 'movies', params, this.http);
  }

  getMovie(movieId: string) {
    return this.http.get<Movie>(this.baseUrl + 'movies/' + movieId);
  }

  addMovie(movie: any) {
    return this.http.post<Movie>(this.baseUrl + 'movies', movie);
  }

  updateMovie(movieId: string, movie: any) {
    return this.http.put(this.baseUrl + 'movies/' + movieId, movie);
  }

  deleteMovie(movieId: string) {
    return this.http.delete(this.baseUrl + 'movies/' + movieId);
  }

  getActorMovies(actorId: string, actorMovieParamys: ActorMovieParams) {
    let params = getPaginationHeaders(actorMovieParamys.pageNumber, actorMovieParamys.pageSize);

    return getPaginatedResult<Movie[]>(
      this.baseUrl + 'movies/actor-movies/' + actorId, params, this.http);
  }

  getMovieNameList() {
    return this.http.get<any[]>(this.baseUrl + 'movies/movie-name');
  }

  getMovieNameListForActor(actorId: string) {
    return this.http.get<any[]>(this.baseUrl + 'movies/movie-name/' + actorId);
  }

  getRatedMoviesForUser() {
    return this.http.get<Movie[]>(this.baseUrl + 'movies/user-rated-movies');
  }

  setMainPoster(movieId: string, posterId: string) {
    return this.http.put(this.baseUrl + 'movies/set-main-poster/' + movieId + '/' + posterId, {});
  }

  deletePoster(movieId: string, posterId: string) {
    return this.http.delete(this.baseUrl + 'movies/delete-movie-poster/' + movieId + '/' + posterId);
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

  getRandomMovie() {
    return this.http.get<string>(this.baseUrl + 'movies/random-movie');
  }

  searchMovies(query: string) {
    return this.http.get<Movie[]>(`${this.baseUrl}movies/search?query=${query}`);
  }

  getSuggestedMovies(movieId: string) {
    return this.http.get<Movie[]>(this.baseUrl + 'movies/suggested-movies/' + movieId);
  }

  addMovieToWantToWachList(movieId: string) {
    return this.http.post(this.baseUrl + 'movies/add-want-to-watch-movie/' + movieId, {});
  }

  removeMovieFromWantToWatchList(movieId: string) {
    return this.http.delete(this.baseUrl + 'movies/remove-want-to-watch-movie/' + movieId);
  }

  getUserWantToWatchList() {
    return this.http.get<Movie[]>(this.baseUrl + 'movies/user-want-to-watch-list');
  }
}