import { IUserService } from "../../interfaces/IUserService";
import {User} from "../../model/User"
import { EnumRole } from "../../interfaces/EnumRole";

export class UserServiceDummy implements IUserService {

    public Login(user: string, password: string): Promise<User> {

        if (user === undefined || user.length===0){
           
            return Promise.reject(new Response("User Name must be supplied!!"));
        }
            

        if (user === "owner")
        {
            return new Promise((resolve) => {
                var userGot = new User();
                userGot.id = 999;
                userGot.name = user;
                userGot.role = EnumRole.Owner;
                
                resolve(userGot);
            });    
        }

        return new Promise((resolve) => {
            var userGot = new User();
            userGot.id = 1;
            userGot.name = user;
            userGot.role = EnumRole.Customer;
            
            resolve(userGot);
        });
    }
}
