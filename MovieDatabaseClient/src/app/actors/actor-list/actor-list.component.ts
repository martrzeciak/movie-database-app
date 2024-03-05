import { Component, OnInit } from '@angular/core';
import { Actor } from 'src/app/_models/actor';
import { ActorService } from 'src/app/_services/actor.service';

@Component({
  selector: 'app-actor-list',
  templateUrl: './actor-list.component.html',
  styleUrls: ['./actor-list.component.css']
})
export class ActorListComponent implements OnInit {
  actors: Actor[] = [];

  constructor(private actorService: ActorService) {}

  ngOnInit(): void {
    this.loadActors();
  }

  loadActors() {
    this.actorService.getActors().subscribe((actors) => {
      this.actors = actors;
    });
  }
}
