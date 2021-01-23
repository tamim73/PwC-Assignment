import { Component, OnInit } from '@angular/core';
import { StoriesService } from '../stories.service';

@Component({
  selector: 'app-stories-list',
  templateUrl: './stories-list.component.html',
  styleUrls: ['./stories-list.component.scss'],
})
export class StoriesListComponent implements OnInit {
  constructor(private storiesService: StoriesService) {}

  state$ = this.storiesService.state$;

  ngOnInit(): void {
    this.storiesService.getAllStories();
  }
}
