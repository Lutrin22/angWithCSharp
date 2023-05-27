// login.component.ts

import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { AuthService } from '../auth.service';

interface LoginData {
  username: string;
  password: string;
}

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  public loginData: LoginData = {
    username: '',
    password: ''
  };

  constructor(private http: HttpClient, private authService: AuthService) {}

  public login(): void {
    this.http.post('http://localhost:5000/auth', this.loginData).subscribe(
      (response: any) => {
        const token = response.token;
        this.authService.setToken(token);
        
      },
      (error) => {
        console.error('Login failed', error);
      }
    );
  }
}
