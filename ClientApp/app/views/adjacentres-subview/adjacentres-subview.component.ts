import { Component, OnInit } from "@angular/core";
import { ParcelRepositoryService } from "../../core/parcel-repository.service";
import { ActivatedRoute } from "@angular/router";
import { Parcel } from "../../core/models/parcel";
import { GeometryItem } from "../../core/models/geometryItem";
import { CfkcAddressApiService } from "../../core/cfkc-address-api.service";
import { RESIDENTIAL_LAND_USE_CODES } from "../../core/models/residentialCodes";

@Component({
    selector: "adjacentres-subview",
    templateUrl: "./adjacentres-subview.component.html",
    styleUrls: ["./adjacentres-subview.component.css"]
})
export class AdjacentresSubviewComponent implements OnInit {

    public parcel: Parcel;
    public geometry: GeometryItem;
    public zoom: number = 18;
    public mainColor = "dodgerblue";
    public adjGeom = [];
    public adjParcels = [];
    public mapIsBusy = false;

    private residentialCodes: number[] = RESIDENTIAL_LAND_USE_CODES;

    constructor(
        private parcelRepo: ParcelRepositoryService,
        private route: ActivatedRoute,
        private cfkcAddressApiRepo: CfkcAddressApiService) { }

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

    private loadAdjacentParcels(geometryId: any): void {

        this.parcelRepo.getAdjacentParcels(geometryId).subscribe(adj => {

            adj.forEach(item => {

                this.parcelRepo.getParcelById(item.parcelId).subscribe(parcel => {

                    this.cfkcAddressApiRepo.getForeignParcelByKiva(parcel.kivapin + "").subscribe(fp => {
                        const code = fp.city_land_use_code.substring(0, 4);

                        const landUseString = fp.city_land_use.toLowerCase();

                        const isResidence = this.residentialCodes.some(p => p === parseFloat(code)) && landUseString.length < 1;


                        if (landUseString === "residential" ||
                            isResidence) {

                            this.adjParcels.push(parcel);
                            this.adjGeom.push(item);
                        }

                    });

                });

                this.mapIsBusy = false;

            });

        });

    }

}
