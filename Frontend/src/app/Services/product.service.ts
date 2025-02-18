import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ProductService {
  

  private apiUrl = 'http://localhost:5000/api/Products/add'; // Adjust URL if needed

  constructor(private http: HttpClient) { }

  addProduct(product: any): Observable<any> {
    return this.http.post(this.apiUrl, product);
  }

  addToCart(cartData: any): Observable<any> {
    return this.http.post('http://localhost:5000/api/cart', cartData);
  }

  getProducts():Observable<any>{
    return this.http.get('http://localhost:5000/api/Products/get');
  }

  getProductById(id: number): Observable<any> {
    return this.http.get(`http://localhost:5000/api/Products/${id}`);
  }
}
