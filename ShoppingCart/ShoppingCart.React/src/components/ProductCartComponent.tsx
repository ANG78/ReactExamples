import React, { Component } from 'react';
import { ProductCart } from "../model/ProductCart";

interface IProps {
  item: ProductCart,
  onRemove(ref:string):any,
  onRefresh():any
}

interface IState {
  current: ProductCart,
  hideDescription: boolean
}

export class ProductCartComponent extends Component<IProps, IState> {
   
  constructor(props: IProps) {
    super(props);

    this.state = {
      current: props.item,
      hideDescription: false,
    }

    this.handlerUp = this.handlerUp.bind(this);
    this.handlerDown = this.handlerDown.bind(this);
  }


  handlerUp(e:React.MouseEvent<HTMLInputElement>, reference:string) {
    e.preventDefault();
    var quantity = this.state.current;
    quantity.quantity = quantity.quantity + 1;
    this.setState({
      current: quantity
    });

    this.props.onRefresh();
  }

  handlerDown(e:React.MouseEvent<HTMLInputElement>,reference:string) {
    e.preventDefault();
    var element = this.state.current;
    element.quantity = element.quantity - 1;
    this.setState({
      current: element
    });

    if (element.quantity <= 0){
      this.props.onRemove(reference);
    }
    else{
      this.props.onRefresh();
    }
  }

  render() {
    return (
      <div key={this.state.current.product.reference} >

      
        <div className="card mt-2">
          <div className="card-body" >

            <div className="row">
                <div className="col-5">
                    <label>Ref:</label>
                    <label className="ml-1 badge badge-primary">{this.state.current.product.reference}</label>

                </div>
            </div>
            <div className="row">
              <div className="col-12">
                <label>Name:</label>
                <label className="ml-1 pt-1 pb-1 badge badge-primary"
                    data-toggle="tooltip" title={this.state.current.product.description} >{this.state.current.product.name}</label>
              </div>
            </div>
            <div className="row">

              <div className="col-7">
                <label>Quant.:</label>
                
                <input type="button" 
                      className="ml-1 btn btn-default"
                      value="+" 
                      onClick={(e) => this.handlerUp(e, this.state.current.product.reference)} />

                {this.state.current.message === "" &&
                    <label className="ml-1 badge badge-success"> {this.state.current.quantity} </label>}

                {this.state.current.message !== "" &&
                    <label
                        className="ml-1 badge badge-danger"
                        data-toggle="tooltip"
                        title={this.state.current.message}> {this.state.current.quantity} </label>}

                <input type="button" 
                      className="ml-1 btn btn-default"
                      value="-" 
                      onClick={(e) => this.handlerDown(e, this.state.current.product.reference)} />
              </div>
              <div className="col-5">
                <label>Price:</label>
                <label
                  className="ml-1 badge badge-secondary"> {this.state.current.product.price} €</label>
              </div>
            </div>

          </div>

        </div>
      </div >
    );
  }
}
