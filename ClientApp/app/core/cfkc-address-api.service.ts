import { Injectable, Inject } from "@angular/core";
import { Http } from "@angular/http";

import { AddressApiParcel } from "./models/addressApiParcel";

import { Observable } from "rxjs/Observable";
import "rxjs/add/operator/map";
import "rxjs/add/operator/filter";
import "rxjs/add/operator/catch";
import "rxjs/add/observable/throw";
import "rxjs/add/observable/of";

@Injectable()
export class CfkcAddressApiService {

    private apiPath = "http://dev-api.codeforkc.org/address-attributes-city-id/V0/";

    private apiPostfix = "?city=Kansas%20City&state=MO";

    constructor(private http: Http, @Inject("BASE_URL") private baseUrl: string) { }

    public getForeignParcelByKiva(kiva: string): Observable<AddressApiParcel>  {

        return this.http.get(this.apiPath + kiva + this.apiPostfix)
            .catch(this.handleError)
            .filter(res => res.status !== 404)
            .map(response => response.json().data as AddressApiParcel);
    }

    private handleError(error: any) {

        if (error.status === 404) {
            return Observable.of(error);
        }

        console.error("Error in AddressApiService");
        return Observable.throw(error);          
    }
}
