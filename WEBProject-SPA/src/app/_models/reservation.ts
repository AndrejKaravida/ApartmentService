import { Apartment } from './apartment';

export interface Reservation {
    id: number;
    apartment: Apartment;
    startDate: Date;
    endDate: Date;
    status: string;
    totalPrice: number;
    numberOfNights: number;
}
