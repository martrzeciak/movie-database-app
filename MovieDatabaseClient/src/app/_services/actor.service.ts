import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Actor } from '../_models/actor';
import { HttpClient } from '@angular/common/http';
import { map, of } from 'rxjs';
import { ActorParams } from '../_models/actorParams';
import { getPaginatedResult, getPaginationHeaders } from './paginationHelper';
import { MovieActorParams } from '../_models/movieActorParams';

@Injectable({
  providedIn: 'root'
})
export class ActorService {
  baseUrl = environment.apiUrl;
  actors: Actor[] = [];
  actorCache = new Map();
  actorParams: ActorParams | undefined;
  movieActorParams: MovieActorParams | undefined;

  constructor(private http: HttpClient) { 
    this.actorParams = new ActorParams();
    this.movieActorParams = new MovieActorParams();
  }

  getActors(actorParams: ActorParams) {
    const response = this.actorCache.get(Object.values(actorParams).join('-'));

    if (response) return of(response);

    let params = getPaginationHeaders(actorParams.pageNumber, actorParams.pageSize);

    return getPaginatedResult<Actor[]>(this.baseUrl + 'actors', params, this.http).pipe(
      map(response => {
        this.actorCache.set(Object.values(actorParams).join('-'), response);
        return response;
      })
    )
  }

  getActor(actorId: string) {
    const actor = [...this.actorCache.values()]
      .reduce((arr, elem) => arr.concat(elem.result), [])
      .find((actor: Actor) => actor.id === actorId);

    if (actor) return of(actor);

    return this.http.get<Actor>(this.baseUrl + 'actors/' + actorId);
  }

  getMovieActors(movieId: string, movieActorParams: MovieActorParams) {
    let params = getPaginationHeaders(movieActorParams.pageNumber, movieActorParams.pageSize);

    return getPaginatedResult<Actor[]>(
      this.baseUrl + 'actors/movie-actors/' + movieId, params, this.http
    );
  }

  getActorParams() {
    return this.actorParams;
  }

  getMovieActorParams() {
    return this.movieActorParams;
  }

  setActorParams(actorParams: ActorParams) {
    this.actorParams = actorParams;
  }

  setMovieActorParams(movieActorParams: MovieActorParams) {
    this.movieActorParams = movieActorParams;
  }

  resetActorParams() {
    this.actorParams = new ActorParams();
    return this.actorParams;
  }

  resetMovieActorParams() {
    this.movieActorParams = new MovieActorParams();
    return this.movieActorParams;
  }
}
