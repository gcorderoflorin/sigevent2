import { AppConfigurator } from '@/layout/component/app.configurator';
import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router, RouterModule } from '@angular/router';
import { ButtonModule } from 'primeng/button';
import { CheckboxModule } from 'primeng/checkbox';
import { InputTextModule } from 'primeng/inputtext';
import { PasswordModule } from 'primeng/password';
import { RippleModule } from 'primeng/ripple';
import { AccountService } from '../account.service';
import { LoginRequest } from './login-request';

@Component({
    selector: 'app-login',
    standalone: true,
    imports: [
        ButtonModule, 
        CheckboxModule, 
        InputTextModule, 
        PasswordModule, 
        FormsModule, 
        RouterModule, 
        RippleModule, 
        AppConfigurator,
        CommonModule,
        ReactiveFormsModule
    ],
    templateUrl: "./login.component.html"
})
export class LoginComponent implements OnInit {
    loginForm!: FormGroup;
    loading = false;
    error: string | null = null;

    constructor(
        private fb: FormBuilder,
        private authService: AccountService,
        private router: Router
    ) {}

    ngOnInit(): void {
        this.loginForm = this.fb.group({
            username: ['', Validators.required],
            password: ['', Validators.required],
            rememberMe: [false]
        });
    }
    
    onSubmit(): void {
        if (this.loginForm.invalid) return;

        this.loading = true;
        this.error = null;

        const dto: LoginRequest = {
            username: this.loginForm.value.username,
            password: this.loginForm.value.password
        };

        this.authService.login(dto).subscribe({
            next: (res) => {
                localStorage.setItem('jwt', res.token);
                localStorage.setItem('jwt-expiration', res.expiration);
                this.router.navigate(['/']);
            },
            error: (err) => {
                console.error(err);
                this.error = err?.error || 'Login failed';
                this.loading = false;
            }
        });
    }
}
