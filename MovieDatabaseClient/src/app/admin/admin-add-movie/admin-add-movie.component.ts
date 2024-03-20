import { Component, HostListener } from '@angular/core';
import { FormBuilder, FormGroup, Validators, FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Genre } from 'src/app/_models/genre';
import { ActorService } from 'src/app/_services/actor.service';
import { GenreService } from 'src/app/_services/genre.service';
import { MovieService } from 'src/app/_services/movie.service';

@Component({
  selector: 'app-admin-add-movie',
  templateUrl: './admin-add-movie.component.html',
  styleUrls: ['./admin-add-movie.component.css']
})
export class AdminAddMovieComponent {
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
    private router: Router,
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
      next: _ => {
        this.toastr.success('Movie added successfully');
        this.addForm?.reset();
        this.router.navigate(['/admin/movie-list']);
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
