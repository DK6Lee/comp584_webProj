import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Brand } from './brand';
import { environment } from 'src/environments/environment.development';


@Component({
  selector: 'app-brands',
  templateUrl: './brands.component.html',
  styleUrls: ['./brands.component.css']
})
export class BrandsComponent {
  brands: Brand[] = [];

  constructor(http: HttpClient){
    http.get<Brand[]>(environment.baseUrl + 'api/brands').subscribe({
      next: result => {
        this.brands = result;
      },
      error: error => {
        console.error(error);
      }
    });
  }
}