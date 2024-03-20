import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { Movie } from 'src/app/_models/movie';
import { MovieParams } from 'src/app/_models/movieParams';
import { Pagination } from 'src/app/_models/pagination';
import { ConfirmService } from 'src/app/_services/confirm.service';
import { MovieService } from 'src/app/_services/movie.service';

@Component({
  selector: 'app-admin-movie-list',
  templateUrl: './admin-movie-list.component.html',
  styleUrls: ['./admin-movie-list.component.css']
})
export class AdminMovieListComponent implements OnInit {
  movies: Movie[] = [];
  pagination: Pagination | undefined;
  movieParams: MovieParams | undefined;

  constructor(private movieService: MovieService, private toastr: ToastrService,
    private confirmService: ConfirmService) {
      this.movieParams = this.movieService.getMovieParams();
    }

  ngOnInit(): void {
    this.loadMovies();
  }

  loadMovies() {
    if (this.movieParams) {
      console.log(this.movieParams.orderBy)
      this.movieService.setMovieParams(this.movieParams);
      this.movieService.getMovies(this.movieParams).subscribe({
        next: response => {
          if (response.result && response.pagination) {
            this.movies = response.result;
            this.pagination = response.pagination;
          }
        }
      })
    }
  }

  deleteMovie(movieId: string) {
    this.confirmService.confirm('Confirmation', 'Are you sure you want to delete this movie?', 'Yes', 'No')
      .subscribe(result => {
        if (result) {
          this.movieService.deleteMovie(movieId).subscribe(() => {
            this.loadMovies();
          });
        }
      });
  }
  
  pageChanged(event: any) {
    if (this.movieParams && this.movieParams?.pageNumber !== event.page) {
      this.movieParams.pageNumber = event.page;
      this.movieService.setMovieParams(this.movieParams);
      this.loadMovies();
    }
  }
}

