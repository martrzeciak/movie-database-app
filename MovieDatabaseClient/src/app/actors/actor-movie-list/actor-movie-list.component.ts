import { Component, Input, OnInit } from '@angular/core';
import { ActorMovieParams } from 'src/app/_models/actorMovieParams';
import { Movie } from 'src/app/_models/movie';
import { Pagination } from 'src/app/_models/pagination';
import { MovieService } from 'src/app/_services/movie.service';

@Component({
  selector: 'app-actor-movie-list',
  templateUrl: './actor-movie-list.component.html',
  styleUrls: ['./actor-movie-list.component.css']
})
export class ActorMovieListComponent implements OnInit {
  @Input() actorId: string | undefined;
  movies: Movie[] = [];
  actorMovieParams: ActorMovieParams | undefined;
  pagination: Pagination | undefined;

  constructor(private movieService: MovieService) {
    this.actorMovieParams = movieService.getActorMovieParams();
  }

  ngOnInit(): void {
    if (this.actorId) {
      this.loadMoviesForActor(this.actorId);
      this.movieService.resetActorMovieParams();
    }
  }

  loadMoviesForActor(actorId: string) {
    if (this.actorMovieParams) {
      this.movieService.setActorMovieParams(this.actorMovieParams);
      this.movieService.getActorMovies(actorId, this.actorMovieParams).subscribe({
        next: response => {
          if (response.result && response.pagination) {
            this.movies = response.result;
            this.pagination = response.pagination;
          }
        }
      })
    }
  }

  pageChanged(event: any) {
    if (this.actorMovieParams && this.actorMovieParams?.pageNumber !== event.page) {
      this.actorMovieParams.pageNumber = event.page;
      this.movieService.setActorMovieParams(this.actorMovieParams);
      
      if (this.actorId) {
        this.loadMoviesForActor(this.actorId);
      }
    }
  }
}
