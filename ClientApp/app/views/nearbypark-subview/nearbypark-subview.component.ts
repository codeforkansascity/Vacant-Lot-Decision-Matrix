import { Component, OnInit } from "@angular/core";
import { Parcel } from "../../core/models/parcel";
import { ParcelRepositoryService } from "../../core/parcel-repository.service";
import { GeometryItem } from "../../core/models/geometryItem";
import { ActivatedRoute } from "@angular/router";
import { ParkDistanceResult } from "../../core/models/parkDistanceResult";

@Component({
    selector: "app-nearbypark-subview",
    templateUrl: "./nearbypark-subview.component.html",
    styleUrls: ["./nearbypark-subview.component.css"]
})
export class NearbyparkSubviewComponent implements OnInit {

    public parcel: Parcel;
    public geometry: GeometryItem;
    public zoom: number = 14;
    public mainColor = "dodgerblue";
    public parks: ParkDistanceResult[];
    public parkGeoms = [];
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

                this.loadNearbyParkData(parcel.parcelId);
            });

        });

    }

    private loadParcelGeometry(geometryId: any): void {

        this.parcelRepo.getGeometry(geometryId)
            .subscribe(geom => this.geometry = geom);
    }

    private loadNearbyParkData(parcelId: any) {

        this.parcelRepo.getNearbyParks(parcelId).subscribe(parks => {

            this.parks = parks;

            parks.forEach(item => {

                this.parcelRepo.getGeometry(item.geometryId).subscribe(pkGeom => {

                    this.parkGeoms.push(pkGeom);

                });

                this.mapIsBusy = false;

            });

        });

    }

}
