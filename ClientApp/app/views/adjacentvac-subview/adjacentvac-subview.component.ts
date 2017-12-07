import { Component, OnInit } from "@angular/core";
import { GeometryItem } from "../../core/models/geometryItem";
import { CfkcAddressApiService } from "../../core/cfkc-address-api.service";
import { ParcelRepositoryService } from "../../core/parcel-repository.service";
import { ActivatedRoute } from "@angular/router";
import { Parcel } from "../../core/models/parcel";

@Component({
    selector: "adjacentvac-subview",
    templateUrl: "./adjacentvac-subview.component.html",
    styleUrls: ["./adjacentvac-subview.component.css"]
})
export class AdjacentvacSubviewComponent implements OnInit {

    public parcel: Parcel;
    public geometry: GeometryItem;
    public zoom: number = 18;
    public mainColor = "dodgerblue";
    public adjGeom = [];
    public adjParcels = [];
    public mapIsBusy = false;

    constructor(
        private parcelRepo: ParcelRepositoryService,
        private route: ActivatedRoute) { }

    public ngOnInit() {
        this.route.parent.params.subscribe((p) => {

            this.mapIsBusy = true;

            this.parcelRepo.getParcelById(p.id).subscribe((parcel: Parcel) => {

                this.parcel = parcel;

                this.loadParcelGeometry(parcel.geometryId);

            });

        });

    }

    private loadParcelGeometry(geometryId: any): void {

        this.parcelRepo.getGeometry(geometryId).subscribe((geom: GeometryItem) => {

            this.geometry = geom;

            this.loadAdjacentParcels(geom.geometryId);

        });
    }

    private loadAdjacentParcels(geometryId: any) {

        this.parcelRepo.getAdjacentParcels(geometryId).subscribe(adjGeom => {

            adjGeom.forEach(item => {

                if (item.isVacant) {

                    this.adjGeom.push(item);

                    this.parcelRepo.getParcelById(item.parcelId).subscribe(parcel => {
                        this.adjParcels.push(parcel);
                    });
                }
                this.mapIsBusy = false;
            });
        });
    }

}
