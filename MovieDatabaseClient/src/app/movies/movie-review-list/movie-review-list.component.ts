import { Component, Input, OnInit } from '@angular/core';
import { Review } from 'src/app/_models/review';
import { ReviewService } from 'src/app/_services/review.service';

@Component({
  selector: 'app-movie-review-list',
  templateUrl: './movie-review-list.component.html',
  styleUrls: ['./movie-review-list.component.css']
})
export class MovieReviewListComponent implements OnInit {
  @Input() movieId: string | undefined; 
  reviews: Review[] = [];

  constructor(private reviewService: ReviewService) {}

  ngOnInit(): void {
    this.loadReviews();
  }

  loadReviews() {
    this.reviewService.getReviewsForMovie(this.movieId!).subscribe((reviews) => {
      this.reviews = reviews;
      console.log(this.reviews)
    })
  }
}
