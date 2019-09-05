import  {EnumStatusTask}   from "./EnumStatusTask";

export interface ITask{
    id: number;
    title: string;
    description: string;
    status: EnumStatusTask;
}
