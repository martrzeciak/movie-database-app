import { Component } from '@angular/core';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { ToastrService } from 'ngx-toastr';
import { CommentInterface } from 'src/app/_models/commentInterface';
import { CommentService } from 'src/app/_services/comment.service';

@Component({
  selector: 'app-edit-comment-modal',
  templateUrl: './edit-comment-modal.component.html',
  styleUrls: ['./edit-comment-modal.component.css']
})
export class EditCommentModalComponent {
  comment: CommentInterface | undefined;
  editedContent: string = '';

  constructor(public bsModalRef: BsModalRef,private toastr: ToastrService,
    private commentService: CommentService) {}

  save() {
    if (this.comment) {
      this.commentService.updateComment(this.comment.id, this.comment).subscribe({
        next: () => {
          this.toastr.success("Comment updated successfully");
          this.bsModalRef.hide(); 
        }
      })
    }
  }
}
