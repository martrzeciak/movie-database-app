import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Genre } from '../_models/genre';
import { map, of } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class GenreService {
  baseUrl = environment.apiUrl;
  genres: Genre[] = [];

  constructor(private http: HttpClient) { }

  getGenres() {
    if (this.genres.length > 0) return of(this.genres);
    return this.http.get<Genre[]>(this.baseUrl + 'genres').pipe(
      map(genres => {
        this.genres = genres;
        return genres;
      })
    )
  }
}
