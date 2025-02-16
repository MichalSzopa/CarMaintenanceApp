import { Injectable } from '@angular/core';
import { Car } from '../interfaces/car';

@Injectable({
  providedIn: 'root',
})
export class CarService {
  private cars: Car[] = [
    {
      id: 1,
      make: 'Toyota',
      model: 'Camry',
      year: 2020,
      lastMaintenance: new Date('2023-01-15'),
    },
    {
      id: 2,
      make: 'Honda',
      model: 'Civic',
      year: 2019,
      lastMaintenance: new Date('2023-03-20'),
    },
  ];

  getCars() {
    return this.cars;
  }

  getCar(id: number) {
    return this.cars.find((car) => car.id === id);
  }
}
