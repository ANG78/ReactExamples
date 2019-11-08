import { User } from "../model/User";

export interface IUserService {
    Login(user: string, password: string): Promise<User>;
} 
