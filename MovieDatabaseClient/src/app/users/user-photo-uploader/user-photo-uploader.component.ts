import { Component, Input, OnInit } from '@angular/core';
import { FileUploader } from 'ng2-file-upload';
import { take } from 'rxjs';
import { Member } from 'src/app/_models/member';
import { User } from 'src/app/_models/user';
import { AccountService } from 'src/app/_services/account.service';
import { MemberService } from 'src/app/_services/member.service';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-user-photo-uploader',
  templateUrl: './user-photo-uploader.component.html',
  styleUrls: ['./user-photo-uploader.component.css']
})
export class UserPhotoUploaderComponent implements OnInit {
  @Input() member: Member | undefined;
  uploader: FileUploader | undefined;
  hasBaseDropZoneOver = false;
  baseUrl = environment.apiUrl;
  user: User | undefined;

  constructor(private accountService: AccountService, private memberService: MemberService) { 
    this.accountService.currentUser$.pipe(take(1)).subscribe({
      next: user => {
        if (user) {
          this.user = user;
        }
      }
    })
  }

  ngOnInit(): void {
    this.initializeFileUploader();
  }

  fileOverBase(e: any) {
    this.hasBaseDropZoneOver = e;
  }

  initializeFileUploader() {
    this.uploader = new FileUploader({
      url: this.baseUrl + 'users/add-user-image',
      authToken: 'Bearer ' + this.user?.token,
      isHTML5: true,
      allowedFileType: ['image'],
      removeAfterUpload: true,
      autoUpload: false,
      maxFileSize: 10 * 1024 * 1024, // 10 MB
      queueLimit: 1
    })

    this.uploader.onAfterAddingFile = (file) => {
      file.withCredentials = false;
    }

    this.uploader.onSuccessItem = (_item, response, status, _headers) => {
      if (response) {
        const photo = JSON.parse(response);
        this.member?.userImages.push(photo);
        if (photo.isMain && this.user && this.member) {
          this.user.imageUrl = photo.imageUrl;
          this.accountService.setCurrentUser(this.user);
          this.member.imageUrl = photo.imageUrl;
        }
      }
    };
  }

  setMainPhoto(photo: any) {
    this.memberService.setMainPhoto(photo.id).subscribe({
      next: () => {
        if (this.user && this.member) {
          this.user.imageUrl = photo.imageUrl;
          this.accountService.setCurrentUser(this.user);
          this.member.imageUrl = photo.imageUrl;
          this.member.userImages.forEach(p => {
            if (p.isMain) p.isMain = false;
            if (p.id === photo.id) p.isMain = true;
          })
        }
      } 
    })
  }

  deletePhoto(photoId: string) {
    this.memberService.deletePhoto(photoId).subscribe({
      next: () => {
        if (this.member) {
          this.member.userImages = this.member.userImages.filter(x => x.id !== photoId);
        }
      }
    })
  }
}
