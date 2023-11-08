import {Component, OnInit} from '@angular/core';
import {DealerService} from "../../services/dealerServices/dealer.service";
import {ActivatedRoute, Router} from "@angular/router";
import {ToastrService} from "ngx-toastr";
import {StorageService} from "../../services/storage.service";
import {OrderService} from "../../services/adminServices/order.service";
import {ProductService} from "../../services/adminServices/product.service";
import {UsersService} from "../../services/adminServices/users.service";
import {AdminDealerService} from "../../services/adminServices/admin-dealer.service";

@Component({
  selector: 'app-order',
  templateUrl: './order.component.html',
  styleUrls: ['./order.component.css']
})
export class OrderComponent implements OnInit{

  order:any;
  product:any;
  public orderId:number=1;
  payPrice:any;
  role:string="";
  dealer:any;
  user:any;

  constructor(
    private route:ActivatedRoute,
    private dealerService:DealerService,
    private toastr:ToastrService,
    private router:Router,
    private storage:StorageService,
    private adminOrderService:OrderService,
    private adminDealerService:AdminDealerService,
    private adminProductService:ProductService,
    private adminUserService:UsersService,
  ) {}

  ngOnInit() {
    this.role=this.storage.getUserRole();
    this.route.params.subscribe(params => {
      this.orderId = +params['id'];
    });
    this.getOrder(this.orderId);

  }

  getUser(id:number) {
    this.adminDealerService.getDealer(id).subscribe(
      (response: any) => {
        if (response.success) {
          this.dealer = response.response;
          this.adminUserService.getUser(response.response.userId).subscribe(
            (response: any) => {
              if (response.success) {
               this.user=response.response
              } else {
                throw new Error(response.message)
              }
            },
            (error: any) => {
              console.error(error);
            }
          );
        } else {
          throw new Error(response.message)
        }
      },
      (error: any) => {
        console.error(error);
      }
    );
  }

  getOrder(id:number) {
   if (this.role=='dealer'){
     this.dealerService.getOrder(id).subscribe(
       (response: any) => {
         if (response.success) {
           this.order = response.response;
           if (this.order.status!="Payment") {
             window.location.href='/orders'
           }
           if(this.order.productId){
             this.getProduct(this.order.productId)
           }
         } else {
           throw new Error(response.message)
         }
       },
       (error: any) => {
         console.error(error);
       }
     );
   }else if(this.role=='admin'){
     this.adminOrderService.getOrder(id).subscribe(
       (response: any) => {
         if (response.success) {
           this.order = response.response;
           if(this.order.productId){
             this.getProduct(this.order.productId)
             this.getUser(response.response.dealerId)
           }
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

  getProduct(id:number) {
    if (this.role=='dealer'){
      this.dealerService.getProduct(id).subscribe(
        (response: any) => {
          if (response.success) {
            this.product = response.response;
            this.payPrice=(response.response.price*this.order.quantity).toFixed(2);
          } else {
            throw new Error(response.message)
          }
        },
        (error: any) => {
          console.error(error);
        }
      );
    } else if (this.role=='admin'){
      this.adminProductService.getProduct(id).subscribe(
        (response: any) => {
          if (response.success) {
            this.product = response.response;
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

  paymentOrder() {
    this.dealerService.postOrderPayment({orderId:this.orderId, amount:this.payPrice}).subscribe(
      (response: any) => {
        if (response.success) {
          this.toastr.success('Payment received successfully')
          this.router.navigate(['orders'])
        } else {
          this.toastr.error(response.message)
          throw new Error(response.message)
        }
      },
      (error: any) => {
        console.error(error);
        this.toastr.error(error.message)
      }
    );
  }



  acceptOrder(id:number) {
    this.adminOrderService.acceptOrder(id).subscribe(
      (response: any) => {
        if (response.success) {
          console.log(response);
          this.toastr.success('Order is accepted');
          this.getOrder(this.orderId);
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
        this.adminOrderService.deleteOrder(id).subscribe(
          (response: any) => {
            if (response.success) {
              this.toastr.success('Order is cancelled');
              this.getOrder(this.orderId);
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
