import { Injectable } from '@angular/core';
import { LoginRequest } from './login-request';
import { environment } from 'src/environments/environment.development';
import { HttpClient } from '@angular/common/http';
import { Observable, tap } from 'rxjs';
import { LoginResult } from './login-result';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor(protected http: HttpClient) { }

  login(loginItem: LoginRequest) : Observable<LoginResult>{
    let url = environment.baseUrl + 'api/Admin';

    return this.http.post<LoginResult>(url, loginItem)
      .pipe(tap((loginResult: LoginResult) => {
        if(loginResult.success && loginResult.token){
          localStorage.setItem("comp584-token", loginResult.token);
        }
      }));
  }
}
