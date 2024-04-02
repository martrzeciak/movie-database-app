import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Actor } from 'src/app/_models/actor';
import { ActorService } from 'src/app/_services/actor.service';

@Component({
  selector: 'app-movie-full-actor-list',
  templateUrl: './movie-full-actor-list.component.html',
  styleUrls: ['./movie-full-actor-list.component.css']
})
export class MovieFullActorListComponent implements OnInit {
  actorList: Actor[] = [];
  movieId: string | null = null;

  constructor(private actorService: ActorService, private route: ActivatedRoute,
    private router: Router) {}

  ngOnInit(): void {
    const movieId = this.route.snapshot.paramMap.get('id');
    if (movieId) {
      this.movieId = movieId;
      this.loadActorList(movieId);
    }
  }

  loadActorList(movieId: string) {
    this.actorService.getMovieActors(movieId).subscribe({
      next: actors => {
        this.actorList = actors;
      }
    })
  }

  navigateBack() {
    this.router.navigate(['/movies/' + this.movieId]);
  }
}
