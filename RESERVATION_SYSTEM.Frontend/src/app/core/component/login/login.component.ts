import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../services/auth.service';

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
  ){}

  ngOnInit() 
  {}

  loginWithGoogle(){
    this.auth$.signInWithGoogle();
  }

  onSubmit() {
    console.log('Sign in with', this.username, this.password);
  }
}
