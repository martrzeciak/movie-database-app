import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { CommentInterface } from 'src/app/_models/commentInterface';

@Injectable({
  providedIn: 'root'
})
export class CommentService {
  baseUrl = environment.apiUrl

  constructor(private http: HttpClient) { }

  getMovieComments(movieId: string) {
    return this.http.get<CommentInterface[]>('https://localhost:7092/api/comments/movie-comments/' + movieId);
  }
}
