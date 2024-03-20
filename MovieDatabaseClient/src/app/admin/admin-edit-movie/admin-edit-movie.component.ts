import { Component, HostListener } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Genre } from 'src/app/_models/genre';
import { Movie } from 'src/app/_models/movie';
import { ActorService } from 'src/app/_services/actor.service';
import { GenreService } from 'src/app/_services/genre.service';
import { MovieService } from 'src/app/_services/movie.service';

@Component({
  selector: 'app-admin-edit-movie',
  templateUrl: './admin-edit-movie.component.html',
  styleUrls: ['./admin-edit-movie.component.css']
})
export class AdminEditMovieComponent {
  @HostListener('window:beforeunload', ['$event']) unloadNotification($event: any) {
    if (this.editForm?.dirty) {
      $event.returnValue = true;
    }
  }
  
  editForm: FormGroup = new FormGroup({});
  movie: Movie | undefined;
  movieActors: any[] = [];
  genreList: Genre[] = [];
  actorList: any[] = [];

  constructor(
    private formBuilder: FormBuilder,
    private toastr: ToastrService,
    private genreService: GenreService,
    private movieService: MovieService,
    private actorService: ActorService,
    private route: ActivatedRoute
  ) {}

  ngOnInit() {
    this.loadMovie();
    this.loadGenres();
    this.loadActors();
    this.initializeForm();
  }

  initializeForm() {
    this.editForm = this.formBuilder.group({
      title: [this.movie ? this.movie.title : '', Validators.required],
      releaseDate: [this.movie ? this.movie.releaseDate : '', Validators.required],
      durationInMinutes: [this.movie ? this.movie.durationInMinutes : '', Validators.required],
      director: [this.movie ? this.movie.director : '', Validators.required],
      description: [this.movie ? this.movie.description : '', Validators.required],
      genreIds: [this.movie?.genres.map(genre => genre.id)],
      actorIds: [this.movieActors.map(actor => actor.id)],
    });
  }

  updateMovie() {
    if (!this.editForm.valid) return;
    
    const values = { ...this.editForm.value };
    if (this.movie) {
      this.movieService.updateMovie(this.movie.id, values).subscribe({
        next: _ => {
          this.toastr.success('Movie updated successfully');
          this.editForm.reset();
          this.loadMovie();
        },
        error: error => {
          console.error(error);
          this.toastr.error('Failed to update movie');
        }
      });
    }
  }

  loadMovie() {
    const movieId = this.route.snapshot.paramMap.get('id');
    if (movieId) {
      this.movieService.getMovie(movieId).subscribe({
        next: movie => {
          this.movie = movie;
          this.loadActorsForMovie(movieId);
        }
      });
    }
  }

  loadGenres() {
    this.genreService.getGenres().subscribe(genres => {
      this.genreList = genres;
    });
  }

  loadActors() {
    this.actorService.getActorNameList().subscribe(actors => {
      this.actorList = actors;
    });
  }

  loadActorsForMovie(movieId: string) {
    this.actorService.getActorNameListForMovie(movieId).subscribe({
      next: actors => {
        this.movieActors = actors;
        this.initializeForm();
      },
    });
  }
}
