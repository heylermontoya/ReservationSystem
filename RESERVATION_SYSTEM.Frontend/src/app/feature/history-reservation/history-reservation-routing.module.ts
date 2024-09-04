import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";
import { HistoryReservationComponent } from "./history-reservation.component";

const routes: Routes = [    
    {
      path: '',
      component: HistoryReservationComponent
    }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class HistoryReservationRoutingModule { }
