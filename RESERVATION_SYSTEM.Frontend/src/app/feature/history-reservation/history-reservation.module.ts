import { CommonModule } from "@angular/common";
import { NgModule } from "@angular/core";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { CoreModule } from "../../core/core.module";
import { SharedModule } from "../../shared/shared.module";
import { DialogService } from "primeng/dynamicdialog";
import { HistoryReservationComponent } from "./history-reservation.component";
import { HistoryReservationRoutingModule } from "./history-reservation-routing.module";
//import { AuthService } from "../../core/services/auth.service";

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
    providers: [
        //CustomerService,
        //DialogService,
        //AuthService
    ],
})
export class HistoryReservationModule {}
