import { Component, OnInit } from '@angular/core';
import {ActivatedRoute, Router} from '@angular/router';
import {DealerService} from "../../services/dealerServices/dealer.service";
import {Product} from "../../Models/product.model";
import {ToastrService} from "ngx-toastr";
import {PostOrder} from "../../Models/order.model";
import {StorageService} from "../../services/storage.service";
import {ProductService} from "../../services/adminServices/product.service";
import {FormControl, FormGroup} from "@angular/forms";

@Component({
  selector: 'app-product',
  templateUrl: './product.component.html',
  styleUrls: ['./product.component.css']
})
export class ProductComponent implements OnInit {
  public productId!: number;
  product:any;
  paymentMethods:any;
  orderCount:number=1
  role:any;

  updateProduct = new FormGroup({
    name: new FormControl(''),
    minStockQuantity: new FormControl(1),
    stockQuantity: new FormControl(1),
    price: new FormControl(0),
  });

  constructor(
    private route: ActivatedRoute,
    private dealerService:DealerService,
    private toastr:ToastrService,
    private router:Router,
    private storage:StorageService,
    private adminProductService:ProductService
  ) {}

  ngOnInit(): void {
    this.route.params.subscribe(params => {
      this.productId = +params['id'];
    });
    this.role=this.storage.getUserRole()
    this.getProduct(this.productId);
    if (this.role=='dealer') {
      this.getPaymentMethods();
    }
  }

  getProduct(id:number) {
    if (this.role=='dealer') {
      this.dealerService.getProduct(id).subscribe(
        (response: any) => {
          if (response.success) {
            this.product = response.response;
            this.orderCount=response.response.minStockQuantity+1
          } else {
            this.toastr.error(response.message)
            this.router.navigate(['products'])
            throw new Error(response.message)
          }
        },
        (error: any) => {
          console.error(error);
          this.toastr.error(error)
          this.router.navigate(['products'])
        }
      );
    } else if (this.role=='admin') {
      this.adminProductService.getProduct(id).subscribe(
        (response: any) => {
          if (response.success) {
            this.product = response.response;
            this.updateProduct.patchValue({
              name: this.product.name,
              price: this.product.price,
              stockQuantity: this.product.stockQuantity,
              minStockQuantity: this.product.minStockQuantity,
            });
          } else {
            this.toastr.error(response.message)
            this.router.navigate(['products'])
            throw new Error(response.message)
          }
        },
        (error: any) => {
          console.error(error);
          this.toastr.error(error)
          this.router.navigate(['products'])
        }
      );
    }
  }

  getPaymentMethods() {
    this.dealerService.getPaymentMethods().subscribe(
      (response: any) => {
        if (response.success) {
          this.paymentMethods = response.response;
        } else {
          this.toastr.error(response.message)
          throw new Error(response.message)
        }
      },
      (error: any) => {
        console.error(error);
      }
    );
  }

  orderCountPlus(){
    if (this.product.stockQuantity > this.orderCount){
      this.orderCount++
    } else {
      this.toastr.warning('You cannot create orders exceeding the maximum product stock.')
    }
  }

  orderCountMinus(){
    if (this.product.minStockQuantity+1 < this.orderCount){
      this.orderCount--
    } else {
      this.toastr.warning('You cannot order less than the minimum order quantity.')
    }
  }

  putProduct () {
    this.adminProductService.putProduct(this.productId,this.updateProduct.value).subscribe(
      (response: any) => {
        if (response.success) {
          this.toastr.success('Your product has been updated successfully')
          this.router.navigate(['products']);
        } else {
          this.toastr.error(response.message)
          throw new Error(response.message)
        }
      },
      (error: any) => {
        console.error(error);
        this.toastr.error(error)
      }
    );
  }

  postOrder (paymentMethod:number) {
    const order:any= {
      dealerId:this.dealerService.dealerInformations.id,
      productId:this.productId,
      quantity:this.orderCount,
      paymentMethodId:paymentMethod
    }
    this.dealerService.postOrder(order).subscribe(
      (response: any) => {
        if (response.success) {
          this.toastr.success('Your order has been created successfully')
          this.router.navigate(['orders']);
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
}
