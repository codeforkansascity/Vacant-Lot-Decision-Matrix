
export interface Parcel {
    parcelId: number;
    kivapin: number;
    apn: string;
    formattedAddress: string;
    fraction?: any;
    ownerName: string;
    ownerFormattedAddress: string;
    ownerCity: string;
    ownerState: string;
    ownerPostalCode: string;
    zone: string;
    shapeLength: number;
    squareFeet: number;
    latitude: number;
    longitude: number;
    geometryId: number;
    isVacant: number;
    geometry?: any;
}
