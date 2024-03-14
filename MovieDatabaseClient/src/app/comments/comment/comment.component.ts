import { Component, Input } from '@angular/core';
import { CommentInterface } from 'src/app/_models/commentInterface';

@Component({
  selector: 'app-comment',
  templateUrl: './comment.component.html',
  styleUrls: ['./comment.component.css']
})
export class CommentComponent {
  @Input() comment: CommentInterface | undefined;
}
