import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Movie } from 'src/app/_models/movie';
import { Review } from 'src/app/_models/review';
import { ReviewService } from 'src/app/_services/review.service';

@Component({
  selector: 'app-movie-review-full-list',
  templateUrl: './movie-review-full-list.component.html',
  styleUrls: ['./movie-review-full-list.component.css']
})
export class MovieReviewFullListComponent implements OnInit {
  movieId: string | null = null;
  reviews: Review[] = [];

  constructor(private router: Router, private reviewService: ReviewService,
    private route: ActivatedRoute) {}

  ngOnInit(): void {
    this.movieId = this.route.snapshot.paramMap.get('id');
    this.loadReviews();
  }

  loadReviews() {
    this.reviewService.getReviewsForMovie(this.movieId!).subscribe((reviews) => {
      this.reviews = reviews;
    })
  }

  navigateBack() {
    this.router.navigate(['/movies/' + this.movieId]);
  }
}
