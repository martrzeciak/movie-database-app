import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Movie } from 'src/app/_models/movie';
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
  rate: number | undefined;
  max = 5;
  isReadonly = false;

  constructor(private movieService: MovieService, private route: ActivatedRoute,
    public accountService: AccountService, private ratingService: RatingService) {}
  
  ngOnInit(): void {
    console.log('onInit movie detail')
    const movieId = this.route.snapshot.paramMap.get('id');
    if (movieId) {
      this.loadMovie(movieId);
      this.loadUserRating(movieId);
    }
  }

  loadMovie(movieId: string) {
    console.log('movie detail - loadMovie')
    this.movieService.getMovie(movieId).subscribe({
      next: movie => {
        this.movie = movie;
      }
    });
  }

  loadUserRating(movieId: string) {
    console.log('movie detail - loadUserRating')
    this.ratingService.getUserRating(movieId).subscribe({
      next: rate => {
        this.rate = rate;
      }
    });
  }

  rateMovie(rate: number) {
    console.log('movie detail - rateMovie')
    if (this.movie) {
      this.ratingService.rateMovie(this.movie.id, rate).subscribe({
        next: () => {
          this.loadMovie(this.movie!.id);
          this.loadUserRating(this.movie!.id);
        }
      });
    }
  }
}
