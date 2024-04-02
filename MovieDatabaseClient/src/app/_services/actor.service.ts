import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Actor } from '../_models/actor';
import { HttpClient } from '@angular/common/http';
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
    let params = getPaginationHeaders(actorParams.pageNumber, actorParams.pageSize);

    params = params.append('orderBy', actorParams.orderBy);
    if (actorParams.gender)
      params = params.append('gender', actorParams.gender);

    return getPaginatedResult<Actor[]>(this.baseUrl + 'actors', params, this.http);
  }

  getActor(actorId: string) {
    return this.http.get<Actor>(this.baseUrl + 'actors/' + actorId);
  }

  addActor(actor: any) {
    return this.http.post<Actor>(this.baseUrl + 'actors', actor);
  }

  updateActor(actorId: string, actor: any) {
    return this.http.put(this.baseUrl + 'actors/' + actorId, actor);
  }

  deleteActor(actorId: string) {
    return this.http.delete(this.baseUrl + 'actors/' + actorId);
  }

  getMovieActors(movieId: string) {
    return this.http.get<Actor[]>(this.baseUrl + 'actors/movie-actors/' + movieId);
  }

  getActorNameList() {
    return this.http.get<any[]>(this.baseUrl + 'actors/actor-name');
  }

  getActorNameListForMovie(movieId: string) {
    return this.http.get<any[]>(this.baseUrl + 'actors/actor-name/' + movieId);
  }

  setMainImage(actorId: string, imageId: string) {
    return this.http.put(this.baseUrl + 'actors/set-main-image/' + actorId + '/' + imageId, {});
  }

  deleteImage(actorId: string, imageId: string) {
    return this.http.delete(this.baseUrl + 'actors/delete-actor-image/' + actorId + '/' + imageId);
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
