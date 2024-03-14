import { Component, Input, OnInit } from '@angular/core';
import { CommentService } from 'src/app/_services/comment.service';
import { CommentInterface } from 'src/app/_models/commentInterface';

@Component({
  selector: 'app-comment-list',
  templateUrl: './comment-list.component.html',
  styleUrls: ['./comment-list.component.css']
})
export class CommentListComponent implements OnInit {
  @Input() movieId: string | undefined;
  comments: CommentInterface[] = [];

  constructor(private commentService: CommentService) {}

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
