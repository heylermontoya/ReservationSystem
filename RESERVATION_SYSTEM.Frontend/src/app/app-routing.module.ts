import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './core/component/login/login.component';
import { AuthGuard } from './core/guards/auth.guard';

const routes: Routes = [    
    {
        path:'reservation',
        loadChildren: () => import('./feature/reservation/reservation.module')
        .then(m => m.ReservationModule),
        canActivate: [AuthGuard]  
    },
    {
        path:'history',
        loadChildren: () => import('./feature/history-reservation/history-reservation.module')
        .then(m => m.HistoryReservationModule),
        canActivate: [AuthGuard]  
    },
    {
        path:'users',
        loadChildren: () => import('./feature/users/users.module')
        .then(m => m.UsersModule),
        canActivate: [AuthGuard]  
    },
    {
        path:'services',
        loadChildren: () => import('./feature/services/services.module')
        .then(m => m.ServicesModule),
        canActivate: [AuthGuard]  
    },
    { path: 'login', component: LoginComponent },
    { path: '', redirectTo: '/login', pathMatch: 'full' },
    { path: '**', redirectTo: '/login' }
];

@NgModule({
    imports: [ RouterModule.forRoot(routes) ],
    exports: [ RouterModule]
})
export class AppRoutingModule {}
