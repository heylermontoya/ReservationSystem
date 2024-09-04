
import { NgModule } from "@angular/core";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { RouterModule } from "@angular/router";
import { ButtonModule } from "primeng/button";
import { CalendarModule } from "primeng/calendar";
import { DropdownModule } from "primeng/dropdown";
import { InputNumberModule } from "primeng/inputnumber";
import { InputTextModule } from "primeng/inputtext";
import { MenubarModule } from 'primeng/menubar';
import { ToolbarModule } from 'primeng/toolbar';
import { DynamicDialogModule } from "primeng/dynamicdialog";
import { DialogModule } from 'primeng/dialog';
import { TableModule } from 'primeng/table';
import { ProgressBarModule } from 'primeng/progressbar';
import { ProgressSpinnerModule } from 'primeng/progressspinner';
import { SliderModule } from 'primeng/slider';
import { CommonModule } from "@angular/common";
import { FormUserComponent } from "../feature/users/component/form-user/form-user.component";
import { ConfirmDialogModule } from 'primeng/confirmdialog';
import { ConfirmationService } from 'primeng/api';
import { FormServiceComponent } from "../feature/services/component/form-service/form-service.component";
import { FormReservationComponent } from "../feature/reservation/component/form-reservation/form-reservation.component";

const primeNgModules = [
    ToolbarModule,
    ButtonModule,
    MenubarModule,
    InputTextModule,
    CalendarModule,
    InputNumberModule,
    DropdownModule,
    TableModule,
    ProgressSpinnerModule,
    ProgressBarModule,
    SliderModule,    
    DialogModule,
    DynamicDialogModule,
    ConfirmDialogModule
]

@NgModule({
    declarations: [
        FormUserComponent,
        FormServiceComponent,
        FormReservationComponent
    ],
    imports: [
        ...primeNgModules,
        ReactiveFormsModule,
        FormsModule,
        RouterModule,
        CommonModule
    ],
    exports: [
        ...primeNgModules
    ],
    providers: [ConfirmationService],
})

export class SharedModule {}
