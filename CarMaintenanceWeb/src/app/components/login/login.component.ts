import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '../../services/auth.service';

@Component({
	selector: 'app-login',
	standalone: true,
	imports: [CommonModule, FormsModule],
	templateUrl: './login.component.html',
	styleUrl: './login.component.scss',
})
export class LoginComponent {
	username = '';
	password = '';
	error = '';

	constructor(private authService: AuthService, private router: Router) {}

	onSubmit() {
		if (this.authService.login(this.username, this.password)) {
			this.router.navigate(['/cars']);
		} else {
			this.error = 'Invalid credentials';
		}
	}
}
