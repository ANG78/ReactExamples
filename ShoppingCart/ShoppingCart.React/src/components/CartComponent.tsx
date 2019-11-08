import React, { Component } from 'react';
import { ProductCart } from "../model/ProductCart";
import { ProductStock } from "../model/ProductStock";
import { ProductCartComponent } from "./ProductCartComponent";
import { FactoryServices } from '../services/FactoryServices';
import { FootCartComponent } from './FootCartComponent';
import { User } from '../model/User';
import { CartProcessingResult } from '../model/CartProcessingResult';



export interface IProps {
  user: User,
  services: FactoryServices,
  onDragAndDrop: (reference: string) => Promise<ProductStock>,
  onPurchasingSuccessfully: () => any
}

export interface State {
  products: ProductCart[],
  isConfirmationMode: boolean
}

export class CartComponent extends Component<IProps, State> {

  constructor(props: IProps) {
    super(props);
    this.state = {
      products: [],
      isConfirmationMode: false
    };

    this.handlerRemove = this.handlerRemove.bind(this);
    this.handlerRefresh = this.handlerRefresh.bind(this);
    this.handlerAccept = this.handlerAccept.bind(this);
    this.handlerCancel = this.handlerCancel.bind(this);
  }



  handlerOnDragOver(ev: React.DragEvent<HTMLDivElement>) {
    ev.preventDefault();
  }

  hadlerOnDropEnd(ev: React.DragEvent<HTMLDivElement>) {
    var reference = ev.dataTransfer.getData("text/plain");
    this.props.onDragAndDrop(reference)
      .then((resul) => {
        //nothing
      })
      .catch((resul) => {
        resul.text().then((data: string) => { alert(data); });
      });
    ev.preventDefault();
  }

  handlerOnClick(ev: React.DragEvent<HTMLHRElement>) {
    ev.preventDefault();
  }

  add(productStock: ProductStock) {

    var found = this.get(productStock.product.reference);

    if (found.product.id === 0) {

      found.product = productStock.product;
      found.quantity = found.quantity + 1;
      this.setState({
        products: [...this.state.products, found]
      }, () => {

              (this.refs.Foot as FootCartComponent).recalculate(this.state.products);
      });

    }
    else {
      found.quantity = found.quantity + 1;
      this.setState({
        products: this.state.products
      }, () => {

              (this.refs.Foot as FootCartComponent).recalculate(this.state.products);
      });
    }

  }

  handlerRemove(reference: string) {
    this.setState({
      products: this.state.products.filter((e) => { return e.product.reference !== reference })
    });

    (this.refs.Foot as FootCartComponent).recalculate(this.state.products);
  }

  public remove(product: ProductCart) {
    this.handlerRemove(product.product.reference);
  }

  public get(reference: string): ProductCart {
    var productFound: ProductCart = new ProductCart();
    var result = this.state.products.filter((e) => { return e.product.reference === reference; });
    if (result.length > 0) {
      productFound = result[0];
    }
    return productFound;
  }

    handlerRefresh() {
        (this.refs.Foot as FootCartComponent).recalculate(this.state.products);
    }

    handlerAccept() {
        if (window.confirm('Are you sure you want to carry on with the purchase?')) {
            this.props.services.PoductCart().SavePurchase(this.props.user, this.state.products)
                .then((response: CartProcessingResult) => {

                    if (response.isOk) {

                        alert('the cart has been processed successfully!!');

                        this.setState({
                            products: []
                        }, () => { (
                                this.refs.Foot as FootCartComponent).recalculate(this.state.products);
                                this.props.onPurchasingSuccessfully();
                        });
                    }
                    else {

                        alert('The cart was not processed correctly.Please, check and try again.');
                        if (response.results.length > 0) {

                            response.results.map((resProductError, index) => {
                                var productCarts = this.state.products.filter((itProdCart, index) => {
                                    return itProdCart.product.id === resProductError.idProduct;
                                });

                                if (productCarts.length > 0) {
                                    productCarts[0].message = resProductError.message;
                                }
                            });

                        };

                        const backup = this.state.products;
                        this.setState({
                            products: []
                        }, () => {

                                this.setState({
                                    products: backup
                                });
                           
                        });
                    }
                })
                .catch((error: Response) => {
                    error.text()
                        .then((data: string) => {
                            alert(data);

                        })
                });
        }

    }

  handlerCancel() {

    if (window.confirm('are you sure you want to cancel?')) {
      this.setState({
        products: []
      }, () => (this.refs.Foot as FootCartComponent).recalculate(this.state.products));
    }

  }

  render() {
    return (
      <form >

        <div className="Cart">

          <div className={"droppable mt-2"}
            onDragOver={(e) => this.handlerOnDragOver(e)}
            onDrop={(e) => this.hadlerOnDropEnd(e)}>

            <div className="row">
              <div className="col-12">
                <h4 className="card col-12 "> Shopping Cart </h4>
              </div>
            </div>


            <div className="col-12">
              <div className="Cart col-12 mt-1 mt-2"
              >

                {this.state.products !== null &&
                  this.state.products.map((iterator, i) => {
                    return <ProductCartComponent
                      key={iterator.product.reference}
                      item={iterator}
                      onRefresh={this.handlerRefresh}
                      onRemove={this.handlerRemove} />;
                  })}



                {(this.state.products === null ||
                  this.state.products.length === 0) &&
                  <div className="card mt-2 ml-2 mr-2" >
                    <div className="card-body" >
                      <div className="row" >
                        <div className="d-flex justify-content-center">
                          <label className="Card" >Drag and Drop your products here!!</label>
                        </div>
                      </div>
                    </div>
                  </div>}

              </div>
            </div>
            <div className="row">
              <div className="col-12">

                <FootCartComponent ref="Foot"></FootCartComponent>
              </div>
            </div>


            <div className="row">
              <div className="col-12">
                <div className="d-flex justify-content-center">

                 
                {this.state.products !== null &&
                    this.state.products.length > 0 &&
                    <input type="button"
                        key="Accept"
                        className="btn btn-primary"
                        value="Accept"
                        onClick={(e) => this.handlerAccept()} />}

                {this.state.products !== null &&
                    this.state.products.length > 0 &&
                    <input type="button"
                        key="Cancel"
                        className="ml-1 btn btn-secondary"
                        value="Cancel"
                        onClick={(e) => this.handlerCancel()} />}

                </div>
              </div>
            </div>
          </div>




        </div>
      </form>


    );
  }
}
