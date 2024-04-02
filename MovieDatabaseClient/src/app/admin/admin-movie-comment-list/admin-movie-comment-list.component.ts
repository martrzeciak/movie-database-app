import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { CommentInterface } from 'src/app/_models/commentInterface';
import { CommentService } from 'src/app/_services/comment.service';
import { ConfirmService } from 'src/app/_services/confirm.service';
import { MovieService } from 'src/app/_services/movie.service';

@Component({
  selector: 'app-admin-movie-comment-list',
  templateUrl: './admin-movie-comment-list.component.html',
  styleUrls: ['./admin-movie-comment-list.component.css']
})
export class AdminMovieCommentListComponent implements OnInit {
  comments: CommentInterface[] = [];
  movieId: string | null = null;
  movieTitle: string | null = null;
  

  constructor(private commentService: CommentService, private route: ActivatedRoute,
    private movieService: MovieService, private confirmService: ConfirmService) {}

  ngOnInit(): void {
    this.movieId = this.route.snapshot.paramMap.get('id');
    this.movieTitle = this.route.snapshot.paramMap.get('title');
    if (this.movieId ) {
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

  navigateBack() {

  }

  deleteComment(commentId: string) {
    this.confirmService.confirm('Confirmation', 'Are you sure you want to delete this comment?', 'Yes', 'No')
    .subscribe(result => {
      if (result) {
        this.commentService.deleteComment(commentId).subscribe(() => {
          this.loadComments(this.movieId!);
        });
      }
    })
  }
}
