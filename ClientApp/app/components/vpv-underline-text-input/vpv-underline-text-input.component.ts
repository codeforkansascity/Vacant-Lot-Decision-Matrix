import { Component, OnInit, ViewChild, ElementRef, EventEmitter, Output } from "@angular/core";
import { Router, NavigationEnd } from "@angular/router";

@Component({
    selector: "vpv-underline-text-input",
    templateUrl: "./vpv-underline-text-input.component.html",
    styleUrls: ["./vpv-underline-text-input.component.css"]
})
export class VpvUnderlineTextInputComponent implements OnInit {
    
    @Output() public searchFocusChanged = new EventEmitter<SearchFocusChangedEventArgs>();
    @Output() public searchSubmitted = new EventEmitter<string>();
    @ViewChild("searchBox") private _sb: ElementRef;
    get searchBox(): HTMLInputElement { return this._sb.nativeElement; }
    @ViewChild("searchIcon") private _si: ElementRef;
    get searchIcon(): HTMLElement { return this._si.nativeElement; }
    @ViewChild("animationComponent") private _ac: ElementRef;
    get animationComponent(): HTMLElement { return this._ac.nativeElement; }
    private hasSearched = false;

    constructor(private router: Router) { }

    public activateSearchBox(): void {

        this.searchBox.placeholder = "Enter kiva or address";

        this.searchBox.style.textAlign = "left";

        this.searchIcon.style.color = "dodgerblue";

        this.animationComponent.classList.add("selected");

        if (window.innerWidth >= 768) {

            const thing: HTMLFormElement | any = this.animationComponent.firstElementChild;

            thing.style.transform = "scale(2)";
        }
    }

    public deactivateSearchBox(): void {

        this.searchBox.placeholder = "Search lots";

        this.animationComponent.classList.remove("selected");

        this.searchIcon.style.color = "lightgrey";

        if (window.innerWidth >= 768) {

            const thing: HTMLFormElement | any = this.animationComponent.firstElementChild;

            thing.style.transform = "scale(1)";

        }

        this.searchBox.value = "";
    }

    public ngOnInit() {
        
        this.searchBox.onfocus = (e) => {

            this.searchFocusChanged.emit(new SearchFocusChangedEventArgs(this.searchBox, true));

            this.activateSearchBox();

        };

        this.searchBox.onblur = (e) => {

            this.searchFocusChanged.emit(new SearchFocusChangedEventArgs(this.searchBox, false));        

        };
    }

    public onSubmit(): void {
        
        if (this.searchBox.value.length > 0) {
        
            this.activateSearchBox();
        
            this.hasSearched = true;
        
            this.animationComponent.classList.add("displayingResults");
        
            this.searchSubmitted.emit(this.searchBox.value);

        }
    }
}

// tslint:disable-next-line:max-classes-per-file
export class SearchFocusChangedEventArgs {

    constructor(public element: HTMLInputElement,
                public hasFocus: boolean) { }
}
