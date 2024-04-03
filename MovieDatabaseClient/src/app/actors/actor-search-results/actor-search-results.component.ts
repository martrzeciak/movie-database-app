import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Actor } from 'src/app/_models/actor';
import { ActorService } from 'src/app/_services/actor.service';

@Component({
  selector: 'app-actor-search-results',
  templateUrl: './actor-search-results.component.html',
  styleUrls: ['./actor-search-results.component.css']
})
export class ActorSearchResultsComponent {
  searchQuery: string = '';
  searchResults: Actor[] = [];

  constructor(private route: ActivatedRoute, private actorService: ActorService) { }

  ngOnInit(): void {
    this.route.queryParams.subscribe(params => {
      this.searchQuery = params['query'];
      if (this.searchQuery) {
        this.searchMovies();
      }
    });
  }

  searchMovies() {
    this.actorService.searchActors(this.searchQuery).subscribe({
      next: actors => {
        this.searchResults = actors;
      }
    });
  }
}
