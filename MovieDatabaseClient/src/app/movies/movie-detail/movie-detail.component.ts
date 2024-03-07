import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Movie } from 'src/app/_models/movie';
import { MovieService } from 'src/app/_services/movie.service';

@Component({
  selector: 'app-movie-detail',
  templateUrl: './movie-detail.component.html',
  styleUrls: ['./movie-detail.component.css']
})
export class MovieDetailComponent implements OnInit {
  movie: Movie | undefined;

  constructor(private movieService: MovieService, private route: ActivatedRoute) {}
  
  ngOnInit(): void {
    this.loadMovie();
  }


  loadMovie() {
    var id = this.route.snapshot.paramMap.get('id');
    console.log(id)
    if (id == null) return;
    this.movieService.getMovie(id).subscribe({
      next: movie => {
        this.movie = movie
      }
    })
  }
}
