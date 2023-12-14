import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { Brand } from './brand';
import { environment } from 'src/environments/environment.development';

@Component({
  selector: 'app-brand-moisturizer',
  templateUrl: './brand-moisturizer.component.html',
  styleUrls: ['./brand-moisturizer.component.css']
})
export class BrandMoisturizerComponent {
  brandsMoisturizer: Brand[] = [];

  constructor(http: HttpClient){
    http.get<Brand[]>(environment.baseUrl + 'api/brands/brandsmoisturizercount').subscribe({
      next: result => {
        this.brandsMoisturizer = result;
      },
      error: error => {
        console.error(error);
      }
    });
  }
}
