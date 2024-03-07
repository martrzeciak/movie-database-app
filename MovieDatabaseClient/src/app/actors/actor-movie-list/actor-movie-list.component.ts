import { Component, Input, OnInit } from '@angular/core';
import { Movie } from 'src/app/_models/movie';
import { MovieService } from 'src/app/_services/movie.service';

@Component({
  selector: 'app-actor-movie-list',
  templateUrl: './actor-movie-list.component.html',
  styleUrls: ['./actor-movie-list.component.css']
})
export class ActorMovieListComponent implements OnInit {
  @Input() actorId: string | undefined;
  movies: Movie[] = [];

  constructor(private movieService: MovieService) {}

  ngOnInit(): void {
    console.log(this.actorId)
    if (this.actorId) {
      this.loadMoviesForActor(this.actorId)
    }
  }

  loadMoviesForActor(actorId: string) {
    this.movieService.getActorMovies(actorId).subscribe({
      next: movies => {
        this.movies = movies;
      }
    });
  }
}
