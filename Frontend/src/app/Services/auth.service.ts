import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, Observable, throwError } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  constructor(private http: HttpClient) {
    console.log("Authservice")
  }

  // Register a new user
  register(data: any): Observable<any> {
    return this.http.post('http://localhost:5000/api/Account/register', data).pipe(
      catchError((error) => {
        console.error('Error during registration:', error);
        return throwError(() => error);
      })
    );
  }

  //login a user
  login(data: any): Observable<any> {
    console.log("login service")
    return this.http.post('http://localhost:5000/api/Account/login', data).pipe(
      catchError((error) => {
        console.error('Error during login:', error);
        return throwError(() => error);
      })
    );
  }

  getaddresses(userId:number):Observable<any>{
    return this.http.get(`http://localhost:5000/api/Address/${userId}`);
  }

  addaddress(address:any): Observable<any>{
    return this.http.post('http://localhost:5000/api/Address/add',address).pipe(
      catchError((error)=>{
        console.error('Error while saving address: ',error);
        return throwError(()=>error);
      })
    )
  }
}
