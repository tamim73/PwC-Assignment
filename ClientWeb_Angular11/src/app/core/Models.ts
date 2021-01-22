export interface IBaseResponse {
    message: string;
    hasError: boolean;
}

export type ISortOrder = 'ASC' | 'DESC';

export interface IBatchRequest {
    pageSize: number;
    pageNo: number;
    sortProperty: string;
    sortOrder: ISortOrder;
}

export interface ISearchResponse {
    rows: any[];
    count: number;
}
