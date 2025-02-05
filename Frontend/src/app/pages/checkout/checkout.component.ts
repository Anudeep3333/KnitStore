import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { FormsModule, NgModel } from '@angular/forms';
import { NgFor, NgIf } from '@angular/common';
import { AuthService } from '../../Services/auth.service';
import { CartService } from '../../Services/cart.service';

@Component({
  selector: 'app-checkout',
  standalone: true,
  imports: [FormsModule,NgIf,NgFor],
  templateUrl: './checkout.component.html',
  styleUrl: './checkout.component.css'
})
export class CheckoutComponent {
  userAddresses: any[] = [];
  checkoutItems: any[] = [];
  totalPrice = 0;
  newAddress = { street: '', city: '', state: '', pincode : '' ,userId:''};

  selectedAddressId: number | null = null;
  isPopupOpen = false;
  userId = parseInt(sessionStorage.getItem('userId') || '0', 10);
  errorMessage = '';

  constructor(
    private http: HttpClient,
    private authService:AuthService,
    private cartService:CartService
  ) {}

  ngOnInit() {
    this.loadUserAddresses();
    this.loadAllProducts();
    this.loadRazorpayScript();
  }

  loadRazorpayScript(): void {
    const script = document.createElement('script');
    script.src = 'https://checkout.razorpay.com/v1/checkout.js';
    script.onload = () => {
      console.log('Razorpay script loaded successfully');
    };
    document.body.appendChild(script);
  }

  loadAllProducts(){
    this.cartService.getCartItems(this.userId).subscribe({
      next: (data) => {
        this.checkoutItems = data;
        this.getTotalPrice();
      },
      error: (err) => {
        console.error('Failed to load checkout items', err);
      }
    });
  }

  getTotalPrice() {
    this.totalPrice= this.checkoutItems.reduce((total, item:any) => total + item.productPrice, 0);
  }

  loadUserAddresses() {
    this.authService.getaddresses(this.userId).subscribe({
      next: (data) => {
        this.userAddresses = data;
        if (this.userAddresses.length > 0) {
          this.selectedAddressId = this.userAddresses[0].id;
        }
      },
      error: (err) => {
        this.errorMessage = err.error?.message || 'Failed to load product details';
      },
    });
  }

  openAddressPopup() {
    this.isPopupOpen = true;
  }

  closePopup() {
    this.isPopupOpen = false;
  }

  saveNewAddress(event: Event) {
    event.preventDefault();
    this.newAddress.userId = this.userId.toString();
  
    this.authService.addaddress(this.newAddress).subscribe({
      next: (data: any) => {
        console.log('Address Added:', data); // Verify the response data from the API
  
        this.userAddresses.push(data);  // Add the new address to the array
        this.selectedAddressId = data.id;  // Select the new address
        this.closePopup();
      },
      error: (err) => {
        console.error('Error adding address:', err);
      }
    });
  }


  payWithRazorpay() {
    //this.totalPrice=Math.ceil(this.totalPrice)
    console.log(typeof(this.totalPrice))
    this.http.post('https://localhost:7181/api/Payment/create-order', this.totalPrice ).subscribe((response: any) => {
      const options = {
        key: 'rzp_test_ouVr2uLbwcqVkb',
        amount: response.amount,
        currency: response.currency,
        name: 'thewoolenstore',
        description: 'Test Transaction',
        order_id: response.id,
        handler: (paymentResponse: any) => {
          alert('Payment Successful!');
          this.verifyPayment(paymentResponse); // Optionally verify the payment
        },
        prefill: {
          name: 'Your Name',
          email: 'your-email@example.com',
          contact: '9999999999'
        },
        theme: {
          color: '#F37254'
        }
      };
  
      if (window.Razorpay) {
        const razorpay = new window.Razorpay(options);
        razorpay.open();
      } else {
        console.error('Razorpay is not loaded');
      }
    });
  }
  

  

  verifyPayment(paymentResponse: any) {
    const verificationPayload = {
      OrderId: paymentResponse.razorpay_order_id,
      PaymentId: paymentResponse.razorpay_payment_id,
      Signature: paymentResponse.razorpay_signature,
      Amount:this.totalPrice,
      AddreddId: this.selectedAddressId,
      UserId:this.userId,
    };
  
    this.http.post('https://localhost:7181/api/Payment/verify',verificationPayload).subscribe({
      next: (response) => {
        console.log('Payment verified successfully', response);
        alert('Payment verified successfully!');
      },
      error: (err) => {
        console.error('Payment verification failed', err);
        alert('Payment verification failed. Please try again.');
      },
    });
  }
  
}
