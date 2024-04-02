import { Component, Input } from '@angular/core';
import { FileUploader } from 'ng2-file-upload';
import { take } from 'rxjs';
import { Actor } from 'src/app/_models/actor';
import { User } from 'src/app/_models/user';
import { AccountService } from 'src/app/_services/account.service';
import { ActorService } from 'src/app/_services/actor.service';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-admin-actor-image-uploader',
  templateUrl: './admin-actor-image-uploader.component.html',
  styleUrls: ['./admin-actor-image-uploader.component.css']
})
export class AdminActorImageUploaderComponent {
  @Input() actor: Actor | undefined;
  uploader: FileUploader | undefined;
  hasBaseDropZoneOver = false;
  baseUrl = environment.apiUrl;
  user: User | undefined;

  constructor(private accountService: AccountService, private actorService: ActorService) { 
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
      url: this.baseUrl + 'actors/add-actor-image/' + this.actor?.id,
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
        const image = JSON.parse(response);
        console.log("response", image)
        this.actor?.images.push(image);
        if (image.isMain && this.actor) {
          this.actor.imageUrl = image.imageUrl;
        }
      }
    };
  }

  setMainImage(actorId: string, image: any) {  
    this.actorService.setMainImage(actorId, image.id).subscribe({
      next: () => {
        if (this.actor) {
          this.actor.imageUrl = image.imageUrl;
          this.actor.images.forEach(p => {
            if (p.isMain) p.isMain = false;
            if (p.id === image.id) p.isMain = true;
          })
        }
      } 
    })
  }

  deleteImage(actorId: string, image: any) {
    this.actorService.deleteImage(actorId, image.id).subscribe({
      next: () => {
        if (this.actor) {
          this.actor.images = this.actor.images.filter(x => x.id !== image.id);
        }
      }
    })
  }
}
