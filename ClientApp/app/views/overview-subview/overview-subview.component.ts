import { Component, OnInit } from "@angular/core";
import { ActivatedRoute } from "@angular/router";
import { ParcelRepositoryService } from "../../core/parcel-repository.service";
import { Parcel } from "../../core/models/parcel";
import { GeometryItem } from "../../core/models/geometryItem";

@Component({
    selector: "overview-subview",
    templateUrl: "./overview-subview.component.html",
    styleUrls: ["./overview-subview.component.css"]
})
export class OverviewSubviewComponent implements OnInit {

    public parcel: Parcel;
    public geometry: GeometryItem;
    public zoom: number = 18;
    public mainColor = "dodgerblue";
    
    constructor(
        private router: ActivatedRoute,
        private parcelRepo: ParcelRepositoryService) {  }

    public ngOnInit() {
        this.router.parent.params.subscribe(p => {

            this.parcelRepo.getParcelById(p.id)
                .subscribe(parcel => {
                    
                    this.parcel = parcel;

                    this.loadParcelGeometry(parcel.geometryId);

                });
        });
    }

    public loadParcelGeometry(geometryId: any) {

        this.parcelRepo.getGeometry(geometryId)
            .subscribe(geom => this.geometry = geom);
        
    }

}
