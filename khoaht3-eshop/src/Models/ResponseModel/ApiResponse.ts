export interface ApiResponse {
  statusCode: number;
  isSuccess: boolean;
  errorMessage: string;
  result: any | Result;
}

export interface Result {
  items: object[];
  totalCount: number;
}
