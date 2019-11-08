import { User } from '../model/User';
import { ProductCart } from '../model/ProductCart';
import { CartProcessingResult } from '../model/CartProcessingResult';

export interface IProductCartService {
    SavePurchase(user: User, products: ProductCart[]): Promise<CartProcessingResult>;
}

