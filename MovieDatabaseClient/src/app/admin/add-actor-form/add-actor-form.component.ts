import { Component, EventEmitter, HostListener, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { Actor } from 'src/app/_models/actor';
import { ActorService } from 'src/app/_services/actor.service';
import { MovieService } from 'src/app/_services/movie.service';

@Component({
  selector: 'app-add-actor-form',
  templateUrl: './add-actor-form.component.html',
  styleUrls: ['./add-actor-form.component.css']
})
export class AddActorFormComponent {
  @Output() addedActor: EventEmitter<Actor> = new EventEmitter<Actor>();
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
    this.actorService.addActor(values).subscribe({
      next: createdActor => {
        this.toastr.success('Actor added successfully');
        this.addForm?.reset();
        this.addedActor.emit(createdActor);
      } 
    })
  }

  loadMovieList() {
    this.movieService.getMovieNameList().subscribe((movies) => {
      this.movieList = movies;
    })
  }
}
