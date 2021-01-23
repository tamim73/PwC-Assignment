import { AfterViewInit, Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { map, switchMap } from 'rxjs/operators';
import { StoriesService } from '../stories.service';

@Component({
  selector: 'app-story-view',
  templateUrl: './story-view.component.html',
  styleUrls: ['./story-view.component.scss'],
})
export class StoryViewComponent implements OnInit, OnDestroy {
  constructor(
    private storiesService: StoriesService,
    private route: ActivatedRoute
  ) {}

  id = +this.route.snapshot.paramMap.get('id');
  state$ = this.storiesService.state$;

  isAddPostToStoryVisible = false;

  ngOnInit(): void {
    this.storiesService.getStory(this.id);
  }

  showAddPostToStory(): void {
    this.isAddPostToStoryVisible = true;
  }

  hideAddPostToStory(): void {
    this.isAddPostToStoryVisible = false;
  }

  ngOnDestroy(): void {}
}
