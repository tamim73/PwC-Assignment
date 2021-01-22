import { IBaseResponse } from '../core/Models';

export interface IUserLoginRequest  {
  username: string;
  password: string;
}

export interface ILoginResponse extends IBaseResponse {
  accessToken: string;
}

export interface IRegisterRequest  {
  name: string;
  username: string;
  password: string;
  role: 'Admin' | 'Writer';
}
