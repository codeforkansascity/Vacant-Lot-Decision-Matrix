import { Injectable, Inject } from "@angular/core";
import { Http } from "@angular/http";

import { Observable } from "rxjs/Observable";
import "rxjs/add/operator/map";
import "rxjs/add/operator/debounceTime";
import "rxjs/add/operator/distinctUntilChanged";
import "rxjs/add/operator/switchMap";

import { StringExtensions } from "../sanitizers/stringSanitizers";
import { SearchResultItem } from "./models/searchResultItem";

@Injectable()
export class ParcelSearchService {

    private apiSearch: string = "api/parcels/search/address?=";
   
    constructor(private http: Http, @Inject("BASE_URL") private baseUrl) { }

    public search(terms: Observable<string>) {
        return terms.debounceTime(300)
            .distinctUntilChanged()
            .switchMap(term => this.searchEntries(term));
    }

    private searchEntries(term: string): Observable<SearchResultItem[]> {
        return this.http.get(this.baseUrl + this.apiSearch + StringExtensions.sanitizeAddress(term))
            .map(res => res.json().results as SearchResultItem[]);
    }

}
