import { Component } from '@angular/core';
import { Actor } from 'src/app/_models/actor';
import { ActorParams } from 'src/app/_models/actorParams';
import { Pagination } from 'src/app/_models/pagination';
import { ActorService } from 'src/app/_services/actor.service';
import { ConfirmService } from 'src/app/_services/confirm.service';

@Component({
  selector: 'app-admin-actor-list',
  templateUrl: './admin-actor-list.component.html',
  styleUrls: ['./admin-actor-list.component.css']
})
export class AdminActorListComponent {
  actors: Actor[] = [];
  pagination: Pagination | undefined;
  actorParams: ActorParams | undefined;

  constructor(private actorService: ActorService, private confirmService: ConfirmService) {
    this.actorParams = this.actorService.getActorParams();
  }

  ngOnInit(): void {
    this.loadActors();
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

  deleteActor(actorId: string) {
    this.confirmService.confirm('Confirmation', 'Are you sure you want to delete this actor?', 'Yes', 'No')
      .subscribe(result => {
        if (result) {
          this.actorService.deleteActor(actorId).subscribe(() => {
            this.loadActors();
          }); 
        }
      });
  }

  pageChanged(event: any) {
    if (this.actorParams && this.actorParams?.pageNumber !== event.page) {
      this.actorParams.pageNumber = event.page;
      this.actorService.setActorParams(this.actorParams);
      this.loadActors();
    }
  }
  
}



