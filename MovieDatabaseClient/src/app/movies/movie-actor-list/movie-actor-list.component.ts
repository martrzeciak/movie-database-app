import { Component, Input, OnInit } from '@angular/core';
import { Router } from '@angular/router';
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

  constructor(private actorService: ActorService, private router: Router) {
    this.movieActorParams = actorService.getMovieActorParams();
  }

  ngOnInit(): void {
    if (this.movieId) {
      this.loadActorsForMovie(this.movieId);
      this.actorService.resetMovieActorParams();
    }
  }

  loadActorsForMovie(movieId: string) {
      this.actorService.getMovieActors(movieId).subscribe({
        next: actors => {
          this.actors = actors;
        }
      });
  }

  navigateToFullActorList() {
    this.router.navigate(['/movies/actor-list/' + this.movieId]);
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
