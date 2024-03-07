import { Component, Input } from '@angular/core';
import { Movie } from 'src/app/_models/movie';

@Component({
  selector: 'app-actor-movie-list-card',
  templateUrl: './actor-movie-list-card.component.html',
  styleUrls: ['./actor-movie-list-card.component.css']
})
export class ActorMovieListCardComponent {
  @Input() movie: Movie | undefined;
}
