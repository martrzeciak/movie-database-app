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

  editComment() {
    this.editedComment.emit(this.comment);
    console.log(this.comment)
  }

  deleteComment() {
    if (this.comment?.id) {
      this.deleteCommentId.emit(this.comment.id);
    }
  }
}
