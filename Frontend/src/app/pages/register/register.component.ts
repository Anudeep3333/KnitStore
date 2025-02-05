import { NgIf } from '@angular/common';
import { Component, inject } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { AuthService } from '../../Services/auth.service'; 
import { Router } from '@angular/router';
import { provideHttpClient, withInterceptorsFromDi } from '@angular/common/http'; // Import provideHttpClient

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [NgIf, ReactiveFormsModule], // Import ReactiveFormsModule for form handling
  providers:[],
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent {
  registrationForm: any;

  constructor(
    private fb: FormBuilder,
    private authService: AuthService,
    private router: Router // To navigate after successful registration
  ) {}

  ngOnInit(): void {
    // Initialize the form
    this.registrationForm = this.fb.group({
      username: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      phoneNumber: ['', [Validators.required /*,Validators.pattern(/^[0-9]{10}$/)*/]],
      password: ['', [Validators.required, Validators.minLength(6)]],
      confirmPassword: ['', [Validators.required]]
    }, { validator: this.passwordMatchValidator });
  }

  // Custom validator to check if password and confirm password match
  passwordMatchValidator(group: FormGroup): null | { passwordMismatch: boolean } {
    const password = group.get('password')?.value;
    const confirmPassword = group.get('confirmPassword')?.value;
    return password === confirmPassword ? null : { passwordMismatch: true };
  }

  // Handle form submission
  onSubmit(): void {
    if (this.registrationForm.valid) {
      const formData = this.registrationForm.value;
      this.authService.register(formData).subscribe({
        next: () => {
          alert('Registration successful!');
          this.router.navigate(['/login']); // Navigate to login after success
        },
        error: (err: { error: { message: any; }; }) => {
          alert(err.error.message || 'Registration failed');
        }
      });
    }
  }
}
