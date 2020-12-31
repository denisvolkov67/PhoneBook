
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { tap } from 'rxjs/operators';
import { Observable } from 'rxjs';


@Injectable({
  providedIn: 'root'
})
export class AuthService {

  readonly urlName = `${environment.apiUserName}`;
  readonly urlRole = `${environment.apiUserRole}`;


  constructor(private http: HttpClient) {
  }

  getName(): Observable<string> {
      return this.http.get(this.urlName, {responseType: 'text'});
  }

  getRole(): Observable<string> {
    return this.http.get(this.urlRole, {responseType: 'text'});
}
}
