import { IBaseResponse, IBatchRequest } from 'src/app/core/Models';

interface IStoryBase {
  id?: number;
  title: string;
  description: string;
  content: string;
  creationDateTime: Date;
  authorName: string;
  authorId: number;
}

export interface AddStoryRequest extends IStoryBase {}
export interface AddStoryResponse extends IBaseResponse {
  storyId: number;
}
export interface EditStoryRequest extends IStoryBase {}
export interface EditStoryResponse extends IBaseResponse {}
export interface DeleteStoryResponse extends IBaseResponse {
  storyId: number;
}
export interface SearchStoriesRequest extends IBatchRequest {
  general: string;
}
export interface StoriesListModel extends IStoryBase {
}

export interface SearchStoriesResponse extends IBaseResponse {
  rows: StoriesListModel[];
  count: number;
}
export interface StoryDetailsResponse extends IBaseResponse, IStoryBase {
  Posts: IStoryBase[];
}
