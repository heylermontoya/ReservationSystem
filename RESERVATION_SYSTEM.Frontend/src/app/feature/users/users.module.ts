import { CommonModule } from "@angular/common";
import { NgModule } from "@angular/core";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { CoreModule } from "../../core/core.module";
import { SharedModule } from "../../shared/shared.module";
import { DialogService } from "primeng/dynamicdialog";
import { UsersComponent } from "./users.component";
import { UsersRoutingModule } from "./users-routing.module";

@NgModule({
    declarations: [
        UsersComponent
    ],
    imports:[
        FormsModule,
        CommonModule,   
        UsersRoutingModule,
        SharedModule,          
        CoreModule,
        ReactiveFormsModule,           
    ],
    providers: [
        //CustomerService,
        DialogService,
        //AuthService
    ],
})
export class UsersModule {}
