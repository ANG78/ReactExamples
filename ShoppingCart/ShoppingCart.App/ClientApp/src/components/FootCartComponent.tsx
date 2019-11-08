import React, { Component } from 'react';
import { ProductCart } from "../model/ProductCart";



export interface IProps {
}

export interface State {
    total: number,
    totalPrice: number
}

export class FootCartComponent extends Component<IProps, State> {

    constructor(props: IProps) {
        super(props);
        this.state = {
            total: 0,
            totalPrice: 0
        };

        this.recalculate = this.recalculate.bind(this);
    }

    recalculate(products: ProductCart[]): any {
 
            var totalAux = 0;
            var totalPriceAux = 0.0;
    
            products.forEach((value: ProductCart, key: number) => {
                totalAux = totalAux + value.quantity;
                totalPriceAux = totalPriceAux + (value.quantity * value.product.price);
                totalPriceAux = Math.round(totalPriceAux*100)/100;
            });
    
            this.setState({
                total: totalAux,
                totalPrice: totalPriceAux
            });
     
    }

    render() {
        return (
            <div className="card mt-2 mb-2">
                <div className="card-body" >

                    <div className="row">
                        <div className="col-6">
                            <label> Total: </label>
                            <label className="ml-1 ml-1 pt-1 pb-1 badge badge-primary"> {this.state.total} </label>
                        </div>

                        <div className="col-6">
                            <label > Total Price: </label>
                            <label className="ml-1 ml-1 pt-1 pb-1 badge badge-primary"> {this.state.totalPrice} €</label>
                        </div>


                    </div>
                    
                </div>
            </div>

        );
    }
}
