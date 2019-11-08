import { IUserService } from "../interfaces/IUserService";
import { User } from "../model/User";

export class UserService implements IUserService {

    private url: string = "/api/user";

    public Login(user: string, password: string): Promise<User> {

        return fetch(this.url + "?user="+ user + "&password=" +password,
            {
                method: 'GET'
            })
            .then((response) => {

                if (!response.ok) {
                    throw response;
                }
                
                return response.json()
                    .then(data => {
                        return (data as User);
                    }) 
                  
            });

    }
}
