import { IProductStockService, IProductStockSupportService } from "../../interfaces/IProductStockService";
import { ProductStock } from "../../model/ProductStock";
import { User } from "../../model/User";
import { EnumRole } from "../../interfaces/EnumRole";

const products: ProductStock[] = [
    {
        "product": {
            "id": 100001,
            "reference": "REF-00001",
            "name": "Angular Ready",
            "description": "desc1 title1 hardcoded  ",
            "price": 20.2
        },
        "quantity": 25,
    },
    {
        "product": {
            "id": 100002,
            "reference": "REF-00002",
            "name": "TypeScript in Action",
            "description": "desc2 title2 hardcoded ",
            "price": 32.27
        },
        "quantity": 20
    },
    {
        "product": {
            "id": 100003,
            "reference": "REF-00003",
            "name": "Asp.net Core jump start",
            "description": "desc3 title3 hardcoded ",
            "price": 50.2
        },
        "quantity": 70
    },
    {
        "product": {
            "id": 100004,
            "reference": "REF-00004",
            "name": "Docker for dummies",
            "description": "desc3 title3 hardcoded ",
            "price": 15.6
        },
        "quantity": 35
    },
];

export class ProductStockServiceDummy implements IProductStockService, IProductStockSupportService {


    async LoadProducts(user: User, textToFilter: string): Promise<ProductStock[]> {

        if (user.role === EnumRole.Owner) {

            if (textToFilter === undefined || textToFilter === "") {
                return new Promise((resolve) => {
                    resolve(
                        products.filter(
                            (x) => {
                                return x.product
                            })
                    )
                });
            }
            else {

                return new Promise((resolve) => {
                    resolve(
                        products.filter(
                            (x) => {
                                return (x.product.name.startsWith(textToFilter) )
                            })
                    )
                });
            }
        }
        else {

            if (textToFilter === undefined || textToFilter === "") {
                return new Promise((resolve) => {
                    resolve(
                        products.filter(
                            (x) => {
                                return ( x.quantity > 0)
                            })
                    )
                });
            }
            else {

                return new Promise((resolve) => {
                    resolve(
                        products.filter((x) => { return (x.product.name.startsWith(textToFilter)) &&
                                                         x.quantity > 0 })
                    )
                });
            }
        }


    }

    bookProductWithReference(idProduct: number, quantity: number, onlyCheck: boolean): number {

       var found = products.filter((it) => {
           return it.product.id === idProduct;
        });

        if ( found.length !== 1 ){
            return -1;
        }

        if ( found[0].quantity >= quantity ){
            if (onlyCheck){
                return found[0].quantity - quantity;
            }
            else{
                found[0].quantity = found[0].quantity - quantity;
                return found[0].quantity;
            }
        }
        else{
            return found[0].quantity - quantity;
        }
        

    }


}


