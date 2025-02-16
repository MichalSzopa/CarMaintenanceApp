import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute } from '@angular/router';
import { CarService } from '../../services/car.service';
import { Car } from '../../interfaces/car';

@Component({
  selector: 'app-car-details',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './car-details.component.html',
  styleUrl: './car-details.component.scss',
})
export class CarDetailsComponent {
  car?: Car;

  constructor(private route: ActivatedRoute, private carService: CarService) {
    const id = parseInt(this.route.snapshot.paramMap.get('id') || '0', 10);
    this.car = this.carService.getCar(id);
  }
}
