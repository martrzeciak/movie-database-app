import { Component, OnInit } from '@angular/core';
import { Actor } from 'src/app/_models/actor';
import { ActorParams } from 'src/app/_models/actorParams';
import { Pagination } from 'src/app/_models/pagination';
import { ActorService } from 'src/app/_services/actor.service';

@Component({
  selector: 'app-actor-list',
  templateUrl: './actor-list.component.html',
  styleUrls: ['./actor-list.component.css']
})
export class ActorListComponent implements OnInit {
  actors: Actor[] = [];
  pagination: Pagination | undefined;
  actorParams: ActorParams | undefined;
  genderList = [null , 'male', 'female'];

  constructor(private actorService: ActorService) {
    this.actorParams = this.actorService.getActorParams();
  }

  ngOnInit(): void {
    this.loadActors();
    //this.actorService.resetActorParams();
  }

  loadActors() : void {
    if (this.actorParams) {
      this.actorService.setActorParams(this.actorParams);
      this.actorService.getActors(this.actorParams).subscribe({
        next: response => {
          if (response.result && response.pagination) {
            this.actors = response.result;
            this.pagination = response.pagination;
          }
        }
      })
    }
  }

  pageChanged(event: any) {
    if (this.actorParams && this.actorParams?.pageNumber !== event.page) {
      this.actorParams.pageNumber = event.page;
      this.actorService.setActorParams(this.actorParams);
      this.loadActors();
    }
  }

  resetFiliters() {
    this.actorParams = this.actorService.resetActorParams();
    this.loadActors();
  }
}
