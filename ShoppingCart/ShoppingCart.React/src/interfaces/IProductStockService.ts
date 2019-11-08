import { ProductStock } from '../model/ProductStock';
import { User } from '../model/User';

export interface IProductStockService {
    LoadProducts(user: User, textToFilter?: string): Promise<ProductStock[]>;
}

//implementations in order to simulate the validations and booking of the products
export interface IProductStockSupportService {
    bookProductWithReference(idProduct: number, quantity: number, onlyCheck: boolean): number;
}


