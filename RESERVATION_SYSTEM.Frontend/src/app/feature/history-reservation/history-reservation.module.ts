import { CommonModule } from "@angular/common";
import { NgModule } from "@angular/core";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { CoreModule } from "../../core/core.module";
import { SharedModule } from "../../shared/shared.module";
import { HistoryReservationComponent } from "./history-reservation.component";
import { HistoryReservationRoutingModule } from "./history-reservation-routing.module";

@NgModule({
    declarations: [
        HistoryReservationComponent
    ],
    imports:[
        FormsModule,
        CommonModule,   
        HistoryReservationRoutingModule,
        SharedModule,          
        CoreModule,
        ReactiveFormsModule,                
    ],
    providers: []
})
export class HistoryReservationModule {}
