import { Component, Input } from '@angular/core';
import { Actor } from 'src/app/_models/actor';

@Component({
  selector: 'app-movie-actor-list-card',
  templateUrl: './movie-actor-list-card.component.html',
  styleUrls: ['./movie-actor-list-card.component.css']
})
export class MovieActorListCardComponent {
  @Input() actor: Actor | undefined;
}
