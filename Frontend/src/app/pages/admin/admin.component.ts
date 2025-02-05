import { Component, NgModule } from '@angular/core';
import { ProductService } from '../../Services/product.service';
import { FormsModule, NgModel, ReactiveFormsModule } from '@angular/forms';
import { NgIf } from '@angular/common';

@Component({
  selector: 'app-admin',
  standalone: true,
  imports: [NgIf,FormsModule, ],
  templateUrl: './admin.component.html',
  styleUrl: './admin.component.css'
})
export class AdminComponent {
  product: any = {
    name: '',
    description: '',
    price: null,
    quantity: null,
    imageUrl: '',
    category: '',
    brand: '',
    sku: '',
    discount: null,
    rating: null,
    reviewsCount: null,
    specifications: '',
    isFeatured: false,
    availabilityStatus: 'In Stock'
  };

  successMessage: string = '';
  errorMessage: string = '';

  constructor(private productService: ProductService) {}

  addProduct() {
    this.productService.addProduct(this.product).subscribe({
      next: (response) => {
        this.successMessage = response.message;
        this.errorMessage = '';
        this.resetForm();
      },
      error: (err) => {
        this.errorMessage = err.error?.message || 'An error occurred';
        this.successMessage = '';
      }
    });
  }

  resetForm() {
    this.product = {
      name: '',
      description: '',
      price: null,
      quantity: null,
      imageUrl: '',
      category: '',
      brand: '',
      sku: '',
      discount: null,
      rating: null,
      reviewsCount: null,
      specifications: '',
      isFeatured: false,
      availabilityStatus: 'In Stock'
    };
  }
}
