import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class RatingService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  rateMovie(movieId: string, rating: number) {
    return this.http.post(`${this.baseUrl}rating/rate-movie/${movieId}?rating=${rating}`, {});
  }

  rateActor(actorId: string, rating: number) {
    return this.http.post(`${this.baseUrl}rating/rate-actor/${actorId}?rating=${rating}`, {});
  }

  getMovieUserRating(movieId: string) {
    return this.http.get<number>(`${this.baseUrl}rating/movie-user-rating/${movieId}`);
  }

  getActorUserRating(actorId: string) {
    return this.http.get<number>(`${this.baseUrl}rating/actor-user-rating/${actorId}`);
  }
}
