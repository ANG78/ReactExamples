import { User } from "../model/User";
import { ProductCart } from "../model/ProductCart";
import { IProductCartService } from "../interfaces/IProductCartService";
import { CartProcessingResult } from "../model/CartProcessingResult";

export class ProductCartService implements IProductCartService {

    private url: string = "/api/productCart";

    async SavePurchase(user: User, products: ProductCart[]): Promise<CartProcessingResult> {

        var cart = {
            idUser : user.id,
            products : products.map((it, ind) => {
                return {
                    idProduct: it.product.id,
                    quantity: it.quantity
                }
            })};
                    
        return await fetch(this.url, {
            method: 'POST',
            body: JSON.stringify(cart),
            headers: {
                'Content-type': 'application/json'
            }
        }).then((response) => {

            if (!response.ok) {
                throw response;
            }

            return response.json()
                .then(data => {
                    return (data as CartProcessingResult)
                })
        })
    }

}
