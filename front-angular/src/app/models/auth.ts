export interface AuthResponse {
  userId: number;
  name: string;
  lastName: string;
  email: string;
  token: string;
}

export interface LoginRequest {
  email: string;
  password: string;
}

export interface SignupRequest {
  name: string;
  lastName: string;
  email: string;
  password: string;
  confirmPassword: string;
  department: string;
  shift: string;
}
