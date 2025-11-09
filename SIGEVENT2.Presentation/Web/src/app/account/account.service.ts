import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { LoginRequest } from './login/login-request';
import { LoginResponse } from './login/login-response';

@Injectable({
    providedIn: 'root'
})
export class AccountService {
    private url = '/api/account'; // change if needed

    constructor(private http: HttpClient) {}

    login(dto: LoginRequest): Observable<LoginResponse> {
        return this.http.post<LoginResponse>(`${this.url}/login`, dto);
    }
}
