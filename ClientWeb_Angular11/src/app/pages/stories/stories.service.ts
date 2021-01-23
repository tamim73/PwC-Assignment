import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { BehaviorSubject, Observable, of } from 'rxjs';
import { finalize } from 'rxjs/operators';
import { IBatchRequest } from 'src/app/core/Models';
import { environment } from 'src/environments/environment';
import {
  AddPostRequest,
  AddPostResponse,
  AddStoryRequest,
  AddStoryResponse,
  DeletePostResponse,
  DeleteStoryResponse,
  EditPostRequest,
  EditPostResponse,
  EditStoryResponse,
  SearchStoriesRequest,
  SearchStoriesResponse,
  StoriesListModel,
  StoryDetailsResponse,
} from './stories.DTO';

interface State {
  // stories list
  storiesList: StoriesListModel[];
  storiesListLoading: boolean;

  // not implemented
  // TODO
  batch: IBatchRequest;
  count: number;
  general: string;

  // selected story
  selectedStory: StoryDetailsResponse;
  selectedStoryLoading: boolean;
}

@Injectable({
  providedIn: 'root',
})
export class StoriesService {
  constructor(private http: HttpClient, private router: Router) {}

  private storiesEndPoint = environment.apiURL + '/stories';
  private postsEndPoint = environment.apiURL + '/posts';

  initialState: State = {
    storiesList: [],
    storiesListLoading: false,
    selectedStory: null,
    selectedStoryLoading: false,
    batch: {
      pageNo: 1,
      pageSize: 10,
      sortOrder: 'ASC',
      sortProperty: '',
    },
    count: 0,
    general: '',
  };

  private _state$ = new BehaviorSubject<State>(this.initialState);
  public state$ = this._state$.asObservable();

  private get state(): State {
    return this._state$.value;
  }

  /* -------------------------------------------------------------------------- */
  /*                                   Actions                                  */
  /* -------------------------------------------------------------------------- */

  /* --------------------------------- stories -------------------------------- */

  getAllStories(filterText: string = ''): void {
    this.getAllStories$(filterText).subscribe((res) => {
      this._state$.next({
        ...this.state,
        storiesList: res,
        storiesListLoading: false,
      });
    });
  }

  getStory(id: number, redirect: boolean = true): void {
    this.getStory$(id).subscribe((res) => {
      this._state$.next({
        ...this.state,
        selectedStory: res,
        selectedStoryLoading: false,
      });

      if (redirect) {
        this.router.navigate(['/pages/stories/view/' + id]);
      }
    });
  }

  addStory(req: AddStoryRequest, redirect: boolean = true): void {
    this.addStory$(req).subscribe((res) => {
      // ...
      if (redirect) {
        this.router.navigate(['/pages/stories/view/' + res.storyId]);
      }
    });
  }

  // did not have time to implement this
  searchStories(): void {
    const { batch, general } = this.state;
    this.searchStories$({
      general,
      ...batch,
    }).subscribe((res) => {
      this._state$.next({
        ...this.state,
        storiesList: res.rows,
        count: res.count,
      });
    });
  }

  deleteStory(id: number): void {
    this.deleteStory$(id).subscribe((res) => {
      // ...
      this.router.navigate(['/pages/stories']);
    });
  }

  refreshSelectedStory(): void {
    this.getStory(this.state?.selectedStory?.storyId);
  }
  /* ---------------------------------- posts --------------------------------- */

  getPost(id: number): void {
    this.getPost$(id).subscribe((res) => {
      // ...
    });
  }

  addPost(req: AddPostRequest, redirect: boolean = true): void {
    this.addPost$(req).subscribe((res) => {
      this.refreshSelectedStory();
    });
  }

  editPost(req: EditPostRequest): void {
    this.editPost$(req).subscribe((res) => {
      this.refreshSelectedStory();
    });
  }

  deletePost(id: number): void {
    this.deletePost$(id).subscribe((res) => {
      // ...
      this.refreshSelectedStory();
    });
  }

  /* -------------------------------------------------------------------------- */
  /*                                     API                                    */
  /* -------------------------------------------------------------------------- */

  /* ---------------------------------- stories --------------------------------- */

  getAllStories$(filterText: string): Observable<StoriesListModel[]> {
    return this.http.get<StoriesListModel[]>(this.storiesEndPoint, {
      params: { general: filterText },
    });
  }

  // not implemented
  searchStories$(req: SearchStoriesRequest): Observable<SearchStoriesResponse> {
    const params = {
      general: req.sortProperty,
      pageNo: req.pageNo.toString(),
      pageSize: req.pageSize.toString(),
      sortOrder: req.sortOrder,
      sortProperty: req.sortProperty,
    };
    return this.http.get<SearchStoriesResponse>(
      this.storiesEndPoint + '/search',
      { params }
    );
  }

  getStory$(id: number): Observable<StoryDetailsResponse> {
    return this.http.get<StoryDetailsResponse>(this.storiesEndPoint + `/${id}`);
  }

  addStory$(req: AddStoryRequest): Observable<AddStoryResponse> {
    return this.http.post<AddStoryResponse>(this.storiesEndPoint, req);
  }

  deleteStory$(id: number): Observable<DeleteStoryResponse> {
    return this.http.delete<DeleteStoryResponse>(
      this.storiesEndPoint + `/${id}`
    );
  }

  /* ---------------------------------- posts ---------------------------------- */

  getPost$(id: number): Observable<StoryDetailsResponse> {
    return this.http.get<StoryDetailsResponse>(this.postsEndPoint + `/${id}`);
  }

  addPost$(req: AddPostRequest): Observable<AddPostResponse> {
    return this.http.post<AddPostResponse>(this.postsEndPoint, req);
  }

  editPost$(req: EditPostRequest): Observable<EditPostResponse> {
    return this.http.put<EditPostResponse>(
      this.postsEndPoint + `/${req.id}`,
      req
    );
  }

  deletePost$(id: number): Observable<DeletePostResponse> {
    return this.http.delete<DeletePostResponse>(this.postsEndPoint + `/${id}`);
  }
}
