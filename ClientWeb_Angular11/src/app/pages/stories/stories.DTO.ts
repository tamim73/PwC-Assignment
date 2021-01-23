import { IBaseResponse, IBatchRequest } from 'src/app/core/Models';

export interface IPostBase {
  id?: number;
  title: string;
  content: string;
  description?: string;
  creationDateTime?: Date;
  authorName?: string;
  authorId?: number;
}

/* --------------------------------- stories -------------------------------- */

export interface AddStoryRequest extends IPostBase {}
export interface AddStoryResponse extends IBaseResponse {
  storyId: number;
}
export interface EditStoryResponse extends IBaseResponse {}
export interface DeleteStoryResponse extends IBaseResponse {
  storyId: number;
}
export interface SearchStoriesRequest extends IBatchRequest {
  general: string;
}
export interface StoriesListModel extends IPostBase {}

export interface SearchStoriesResponse extends IBaseResponse {
  rows: StoriesListModel[];
  count: number;
}
export interface StoryDetailsResponse extends IBaseResponse, IPostBase {
  posts: IPostBase[];
}

/* ---------------------------------- posts --------------------------------- */

export interface PostDetailsResponse extends IPostBase, IPostBase {}
export interface EditPostRequest extends IPostBase {}
export interface EditPostResponse extends IBaseResponse {}
export interface DeletePostResponse extends IBaseResponse {
  postId: number;
}
export interface AddPostRequest extends IPostBase {
  storyId: number;
}
export interface AddPostResponse extends IBaseResponse {
  postId: number;
}
