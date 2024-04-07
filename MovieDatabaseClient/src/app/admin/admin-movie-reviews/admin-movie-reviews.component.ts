import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Route } from '@angular/router';
import { Review } from 'src/app/_models/review';
import { ConfirmService } from 'src/app/_services/confirm.service';
import { ReviewService } from 'src/app/_services/review.service';

@Component({
  selector: 'app-admin-movie-reviews',
  templateUrl: './admin-movie-reviews.component.html',
  styleUrls: ['./admin-movie-reviews.component.css']
})
export class AdminMovieReviewsComponent implements OnInit {
  reviewList: Review[] = [];

  constructor(
    private reviewService: ReviewService, 
    private route: ActivatedRoute,
    private confirmService: ConfirmService) {}

  ngOnInit(): void {
    this.loadReviews();
  }

  loadReviews() {
    const movieId = this.route.snapshot.paramMap.get('id');
      this.reviewService.getReviewsForMovie(movieId!).subscribe({
        next: reviewList => {
          this.reviewList = reviewList;
          console.log(this.reviewList)
        }
      })
  }

  deleteReview(reviewId: string) {
    this.confirmService.confirm('Confirmation', 'Are you sure you want to delete this review?', 'Yes', 'No')
      .subscribe(result => {
        if (result) {
          this.reviewService.deleteReview(reviewId).subscribe(() => {
            this.loadReviews();
          });
        }
      });
  }
}
