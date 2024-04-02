import { Component, EventEmitter, HostListener, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { Genre } from 'src/app/_models/genre';
import { Movie } from 'src/app/_models/movie';
import { ActorService } from 'src/app/_services/actor.service';
import { GenreService } from 'src/app/_services/genre.service';
import { MovieService } from 'src/app/_services/movie.service';

@Component({
  selector: 'app-add-movie-form',
  templateUrl: './add-movie-form.component.html',
  styleUrls: ['./add-movie-form.component.css']
})
export class AddMovieFormComponent {
  @Output() addedMovie: EventEmitter<Movie> = new EventEmitter<Movie>();
  @HostListener('window:beforeunload', ['$event']) unloadNotification($event: any) {
    if (this.addForm?.dirty) {
      $event.returnValue = true;
    }
  }
  addForm: FormGroup = new FormGroup({});
  validationErrors: string[] | undefined;
  genreList: Genre[] = [];
  actorList: any[] = [];


  constructor(
    private formBuilder: FormBuilder,
    private toastr: ToastrService,
    private genreService: GenreService,
    private movieService: MovieService,
    private actorService: ActorService) {}

  ngOnInit() {
    this.initalizeForm();
    this.loadGenres();
    this.loadActors();
  }
  
  initalizeForm() {
    this.addForm = this.formBuilder.group({
      title: ['', Validators.required],
      releaseDate: ['', Validators.required],
      durationInMinutes: ['', Validators.required],
      director: ['', Validators.required],
      description: ['', Validators.required],
      genreIds: [[]],
      actorIds: [[]],
    });
  }

  addMovie() {
    const values = {...this.addForm.value};
    console.log(this.addForm?.value)
    this.movieService.addMovie(values).subscribe({
      next: createdMovie => {
        this.toastr.success('Movie added successfully');
        this.addForm?.reset();
        this.addedMovie.emit(createdMovie);
      } 
    })
  }

  loadGenres() {
    this.genreService.getGenres().subscribe((genres) => {
      this.genreList = genres;
    });
  }

  loadActors() {
    this.actorService.getActorNameList().subscribe((actors) => {
      this.actorList = actors;
    })
  }
}
