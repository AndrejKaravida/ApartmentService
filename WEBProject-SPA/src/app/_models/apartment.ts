export interface Apartment {
    id: number;
    type: string;
    numberOfRooms: number;
    numberOfGuests: number;
    pricePerNight: number;
    timeToArrive: string;
    timeToLeave: string;
    status: string;
    location?: string;
    photo?: string;
}
