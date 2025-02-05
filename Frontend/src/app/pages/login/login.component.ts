import { NgIf } from '@angular/common';
import { Component } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '../../Services/auth.service';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [ReactiveFormsModule,NgIf],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {
    loginForm: any;
    constructor(
        private fb: FormBuilder,
        private authService: AuthService,
        private router: Router
    ) {}

    ngOnInit(): void {
    // Initialize the login form with validation rules
        this.loginForm = this.fb.group({
            email: ['', [Validators.required, Validators.email]], // Required username field
            password: ['', Validators.required]   // Required password field
        });
        console.log("validations")
    }

    // Handle form submission
    onSubmit(): void {
        if (this.loginForm.valid) {
            const formData = this.loginForm.value;
            this.authService.login(formData).subscribe({
                next: (response) => {
                    localStorage.setItem('role', response.role); // Store role in localStorage
                    sessionStorage.setItem('userId', response.id);
                    if (response.role === 'Admin') {
                        this.router.navigate(['/admin']);
                    } else {
                        this.router.navigate(['/dashboard']);
                    }
                },
                error: (err) => {
                    alert(err.error.message || 'Login failed');
                }
            });
        }
        else {
            console.log('Fill the form correctly');
        }
    }
}
