import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {ReplaySubject} from "rxjs";
import {tap, map} from "rxjs/operators";
import {User} from "../_models/user";

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  baseUrl = 'https://localhost:5001/api/';
  private _currentUserSource = new ReplaySubject<User | null>(1);
  currentUser$ = this._currentUserSource.asObservable();

  constructor(private http: HttpClient) { }

  login(model: any)
  {
    return this.http.post<User>(this.baseUrl + 'account/login', model)
      .pipe(
        tap((response: User) => {
          const user = response;
          if(user)
          {
            localStorage.setItem('user', JSON.stringify(user));
            this._currentUserSource.next(user);
          }
        })
      );
  }

  register(model : any)
  {
    return this.http.post(this.baseUrl + 'account/register', model).pipe(
      map((user: any) => {
        if(user)
        {
          localStorage.setItem('user', JSON.stringify(user));
          this._currentUserSource.next(user);
        }
      })
    )
  }

  setCurrentUser(user: User) {
    this._currentUserSource.next(user);
  }

  logout()
  {
    localStorage.removeItem('user');
    this._currentUserSource.next(null);
  }
}
