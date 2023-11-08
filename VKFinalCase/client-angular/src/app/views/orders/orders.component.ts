import {Component, OnInit} from '@angular/core';
import {StorageService} from "../../services/storage.service";
import {DealerService} from "../../services/dealerServices/dealer.service";
import {ToastrService} from "ngx-toastr";
import {OrderService} from "../../services/adminServices/order.service";
import {ProductService} from "../../services/adminServices/product.service";
import {UsersService} from "../../services/adminServices/users.service";

@Component({
  selector: 'app-orders',
  templateUrl: './orders.component.html',
  styleUrls: ['./orders.component.css']
})
export class OrdersComponent implements OnInit{
  searchTerm: string = '';
  orders:any[] = []
  products:any[]=[];
  users:any[]=[];
  role:string = "";
  constructor(
    private storage:StorageService,
    private dealerService:DealerService,
    private adminOrderService:OrderService,
    private adminProductService:ProductService,
    private toastr:ToastrService,
  ) {}
  ngOnInit() {
    this.role=this.storage.getUserRole();
    this.getOrders();
    this.getProducts();
  }

  getProduct(id$: number) {
    return this.products.filter((product) => product.id === id$);
  }

  getOrders() {
   if (this.role=='dealer'){
     this.dealerService.getOrders().subscribe(
       (response: any) => {
         if (response.success) {
           this.orders = response.response;
         } else {
           throw new Error(response.message)
         }
       },
       (error: any) => {
         console.error(error);
       }
     );
   } else if (this.role=='admin'){
     this.adminOrderService.getOrders().subscribe(
       (response: any) => {
         if (response.success) {
           this.orders = response.response;
         } else {
           throw new Error(response.message)
         }
       },
       (error: any) => {
         console.error(error);
       }
     );
   }
  }

  acceptOrder(id:number) {
    this.adminOrderService.acceptOrder(id).subscribe(
      (response: any) => {
        if (response.success) {
          this.toastr.success('Order is accepted');
          this.getOrders();
        } else {
          this.toastr.error(response.message)
          throw new Error(response.message)
        }
      },
      (error: any) => {
        this.toastr.error(error)
        console.error(error);
      }
    );
  }
  deleteOrder(id:number) {
    if(confirm('Are you sure cancel this order?')) {
      if (this.role=='dealer') {
        this.dealerService.deleteOrder(id).subscribe(
          (response: any) => {
            if (response.success) {
              this.toastr.success('Order is cancelled');
              this.getOrders();
            } else {
              this.toastr.error(response.message)
              throw new Error(response.message)
            }
          },
          (error: any) => {
            this.toastr.error(error)
            console.error(error);
          }
        );
      } else if(this.role=='admin') {
        this.adminOrderService.deleteOrder(id).subscribe(
          (response: any) => {
            if (response.success) {
              console.log(response);
              this.toastr.success('Order is cancelled');
              this.getOrders();
            } else {
              this.toastr.error(response.message)
              throw new Error(response.message)
            }
          },
          (error: any) => {
            this.toastr.error(error)
            console.error(error);
          }
        );
      }
    }
  }

  getProducts() {
    if (this.role=='dealer'){
      this.dealerService.getProducts().subscribe(
        (response: any) => {
          if (response.success) {
            this.products = response.response;
          } else {
            throw new Error(response.message)
          }
        },
        (error: any) => {
          console.error(error);
        }
      );
    } else if (this.role=='admin'){
      this.adminProductService.getProducts().subscribe(
        (response: any) => {
          if (response.success) {
            this.products = response.response;
          } else {
            throw new Error(response.message)
          }
        },
        (error: any) => {
          console.error(error);
        }
      );
    }
  }
}
