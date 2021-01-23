import { AfterViewInit, Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { map, switchMap } from 'rxjs/operators';
import { StoriesService } from '../stories.service';

@Component({
  selector: 'app-story-view',
  templateUrl: './story-view.component.html',
  styleUrls: ['./story-view.component.scss'],
})
export class StoryViewComponent implements OnInit, AfterViewInit, OnDestroy {
  constructor(
    private storiesService: StoriesService,
    private route: ActivatedRoute
  ) {}

  id = 0;
  ngOnInit(): void {
    // this.id = +this.route.snapshot.paramMap.get('id');
    // console.log(this.id);
  }

  ngAfterViewInit() {
    // this.storiesService.getStory$(1).subscribe((res) => {
    //   console.log(res);
    // });
  }

 

  ngOnDestroy(): void {}
}
