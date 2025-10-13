export interface User {
  id?: number;
  name: string;
  lastName: string;
  department: string;
  active: boolean;
  shift: string;
  createdAt?: string;
  updatedAt?: string;
}
