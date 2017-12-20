import { Component, OnInit, Input } from "@angular/core";

@Component({
    selector: "vpv-wait-cursor",
    templateUrl: "./vpv-wait-cursor.component.html",
    styleUrls: ["./vpv-wait-cursor.component.css"]
})
export class VpvWaitCursorComponent implements OnInit {

    @Input() public displayWhen: boolean;

    constructor() { }

    public ngOnInit() { }

}
