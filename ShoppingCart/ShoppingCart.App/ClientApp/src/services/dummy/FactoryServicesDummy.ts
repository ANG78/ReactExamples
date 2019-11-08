import { IUserService } from "../../interfaces/IUserService";
import { IProductStockService } from "../../interfaces/IProductStockService";
import { IProductCartService } from "../../interfaces/IProductCartService";
import { UserServiceDummy } from "./UserServiceDummy";
import { ProductStockServiceDummy } from "./ProductStockServiceDummy";
import { ProductCartServiceDummy } from "./ProductCartServiceDummy";
import { IFactoryServices } from "../../interfaces/IFactoryServices";

export class FactoryServicesDummy implements IFactoryServices {

    productStockShare : ProductStockServiceDummy = new ProductStockServiceDummy();

    public User(): IUserService {
        return new UserServiceDummy();
    }
    public PoductStock(): IProductStockService {
        return this.productStockShare as IProductStockService;
    }
    public PoductCart(): IProductCartService {
        return new ProductCartServiceDummy( this.productStockShare);
    }
    
}
