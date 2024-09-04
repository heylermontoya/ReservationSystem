import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './core/component/login/login.component';

const routes: Routes = [    
    {
        path:'reservation',
        loadChildren: () => import('./feature/reservation/reservation.module')
        .then(m => m.ReservationModule)
    },
    {
        path:'history',
        loadChildren: () => import('./feature/history-reservation/history-reservation.module')
        .then(m => m.HistoryReservationModule)
    },
    {
        path:'users',
        loadChildren: () => import('./feature/users/users.module')
        .then(m => m.UsersModule)
    },
    {
        path:'services',
        loadChildren: () => import('./feature/services/services.module')
        .then(m => m.ServicesModule)
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
