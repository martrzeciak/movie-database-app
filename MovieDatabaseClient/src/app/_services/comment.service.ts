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

  addComment(movieId: string, comment: any) {
    return this.http.post<any>('https://localhost:7092/api/comments/' + movieId, comment);
  }

  updateComment(commentId: string, comment: any) {
    return this.http.put(this.baseUrl +  'comments/' + commentId, comment);
  }

  deleteComment(commentId: string) {
    return this.http.delete(this.baseUrl + 'comments/' + commentId);
  }

  addLike(commentId: string) {
    return this.http.post(this.baseUrl + 'comments/add-like/' + commentId, {});
  }

  removeLike(commentId: string) {
    return this.http.delete(this.baseUrl + 'comments/remove-like/' + commentId);
  }
}
