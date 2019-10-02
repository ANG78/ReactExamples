import { ITask } from "./ITask";

export interface ITaskService{
    LoadTasks( ) : Promise<ITask[]>;
    AddTask( task: ITask) : Promise<ITask>;
    ToggleStatus( task: ITask) :  Promise<ITask>;
}
