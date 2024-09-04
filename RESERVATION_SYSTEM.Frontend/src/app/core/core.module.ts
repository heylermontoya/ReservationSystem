import { CommonModule } from "@angular/common";
import { NgModule } from "@angular/core";
//import { AuthService } from "./services/auth.service";
//import { HeaderComponent } from "./components/header/header.component";
import { AngularFireModule } from '@angular/fire/compat';
import { AngularFireAuthModule } from '@angular/fire/compat/auth';
import { environment } from "../../environments/environment";
import { AuthService } from "./services/auth.service";
import { LoginComponent } from "./component/login/login.component";
import { FormsModule } from "@angular/forms";
import { SharedModule } from "../shared/shared.module";
import { CheckboxModule } from 'primeng/checkbox';
import { HeaderComponent } from "./component/header/header.component";
import { ToolbarModule } from "primeng/toolbar";
import { AvatarModule } from "primeng/avatar";
import { SidebarModule } from 'primeng/sidebar';

@NgModule({
    declarations: [
        HeaderComponent,
        LoginComponent
    ],
    exports: [
        HeaderComponent,
        LoginComponent
    ],
    imports: [
        ToolbarModule,
        CommonModule,
        AngularFireModule.initializeApp(environment.firebaseConfig),
        AngularFireAuthModule,
        SharedModule,
        FormsModule,
        CheckboxModule,
        AvatarModule,
        SidebarModule        
    ],
    providers: [
        //AuthService
    ]
})
export class CoreModule { }
