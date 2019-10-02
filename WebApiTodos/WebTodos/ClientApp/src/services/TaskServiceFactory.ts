import { ITaskService } from "../interfaces/ITaskService";
import { TaskServiceDummy } from "./TaskServiceDummy";
import { EnumTypeController } from "./EnumTypeController";
import { TaskService } from "./TaskService";


export class TaskServiceFactory {

    public Get(typeDispatcher: EnumTypeController): ITaskService {
        if (typeDispatcher == EnumTypeController.TaskController) {
            return new TaskService(true);
        }
        else if (typeDispatcher == EnumTypeController.TaskServiceController) {
            return new TaskService(false);
        }
        return new TaskServiceDummy();
    }
}
