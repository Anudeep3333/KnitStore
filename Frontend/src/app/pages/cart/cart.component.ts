import { NgFor, NgIf } from '@angular/common';
import { Component } from '@angular/core';
import { ProductService } from '../../Services/product.service';
import { CartService } from '../../Services/cart.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-cart',
  standalone: true,
  imports: [NgIf, NgFor],
  templateUrl: './cart.component.html',
  styleUrl: './cart.component.css'
})
export class CartComponent {
  cartItems: any[] = [];
  userId = parseInt(sessionStorage.getItem('userId') || '0', 10); 

  constructor(
    private cartService: CartService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.loadCartItems();
  }

  loadCartItems() {
    this.cartService.getCartItems(this.userId).subscribe({
      next: (data) => {
        this.cartItems = data;
      },
      error: (err) => {
        console.error('Failed to load cart items', err);
      }
    });
  }

  increaseQuantity(item: any): void {
    item.quantity += 1;
    item.productPrice = item.product.price * item.quantity;
    this.updateCart(item);
  }

  decreaseQuantity(item: any): void {
    if (item.quantity > 1) {
      item.quantity -= 1;
      item.productPrice = item.product.price * item.quantity;
      this.updateCart(item);
    } else {
      this.deleteItem(item);
    }
  }

  deleteItem(item: any): void {
    this.cartService.deleteCartItem(item.id).subscribe({
      next: () => {
        this.cartItems = this.cartItems.filter(cartItem => cartItem.id !== item.id);
      },
      error: (err) => {
        console.error('Failed to delete cart item', err);
      }
    });
  }

  updateCart(item: any): void {
    this.cartService.updateCartItem(item).subscribe({
      next: () => {
        console.log('Cart updated successfully.');
      },
      error: (err) => {
        console.error('Failed to update cart item', err);
      }
    });
  }

  checkout(): void {
    this.router.navigate(['/checkout']);
    // this.cartService.checkout().subscribe({
    //   next: () => {
    //     alert('Checkout successful!');
    //     this.cartItems = [];
    //   },
    //   error: (err) => {
    //     console.error('Checkout failed', err);
    //   }
    // });
  }
}
