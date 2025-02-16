import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { CarService } from '../../services/car.service';
import { Car } from '../../interfaces/car';

@Component({
  selector: 'app-car-list',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './car-list.component.html',
  styleUrl: './car-list.component.scss',
})
export class CarListComponent {
  cars: Car[] = [];

  constructor(private carService: CarService) {
    this.cars = this.carService.getCars();
  }

  navigateToCar(id: number) {
    // Navigation will be handled by router link
  }
}
