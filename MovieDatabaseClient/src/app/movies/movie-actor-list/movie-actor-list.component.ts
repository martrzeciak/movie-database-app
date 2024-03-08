import { Component, Input, OnInit } from '@angular/core';
import { Actor } from 'src/app/_models/actor';
import { ActorService } from 'src/app/_services/actor.service';

@Component({
  selector: 'app-movie-actor-list',
  templateUrl: './movie-actor-list.component.html',
  styleUrls: ['./movie-actor-list.component.css']
})
export class MovieActorListComponent implements OnInit {
  @Input() movieId: string | undefined;
  actors: Actor[] = [];

  constructor(private actorService: ActorService) {}

  ngOnInit(): void {
    if (this.movieId) {
      this.loadActorsForMovie(this.movieId);
    }
  }

  loadActorsForMovie(movieId: string) {
    this.actorService.getMovieActors(movieId).subscribe({
      next: actors => {
        this.actors = actors
      }
    });
  }
}
