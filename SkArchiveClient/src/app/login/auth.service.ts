import { Injectable } from '@angular/core';
import { LoginRequest } from './login-request';
import { environment } from 'src/environments/environment.development';
import { HttpClient } from '@angular/common/http';
import { Observable, Subject, tap } from 'rxjs';
import { LoginResult } from './login-result';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  key = "skArchive-token";
  private _authStatus = new Subject<boolean>();
  public authStatus = this._authStatus.asObservable();

  constructor(protected http: HttpClient) { }

  init(){
    if(this.isAuthenticated()){
      this.setAuthStatus(true);
    }
  }

  getToken(): string | null {
    return localStorage.getItem(this.key);
  }

  isAuthenticated(): boolean {
    return this.getToken() != null;
  }

  setAuthStatus(isAuthenticated: boolean) {
    this._authStatus.next(isAuthenticated);
  }

  login(loginItem: LoginRequest) : Observable<LoginResult>{
    let url = environment.baseUrl + 'api/Admin';

    return this.http.post<LoginResult>(url, loginItem)
      .pipe(tap((loginResult: LoginResult) => {
        if(loginResult.success && loginResult.token){
          localStorage.setItem("skArchive-token", loginResult.token);
          this.setAuthStatus(true);
        }
      }));
  }

  logout() {
    localStorage.removeItem(this.key);
    this.setAuthStatus(false);
  }
}
