import { ITaskService } from "../interfaces/ITaskService";
import { ITask } from "../interfaces/ITask";
import { EnumStatusTask } from "../interfaces/EnumStatusTask";


export class TaskServiceDummy implements ITaskService {

    LoadTasks(): Promise<ITask[]> {

        const id = new Date().getTime();
        return new Promise((resolve) => {

            resolve(
                [
                    {
                        "id": id,
                        "title": "title1",
                        "description": "desc1 title1 hardcoded in TaskServiceDummy ",
                        "status": EnumStatusTask.Pending,
                    },
                    {
                        "id": id + 1,
                        "title": "title2",
                        "description": "desc2 title2 hardcoded in TaskServiceDummy",
                        "status": EnumStatusTask.Pending,
                    },
                    {
                        "id": id + 2,
                        "title": "title3",
                        "description": "desc3 title3 hardcoded in TaskServiceDummy",
                        "status": EnumStatusTask.Completed,
                    }
                ]
            )
        });
    }

    AddTask(task: ITask): Promise<ITask> {

        if ((task.title && task.title.length > 0)
            && (task.description && task.description.length > 0)) {

            task.id = new Date().getTime();
            return new Promise((resolve) => {
                resolve(task);
            });
        }
        else {

            return Promise.reject(new Response("Title or/and Decription are empty"));
        }
    }

    ToggleStatus(task: ITask): Promise<ITask> {

        return new Promise((resolve) => {

            if (task.status === EnumStatusTask.Completed) {
                task.status = EnumStatusTask.Pending;
            }
            else {
                task.status = EnumStatusTask.Completed;
            }

            resolve(task)
        });
    }

}
