import { Component, EventEmitter, Input, Output } from '@angular/core';
import { CommentInterface } from 'src/app/_models/commentInterface';
import { User } from 'src/app/_models/user';

@Component({
  selector: 'app-comment',
  templateUrl: './comment.component.html',
  styleUrls: ['./comment.component.css']
})
export class CommentComponent {
  @Input() comment: CommentInterface | undefined;
  @Input() user: User | null = null;
  @Input() currentUserId: string | undefined;
  @Output() deleteCommentId: EventEmitter<string> = new EventEmitter<string>();
  @Output() editedComment: EventEmitter<CommentInterface> = new EventEmitter<CommentInterface>();
  @Output() likedCommentId: EventEmitter<string> = new EventEmitter<string>();
  @Output() unlikedCommentId: EventEmitter<string> = new EventEmitter<string>();

  editComment() {
    this.editedComment.emit(this.comment);
    console.log(this.comment)
  }

  deleteComment() {
    if (this.comment?.id) {
      this.deleteCommentId.emit(this.comment.id);
    }
  }

  addLike() {
    if (this.comment?.id) {
      this.comment.isCommentLikedByCurrentUser = true;
      this.comment.likes = this.comment.likes + 1;
      this.likedCommentId.emit(this.comment.id);
    }
  }

  removeLike() {
    if (this.comment?.id) {
      this.comment.isCommentLikedByCurrentUser = false;
      this.comment.likes = this.comment.likes - 1;
      this.unlikedCommentId.emit(this.comment.id);
    }
  }
}
