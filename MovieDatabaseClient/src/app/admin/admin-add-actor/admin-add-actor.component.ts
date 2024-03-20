import { Component, HostListener, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { ActorService } from 'src/app/_services/actor.service';
import { MovieService } from 'src/app/_services/movie.service';

@Component({
  selector: 'app-admin-add-actor',
  templateUrl: './admin-add-actor.component.html',
  styleUrls: ['./admin-add-actor.component.css']
})
export class AdminAddActorComponent implements OnInit{
  @HostListener('window:beforeunload', ['$event']) unloadNotification($event: any) {
    if (this.addForm?.dirty) {
      $event.returnValue = true;
    }
  }
  addForm: FormGroup = new FormGroup({});
  validationErrors: string[] | undefined;
  movieList: any[] = [];

  constructor(
    private formBuilder: FormBuilder,
    private router: Router,
    private toastr: ToastrService,
    private movieService: MovieService,
    private actorService: ActorService) {}

  ngOnInit(): void {
    this.loadMovieList();
    this.initalizeForm();
  }

  initalizeForm() {
    this.addForm = this.formBuilder.group({
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      dateOfBirth: ['', Validators.required],
      birthplace: ['', Validators.required],
      biography: ['', Validators.required],
      heightInCentimeters: ['', Validators.required],
      gender: ['male'],
      movieIds: [[]],
    });
  }

  addActor() {
    const values = {...this.addForm.value};
    console.log(this.addForm?.value)
    this.actorService.addActor(values).subscribe({
      next: _ => {
        this.toastr.success('Actor added successfully');
        this.addForm?.reset();
        this.router.navigate(['/admin/actor-list']);
      } 
    })
  }

  loadMovieList() {
    this.movieService.getMovieNameList().subscribe((movies) => {
      this.movieList = movies;
    })
  }
}
