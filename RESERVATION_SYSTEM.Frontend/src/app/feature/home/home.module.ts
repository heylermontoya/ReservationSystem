import { HomeComponent } from "./home.component";
import { CommonModule } from "@angular/common";
import { NgModule } from "@angular/core";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { CoreModule } from "../../core/core.module";
import { SharedModule } from "../../shared/shared.module";
import { DialogService } from "primeng/dynamicdialog";
import { HomeRoutingModule } from "./home-routing.module";
//import { AuthService } from "../../core/services/auth.service";

@NgModule({
    declarations: [
        HomeComponent
    ],
    imports:[
        FormsModule,
        CommonModule,   
        HomeRoutingModule,
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
export class HomeModule {}
