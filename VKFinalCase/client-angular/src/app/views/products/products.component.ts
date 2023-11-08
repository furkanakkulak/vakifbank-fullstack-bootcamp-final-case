import {Component, OnInit} from '@angular/core';
import {StorageService} from "../../services/storage.service";
import {DealerService} from "../../services/dealerServices/dealer.service";
import {ProductService} from "../../services/adminServices/product.service";
import {FormControl, FormGroup} from "@angular/forms";
import {ToastrService} from "ngx-toastr";

@Component({
  selector: 'app-products',
  templateUrl: './products.component.html',
  styleUrls: ['./products.component.css']
})
export class ProductsComponent implements OnInit{
  searchTerm: string = '';
  products:any[] = []
  role:string = "";
  createProduct = new FormGroup({
    name: new FormControl(''),
    minStockQuantity: new FormControl(1),
    stockQuantity: new FormControl(1),
    price: new FormControl(0),
  });
  constructor(
    private storage:StorageService,
    private dealerService:DealerService,
    private adminProductService:ProductService,
    private toastr:ToastrService
  ) {
  }
  ngOnInit() {
    this.role=this.storage.getUserRole();
    this.getProducts();
  }

  addProduct(){
    const { name, minStockQuantity,stockQuantity,price } = this.createProduct.value;

    if (name==""||name==null || minStockQuantity==null || minStockQuantity==0 || stockQuantity==null || stockQuantity==0 || price==null || price == 0) {
      this.toastr.warning('Empty space cannot be left.\n' +
        'Minimum stock, product stock and wage cannot be 0 or less than 0.')
    } else {
    this.adminProductService.postProduct({
      name:name,
      minStockQuantity:minStockQuantity,
      stockQuantity:stockQuantity,
      price:price,

    }).subscribe(
      (response: any) => {
        if (response.success) {
          this.toastr.success('Product succesfully created')
          this.getProducts()
        } else {
          this.toastr.error('An error was encountered while creating the product')
          throw new Error(response.message)
        }
      },
      (error: any) => {
        console.error(error);
      }
    );
    }
  }
  deleteProduct(id:number) {
    if(confirm('Are you sure delete this product?')) {
      this.adminProductService.deleteProduct(id).subscribe(
        (response: any) => {
          if (response.success) {
            this.toastr.success('Product is deleted');
            this.getProducts();
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
  getProducts() {
    if (this.role=='dealer') {
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


  get filteredProducts() {
    if (this.searchTerm === '') {
      return this.products;
    } else {
      return this.products.filter(product => {
        return product.name.toLowerCase().includes(this.searchTerm.toLowerCase());
      });
    }
  }

  protected readonly name = name;
}
