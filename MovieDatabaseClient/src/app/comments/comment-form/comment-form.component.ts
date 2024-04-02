import { Component, EventEmitter, Input, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { CommentInterface } from 'src/app/_models/commentInterface';
import { User } from 'src/app/_models/user';
import { CommentService } from 'src/app/_services/comment.service';

@Component({
  selector: 'app-comment-form',
  templateUrl: './comment-form.component.html',
  styleUrls: ['./comment-form.component.css']
})
export class CommentFormComponent {
  @Input() movieId: string | undefined; 
  @Input() user: User | null = null; 
  @Output() createdComment: EventEmitter<CommentInterface> = new EventEmitter<CommentInterface>();
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
    const values = {...this.commentForm.value};
    console.log(values)
    if (this.movieId) {
      this.commentService.addComment(this.movieId, values).subscribe({
        next: comment => {
          this.toastr.success('Comment added successfully');
          this.commentForm.reset();
          this.createdComment.emit(comment);
        }  
      })
    }
    
  }

  cancel() {
    this.commentForm.reset();
  }
}
