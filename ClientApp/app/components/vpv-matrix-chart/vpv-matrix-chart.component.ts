import { Component, OnInit, Input } from "@angular/core";
import { MatrixStats, UsageScore } from "../../core/matrix/matrixStats";

import { } from "@types/chartjs";

@Component({
    selector: "vpv-matrix-chart",
    templateUrl: "./vpv-matrix-chart.component.html",
    styleUrls: ["./vpv-matrix-chart.component.css"]
})
export class VpvMatrixChartComponent implements OnInit {

    get matrixStats() { return this._matrixStats; }

    @Input() set matrixStats(value: MatrixStats) {

        if (value) {

            this._matrixStats = value;

            value.usageScores.forEach((p: UsageScore) => {

                this.chartDataSets.push({
                    label: p.use,
                    backgroundColor: "red",
                    borderColor: "red",
                    borderWidth: 1,
                    data: [p.score]
                });

            });
        }
    }

    public options = { scales: { yAxes: [{ ticks: { beginAtZero: true } }] } };

    public chartDataSets: any[] = [];

    private _matrixStats: MatrixStats;

    constructor() { }

    public ngOnInit() { }

}
