import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Actor } from 'src/app/_models/actor';
import { ActorService } from 'src/app/_services/actor.service';

@Component({
  selector: 'app-movie-full-actor-list',
  templateUrl: './movie-full-actor-list.component.html',
  styleUrls: ['./movie-full-actor-list.component.css']
})
export class MovieFullActorListComponent implements OnInit {
  actorList: Actor[] = [];

  constructor(private actorService: ActorService, private route: ActivatedRoute) {}

  ngOnInit(): void {
    const movieId = this.route.snapshot.paramMap.get('id');
    if (movieId) {
      this.loadActorList(movieId);
    }
  }

  loadActorList(movieId: string) {
    //this.actorService.getMovieActors()
  }
}
