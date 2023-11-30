import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { CookieService } from 'ngx-cookie';
import { map } from 'rxjs';
import { User } from '../models/user';

@Injectable({
  providedIn: 'root'
})
export class AuthService {


  public user: User | null = null;

  constructor(
    private router: Router,
    private http: HttpClient,
    private cookieService: CookieService
  ) { }

  getToken() {
    return this.cookieService.get("token");
  }


  login(username: string, password: string) {
     return this.http.post("/api/auth/login", { username, password })
      .pipe(map(user => {
        const u = user as User;
        this.cookieService.put("token", u.token);
        return user;
      }));
  }

  logout() {
    // remove user from local storage to log user out
    this.cookieService.remove("token");
    this.router.navigate(['/']);
    window.location.reload();
  }
}
