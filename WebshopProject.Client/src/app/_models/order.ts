import { CartItem } from "./cartItem";

export interface Order {
    id?: number;
    customerId: number;
    orderDetails: CartItem[];
    orderDate?: Date;
}