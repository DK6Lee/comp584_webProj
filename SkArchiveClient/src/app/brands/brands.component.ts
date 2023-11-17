import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';


@Component({
  selector: 'app-brands',
  templateUrl: './brands.component.html',
  styleUrls: ['./brands.component.css']
})
export class BrandsComponent {
  brands: BrandData[] = [];
  baseUrl: string = 'https://localhost:7240';

  constructor(http: HttpClient){
    http.get<BrandData[]>(this.baseUrl + '/api/brands').subscribe({
      next: result => {
        this.brands = result;
      },
      error: error => {
        console.error(error);
      }
    });
  }
}

interface BrandData{
  id: number;
  name: string;
  iso2: string;
  iso3: string;
}