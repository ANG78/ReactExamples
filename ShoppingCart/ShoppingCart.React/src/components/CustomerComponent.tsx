import * as React from 'react';
import { FactoryServices } from '../services/FactoryServices';
import { ContainerProductsComponent } from "./ContainerProductsComponent";
import { CartComponent } from './CartComponent';
import { ProductStock } from '../model/ProductStock'
import { User } from '../model/User';
import { EnumRole } from '../interfaces/EnumRole';

interface IProps {
  user: User,
  services: FactoryServices
}

interface IState {
  user: User,
  isEditionMode: boolean;
}

export class CustomerComponent extends React.Component<IProps, IState>{

  constructor(props: IProps) {
    super(props);

    this.handleCloseTask = this.handleCloseTask.bind(this);
    this.handleOnDragAndDrop = this.handleOnDragAndDrop.bind(this);
    this.handleOnPurchasingSuccessfully = this.handleOnPurchasingSuccessfully.bind(this);

    this.state = {
      isEditionMode: false,
      user: this.props.user
    }
  }

  handleCloseTask(): any {
    this.setState({
      isEditionMode: false
    });
  }

  handleOnDragAndDrop(reference: string): Promise<ProductStock> {

      var products: ContainerProductsComponent = this.refs.ContainerProducts as ContainerProductsComponent;
    var productFound = products.get(reference);

    if (productFound.product.id > 0) {
        (this.refs.Cart as CartComponent).add(productFound);
    }

    return new Promise((e) => {
      return productFound;
    });
  }

  handleOnPurchasingSuccessfully():any{
      (this.refs.ContainerProducts as ContainerProductsComponent ).refresh();
  }

  handleEdition(e: React.MouseEvent<HTMLButtonElement, MouseEvent>) {
    e.preventDefault();
    this.setState({
      isEditionMode: !this.state.isEditionMode
    });
  }

  render() {

      const containerProducts = <ContainerProductsComponent
      ref="ContainerProducts"
      title={this.state.user.id>0 ? "Products" : "Products"}
      user={this.state.user}
      services={this.props.services}
      onDragAndDrop={this.handleOnDragAndDrop}
    />

      const cart = <CartComponent
      ref="Cart"
      user={this.state.user}
      services={this.props.services}
      onDragAndDrop={this.handleOnDragAndDrop}
      onPurchasingSuccessfully={this.handleOnPurchasingSuccessfully}
    />

    return (
      <div className="row">

        <div className={this.state.user.id>0 ? "col-8" : "col-12"}>
          {
            containerProducts
          }
        </div>

        {
          this.state.user.id > 0 &&
          this.state.user.role === EnumRole.Customer &&
          <div className='col-4'>
            {
              cart
            }
          </div>
        }
      </div>


    );
  }

}
