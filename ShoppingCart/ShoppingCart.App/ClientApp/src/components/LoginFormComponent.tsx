import React, { Component } from 'react';
import { User }  from '../model/User';
import { IFactoryServices } from '../interfaces/IFactoryServices';

interface IProps {
    onLoggin(user: User): any,
    factory: IFactoryServices
}

interface IState {
  user: string,
  password: string,
  isLoading: boolean,
  messageError: string,
  isOpen : boolean
}

export class LoginFormComponent extends Component<IProps, IState> {

  constructor(props: IProps) {
    super(props);
    this.state = {
      user: "",
      password: "",
      isLoading: false,
      messageError: "",
      isOpen : true
      };

  }


  private handleError( mssErr  : string)
  {
    this.setState({
      isLoading: false,
      messageError : mssErr
    });
  }

  handleSubmit(e:React.FormEvent){
    e.preventDefault();

    var user = (this.refs.user as HTMLInputElement).value;
    var password = (this.refs.user as HTMLInputElement).value;
    
    this.props.factory.User()
                  .Login(user,password)
                    .then((response:User) => {

                      this.setState({
                        isLoading: false,
                        messageError : "",
                        isOpen : false
                      },
                        () => {
                            this.props.onLoggin(response);
                        });

                      

                  }).catch((response: Response) => {

                    if (response.text !== undefined) {
                      response.text()
                        .then((data: string) => {
                          this.handleError(data);
                        });
                    }
                })
                .catch((response: string) => {
                  this.handleError(response);
                });
  }

  render() {
    return (
      this.state.isOpen &&
        <div className="card LoginForm">
      
        <form className="card-body" onSubmit={e => this.handleSubmit(e)} >
          <div className="form-group">
            <label>User:</label>
            <input
              ref="user"
              className="form-control"
              type="text"
              name="user"
            ></input>
          </div>

          <div className="form-group">
            <label>Password:</label>
            <input
              ref="password"
              type="password"
              className="form-control"
              name="password"
            ></input>
          </div>

          {this.state.messageError !== "" &&
            <div className="form-group ">
              <label 
               className="badge bagde-pill badge-danger">Error:</label>
              <textarea
                className="form-control messageError"
                name="messageError"
                placeholder="messageError"
                value={this.state.messageError}
                disabled={true}
              ></textarea>

            </div>}

          <div className="container">
            <div className="row">
              <div className="col-12">
                <button
                  type="submit"
                  className="btn btn-primary"
                  name="Login"
                  disabled={this.state.isLoading}
                  value="Login">Login
              </button>
              </div>
             
            </div>
          </div>
        </form>
      </div>

    );
  }
}
