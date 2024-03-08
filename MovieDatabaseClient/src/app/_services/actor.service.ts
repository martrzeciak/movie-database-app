import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Actor } from '../_models/actor';
import { HttpClient } from '@angular/common/http';
import { map, of } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ActorService {
  baseUrl = environment.apiUrl;
  actors: Actor[] = [];

  constructor(private http: HttpClient) { }

  getActors() {
    if (this.actors.length > 0) return of(this.actors);
    return this.http.get<Actor[]>(this.baseUrl + 'actors').pipe(
      map(actors => {
        this.actors = actors;
        return actors;
      })
    )
  }

  getActor(actorId: string) {
    const actor = this.actors.find(x => x.id === actorId);
    if (actor) return of(actor);
    return this.http.get<Actor>(this.baseUrl + 'actors/' + actorId);
  }

  getMovieActors(movieId: string) {
    return this.http.get<Actor[]>(this.baseUrl + 'actors/movie-actors/' + movieId);
  }
}
