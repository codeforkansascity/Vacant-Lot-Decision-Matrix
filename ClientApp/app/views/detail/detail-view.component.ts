
import { Component, OnInit, ElementRef, ViewChild } from "@angular/core";
import { ActivatedRoute } from "@angular/router";
import { AgmMarker } from "@agm/core";
import { Observable } from "rxjs/Observable";
import "rxjs/add/operator/toPromise";
import { ParcelRepositoryService } from "../../core/parcel-repository.service";
import { CfkcAddressApiService } from "../../core/cfkc-address-api.service";
import { GeometryItem } from "../../core/models/geometryItem";
import { AddressApiParcel } from "../../core/models/addressApiParcel";
import { Parcel } from "../../core/models/parcel";
import { MapMarker } from "../../core/models/mapMarker";
import { MatrixRepositoryService } from "../../core/matrix/matrix-repository.service";
import { MatrixStats, ConcreteAnswer } from "../../core/matrix/matrixStats";
import { TabInfo } from "./tabInfo";

@Component({
    selector: "detail-view",
    templateUrl: "./detail-view.component.html",
    styleUrls: ["./detail-view.component.css"],

})
export class DetailViewComponent implements OnInit {

    public matrixStats: MatrixStats;
    public parcel: Parcel;
    public usageHeader: string = "";
    public loadingMessage: string;
    public _matrixIsBusy = false;

    public tabs: ConcreteAnswer[];

    get matrixIsBusy() { return this._matrixIsBusy; }

    set matrixIsBusy(value: boolean) {
        
        if (value) {

            this.loadingMessageSequence();

        } else {

            this.usageHeader = this.matrixStats.bestUses.length > 1 ? "Top uses" : "Best use";
            this.loadingMessage = this.matrixStats.bestUses.join(", ");
        }

        this._matrixIsBusy = value;
    }

    constructor(
        private route: ActivatedRoute,
        private parcelApi: ParcelRepositoryService,
        private matrixRepo: MatrixRepositoryService) { }

    public ngOnInit(): void {

        this.route.params.subscribe(params => {

            this.loadMatrixStats(params.id);

            this.loadParcelData(params.id);

        });

    }
    private loadingMessageSequence(): void {
        this.loadingMessage = "Locating parcel ...";
        setTimeout(() => {
            this.loadingMessage = "Computing best use...";
        }, 1100);
    }

    private loadMatrixStats(id: number) {

        this.matrixIsBusy = true;

        this.matrixRepo.getMatrixStatsById(id)
            .subscribe(stats => {

                this.matrixStats = stats;

                this.tabs = this.matrixStats.concreteAnswers;

                this.matrixIsBusy = false;
            });
    }

    private loadParcelData(id: number) {

        this.parcelApi.getParcelById(id)
            .subscribe(parcel => this.parcel = parcel);

    }

}
