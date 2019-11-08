import { ProductCartProcessingResult } from "./ProductCartProcessingResult";

export class CartProcessingResult {
    public isOk: boolean = true;
    public results: ProductCartProcessingResult[] = [];

}
