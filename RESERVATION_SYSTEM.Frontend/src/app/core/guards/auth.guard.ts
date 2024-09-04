import { Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { map, tap } from 'rxjs/operators';
import { AuthService } from '../services/auth.service';
//import { AuthService } from '../services/auth.service';

@Injectable({
    providedIn: 'root'
  })
  export class AuthGuard implements CanActivate {

    constructor(
      private router: Router,
      private authService: AuthService
    ) { }
  
    canActivate(): Observable<boolean>| boolean {
      /*
      return this.authService.isLoggedIn().pipe(
        tap(isLoggedIn => {
          if (!isLoggedIn) {
            this.router.navigate(['/login']); // Redirige al login si no está autenticado
          }
        })
      );      
      */
     return true;
    }
  }