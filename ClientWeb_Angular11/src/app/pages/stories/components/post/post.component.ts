import { Component, Input, OnInit } from '@angular/core';
import { AuthService } from 'src/app/auth/auth.service';
import { IPostBase } from '../../stories.DTO';
import { StoriesService } from '../../stories.service';

@Component({
  selector: 'app-post',
  templateUrl: './post.component.html',
  styleUrls: ['./post.component.scss'],
})
export class PostComponent implements OnInit {
  constructor(
    private authService: AuthService,
    private storiesService: StoriesService
  ) {}

  @Input() post: IPostBase;
  @Input() isTopic: boolean;

  isEditing = false;
  canEdit: boolean;

  ngOnInit(): void {
    console.log(this.post);
    this.canEdit = this.checkCanEdit();
  }

  showEditor(): void {
    if (this.canEdit) {
      this.isEditing = true;
    }
  }

  hideEditor(): void {
    this.isEditing = false;
  }

  delete(): void {
    if (this.canEdit) {
      if (confirm('Are you sure you ?')) {
        if (this.isTopic) {
          this.storiesService.deleteStory(this.post.id);
        } else {
          this.storiesService.deletePost(this.post.id);
        }
      }
    }
  }

  checkCanEdit(): boolean {
    console.log(this.authService.isAdmin(), this.authService.getUserId(), this.post?.authorId);

    return (
      this.authService.isAdmin() ||
      this.authService.getUserId() === this.post?.authorId
    );
  }
}
