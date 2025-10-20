export interface User {
  id?: number;
  name: string;
  lastName: string;
  email: string;
  department: string;
  active: boolean;
  shift: string;
  createdAt?: string;
  updatedAt?: string;
}

export interface CreateUserRequest {
  name: string;
  lastName: string;
  email: string;
  password: string;
  department: string;
  shift: string;
}

export interface UpdateUserRequest {
  id: number;
  name: string;
  lastName: string;
  email: string;
  department: string;
  shift: string;
}
