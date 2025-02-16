import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private isAuthenticatedSubject = new BehaviorSubject<boolean>(false);
  isAuthenticated$ = this.isAuthenticatedSubject.asObservable();

  login(username: string, password: string): boolean {
    // Simplified login logic - replace with real authentication
    if (username === 'admin' && password === 'admin') {
      this.isAuthenticatedSubject.next(true);
      return true;
    }
    return false;
  }

  logout() {
    this.isAuthenticatedSubject.next(false);
  }
}
