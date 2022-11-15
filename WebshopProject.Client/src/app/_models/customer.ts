export interface Customer {
  id: number;
  email: string;
  password: string;
  firstName: string;
  lastName: string;
  address: string;
  telephone: string;
  role?: Role;
  token?: string;
}

export enum Role {
  Customer = 'Customer',
  Admin = 'Admin'
}
