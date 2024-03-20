import { Component, HostListener } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Actor } from 'src/app/_models/actor';
import { ActorService } from 'src/app/_services/actor.service';
import { GenreService } from 'src/app/_services/genre.service';
import { MovieService } from 'src/app/_services/movie.service';

@Component({
  selector: 'app-admin-edit-actor',
  templateUrl: './admin-edit-actor.component.html',
  styleUrls: ['./admin-edit-actor.component.css']
})
export class AdminEditActorComponent {
  @HostListener('window:beforeunload', ['$event']) unloadNotification($event: any) {
    if (this.editForm?.dirty) {
      $event.returnValue = true;
    }
  }
  editForm: FormGroup = new FormGroup({});
  validationErrors: string[] | undefined;
  actor: Actor | undefined;
  movieList: any[] = [];
  actorMovies: any[] = [];

  constructor(
    private formBuilder: FormBuilder,
    private toastr: ToastrService,
    private movieService: MovieService,
    private actorService: ActorService,
    private route: ActivatedRoute,) {}

  ngOnInit() {
    this.loadActor();
    this.loadMovieList();
    this.initalizeForm();
  }

  initalizeForm() {
    console.log(this.actor?.dateOfBirth)
    this.editForm = this.formBuilder.group({
      firstName: [this.actor?.firstName, Validators.required],
      lastName: [this.actor?.lastName, Validators.required],
      dateOfBirth: [this.actor?.dateOfBirth, Validators.required],
      birthplace: [this.actor?.birthplace, Validators.required],
      biography: [this.actor?.biography, Validators.required],
      heightInCentimeters: [this.actor?.heightInCentimeters, Validators.required],
      gender: [this.actor?.gender],
      movieIds: [this.actorMovies.map(movie => movie.id)],
    });
  }

  updateActor() {
    if (!this.editForm.valid) return;
    
    const values = { ...this.editForm.value };
    if (this.actor) {
      this.actorService.updateActor(this.actor.id, values).subscribe({
        next: _ => {
          this.toastr.success('Actor updated successfully');
          this.editForm.reset();
          this.loadActor()
          
        },
        error: error => {
          console.error(error);
          this.toastr.error('Failed to update actor');
        }
      });
    }
  }

  loadActor() {
    const actorId = this.route.snapshot.paramMap.get('id');
    if (actorId) {
      this.actorService.getActor(actorId).subscribe({
        next: actor => {
          this.actor = actor;
          console.log(actorId)
          this.loadMoviesForActor(actorId);
        }
      })
    }
  }

  loadMovieList() {
    this.movieService.getMovieNameList().subscribe((movies) => {
      this.movieList = movies;
    })
  }

  loadMoviesForActor(actorId: string) {
    this.movieService.getMovieNameListForActor(actorId).subscribe({
      next: movies => {
        this.actorMovies = movies;
        console.log(movies);
        console.log(this.actorMovies);
        this.initalizeForm();
      }
    })
  }
}
