import { Injectable, Inject } from "@angular/core";
import { Http } from "@angular/http";

import { Observable } from "rxjs/Observable";
import "rxjs/add/operator/map";
import "rxjs/add/operator/filter";
import "rxjs/add/operator/catch";
import "rxjs/add/observable/throw";
import "rxjs/add/observable/of";
import { MatrixStats } from "./matrixStats";

@Injectable()
export class MatrixRepositoryService {

    private apiPath: string = "api/parcels/matrix/";

    constructor(private http: Http, @Inject("BASE_URL") private baseUrl: string) { }

    public getMatrixStatsById(id: any): Observable<MatrixStats> {
        return this.http.get(this.baseUrl + this.apiPath + "id?=" + id)
            .catch(this.handleError)
            .filter(res => res.status !== 404)
            .map(res => res.json() as MatrixStats);
    }
    private handleError(error: any) {

        if (error.status === 404) {
            return Observable.of(error);
        }

        console.error("Error in MatrixRepositoryService");
        return Observable.throw(error);
    }
}
