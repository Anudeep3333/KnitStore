import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ProductService } from '../../Services/product.service';
import { CommonModule } from '@angular/common';
import { CartService } from '../../Services/cart.service';

@Component({
  selector: 'app-product-details',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './product-details.component.html',
  styleUrl: './product-details.component.css'
})
export class ProductDetailsComponent implements OnInit {

  product: any = null;
  errorMessage: string = '';

  constructor(
    private route: ActivatedRoute,
    private productService: ProductService,
    private cartservice: CartService
  ) {}

  ngOnInit(): void {
    const productId = this.route.snapshot.params['id'];
    this.loadProduct(productId);
  }

  loadProduct(id: number) {
    this.productService.getProductById(id).subscribe({
      next: (data) => {
        this.product = data;
      },
      error: (err) => {
        this.errorMessage = err.error?.message || 'Failed to load product details';
      },
    });
  }
  
  addToCart(productId: number) {
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
  
    this.cartservice.addToCart(cartData).subscribe({
      next: (res) => {
        alert('Product added to cart!');
      },
      error: (err) => {
        console.error('Failed to add product to cart', err);
      }
    });
  }
}
