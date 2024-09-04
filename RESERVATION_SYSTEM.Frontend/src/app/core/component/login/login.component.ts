import { Component } from '@angular/core';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss'
})
export class LoginComponent {

  isLoggedIn: boolean = false;
  username: string = '';
  password: string = '';

  constructor(private auth$: AuthService) {}

  loginWithGoogle(): void {
    this.auth$.signInWithGoogle();
  }  
}
