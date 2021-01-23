import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { IBatchRequest } from 'src/app/core/Models';
import { environment } from 'src/environments/environment';
import {
  AddStoryRequest,
  AddStoryResponse,
  DeleteStoryResponse,
  EditStoryRequest,
  EditStoryResponse,
  SearchStoriesRequest,
  SearchStoriesResponse,
  StoriesListModel,
  StoryDetailsResponse,
} from './stories.DTO';

interface State {
  storiesList: StoriesListModel[];
  batch: IBatchRequest;
  count: number;
  general: string;
}

@Injectable({
  providedIn: 'root',
})
export class StoriesService {
  constructor(private http: HttpClient) {}

  private storyEndPoint = environment.apiURL + '/stories';

  initialState: State = {
    storiesList: [],
    batch: {
      pageNo: 1,
      pageSize: 10,
      sortOrder: 'ASC',
      sortProperty: '',
    },
    count: 0,
    general: '',
  };

  private _state = new BehaviorSubject<State>(this.initialState);
  public state$ = this._state.asObservable();

  private get state(): State {
    return this._state.value;
  }

  /* -------------------------------------------------------------------------- */
  /*                                   Actions                                  */
  /* -------------------------------------------------------------------------- */

  searchStories() {
    const { batch, general } = this.state;
    this.searchStories$({
      general,
      ...batch,
    }).subscribe((res) => {
      this._state.next({
        ...this.state,
        storiesList: res.rows,
        count: res.count,
      });
    });
  }

  /* -------------------------------------------------------------------------- */
  /*                                     API                                    */
  /* -------------------------------------------------------------------------- */

  searchStories$(req: SearchStoriesRequest): Observable<SearchStoriesResponse> {
    const params = {
      general: req.sortProperty,
      pageNo: req.pageNo.toString(),
      pageSize: req.pageSize.toString(),
      sortOrder: req.sortOrder,
      sortProperty: req.sortProperty,
    };
    return this.http.get<SearchStoriesResponse>(this.storyEndPoint, { params });
  }

  getStory$(id: number): Observable<StoryDetailsResponse> {
    return this.http.get<StoryDetailsResponse>(this.storyEndPoint + `/${id}`);
  }

  addStory$(req: AddStoryRequest): Observable<AddStoryResponse> {
    return this.http.post<AddStoryResponse>(this.storyEndPoint, req);
  }

  editStory$(req: EditStoryRequest): Observable<EditStoryResponse> {
    return this.http.put<EditStoryResponse>(
      this.storyEndPoint + `/${req.id}`,
      req
    );
  }

  deleteStory$(id: number): Observable<DeleteStoryResponse> {
    return this.http.delete<DeleteStoryResponse>(this.storyEndPoint + `/${id}`);
  }
}
