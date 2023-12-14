import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { BrandsComponent } from './brands/brands.component';
import { LoginComponent } from './login/login.component';
import { ProductsComponent } from './products/products.component';
import { BrandCategoryComponent } from './brand-category/brand-category.component';
import { BrandMoisturizerComponent } from './brand-moisturizer/brand-moisturizer.component';

const routes: Routes = [
  { path: '', component: HomeComponent, pathMatch: 'full' },
  { path: 'brands', component: BrandsComponent },
  { path: 'products', component: ProductsComponent },
  { path: 'brand-category', component: BrandCategoryComponent },
  { path: 'brand-moisturizer', component: BrandMoisturizerComponent },
  { path: 'login', component: LoginComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
