import { Component } from '@angular/core';
import { Product } from './product';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment.development';

@Component({
  selector: 'app-products',
  templateUrl: './products.component.html',
  styleUrls: ['./products.component.css']
})
export class ProductsComponent {
  products: Product[] = [];

  constructor(http: HttpClient){
    http.get<Product[]>(environment.baseUrl + 'api/product').subscribe({
      next: result => {
        this.products = result;
      },
      error: error => {
        console.error(error);
      }
    });
  }
}
