import { IUserService } from "../interfaces/IUserService";
import { IProductStockService } from "../interfaces/IProductStockService";
import { IProductCartService } from "../interfaces/IProductCartService";
import { UserService } from "./UserService";
import { ProductStockService } from "./ProductStockService";
import { ProductCartService } from "./ProductCartService";
import { IFactoryServices } from "../interfaces/IFactoryServices";


export class FactoryServices implements IFactoryServices {

    public User(): IUserService {
        return new UserService();
    }

    public PoductStock(): IProductStockService {
        return new ProductStockService();
    }

    public PoductCart(): IProductCartService {
        return new ProductCartService();
    }
}




