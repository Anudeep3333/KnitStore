import { CurrencyPipe, NgFor } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { ProductService } from '../../Services/product.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [NgFor,CurrencyPipe],
  templateUrl: './dashboard.component.html',
  styleUrl: './dashboard.component.css'
})
export class DashboardComponent implements OnInit{

  banners: string[] = [
    'assets/3rd.png',
    'assets/1st.png',
    'assets/2nd.png'
    
  ];
  
  currentSlide: number = 0;

  products: any[] = [];
  successMessage: string = '';
  errorMessage: string = '';

  constructor(
    private http: HttpClient,
    private productService: ProductService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.fetchProducts();
    this.startAutoSlide();
  }

  // Fetch product data from backend (example API endpoint)
  fetchProducts(): void {
    this.productService.getProducts().subscribe({
      next: (response) => {
        this.products = response;
      },
      error: (err) => {
        this.errorMessage = err.error?.message || 'An error occurred';
        this.successMessage = '';
      }
    });
  }

  // Banner Slider Logic
  nextSlide(): void {
    this.currentSlide = (this.currentSlide + 1) % this.banners.length;
  }

  prevSlide(): void {
    this.currentSlide = (this.currentSlide - 1 + this.banners.length) % this.banners.length;
  }

  startAutoSlide(): void {
    setInterval(() => this.nextSlide(), 5000); // Auto-slide every 5 seconds
  }

  addtocart(event: Event,productId:number):void{
    event.stopPropagation();
    var user = parseInt(sessionStorage.getItem('userId') || '0', 10);
    if (user==0){
      alert('You need to login to add the products to cart')
      return;
    }
    const cartData = {
      userId: user, // Replace with actual logged-in user ID
      productId: productId,
      quantity: 1
    };
  
    this.productService.addToCart(cartData).subscribe({
      next: (res) => {
        alert('Product added to cart!');
      },
      error: (err) => {
        console.error('Failed to add product to cart', err);
      }
    });
  }

  viewProductPage(id: any) {
    this.router.navigate(['/Products', id]);
  }
}
