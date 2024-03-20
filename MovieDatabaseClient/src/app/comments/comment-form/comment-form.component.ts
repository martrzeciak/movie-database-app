import { Component, Input } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { take } from 'rxjs';
import { CommentInterface } from 'src/app/_models/commentInterface';
import { User } from 'src/app/_models/user';
import { AccountService } from 'src/app/_services/account.service';
import { CommentService } from 'src/app/_services/comment.service';

@Component({
  selector: 'app-comment-form',
  templateUrl: './comment-form.component.html',
  styleUrls: ['./comment-form.component.css']
})
export class CommentFormComponent {
  @Input() movieId: string | undefined; 
  @Input() user: User | null = null; 
  comment: CommentInterface | undefined;

  commentForm!: FormGroup;

  constructor(private fb: FormBuilder, private commentService: CommentService,
    private toastr: ToastrService) { }

  ngOnInit(): void {
    this.commentForm = this.fb.group({
      commentContent: [this.comment?.commentContent, Validators.required],
    });
  }

  addComment() {
    if (this.movieId && this.comment)
    this.commentService.addComment(this.movieId, this.comment).subscribe({
      next: _ => {
        this.toastr.success('Comment added successfully');
        this.commentForm.reset();
      }  
    })
  }
}
