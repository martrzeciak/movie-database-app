import { Component, Input } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { Movie } from 'src/app/_models/movie';
import { User } from 'src/app/_models/user';
import { MovieService } from 'src/app/_services/movie.service';

@Component({
  selector: 'app-movie-card',
  templateUrl: './movie-card.component.html',
  styleUrls: ['./movie-card.component.css']
})
export class MovieCardComponent {
  @Input() movie: Movie | undefined;
  @Input() user: User | null = null;

  constructor(private movieService: MovieService, private tostr: ToastrService) {}

  addMovieToWantToWatchList(event: MouseEvent) {
    event.stopPropagation();
    if (this.movie) {
      this.movieService.addMovieToWantToWachList(this.movie.id).subscribe({
        next: () => {
          event.preventDefault();
          this.tostr.success("Added movie to want-to-watch list");
        }
      })
    }
    return false;
  }
}
