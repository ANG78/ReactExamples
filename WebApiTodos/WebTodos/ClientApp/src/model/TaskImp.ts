import { ITask } from "../interfaces/ITask";
import { EnumStatusTask } from "../interfaces/EnumStatusTask";

export class TaskImp implements ITask{
    id: number = 0;    
    title: string = "";
    description: string = "";
    status: EnumStatusTask = EnumStatusTask.Pending ;
}