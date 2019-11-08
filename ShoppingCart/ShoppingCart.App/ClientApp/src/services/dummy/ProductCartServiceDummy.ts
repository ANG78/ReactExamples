import { User } from "../../model/User";
import { ProductCart } from "../../model/ProductCart";
import { IProductCartService } from "../../interfaces/IProductCartService";
import { IProductStockSupportService } from "../../interfaces/IProductStockService";
import { CartProcessingResult } from "../../model/CartProcessingResult";
import { ProductCartProcessingResult } from "../../model/ProductCartProcessingResult";

export class ProductCartServiceDummy implements IProductCartService {

    productStockService: IProductStockSupportService;

    constructor(productStockService: IProductStockSupportService) {
        this.productStockService = productStockService;
    }

    async SavePurchase(user: User, products: ProductCart[]): Promise<CartProcessingResult> {


        var result = new CartProcessingResult();
   
        products.map((productCart, index) => {
            var quantity = this.productStockService.bookProductWithReference(productCart.product.id, productCart.quantity, true);
            if (quantity < 0) {
                var prod = new ProductCartProcessingResult();
                prod.idProduct = productCart.product.id;
                prod.message = " Quantity < 0 ";
                result.results = [...result.results, prod]
            }
        }); 

        products.map((productCart, index) => {
             this.productStockService.bookProductWithReference(productCart.product.id, productCart.quantity, false);
        });

        
        return new Promise((resolve) => {
            resolve(result)
        });
    }
}
