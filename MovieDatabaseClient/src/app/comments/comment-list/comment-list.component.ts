import { Component, Input, OnInit } from '@angular/core';
import { CommentService } from 'src/app/_services/comment.service';
import { CommentInterface } from 'src/app/_models/commentInterface';
import { AccountService } from 'src/app/_services/account.service';
import { take } from 'rxjs';
import { User } from 'src/app/_models/user';
import { Member } from 'src/app/_models/member';
import { MemberService } from 'src/app/_services/member.service';
import { ConfirmService } from 'src/app/_services/confirm.service';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { EditCommentModalComponent } from 'src/app/modals/edit-comment-modal/edit-comment-modal.component';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-comment-list',
  templateUrl: './comment-list.component.html',
  styleUrls: ['./comment-list.component.css']
})
export class CommentListComponent implements OnInit {
  @Input() movieId: string | undefined;
  comments: CommentInterface[] = [];
  user: User | null = null;
  currentMember: Member | undefined;
  bsModalRef: BsModalRef<EditCommentModalComponent> = new BsModalRef<EditCommentModalComponent>();

  constructor(private commentService: CommentService, public accountService: AccountService,
    private memberService: MemberService, private confirmService: ConfirmService,
    private modalService: BsModalService, private toastr: ToastrService) {
    this.accountService.currentUser$.pipe(take(1)).subscribe({
      next: user => {
        this.user = user;
      }
    })
  }

  ngOnInit(): void {
    if (this.movieId) {
      this.loadComments(this.movieId);
    }
    if (this.user) {
      this.loadCurrentMember(this.user.userName);
    }
  }

  loadComments(movieId: string) {
    this.commentService.getMovieComments(movieId).subscribe({
      next: comments => {
        this.comments = comments;
        
      console.log(this.comments)
      }
    })
  }

  handleCommentCreation(comment: CommentInterface) {
    this.comments.unshift(comment);
  }

  addLike(commentId: string) {
    this.commentService.addLike(commentId).subscribe(() => {
      this.toastr.success("Like added successfully");
    })
  }

  removeLike(commentId: string) {
    this.commentService.removeLike(commentId).subscribe(() => {
      this.toastr.success("Like removed successfully");
    })
  }

  loadCurrentMember(username: string) {
    this.memberService.getMember(username).subscribe((member) => {
      this.currentMember = member;
    })
  }

  deleteComment(commentId: string) {
    this.confirmService.confirm('Confirmation', 'Are you sure you want to delete comment?', 'Yes', 'No')
      .subscribe(result => {
        if (result) {
          this.commentService.deleteComment(commentId).subscribe(() => {
            if (this.movieId) {
              this.loadComments(this.movieId);
            }
          });
        }
      });
  }

  openEditModal(editedComment: CommentInterface) {
    var editedCommentCopy = { ...editedComment };
    const config = {
      class: 'modal-dialog-centered modal-lg',
      initialState: {
        comment: editedCommentCopy
      }
    }
    this.bsModalRef = this.modalService.show(EditCommentModalComponent, config);
    this.bsModalRef.onHide?.subscribe({
      next: () => {
        if (this.movieId) {
          this.loadComments(this.movieId);
        }
      }
    })
  }
}
