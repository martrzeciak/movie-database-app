import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Actor } from 'src/app/_models/actor';
import { ActorService } from 'src/app/_services/actor.service';

@Component({
  selector: 'app-actor-detail',
  templateUrl: './actor-detail.component.html',
  styleUrls: ['./actor-detail.component.css']
})
export class ActorDetailComponent implements OnInit {
  actor: Actor | undefined;

  constructor(private actorService: ActorService, private route: ActivatedRoute) {}
  
  ngOnInit(): void {
    this.loadActor();
  }

  loadActor() {
    var id = this.route.snapshot.paramMap.get('id');
    console.log(id);
    if (id == null) return;
    this.actorService.getActor(id).subscribe({
      next: actor => {
        this.actor = actor
      }
    })
  }
}
