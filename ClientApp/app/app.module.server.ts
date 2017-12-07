import { NgModule } from "@angular/core";
import { ServerModule } from "@angular/platform-server";
import { AppModuleShared } from "./app.module.shared";
import { VpvRootComponent } from "./components/vpv-root/vpv-root.component";

@NgModule({
    bootstrap: [ 
        VpvRootComponent 
    ],
    imports: [
        ServerModule,
        AppModuleShared
    ]
})
export class AppModule { }
