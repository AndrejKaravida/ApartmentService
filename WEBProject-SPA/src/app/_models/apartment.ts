export interface Apartment {
    id: number;
    type: string;
    numberOfRooms: number;
    numberOfGuests: number;
    pricePerNight: number;
    timeToArrive: string;
    timeToLeave: string;
    status: string;
    amentities?: string;
    city?: string;
    country?: string;
    zip?: number;
    street?: string;
    location?: string;
    apt?: string;
    photo?: string;
}
