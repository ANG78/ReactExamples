import React, { Component } from 'react';
import { ProductStockComponent } from "./ProductStockComponent";
import { ProductStock } from "../model/ProductStock";
import { FactoryServices } from '../services/FactoryServices';
import { User } from '../model/User';

export interface IProps {
  title: String,
  onDragAndDrop: (reference: string) => Promise<ProductStock>,
  services: FactoryServices,
  user: User
}

export interface State {
  products: ProductStock[],
  isLoading: boolean;
  isError: boolean;
}

export class ContainerProductsComponent extends Component<IProps, State> {

  constructor(props: IProps) {
    super(props);
    this.state = {
      products: [],
      isLoading: false,
      isError: false
    };
  }

  componentDidMount() {

    this.props.services.PoductStock().LoadProducts(this.props.user)
      .then((response) => {

        this.setState({
          isLoading: false,
          products: response
        });

      }).catch((error: Response) => {

        error.text()
          .then((data: string) => {
            alert(data);
            this.setState({
              isError: true,
              isLoading: false
            });
          })
      });

  }

  handlerOnChangeInput(ev: React.ChangeEvent<HTMLInputElement>) {
    ev.preventDefault();
    const newValue = ev.target.value;

    this.props.services.PoductStock()
      .LoadProducts(this.props.user,newValue).then((response) => {

        this.setState({
          isLoading: false,
          products: response
        });

      }).catch((error: Response) => {

        error.text()
          .then((data: string) => {
            alert(data);
            this.setState({
              isError: true,
              isLoading: false
            });
          })
      });

  }

  handlerOnDragOver(ev: React.DragEvent<HTMLDivElement>) {
    ev.preventDefault();
  }

  public add(product: ProductStock) {
    this.setState({
      products: [...this.state.products, product]
    });
  }
  public remove(product: ProductStock) {
    this.setState({
      products: this.state.products.filter((e) => { return e.product.reference !== product.product.reference; })
    });
  }
  public get(reference: string): ProductStock {
    var taskFound: ProductStock = new ProductStock();
    var result = this.state.products.filter((e) => { return e.product.reference === reference; });
    if (result.length > 0) {
      taskFound = result[0];
    }
    return taskFound;
  }

  public refresh(){
      this.props.services.PoductStock().LoadProducts(this.props.user)
          .then((response: ProductStock[]) => {

              this.setState({
                  isLoading: false,
                  products: []
              }, () => {
                  this.setState({
                      isLoading: false,
                      products: response
                  })
              });

          }).catch((error: Response) => {

        error.text()
          .then((data: string) => {
            alert(data);
            this.setState({
              isError: true,
              isLoading: false
            });
          })
      });

  }

  render() {
    return (

      <div className="row">
        <div className="col-md-12 nopadding">
          <div className={"droppable mt-2"}
            onDragOver={(e) => this.handlerOnDragOver(e)}
          >

            <div className="container-drag containertask">

              <div className="row">
                <div className="col-12 mt-2 ml-2 mr-2">
                  <div className="input-group mb-3">
                    <input type="text"
                      className="form-control"
                      placeholder="Search products here"
                      aria-describedby="basic-addon2"
                      onChange={e => this.handlerOnChangeInput(e)}
                    ></input>

                    <div className="input-group-append">
                      <span className="input-group-text" id="basic-addon2">search</span>
                    </div>
                  </div>
                </div>
              </div>

              <div className="row">
                <div className="col-12 mt-1">
                  {this.state.products !== null && this.state.products.map((iterator, i) => {
                      return <ProductStockComponent
                          key={iterator.product.reference}
                          item={iterator} />;
                  })}
                </div>
              </div>

              {(this.state.products === null || this.state.products.length === 0) &&
                  <div className="card mt-2 ml-2 mr-2" >
                    <div className="card-body" >
                      <div className="row" >
                        <div className="d-flex justify-content-center">
                          <label className="Card" >Not Found Products</label>
                        </div>
                      </div>
                    </div>
                  </div>}

            </div>
          </div>
        </div>
      </div>);
  }


}


