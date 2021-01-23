import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormControl } from '@angular/forms';
import { Subscription } from 'rxjs';
import { debounceTime } from 'rxjs/operators';
import { AuthService } from 'src/app/auth/auth.service';
import { StoriesService } from '../stories.service';

@Component({
  selector: 'app-stories-list',
  templateUrl: './stories-list.component.html',
  styleUrls: ['./stories-list.component.scss'],
})
export class StoriesListComponent implements OnInit, OnDestroy {
  constructor(
    private storiesService: StoriesService,
    private authService: AuthService
  ) {}
  filterCtrl = new FormControl();
  sub: Subscription;
  state$ = this.storiesService.state$;
  ngOnInit(): void {
    this.storiesService.getAllStories();

    this.sub = this.filterCtrl.valueChanges
    .pipe(debounceTime(737))
    .subscribe((value: string) => {
      this.storiesService.getAllStories(value.trim());
    });
  }
  
  ngOnDestroy(): void {
    this.sub.unsubscribe();
  }

  public get isLoggedIn(): boolean {
    return this.authService.isLoggedIn();
  }
}
