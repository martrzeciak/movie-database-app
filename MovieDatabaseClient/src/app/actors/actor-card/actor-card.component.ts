import { Component, Input } from '@angular/core';
import { Actor } from 'src/app/_models/actor';

@Component({
  selector: 'app-actor-card',
  templateUrl: './actor-card.component.html',
  styleUrls: ['./actor-card.component.css']
})
export class ActorCardComponent {
  @Input() actor: Actor | undefined;
}
