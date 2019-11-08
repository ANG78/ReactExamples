import { EnumRole } from "../interfaces/EnumRole";


export class User {

    public id: number = 0;    
    public name: string = "";
    public role: EnumRole = EnumRole.Visitor;
}

