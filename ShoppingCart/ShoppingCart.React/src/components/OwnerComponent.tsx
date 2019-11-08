import * as React from 'react';
import { FactoryServices } from '../services/FactoryServices';
import { ContainerProductsComponent } from "./ContainerProductsComponent";
import { ProductStock } from '../model/ProductStock'
import { User } from '../model/User';

interface IProps {
  services: FactoryServices,
  user: User
}

interface IState {
  products: ProductStock[];
  isLoading: boolean;
  isError: boolean;
  isEditionMode: boolean;
}

export class OwnerComponent extends React.Component<IProps, IState>{

  constructor(props: IProps) {
    super(props);

    this.handleOnDragAndDrop = this.handleOnDragAndDrop.bind(this);

    this.state = {
      products: [],
      isLoading: true,
      isError: false,
      isEditionMode: false,
    }

  }

  componentDidMount() {

    this.props.services.PoductStock().LoadProducts(this.props.user)
      .then((response) => {

        this.setState({
          products: response,
          isLoading: false
        });

      }).catch((error: Response) => {

        //console.log(error);
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



  handleOnDragAndDrop(reference: string): Promise<ProductStock> {
    return new Promise((resolve) => {
      new ProductStock();
    });
  }

  render() {

      const containerProducts = <ContainerProductsComponent
      ref="ContainerProducts"
      title="Products"
      onDragAndDrop={this.handleOnDragAndDrop}
      user={this.props.user}
      services={this.props.services}
    />

    return (
      <div className="row">

        <div className="col-12">
          {
            !this.state.isError &&
            !this.state.isLoading &&
            containerProducts
          }
        </div>
      </div>

    );
  }

}
