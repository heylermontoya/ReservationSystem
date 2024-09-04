import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../../services/auth.service';
//import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-login',
  
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss'
})
export class LoginComponent implements OnInit{

  isLoggedIn: boolean = false;
  username: string = '';
  password: string = '';


  constructor(
    private auth$: AuthService, 
    private router: Router
  ){}

  ngOnInit() {
    /*
    this.auth$.isLoggedIn().subscribe(isLoggedIn => {
      this.isLoggedIn = isLoggedIn;
      if (isLoggedIn) {
        this.router.navigate(['home']);
      }
    });
    */
  }

  loginWithGoogle(){
    this.auth$.loginConGoogle();
  }

  onSubmit() {
    // Manejar el inicio de sesión con usuario y contraseña
    console.log('Iniciar sesión con', this.username, this.password);
  }
}
