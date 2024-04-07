import { Component, HostListener, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Review } from 'src/app/_models/review';
import { ReviewService } from 'src/app/_services/review.service';

@Component({
  selector: 'app-edit-review',
  templateUrl: './edit-review.component.html',
  styleUrls: ['./edit-review.component.css']
})
export class EditReviewComponent implements OnInit {
  @HostListener('window:beforeunload', ['$event']) unloadNotification($event: any) {
    if (this.editForm?.dirty) {
      $event.returnValue = true;
    }
  }

  editForm: FormGroup = new FormGroup({});
  review: Review | undefined;

  constructor(
    private formBuilder: FormBuilder,
    private toastr: ToastrService,
    private route: ActivatedRoute,
    private reviewService: ReviewService
  ) {}

  ngOnInit(): void {
    this.loadReview();
  }

  initializeForm() {
    this.editForm = this.formBuilder.group({
      rating: [this.review ? this.review.rating : '', Validators.required],
      reviewContent: [this.review ? this.review.reviewContent : '', Validators.required],
    });
  }

  updateReview() {
    if (this.editForm.valid) {
      const values = { ...this.editForm.value };
      if (this.review) {
        this.reviewService.updateReview(this.review.id, values).subscribe({
          next: _ => {
            this.toastr.success('Review updated successfully');
            this.editForm.reset();
            console.log(values)
            this.loadReview();
          },
          error: error => {
            console.error(error);
            this.toastr.error('Failed to update review');
          }
        });
      }
    }
  }

  loadReview() {
    const reviewId = this.route.snapshot.paramMap.get('id');
    if (reviewId) {
      this.reviewService.getReview(reviewId).subscribe({
        next: review => {
          this.review = review;
          console.log(this.review)
          this.initializeForm();
        }
      });
    }
  }
}
