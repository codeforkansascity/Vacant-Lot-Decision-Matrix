import { Component, Inject, ViewChild, ElementRef, OnInit } from "@angular/core";

import { Router, NavigationEnd } from "@angular/router";
import { Http } from "@angular/http";

import { SearchFocusChangedEventArgs } from "../../components/vpv-underline-text-input/vpv-underline-text-input.component";

import { Observable } from "rxjs/Observable";
import { Subject } from "rxjs/Subject";

import { StringExtensions } from "../../sanitizers/stringSanitizers";

import { ParcelSearchService } from "../../core/parcel-search.service";
import { ParcelRepositoryService } from "../../core/parcel-repository.service";
import { SearchResultItem } from "../../core/models/searchResultItem";

@Component({
    selector: "search-view",
    templateUrl: "./search-view.component.html",
    styleUrls: ["./search-view.component.css"]
})
export class SearchViewComponent implements OnInit {

    public results: SearchResultItem[];

    public searchTerm$ = new Subject<string>();

    @ViewChild("bgScreen") public _bgScreen: ElementRef;

    get bgScreen(): HTMLDivElement { return this._bgScreen.nativeElement; }

    constructor(
                private searchService: ParcelSearchService,
                private parcelApiService: ParcelRepositoryService,
                private router: Router) { }

    public ngOnInit(): void {
        this.searchService.search(this.searchTerm$)
            .subscribe(results => this.results = results);
    }

    public doAnimations(e: SearchFocusChangedEventArgs) {
        if (e.hasFocus) {
            this.bgScreen.classList.add("search-active");
        } else {
            this.bgScreen.classList.remove("search-active");
        }
    }

    public onItemClicked(item: SearchResultItem) {
        this.parcelApiService.getParcelById(item.parcelId)
            .subscribe(parcel => {
                this.router.navigate(["detail", parcel.parcelId]);
            });
    }

    public onSubmitted(searchInput: string) {

        const queryString = this.sanitizeQueryString(searchInput);
        
        if (queryString.length < 1) {
            return;
        }

        if (this.isAddress(queryString)) {

            this.parcelApiService.getParcelByAddress(queryString)
                .subscribe(parcel => this.router.navigate(["detail", parcel.parcelId]));
        } else {

            this.parcelApiService.getParcelByKiva(queryString).subscribe(parcel => {

                        this.router.navigate(["detail", parcel.parcelId]);
            });
        }
    }

    private sanitizeQueryString(str: string): string {
        const regex = /[^ A-Za-z0-9]/g;
        if (regex.test(str)) {
            str = str.replace(regex, "");
        }
        
        return str.trim();
    }

    private isAddress(str: string): boolean {
        // if it has letters it's  an address kivas only contains numbers
        return /[A-Za-z]/.test(str);
    }
}
