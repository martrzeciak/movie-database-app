import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, NavigationEnd, Router } from '@angular/router';
import { filter } from 'rxjs';
import { Review } from 'src/app/_models/review';
import { ReviewService } from 'src/app/_services/review.service';

@Component({
  selector: 'app-review-detail',
  templateUrl: './review-detail.component.html',
  styleUrls: ['./review-detail.component.css']
})
export class ReviewDetailComponent implements OnInit {
  reviewId: string | null = null;
  review: Review | undefined;
  previousUrl: string | undefined;

  constructor(private reviewService: ReviewService, private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.reviewId = this.route.snapshot.paramMap.get('id');
    this.loadReview();
  }

  loadReview() {
    if (this.reviewId) {
      this.reviewService.getReview(this.reviewId).subscribe((review) =>{
        this.review = review;
      })
    }
  }
}
