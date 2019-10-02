import { ITaskService } from "../interfaces/ITaskService";
import { ITask } from "../interfaces/ITask";
import { EnumStatusTask } from "../interfaces/EnumStatusTask";


export class TaskService implements ITaskService {

    private url: string = "/api/task";

    constructor(isTaskController: boolean) {

        if (!isTaskController) {
            this.url = "/api/taskService";
        }
        
    }

    LoadTasks(): Promise<ITask[]> {

        return fetch(this.url,
            {
                method: 'GET'
            })
            .then((response) => {

                if (!response.ok) {
                    throw response;
                }

                return response.json()
                    .then(data => data as ITask[]);
            });
    }


    AddTask(task: ITask): Promise<ITask> {

        return fetch(this.url,
            {
                method: 'POST',
                body: JSON.stringify(task),
                headers: {
                    'Content-type': 'application/json'
                }
            })
            .then((response) => {

                if (!response.ok) {
                    throw response;
                }

                return response.json()
                    .then(data => data as ITask);

            });
    }


    ToggleStatus(task: ITask): Promise<ITask> {

        console.log(task.id);
        if (task.status === EnumStatusTask.Completed) {
            task.status = EnumStatusTask.Pending;
        }
        else {
            task.status = EnumStatusTask.Completed;
        }

        return fetch(this.url + "/" + task.id,
            {
                method: 'PUT',
                body: JSON.stringify(task),
                headers: {
                    'Content-type': 'application/json'
                }
            })
            .then((response) => {

                if (!response.ok) {
                    throw response;
                }
                
                return response.json()
                    .then(data => data as ITask);

            });
    }
}
