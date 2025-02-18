import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class CartService {

    private apiUrl = 'http://localhost:5000/api/cart';

    constructor(
      private http: HttpClient
    ) { }

    addToCart(cartData: any): Observable<any> {
      return this.http.post('http://localhost:5000/api/cart', cartData);
    }
    
    getCartItems(userId: number): Observable<any> {
      return this.http.get(`http://localhost:5000/api/cart/${userId}`);
    }

    updateCartItem(item: any): Observable<any> {
      return this.http.put(`${this.apiUrl}/${item.id}`, item);
    }

    deleteCartItem(itemId: number): Observable<any> {
      return this.http.delete(`${this.apiUrl}/${itemId}`);
    }

    checkout(): Observable<void> {
      return this.http.post<void>(`${this.apiUrl}/checkout`, {});
    }
    
}
