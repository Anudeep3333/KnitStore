import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ProductService {
  

  private apiUrl = 'https://localhost:7181/api/Products/add'; // Adjust URL if needed

  constructor(private http: HttpClient) { }

  addProduct(product: any): Observable<any> {
    return this.http.post(this.apiUrl, product);
  }

  addToCart(cartData: any): Observable<any> {
    return this.http.post('https://localhost:7181/api/cart', cartData);
  }

  getProducts():Observable<any>{
    return this.http.get('https://localhost:7181/api/Products/get');
  }

  getProductById(id: number): Observable<any> {
    return this.http.get(`https://localhost:7181/api/Products/${id}`);
  }
}
