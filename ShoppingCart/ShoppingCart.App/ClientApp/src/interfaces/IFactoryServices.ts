import { IUserService } from "./IUserService";
import { IProductStockService } from "./IProductStockService";
import { IProductCartService } from "./IProductCartService";

export interface IFactoryServices{
    User(): IUserService;
    PoductStock(): IProductStockService;
    PoductCart(): IProductCartService;
}