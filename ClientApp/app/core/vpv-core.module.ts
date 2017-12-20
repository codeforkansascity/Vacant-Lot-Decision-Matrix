import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";

import { ParcelSearchService } from "./parcel-search.service";
import { ParcelRepositoryService } from "./parcel-repository.service";
import { CfkcAddressApiService } from "./cfkc-address-api.service";
import { MatrixRepositoryService } from "./matrix/matrix-repository.service";
import { VpvWaitCursorComponent } from "./vpv-wait-cursor/vpv-wait-cursor.component";
import { AngularFontAwesomeModule } from "angular-font-awesome";

@NgModule({
    imports: [
        CommonModule,
        AngularFontAwesomeModule
    ],
    declarations: [ 
        VpvWaitCursorComponent 
    ],
    providers: [
        ParcelSearchService,
        ParcelRepositoryService,
        CfkcAddressApiService,
        MatrixRepositoryService,
    ],
    exports: [
        VpvWaitCursorComponent
    ]

})
export class VpvCoreModule { }
