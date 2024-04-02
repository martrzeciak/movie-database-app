import { Component } from '@angular/core';
import { Actor } from 'src/app/_models/actor';

@Component({
  selector: 'app-admin-add-actor',
  templateUrl: './admin-add-actor.component.html',
  styleUrls: ['./admin-add-actor.component.css']
})
export class AdminAddActorComponent{
  actor: Actor | undefined;
  isActorAdded = false;

  handleActorCreation(createdActor: Actor) {
   this.actor = createdActor;
   this.isActorAdded = true;
  }
}
