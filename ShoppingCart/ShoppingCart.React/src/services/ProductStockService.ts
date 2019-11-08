import { IProductStockService } from "../interfaces/IProductStockService";
import { ProductStock } from "../model/ProductStock";
import { User } from "../model/User";

export class ProductStockService implements IProductStockService {


    private url: string = "/api/productStock";

    public async LoadProducts(user: User, textToFilter: string): Promise<ProductStock[]> {

        if (textToFilter === undefined) {
            textToFilter = "";
        }

        var urlAux = this.url + "?idUser=" + user.id + "&text=" + textToFilter;

        return await fetch(urlAux, {
                                    method: 'GET'
                        }).then((response) => {

                            if (!response.ok) {
                                throw response;
                            }

                            return response.json()
                                .then(data => {
                                    return (data as ProductStock[])
                                })
                        })
    }

}



