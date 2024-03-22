import { Component, Input, OnInit } from '@angular/core';
import { FileUploader } from 'ng2-file-upload';
import { take } from 'rxjs';
import { Movie } from 'src/app/_models/movie';
import { User } from 'src/app/_models/user';
import { AccountService } from 'src/app/_services/account.service';
import { MovieService } from 'src/app/_services/movie.service';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-admin-movie-poster-uploader',
  templateUrl: './admin-movie-poster-uploader.component.html',
  styleUrls: ['./admin-movie-poster-uploader.component.css']
})
export class AdminMoviePosterUploaderComponent implements OnInit {
  @Input() movie: Movie | undefined;
  uploader: FileUploader | undefined;
  hasBaseDropZoneOver = false;
  baseUrl = environment.apiUrl;
  user: User | undefined;

  constructor(private accountService: AccountService, private movieService: MovieService) { 
    this.accountService.currentUser$.pipe(take(1)).subscribe({
      next: user => {
        if (user) {
          this.user = user;
        }
      }
    })
  }

  ngOnInit(): void {
    this.initializeFileUploader();
  }

  fileOverBase(e: any) {
    this.hasBaseDropZoneOver = e;
  }

  initializeFileUploader() {
    this.uploader = new FileUploader({
      url: this.baseUrl + 'movies/add-movie-poster/' + this.movie?.id,
      authToken: 'Bearer ' + this.user?.token,
      isHTML5: true,
      allowedFileType: ['image'],
      removeAfterUpload: true,
      autoUpload: false,
      maxFileSize: 10 * 1024 * 1024, // 10 MB
      queueLimit: 1
    })

    this.uploader.onAfterAddingFile = (file) => {
      file.withCredentials = false;
    }

    this.uploader.onSuccessItem = (_item, response, status, _headers) => {
      if (response) {
        const poster = JSON.parse(response);
        this.movie?.posters.push(poster);
        if (poster.isMain && this.movie) {
          this.movie.posterUrl = poster.posterUrl;
        }
      }
    };
  }

  setMainPoster(movieId: string, poster: any) {  
    this.movieService.setMainPoster(movieId, poster.id).subscribe({
      next: () => {
        if (this.movie) {
          this.movie.posterUrl = poster.posterUrl;
          this.movie.posters.forEach(p => {
            if (p.isMain) p.isMain = false;
            if (p.id === poster.id) p.isMain = true;
          })
        }
      } 
    })
  }

  deletePoster(movieId: string, poster: any) {
    this.movieService.deletePoster(movieId, poster.id).subscribe({
      next: () => {
        if (this.movie) {
          this.movie.posters = this.movie.posters.filter(x => x.id !== poster.id);
        }
      }
    })
  }
}
