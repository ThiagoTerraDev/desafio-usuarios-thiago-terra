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
