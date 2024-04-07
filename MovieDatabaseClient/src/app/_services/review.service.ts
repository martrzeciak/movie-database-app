import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Review } from '../_models/review';

@Injectable({
  providedIn: 'root'
})
export class ReviewService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  getReview(reviewId: string) {
    return this.http.get<Review>(this.baseUrl + 'reviews/' + reviewId);
  }

  getReviewsForMovie(username: string) {
    return this.http.get<Review[]>(this.baseUrl + 'reviews/movie/' + username);
  }

  getReviewsForUser(userId: string) {
    return this.http.get<Review[]>(this.baseUrl + 'reviews/user/' + userId);
  }

  addReview(review: any) {
    return this.http.post(this.baseUrl + 'reviews/' + review.movieId, review);
  }

  updateReview(reviewId: string, review: any) {
    return this.http.put(this.baseUrl + 'reviews/' + reviewId, review);
  }

  deleteReview(reviewId: string) {
    return this.http.delete(this.baseUrl + 'reviews/' + reviewId);
  }
}
