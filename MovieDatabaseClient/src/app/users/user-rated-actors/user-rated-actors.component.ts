import { Component, OnInit } from '@angular/core';
import { Actor } from 'src/app/_models/actor';
import { ActorService } from 'src/app/_services/actor.service';

@Component({
  selector: 'app-user-rated-actors',
  templateUrl: './user-rated-actors.component.html',
  styleUrls: ['./user-rated-actors.component.css']
})
export class UserRatedActorsComponent implements OnInit {
  actors: Actor[] = [];

  constructor(private actorService: ActorService) {}

  ngOnInit(): void {
    this.loadActors();
  }

  loadActors() {
    this.actorService.getRatedActorsForUser().subscribe({
      next: actor => {
        this.actors = actor;
      }
    })
  }
}
