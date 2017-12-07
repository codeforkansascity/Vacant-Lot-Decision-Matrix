import { Component, OnInit, Input } from "@angular/core";
import { Parcel } from "../../core/models/parcel";
import { ConcreteAnswer } from "../../core/matrix/matrixStats";

@Component({
    selector: "vpv-tabbed-console",
    templateUrl: "./vpv-tabbed-console.component.html",
    styleUrls: ["./vpv-tabbed-console.component.css"]
})
export class VpvTabbedConsoleComponent implements OnInit {
    
    public tabItems: any;
    
    @Input() public parcel: Parcel;

    private _concreteAnswers: ConcreteAnswer[];

    @Input() set tabs(value: ConcreteAnswer[]) {

        this._concreteAnswers = value;

        const items = value.reduce((a, c) => {

            const foundPath = this.getRelevantPath(c.question.toLowerCase());

            a.push({
                question: c.question,
                answer: c.answer,
                path: foundPath
            });

            return a;
        }, []);

        this.tabItems = items;
    }

    constructor() { }

    public ngOnInit() { }
    
    public getRelevantPath(use: string) {
        const keywords = [
            { matchWord: "resident", path: "adjacentres" },
            { matchWord: "vacant", path: "adjacentvac" },
            { matchWord: "floodplain", path: "floodplain" },
            { matchWord: "sqft", path: "size" },
            { matchWord: "contamination", path: "soilcontamination" },
            { matchWord: "park", path: "nearbypark" }
        ];

        let path = "";

        keywords.forEach((pair) => {
    
            if (use.includes(pair.matchWord)) {
                path = pair.path;
            }
        });

        return path;
    }

}
