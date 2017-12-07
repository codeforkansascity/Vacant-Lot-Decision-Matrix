import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { FormsModule } from "@angular/forms";
import { HttpModule } from "@angular/http";
import { RouterModule } from "@angular/router";

import { ChartsModule } from "ng2-charts";

import { SearchViewComponent } from "./views/search/search-view.component";
import { DetailViewComponent } from "./views/detail/detail-view.component";
import { VpvJumboListComponent } from "./components/vpv-jumbo-list/vpv-jumbo-list.component";
import { VpvUnderlineTextInputComponent } from "./components/vpv-underline-text-input/vpv-underline-text-input.component";

import { AgmCoreModule } from "@agm/core";
import { AngularFontAwesomeModule } from "angular-font-awesome";
import { VpvTabbedConsoleComponent } from "./components/vpv-tabbed-console/vpv-tabbed-console.component";
import { VpvCoreModule } from "./core/vpv-core.module";
import { VpvRootComponent } from "./components/vpv-root/vpv-root.component";
import { VpvMatrixChartComponent } from "./components/vpv-matrix-chart/vpv-matrix-chart.component";
import { OverviewSubviewComponent } from "./views/overview-subview/overview-subview.component";
import { AdjacentresSubviewComponent } from "./views/adjacentres-subview/adjacentres-subview.component";
import { AdjacentvacSubviewComponent } from "./views/adjacentvac-subview/adjacentvac-subview.component";
import { FloodplainSubviewComponent } from "./views/floodplain-subview/floodplain-subview.component";
import { NearbyparkSubviewComponent } from "./views/nearbypark-subview/nearbypark-subview.component";
import { SizeSubviewComponent } from "./views/size-subview/size-subview.component";
import { SoilcontaminationSubviewComponent } from "./views/soilcontamination-subview/soilcontamination-subview.component";

@NgModule({
    declarations: [
        VpvRootComponent,
        VpvUnderlineTextInputComponent,
        VpvJumboListComponent,
        VpvTabbedConsoleComponent,
        VpvMatrixChartComponent,
        SearchViewComponent,
        DetailViewComponent,
        OverviewSubviewComponent,
        AdjacentresSubviewComponent,
        AdjacentvacSubviewComponent,
        FloodplainSubviewComponent,
        NearbyparkSubviewComponent,
        SizeSubviewComponent,
        SoilcontaminationSubviewComponent
    ],
    imports: [
        CommonModule,
        VpvCoreModule,
        HttpModule,
        FormsModule,
        ChartsModule,
        RouterModule.forRoot([
            { path: "", redirectTo: "search", pathMatch: "full" },
            { path: "search", component: SearchViewComponent },
            {
                path: "detail/:id",
                component: DetailViewComponent,
                children: [
                    { path: "", redirectTo: "overview", pathMatch: "full" },
                    { path: "overview", component: OverviewSubviewComponent },
                    { path: "adjacentres", component: AdjacentresSubviewComponent },
                    { path: "adjacentvac", component: AdjacentvacSubviewComponent},
                    { path: "floodplain", component: FloodplainSubviewComponent},
                    { path: "size", component: SizeSubviewComponent},
                    { path: "soilcontamination", component: SoilcontaminationSubviewComponent},
                    { path: "nearbypark", component: NearbyparkSubviewComponent}
                ]
            },
            { path: "**", redirectTo: "search" }
        ]),
        AgmCoreModule.forRoot({
            apiKey: "AIzaSyBuDnFCNfwzlrVOBAX8LXWJo8Hb6dDHA4g",
            libraries: ["geometry"],
        }),
        AngularFontAwesomeModule

    ]
})
export class AppModuleShared { }
