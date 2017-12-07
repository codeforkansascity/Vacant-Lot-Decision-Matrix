import { Injectable, Inject } from "@angular/core";
import { Http } from "@angular/http";

import { Parcel } from "./models/parcel";
import { GeometryItem } from "./models/geometryItem";
import { ParkDistanceResult } from "./models/parkDistanceResult";

import { Observable } from "rxjs/Observable";
import "rxjs/add/operator/map";
import "rxjs/add/operator/filter";
import "rxjs/add/operator/catch";
import "rxjs/add/observable/throw";
import "rxjs/add/observable/of";

@Injectable()
export class ParcelRepositoryService {

    private apiParcels = "api/parcels/";
    private apiGeometries = "api/parcels/geometries/";
    private apiParks = "api/parcels/parks/proximity/";

    constructor(private http: Http, @Inject("BASE_URL") private baseUrl: string) { }

    public getParcelByKiva(kiva: string): Observable<Parcel> {
        return this.http.get(this.baseUrl + this.apiParcels + "kiva?=" + kiva)
            .catch(this.handleError)
            .filter(res => res.status !== 404)
            .map(response => response.json().results[0] as Parcel);
            
    }

    public getParcelByAddress(address: string): Observable<Parcel> {
        return this.http.get(this.baseUrl + this.apiParcels + "address?=" + address)
            .catch(this.handleError)
            .filter(res => res.status !== 404)
            .map(response => response.json().results[0] as Parcel);
    }

    public getParcelById(id: number): Observable<Parcel> {
        return this.http.get(this.baseUrl + this.apiParcels + "id?=" + id)
            .catch(this.handleError)
            .filter(res => res.status !== 404)
            .map(response => response.json().results[0] as Parcel);
    }

    public getGeometry(geometryId: number): Observable<GeometryItem> {
        return this.http.get(this.baseUrl + this.apiGeometries + "id?=" + geometryId)
            .catch(this.handleError)
            .filter(res => res.status !== 404)
            .map(this.removeClosingPoints);
    }

    public getAdjacentParcels(geometryId: number): Observable<GeometryItem[]> {
        return this.http.get(this.baseUrl + this.apiGeometries + "adjacent/" + "id?=" + geometryId)
            .catch(this.handleError)
            .filter(res => res.status !== 404)
            .map(response => response.json().results as GeometryItem[]);
    }

    public getNearbyParks(parcelId: number): Observable<ParkDistanceResult[]> {
        return this.http.get(this.baseUrl + this.apiParks + "id?=" + parcelId)
            .map(response => response.json().results as ParkDistanceResult[]);
    }

    /*
    The Google Maps JavaScript API will automatically complete the polygon by drawing a
    stroke connecting the last location back to the first location for any given path.
    see => https://developers.google.com/maps/documentation/javascript/shapes#polygons

    **Our api returns a collection of points. The final point closes the polygon --per OGC standards.
    https://en.wikipedia.org/wiki/Open_Geospatial_Consortium

    -Brett
    */
    private removeClosingPoints(response: any): GeometryItem {

        const geometry = response.json().results[0] as GeometryItem;

        geometry.polygonCollection.forEach(polygon => polygon.pop());

        return geometry as GeometryItem;
        
    }

    private handleError(error: any) {

        if (error.status === 404) {
            return Observable.of(error);
        }

        console.error("Error in ParcelRepository");
        return Observable.throw(error);

    }
}
