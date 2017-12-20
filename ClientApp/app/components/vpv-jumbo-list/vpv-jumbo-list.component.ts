import { Component, OnInit, Input, Output, EventEmitter } from "@angular/core";
import { SearchResultItem } from "../../core/models/searchResultItem";

@Component({
    selector: "vpv-jumbo-list",
    templateUrl: "./vpv-jumbo-list.component.html",
    styleUrls: ["./vpv-jumbo-list.component.css"]
})
export class VpvJumboListComponent implements OnInit {

    @Input() public searchResults: SearchResultItem[];

    @Output() public itemClicked = new EventEmitter<SearchResultItem>();

    constructor() { }

    public ngOnInit() { }

    public onItemSelected(item: SearchResultItem) {
        console.warn("SEARCH RESULT CLICKED! ", item);
        this.itemClicked.emit(item);
    }
}
