import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { take } from 'rxjs';
import { Actor } from 'src/app/_models/actor';
import { User } from 'src/app/_models/user';
import { AccountService } from 'src/app/_services/account.service';
import { ActorService } from 'src/app/_services/actor.service';
import { RatingService } from 'src/app/_services/rating.service';

@Component({
  selector: 'app-actor-detail',
  templateUrl: './actor-detail.component.html',
  styleUrls: ['./actor-detail.component.css']
})
export class ActorDetailComponent implements OnInit {
  actor: Actor | undefined;
  rating: number | undefined;
  user: User | null = null;
  max: number = 5;

  constructor(private actorService: ActorService, private route: ActivatedRoute,
    public accountService: AccountService, private ratingService: RatingService) {
      this.accountService.currentUser$.pipe(take(1)).subscribe({
        next: user => {
          this.user = user;
        }
     })
    }
  
  ngOnInit(): void {
    const actorid = this.route.snapshot.paramMap.get('id');
    if (actorid) {
      this.loadActor(actorid);
      if (this.user) {
        this.loadUserRating(actorid);
      }
    }
  }

  loadActor(actorId: string) {
    this.actorService.getActor(actorId).subscribe({
      next: actor => {
        this.actor = actor
      }
    })
  }

  loadUserRating(actorId: string) {
    this.ratingService.getActorUserRating(actorId).subscribe({
      next: rating => {
        this.rating = rating;
      }
    });
  }

  rateActor(rating: number) {
    if (this.actor) {
      this.ratingService.rateActor(this.actor.id, rating).subscribe({
        next: () => {
          this.loadActor(this.actor!.id);
          this.loadUserRating(this.actor!.id);
        }
      });
    }
  }
}
