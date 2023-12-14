import { Component } from '@angular/core';
import { Brand } from './brand';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment.development';

@Component({
  selector: 'app-brand-category',
  templateUrl: './brand-category.component.html',
  styleUrls: ['./brand-category.component.css']
})
export class BrandCategoryComponent {
  brandsCategory: Brand[] = [];

  constructor(http: HttpClient){
    http.get<Brand[]>(environment.baseUrl + 'api/brands/brandcategorycount').subscribe({
      next: result => {
        this.brandsCategory = result;
      },
      error: error => {
        console.error(error);
      }
    });
  }
}
