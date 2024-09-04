import { Routes } from '@angular/router';
import { LoginComponent } from './core/component/login/login.component';
import { AuthGuard } from './core/guards/auth.guard';

export const routes: Routes = [
    {
        path:'home',
        loadChildren: () => import('./feature/home/home.module')
        .then(m => m.HomeModule),
        canActivate: [AuthGuard]
    },
    { path: 'login', component: LoginComponent }, // Ruta pública
    { path: '', redirectTo: '/login', pathMatch: 'full' },
    { path: '**', redirectTo: '/login' } // Ruta por defecto
];
