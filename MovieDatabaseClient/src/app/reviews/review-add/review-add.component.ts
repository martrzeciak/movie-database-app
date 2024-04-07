import { Component, HostListener, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { MovieService } from 'src/app/_services/movie.service';
import { ReviewService } from 'src/app/_services/review.service';

@Component({
  selector: 'app-review-add',
  templateUrl: './review-add.component.html',
  styleUrls: ['./review-add.component.css']
})
export class ReviewAddComponent implements OnInit {
  @HostListener('window:beforeunload', ['$event']) unloadNotification($event: any) {
    if (this.addForm?.dirty) {
      $event.returnValue = true;
    }
  }
  addForm: FormGroup = new FormGroup({});
  validationErrors: string[] | undefined;
  movieList: any[] = []

  constructor(
    private movieService: MovieService,
    private formBuilder: FormBuilder,
    private toastr: ToastrService,
    private reviewService: ReviewService,
    private router: Router) {}

  ngOnInit(): void {
    this.loadMovies();
    this.initalizeForm();
  }

  initalizeForm() {
    this.addForm = this.formBuilder.group({
      rating: ['', Validators.required],
      reviewContent: ['', Validators.required],
      movieId: [[]],
    });
  }

  addReview() {
    const values = {...this.addForm.value};
    console.log(values)
    this.reviewService.addReview(values).subscribe({
      next: _ => {
        this.toastr.success('Review added successfully');
        this.addForm?.reset();
        this.router.navigate(['/review/user-list']);
      } 
    })
  }

  loadMovies() {
    this.movieService.getMovieNameList().subscribe((movies) => {
      this.movieList = movies;
    })
  }
}
