import React, { Component } from 'react';
import { User } from "../model/User";
import { EnumRole } from '../interfaces/EnumRole';

export interface IProps {
    onLogInNavigationClick(): any
}

export interface State {
    user: User
    isLogginMode: boolean
}

export class NavigationComponent extends Component<IProps, State> {

    constructor(props: IProps) {
        super(props);
        this.state = {
            user: new User(),
            isLogginMode : false
        };

    }

    public add(user:User){
        this.setState({
            user: user
        });
    }

    render() {
        return <div className="App col-md-12 nopadding">
            <header className="App-header">

                <nav className="navbar navbar-expand-lg navbar-light bg-light">

                 
                    {this.state.user.id > 0 &&
                        <a className="navbar-brand btn btn-primary btn-lg" href="/" >
                            Sign out
                        </a>}
                    

                    {this.state.user.id === 0 &&
                        <input type="button"
                            ref="login"
                            key="Login"
                        className="navbar-brand btn btn-primary btn-lg"
                            value="Log in"
                            onClick={(e) => this.props.onLogInNavigationClick()} />}                        

                    <button type="button" className="navbar-brand btn btn-success btn-lg ">Role: <span className="badge">{this.state.user.role}</span></button>
                    <button type="button" className="navbar-brand btn btn-success btn-lg ">Name: <span className="badge">{this.state.user.name}</span></button>

                      
                </nav>
            </header>

        </div>

    }


}
