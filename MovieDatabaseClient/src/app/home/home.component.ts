import { Component } from '@angular/core';
import { Movie } from '../_models/movie';
import { MovieService } from '../_services/movie.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent {
  randomMovieId: string | undefined;

  constructor(private movieService: MovieService, private router: Router) {}

  navigateToRandomMovie() {
    this.movieService.getRandomMovie().subscribe({
      next: randomMovieId => {
        this.randomMovieId = randomMovieId;
        this.router.navigate(['movies/' + randomMovieId]);
      }
    })
  }
}
