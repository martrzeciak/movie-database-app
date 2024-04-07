import { Component, OnInit } from '@angular/core';
import { take } from 'rxjs';
import { Review } from 'src/app/_models/review';
import { User } from 'src/app/_models/user';
import { AccountService } from 'src/app/_services/account.service';
import { ConfirmService } from 'src/app/_services/confirm.service';
import { ReviewService } from 'src/app/_services/review.service';

@Component({
  selector: 'app-review-list',
  templateUrl: './review-list.component.html',
  styleUrls: ['./review-list.component.css']
})
export class ReviewListComponent implements OnInit {
  reviewList: Review[] = [];
  user: User | null = null;

  constructor(private reviewService: ReviewService, private accountService: AccountService,
    private confirmService: ConfirmService) {
    this.accountService.currentUser$.pipe(take(1)).subscribe({
      next: user => {
        this.user = user;
      }
    })
  }

  ngOnInit(): void {
    this.loadReviews();
  }

  loadReviews() {
    if (this.user) {
      this.reviewService.getReviewsForUser(this.user.userName).subscribe({
        next: reviewList => {
          this.reviewList = reviewList;
          console.log(this.reviewList)
        }
      })
    }
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
