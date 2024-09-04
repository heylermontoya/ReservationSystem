import { Injectable } from '@angular/core';
import { Auth, GoogleAuthProvider, signInWithPopup, signOut, user } from '@angular/fire/auth';
import { Router } from '@angular/router';
import { map, Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  //user$: Observable<any>;

  constructor(
    //private auth:Auth,
    private router: Router
  ) {
    //this.user$ = user(this.auth); // Observa el estado de autenticación
   }

  loginConGoogle() {
    /*
    return signInWithPopup(this.auth, new GoogleAuthProvider())
      .then((result) => {debugger;
        // Redirige al usuario después del login exitoso
        this.router.navigate(['home']); // Cambia '/dashboard' por la ruta deseada
      })
      .catch((error) => {debugger;
        // Manejo de errores
        console.error('Error durante el login:', error);
      });
      */
  }
  
  CerrarSesion() {
    //return signOut(this.auth);
  }

  isLoggedIn(): Observable<boolean> | boolean {
    /*
    return this.user$.pipe(
      map(user => !!user) // Devuelve true si el usuario está logado, false si no
    );
    */
   return true;
  }
}
