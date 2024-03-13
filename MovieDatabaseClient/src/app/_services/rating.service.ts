import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Rating } from '../_models/rating';

@Injectable({
  providedIn: 'root'
})
export class RatingService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  rateMovie(movieId: string, rating: number) {
    return this.http.post(`${this.baseUrl}rating/rate-movie/${movieId}?rating=${rating}`, {});
  }

  getUserRating(movieId: string) {
    return this.http.get<number>(`${this.baseUrl}rating/movie-user-rating/${movieId}`);
  }
}
