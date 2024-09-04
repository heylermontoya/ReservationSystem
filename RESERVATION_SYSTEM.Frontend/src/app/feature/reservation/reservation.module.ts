import { CommonModule } from "@angular/common";
import { NgModule } from "@angular/core";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { CoreModule } from "../../core/core.module";
import { SharedModule } from "../../shared/shared.module";
import { DialogService } from "primeng/dynamicdialog";
import { ReservationComponent } from "./reservation.component";
import { ReservationRoutingModule } from "./reservation-routing.module";

@NgModule({
    declarations: [
        ReservationComponent
    ],
    imports:[
        FormsModule,
        CommonModule,   
        ReservationRoutingModule,
        SharedModule,
          
        CoreModule,
        ReactiveFormsModule,      
          
    ],
    providers: [
        DialogService
    ],
})
export class ReservationModule {}
