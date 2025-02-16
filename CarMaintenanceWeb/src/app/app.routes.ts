import { Routes } from '@angular/router';
import { LoginComponent } from './components/login/login.component';
import { CarListComponent } from './components/car-list/car-list.component';
import { CarDetailsComponent } from './components/car-details/car-details.component';
import { AuthGuard } from './guards/auth.guard';

export const routes: Routes = [
  { path: '', redirectTo: '/login', pathMatch: 'full' },
  { path: 'login', component: LoginComponent },
  { path: 'cars', component: CarListComponent, canActivate: [AuthGuard] },
  {
    path: 'cars/:id',
    component: CarDetailsComponent,
    canActivate: [AuthGuard],
  },
];
