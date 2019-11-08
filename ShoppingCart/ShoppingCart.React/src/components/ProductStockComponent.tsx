import React, { Component } from 'react';
import { ProductStock } from "../model/ProductStock";

interface IProps {
  item: ProductStock
}

interface IState {
  current: ProductStock
}

export class ProductStockComponent extends Component<IProps, IState> {

  constructor(props: IProps) {
    super(props);

    this.state = {
      current: props.item,
    }

  }

  handlerOnDraggStart(ev: React.DragEvent<HTMLDivElement>): void {
    const id = this.state.current.product.reference;
    ev.dataTransfer.setData("text/plain", id);
  }


  render() {
    return (
      <div key={this.state.current.product.reference}
        className="draggable"
        draggable
        onDragStart={(e) => { this.handlerOnDraggStart(e) }}
      >

        <div className="card mt-2 ml-2 mr-2">
          <div className="card-body" >
            <div className="row">
              <div className="col-2">
                <label>Ref:</label>
                <label className="ml-1 badge badge-primary">{this.state.current.product.reference}</label>
              </div>
              <div className="col-6 ">
                <label>Name:</label>
                <label className="ml-1 pt-1 pb-1 badge badge-primary NameProduct">{this.state.current.product.name}</label>
              </div>
            </div>

            <div className="row">

              <div className="col-9">               
                <textarea
                  className="form-control ml-10"
                  readOnly={true}
                  value={this.state.current.product.description}
                ></textarea>
              </div>

              <div className="col-3">
                <div className="row">
                  <div className="col-12">
                    <label>Price:</label>
                    <label
                      className="ml-1 badge badge-primary"> {this.state.current.product.price} €</label>
                  </div>

                  <div className="col-12">
                    <label>Stock Quantity:</label>
                    <label
                      className="ml-1 badge badge-primary"
                    > {this.state.current.quantity} </label>
                  </div>
                </div>

              </div>

            </div>

          </div>
        </div>
      </div>
    );
  }
}
