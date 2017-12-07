
export interface GeometryItem {
    geometryId: number;
    parcelId: number;
    polygonCollection: Array<any[]>;
    lat: number;
    lng: number;
    formattedAddress: string;
    isVacant: boolean;
}
