import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/app/auth/auth.service';
import { StoriesService } from '../stories.service';

@Component({
  selector: 'app-stories-list',
  templateUrl: './stories-list.component.html',
  styleUrls: ['./stories-list.component.scss'],
})
export class StoriesListComponent implements OnInit {
  constructor(
    private storiesService: StoriesService,
    private authService: AuthService
  ) {}

  state$ = this.storiesService.state$;
  ngOnInit(): void {
    this.storiesService.getAllStories();
  }

  public get isLoggedIn(): boolean {
    return this.authService.isLoggedIn();
  }
}
