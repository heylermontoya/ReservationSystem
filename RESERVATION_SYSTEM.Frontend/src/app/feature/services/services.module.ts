import { CommonModule } from "@angular/common";
import { NgModule } from "@angular/core";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { CoreModule } from "../../core/core.module";
import { SharedModule } from "../../shared/shared.module";
import { ServicesComponent } from "./services.component";
import { ServicesRoutingModule } from "./services-routing.module";
import { DialogService } from "primeng/dynamicdialog";

@NgModule({
    declarations: [
        ServicesComponent
    ],
    imports:[
        FormsModule,
        CommonModule,   
        ServicesRoutingModule,
        SharedModule,
          
        CoreModule,
        ReactiveFormsModule,      
          
    ],
    providers: [
        DialogService
    ],
})
export class ServicesModule {}
