import { Routes } from '@angular/router';
import { LoginComponent } from './pages/login/login.component';
import { RegisterComponent } from './pages/register/register.component'; 
import { DashboardComponent } from './pages/dashboard/dashboard.component';
import { AdminComponent } from './pages/admin/admin.component';
import { ProductDetailsComponent } from './pages/product-details/product-details.component';
import { CartComponent } from './pages/cart/cart.component';
import { CheckoutComponent } from './pages/checkout/checkout.component';

export const routes: Routes = [
    {path:'',component:DashboardComponent},
    {path:'login',component:LoginComponent},
    {path:'register',component:RegisterComponent},
    {path:'dashboard',component:DashboardComponent},
    {path:'admin',component:AdminComponent},
    { path: 'Products/:id', component: ProductDetailsComponent },
    {path: 'cart', component:CartComponent},
    {path: 'checkout', component:CheckoutComponent},
];
