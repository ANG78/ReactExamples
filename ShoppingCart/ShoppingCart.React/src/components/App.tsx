import * as React from 'react';
import { CustomerComponent } from './CustomerComponent';
import { OwnerComponent } from './OwnerComponent';
import { EnumRole } from "../interfaces/EnumRole";
import { User } from '../model/User';
import { NavigationComponent } from "./NavigationComponent"
import { FactoryServices } from "../services/FactoryServices"
import { LoginFormComponent } from './LoginFormComponent';
import { FactoryServicesDummy } from '../services/dummy/FactoryServicesDummy';


interface IProps {
}

interface IState {
    user: User,
    isLoginMode: boolean
}


export class App extends React.Component<IProps, IState>{

    isDummyServices: boolean = true;
    factory: FactoryServices = this.isDummyServices ? new FactoryServicesDummy() : new FactoryServices();

    constructor(props: IProps) {
        super(props);

        this.state = {
            user: new User(),
            isLoginMode: false
        }

        this.handlerOnLogin = this.handlerOnLogin.bind(this);
        this.hadlerOnLogged = this.hadlerOnLogged.bind(this);
        this.hanglerOnSignOut = this.hanglerOnSignOut.bind(this);
    }

    handlerOnLogin() {
        this.setState({
            isLoginMode: true
        });
    }

    hanglerOnSignOut() {
        this.hadlerOnLogged(new User());
    }

    hadlerOnLogged(user: User) {

        this.setState({
            isLoginMode: false,
            user: user
        }, () => {
            (this.refs.navigation as NavigationComponent).add(user);
        });
    }


    public add(user: User) {
        this.setState({
            user: user
        });
    }

    render() {

        return (
            <div>
                <NavigationComponent
                    ref="navigation"
                    onLogInNavigationClick={this.handlerOnLogin}
                />

                {
                    !this.state.isLoginMode &&
                    (this.state.user.id === 0 || this.state.user.role === EnumRole.Customer) &&
                    <div className="col-md-12">
                        <CustomerComponent
                            services={this.factory}
                            user={this.state.user} />
                    </div>
                }

                {
                    !this.state.isLoginMode &&
                    this.state.user.id > 0 &&
                    this.state.user.role === EnumRole.Owner &&
                    <div className="col-md-12">
                        <OwnerComponent services={this.factory}
                            user={this.state.user} />
                    </div>
                }

                {this.state.isLoginMode &&
                    <div className="col-md-12">
                        <LoginFormComponent
                            onLoggin={this.hadlerOnLogged}
                        />
                    </div>
                }

            </div>
        );
    }

}
