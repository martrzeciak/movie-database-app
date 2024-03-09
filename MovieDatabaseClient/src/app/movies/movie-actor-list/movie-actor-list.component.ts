import { Component, Input, OnInit } from '@angular/core';
import { Actor } from 'src/app/_models/actor';
import { MovieActorParams } from 'src/app/_models/movieActorParams';
import { Pagination } from 'src/app/_models/pagination';
import { ActorService } from 'src/app/_services/actor.service';

@Component({
  selector: 'app-movie-actor-list',
  templateUrl: './movie-actor-list.component.html',
  styleUrls: ['./movie-actor-list.component.css']
})
export class MovieActorListComponent implements OnInit {
  @Input() movieId: string | undefined;
  actors: Actor[] = [];
  movieActorParams: MovieActorParams | undefined;
  pagination: Pagination | undefined;

  constructor(private actorService: ActorService) {
    this.movieActorParams = actorService.getMovieActorParams();
  }

  ngOnInit(): void {
    if (this.movieId) {
      this.loadActorsForMovie(this.movieId);
      this.actorService.resetMovieActorParams();
    }
  }

  loadActorsForMovie(movieId: string) {
    if (this.movieActorParams) {
      this.actorService.setMovieActorParams(this.movieActorParams);
      this.actorService.getMovieActors(movieId, this.movieActorParams).subscribe({
        next: response => {
          if (response.result && response.pagination) {
            this.actors = response.result;
            this.pagination = response.pagination;
          }
        }
      })
    }
  }

  pageChanged(event: any) {
    if (this.movieActorParams && this.movieActorParams?.pageNumber !== event.page) {
      this.movieActorParams.pageNumber = event.page;
      this.actorService.setMovieActorParams(this.movieActorParams);
      
      if (this.movieId) {
        this.loadActorsForMovie(this.movieId);
      }
    }
  }
}
