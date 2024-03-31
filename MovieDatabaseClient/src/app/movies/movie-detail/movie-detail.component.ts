import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { take } from 'rxjs';
import { Movie } from 'src/app/_models/movie';
import { User } from 'src/app/_models/user';
import { AccountService } from 'src/app/_services/account.service';
import { MovieService } from 'src/app/_services/movie.service';
import { RatingService } from 'src/app/_services/rating.service';

@Component({
  selector: 'app-movie-detail',
  templateUrl: './movie-detail.component.html',
  styleUrls: ['./movie-detail.component.css']
})
export class MovieDetailComponent implements OnInit {
  movie: Movie | undefined;
  rating: number | undefined;
  user: User | null = null;
  max: number = 5;

  constructor(private movieService: MovieService, private route: ActivatedRoute,
    public accountService: AccountService, private ratingService: RatingService,
    private router: Router) {
      this.accountService.currentUser$.pipe(take(1)).subscribe({
        next: user => {
          this.user = user;
        }
      })
      this.router.routeReuseStrategy.shouldReuseRoute = function() {
        return false;
      };
    }
  
  ngOnInit(): void {
    const movieId = this.route.snapshot.paramMap.get('id');
    if (movieId) {
      this.loadMovie(movieId);
      if (this.user) {
        this.loadUserRating(movieId);
      }
    }
  }

  loadMovie(movieId: string) {
    this.movieService.getMovie(movieId).subscribe({
      next: movie => {
        this.movie = movie;
      }
    });
  }

  loadUserRating(movieId: string) {
    this.ratingService.getMovieUserRating(movieId).subscribe({
      next: rating => {
        this.rating = rating;
      }
    });
  }

  rateMovie(rating: number) {
    if (this.movie) {
      this.ratingService.rateMovie(this.movie.id, rating).subscribe({
        next: () => {
          this.loadMovie(this.movie!.id);
          this.loadUserRating(this.movie!.id);
        }
      });
    }
  }
}
