import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../../core/services/auth.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrl: './home.component.scss'
})
export class HomeComponent {

  isLoggedIn: boolean = false;

  constructor(private auth$: AuthService,private router: Router){}

  ngOnInit() {
    /*
    this.auth$.isLoggedIn().subscribe(isLoggedIn => {
      this.isLoggedIn = isLoggedIn;
      if (!isLoggedIn) {
        // Redirige al usuario a la página de login si no está autenticado
        //this.router.navigate(['']);
      }
    });
    */
  }

  CerrarSesion(){
    //this.auth$.loginConGoogle();
/*
      this.auth$.CerrarSesion()
        .then(() => {
          // Redirige a la página de inicio o de login después de cerrar sesión
            this.router.navigate(['']); // Cambia '/login' por la ruta deseada
        })
        .catch((error) => {
          console.error('Error durante el cierre de sesión:', error);
        });
    */
  }
  
}
