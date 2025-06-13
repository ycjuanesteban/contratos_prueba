export interface User {
  id: string,
  name: string,
  lastName: string,
  dni: string
}

export interface UserApiResponse {
  id: string;
  name: string;
  lastName: string;
  dni: string;
}

export interface CreateUserRequest {
  name: string;
  lastName: string;
  dni: string;
}