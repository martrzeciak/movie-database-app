import { Component, Input, OnInit } from '@angular/core';
import { CommentService } from 'src/app/_services/comment.service';
import { CommentInterface } from 'src/app/_models/commentInterface';
import { AccountService } from 'src/app/_services/account.service';
import { take } from 'rxjs';
import { User } from 'src/app/_models/user';

@Component({
  selector: 'app-comment-list',
  templateUrl: './comment-list.component.html',
  styleUrls: ['./comment-list.component.css']
})
export class CommentListComponent implements OnInit {
  @Input() movieId: string | undefined;
  comments: CommentInterface[] = [];
  user: User | null = null;

  constructor(private commentService: CommentService, public accountService: AccountService) {
    this.accountService.currentUser$.pipe(take(1)).subscribe({
      next: user => {
        this.user = user;
      }
   })
  }

  ngOnInit(): void {
    if(this.movieId) {
      this.loadComments(this.movieId);
    }
  }

  loadComments(movieId: string) {
    this.commentService.getMovieComments(movieId).subscribe({
      next: comments => {
        this.comments = comments;
      }
    })
  }
}
