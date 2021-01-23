import { Component, OnDestroy, OnInit } from '@angular/core';
import { StoriesService } from '../stories.service';

@Component({
  selector: 'app-story-view',
  templateUrl: './story-view.component.html',
  styleUrls: ['./story-view.component.scss'],
})
export class StoryViewComponent implements OnInit, OnDestroy {
  constructor(
    private storiesService: StoriesService
  ) {}

  ngOnInit(): void {
    console.log('StoryViewComponent');
  }

  ngOnDestroy(): void {
    this.storiesService.clearSelectedStory();
  }
}
